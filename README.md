# CsvSearch

CsvSearch is a command-line tool developed in C# that allows you to search for specific values in a CSV file. The tool is designed to work seamlessly with CSV files both with and without headers.

## Usage
Assuming Date format is dd/mm/yyyy. Please ensure csv contains the same format and the command line as well if you are searching using Date.

To use CsvSearch, navigate to the root folder of the project and execute the following command in the terminal:
```
dotnet run ./file.csv 2 Alberto
```

- `./file.csv`: The path to the CSV file you want to search in.
- `2`: The column number (index) to search in.
- `Alberto`: The search key you want to find in the specified column.

## Features

CsvSearch is primarily designed to handle CSV files without headers, but works seamlessly with Headers as well.

### CSV File

When CsvSearch encounters a CSV file without headers, you need to provide the column number (index) to specify the search column. For example, if the CSV file does not have headers and looks like this:

```
1,John,Doe,1990-06-15
2,Jane,Smith,1985-09-20
3,Alberto,Vega,1987-11-25
```

The command `dotnet run ./file.csv 2 Alberto` will search for the value `Alberto` in the second column (index 2). 

## Dependencies

CsvSearch uses the CsvHelper library to handle CSV parsing and manipulation. This library ensures smooth reading and searching of CSV data.

## Installation

To install CsvSearch, you need to have the .NET SDK installed on your machine. You can download it from the official .NET website: https://dotnet.microsoft.com/download

Once you have the .NET SDK installed, clone the CsvSearch repository from GitHub and navigate to the root folder of the project in your terminal.

