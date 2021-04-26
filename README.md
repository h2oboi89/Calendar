# Calendar

## Description
Reimplemented a homework problem from freshmen year (2007) at Rose-Hulman for giggles because it kept bouncing around in my head. Have made some improvements beyond what I remember the actual requirements being.

* Calendar is EXE project. 
* Calendar.Library has most of the source. 
* Calendar.Tests has test code

`Calendar.exe --help` for usage

## Sample Output
```
> .\Calendar.exe --help
Calendar v1.0.0.0:Release:82a3e524d309adcc351328cb691e20d351ea852e
Copyright c  2021

  -y, --year     [int] Year to print calendar for (default is current year)

  -m, --month    [int] 1-12. The month to print. Limits calendar to print just specified month.

  -w, --width    [int] How many months wide the calendar is (default is 4)

  -d, --date     [string] MM-dd. Date to highlight. Months part is optional (default is current
                 month).

  --today        [bool] If set today's date will be highlighted (default is false).

  --help         Display this help screen.

  --version      Display version information.

```

```
> .\Calendar.exe
======================================================================================

                                         2021

======================================================================================

      January               February               March                 April
--------------------------------------------------------------------------------------
 S  M  T  W  T  F  S   S  M  T  W  T  F  S   S  M  T  W  T  F  S   S  M  T  W  T  F  S
                1  2      1  2  3  4  5  6      1  2  3  4  5  6               1  2  3
 3  4  5  6  7  8  9   7  8  9 10 11 12 13   7  8  9 10 11 12 13   4  5  6  7  8  9 10
10 11 12 13 14 15 16  14 15 16 17 18 19 20  14 15 16 17 18 19 20  11 12 13 14 15 16 17
17 18 19 20 21 22 23  21 22 23 24 25 26 27  21 22 23 24 25 26 27  18 19 20 21 22 23 24
24 25 26 27 28 29 30  28                    28 29 30 31           25 26 27 28 29 30
31

        May                   June                  July                 August
--------------------------------------------------------------------------------------
 S  M  T  W  T  F  S   S  M  T  W  T  F  S   S  M  T  W  T  F  S   S  M  T  W  T  F  S
                   1         1  2  3  4  5               1  2  3   1  2  3  4  5  6  7
 2  3  4  5  6  7  8   6  7  8  9 10 11 12   4  5  6  7  8  9 10   8  9 10 11 12 13 14
 9 10 11 12 13 14 15  13 14 15 16 17 18 19  11 12 13 14 15 16 17  15 16 17 18 19 20 21
16 17 18 19 20 21 22  20 21 22 23 24 25 26  18 19 20 21 22 23 24  22 23 24 25 26 27 28
23 24 25 26 27 28 29  27 28 29 30           25 26 27 28 29 30 31  29 30 31
30 31

     September              October               November              December
--------------------------------------------------------------------------------------
 S  M  T  W  T  F  S   S  M  T  W  T  F  S   S  M  T  W  T  F  S   S  M  T  W  T  F  S
          1  2  3  4                  1  2      1  2  3  4  5  6            1  2  3  4
 5  6  7  8  9 10 11   3  4  5  6  7  8  9   7  8  9 10 11 12 13   5  6  7  8  9 10 11
12 13 14 15 16 17 18  10 11 12 13 14 15 16  14 15 16 17 18 19 20  12 13 14 15 16 17 18
19 20 21 22 23 24 25  17 18 19 20 21 22 23  21 22 23 24 25 26 27  19 20 21 22 23 24 25
26 27 28 29 30        24 25 26 27 28 29 30  28 29 30              26 27 28 29 30 31
                      31

```

## Development
* `make tdd` for tests
* `make release` for full build

See [Makefile](./Makefile) for more details.

You can also build and test via Visual Studio.

Targeting .NET 4.8 using Visual Studio 19 (Community).

