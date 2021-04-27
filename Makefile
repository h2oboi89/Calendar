################################################################################
# Makefile for C# projects.
################################################################################

###############################
# Variables
###############################
# Build tools and related variables
SHELL := cmd.exe
ZIP_TOOL := 7z.exe
COMPILER := MSBuild.exe
PYTHON3 := py -3
LUA53 := lua53.exe

# TDD tools and related variables (Update versions to match latest used)
PACKAGE_ROOT := $(shell echo $(USERPROFILE))\.nuget\packages

TDD_TOOL := $(PACKAGE_ROOT)\nunit.consolerunner\3.12.0\tools\nunit3-console.exe
TDD_DIR := .\OpenCover

COVERAGE_TOOL := $(PACKAGE_ROOT)\opencover\4.7.922\tools\OpenCover.Console.exe
COVERAGE_REPORT_TOOL := $(PACKAGE_ROOT)\reportgenerator\4.8.8\tools\net47\ReportGenerator.exe
COVERAGE_REPORT := $(TDD_DIR)\results.xml

TEST_PROJ = Calendar.Tests
TESTS = .\$(TEST_PROJ)\bin\$(CONFIG)\$(TEST_PROJ).dll

OPENCOVER_ASSEMBLY_FILTER := -nunit.framework;-$(TEST_PROJ);

# Version and Git related variables
GIT_LONG_HASH := $(shell git rev-parse HEAD)
GIT_SHORT_HASH := $(shell git rev-parse --short HEAD)
SHARED_ASSEMBLY_FILE := .\SharedAssemblyInfo.cs
VERSION_FILE := .\version.txt

# Solution Information
SOLUTION := Calendar
SOLUTION_FILE := .\$(SOLUTION).sln

# Build output variables
RELEASE_DIR := .\Release
ARTIFACTS_DIR := .\artifacts

# default (debug) values (overridden during non-debug builds)
CONFIG := Debug
VERSION := 0.0.0.0
ZIP_TAG := $(GIT_SHORT_HASH)

include .\make\utilities.mk

# Overwrites SharedAssemblyInfo.cs
# $1 Informational Version
# $2 Version
define set_assembly_info
	@echo using System.Reflection;>$(SHARED_ASSEMBLY_FILE)
	@echo [assembly: AssemblyInformationalVersion("$1")]>>$(SHARED_ASSEMBLY_FILE)
	@echo [assembly: AssemblyVersion("$2")]>> $(SHARED_ASSEMBLY_FILE)
	@echo [assembly: AssemblyFileVersion("$2")]>>$(SHARED_ASSEMBLY_FILE)
endef

# Copies contents of bin\<CONFIG> folder to another folder
# $1 project name
# $2 is folder to copy to
define copy_bin_to_folder
	$(call copy_to_folder,.\$1\bin\$(CONFIG),$2)
endef

# Copies all output files for a specified project to $(RELEASE_DIR)
# $1 Name of project to copy files for
define copy_exe_to_release
	$(call copy_bin_to_folder,$1,$(RELEASE_DIR)\$1)
endef

###############################
# Make rules
###############################
.DEFAULT_GOAL := tdd

# Overwrites $(SHARED_ASSEMBLY_FILE) information for build using version and git hash
.PHONY: set_assembly_info
set_assembly_info:
	@echo _
	@echo -----------------------------------
	@echo Injecting version and git hash:
	@echo Version: $(VERSION) ($(CONFIG))
	@echo Githash: $(GIT_LONG_HASH)
	@echo -----------------------------------
	$(call set_assembly_info,v$(VERSION):$(CONFIG):$(GIT_LONG_HASH),$(VERSION))

# Overwrites $(SHARED_ASSEMBLY_FILE) information back to defaults
.PHONY: clear_assembly_info
clear_assembly_info:
	@echo _
	@echo -----------------------------------
	@echo Erasing version and git hash to
	@echo prevent rogue local builds ...
	@echo -----------------------------------
	$(call set_assembly_info,LOCAL,0.0.0.0)

## Builds the solution
.PHONY: build
build:
	@echo _
	@echo -----------------------------------
	@echo Building Solution ($(CONFIG)) ...
	@echo -----------------------------------
	$(COMPILER) $(SOLUTION_FILE) -v:m -nologo -t:Rebuild -p:Configuration=$(CONFIG) -restore -m -nr:False

# Runs test framework and coverage tool
.PHONY: tdd
tdd: build
	@echo _
	@echo -----------------------------------
	@echo Running TDD tests w/ coverage ...
	@echo -----------------------------------
	$(call delete_dir,$(TDD_DIR))
	@md $(TDD_DIR)
	$(COVERAGE_TOOL) -target:$(TDD_TOOL) -targetargs:"$(TESTS) --work=$(TDD_DIR)" -register:user -output:$(COVERAGE_REPORT)
	$(COVERAGE_REPORT_TOOL) -reports:$(COVERAGE_REPORT) -targetdir:$(TDD_DIR) -assemblyFilters:$(OPENCOVER_ASSEMBLY_FILTER) -verbosity:Warning -tag:$(GIT_LONG_HASH)

# Copies output from bin/$(CONFIG) -> Release directory
.PHONY: copy_to_release
copy_to_release: build
	@echo _
	@echo -----------------------------------
	@echo Copying build output to $(RELEASE_DIR) ...
	@echo -----------------------------------
	$(call copy_exe_to_release,Calendar)

# This rule packages build output to $(ARTIFACTS_DIR) directory
.PHONY: create_artifacts
create_artifacts: copy_to_release
	@echo _
	@echo -----------------------------------
	@echo Zipping Projects to $(ARTIFACTS_DIR) ...
	@echo -----------------------------------
	$(call make_dir,$(ARTIFACTS_DIR))
	$(call zip_files,$(RELEASE_DIR)\Calendar,Calendar_$(CONFIG)_$(ZIP_TAG).zip,$(ARTIFACTS_DIR))
	$(call zip_files,$(TDD_DIR),Calendar_Coverage_$(ZIP_TAG).zip,$(ARTIFACTS_DIR))

# This rule runs main rules for builds
.PHONY: build_configuration
build_configuration: set_assembly_info tdd create_artifacts clear_assembly_info

## Executes a release build
.PHONY: release
release: CONFIG := Release
release: VERSION := $(shell type $(VERSION_FILE))
release: ZIP_TAG := $(VERSION)
release: build_configuration

## Executes a debug build
.PHONY: debug
debug: build_configuration

## This rule cleans the solution and deletes $(RELEASE_DIR) and $(ARTIFACTS)
.PHONY: clean
clean: clear_assembly_info
	@echo _
	@echo -----------------------------------
	@echo Cleaning solution ...
	@echo -----------------------------------
	$(call delete_dir,$(ARTIFACTS_DIR))
	$(call delete_dir,$(RELEASE_DIR))
	$(COMPILER) $(SOLUTION_FILE) -v:m -nologo -t:Clean -p:Configuration=Debug -m -nr:False
	$(COMPILER) $(SOLUTION_FILE) -v:m -nologo -t:Clean -p:Configuration=Release -m -nr:False