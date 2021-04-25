###############################
# Utility functions
###############################
# Zips a the specified files and move it to the specified location
# $1 Files to be zipped
# $2 Name of the zip file
# $3 Location to place the zip file
# Pre - If zipping on Windows 'ZIP_TOOL' needs to be defined
define zip_files
	if not ["$1"]==[] ($(subst /,\,$(ZIP_TOOL)) a -tzip -mx9 $2 $(subst /,\,$1) && @move $2 $3) ELSE ( echo Error: No files && exit 1 )
endef

# Deletes directory if it exists
# $1 Directory to delete
define delete_dir
	@if EXIST $1 rmdir $1 /s /q;
endef

# Deletes file if it exists
# $1 File to delete
define delete_file
	@if EXIST $1 del /f $1
endef

# Creates a directory if it does not exist
# $! Directory to create
define make_dir
	@if NOT EXIST $1 mkdir $1;
endef

# copies contents of a folder to another folder
# $1 folder to copy from
# $2 folder to copy to
define copy_to_folder
	@echo $1 ^> $2
	@(robocopy $1 $2 /S /NFL /NDL /NJH /NJS /NC /NS /NP) ^& if %ERRORLEVEL% leq 1 exit 0
endef