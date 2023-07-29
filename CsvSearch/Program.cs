using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
#nullable enable

if (args.Length < 3)
{
    Console.WriteLine("Enter Correct Arguments: <file_path> <column_index> <search_key>");
    return;
}

string filePath = args[0];
int columnIndex = int.Parse(args[1]);
string searchKey = args[2];
List<ContactInformation> records;
if (!File.Exists(filePath))
{
    Console.WriteLine("File not found.");
    return;
}
if(columnIndex < 0 || columnIndex > 3)
{
    Console.WriteLine("Please enter the correct Column");
    return;
}
ReadCsv();
CheckRecord();

void ReadCsv()
{
    var config = new CsvConfiguration(CultureInfo.InvariantCulture);
    config.HasHeaderRecord = false;
    using (var reader = new StreamReader(filePath))
    using (var csv = new CsvReader(reader, config))
    {
        csv.Context.TypeConverterCache.AddConverter<DateTime>(new CustomDateTimeConverter());
        records = csv.GetRecords<ContactInformation>().ToList();
    }
}

void CheckRecord()
{
    bool result=false;
    foreach (var item in records)
    {
        result = GetColumnValue(item, searchKey);
        if (result)
        {
            DisplayResult(item);
            break;
        }   
    }
    if(!result)
    Console.WriteLine("No matching record found.");
}
void DisplayResult(ContactInformation contactInformation)
{
    Console.WriteLine($"{contactInformation.Id}, {contactInformation.FirstName}, {contactInformation.LastName}, {contactInformation.DateOfBirth:dd/MM/yyyy};");
}
bool GetColumnValue(ContactInformation record,string searchKey)
{
    if (columnIndex == 0)
        return record?.Id?.ToString()?.Equals(searchKey, StringComparison.OrdinalIgnoreCase) ?? false;
    else if (columnIndex == 1)
        return record.FirstName?.Equals(searchKey, StringComparison.OrdinalIgnoreCase) ?? false;
    else if (columnIndex == 2)
        return record?.LastName?.Equals(searchKey, StringComparison.OrdinalIgnoreCase) ?? false;
    else if (columnIndex == 3)
    {
        try
        {
            if (searchKey.EndsWith(";"))
                searchKey = searchKey.TrimEnd(';');
            DateTime dd = DateTime.ParseExact(searchKey, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return record.DateOfBirth.Date == dd.Date;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Search Key format is not correct.It should be DD/MM/YYYY");
            throw new Exception(ex.Message);
        }
    }
    else
        throw new ArgumentException($"Ensure Csv file has  column index: {columnIndex}");
}

public class CustomDateTimeConverter : CsvHelper.TypeConversion.DateTimeConverter
{
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        try
        {
            if (text == null)
            {
                return null;
            }
            if (text.EndsWith(";"))
                text = text.TrimEnd(';');
            DateTime dd = DateTime.ParseExact(text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (!DateTime.TryParse(dd.ToString(), out DateTime result))
                return base.ConvertFromString(DateTime.MinValue.ToString(), row, memberMapData);
            return base.ConvertFromString(dd.ToString(), row, memberMapData);
        }
        catch (Exception)
        {
            Console.WriteLine("Date Format is wrong in the Csv file.It should be DD/MM/YYYY");
        }
        return DateTime.MinValue;
    }
}
public class ContactInformation
{
    public string? Id
    {
        get;
        set;
    }
    public string? FirstName
    {
        get;
        set;
    }
    public string? LastName
    {
        get;
        set;
    }
    public DateTime DateOfBirth
    {
        get;
        set;
    }
}