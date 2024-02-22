// See https://aka.ms/new-console-template for more information

using Db3Parser;

Console.WriteLine("Enter folder location of db3 files:");
var folderLocation = Console.ReadLine();

if (folderLocation == null)
{
    Console.WriteLine("Invalid folder location, exiting.");
    return;
}
Console.WriteLine("By passing in the table, column(s) and value(s), this program will return the files that match those criteria.  For example, to find all files that have NotifyCircuitId set to true in NetworkConfiguration, pass in the following: Table: NetworkConfiguration, Column: NotifyCircuitId, Value: 1");
Console.WriteLine();

Console.WriteLine("Enter Table:");
var table = Console.ReadLine();
Console.WriteLine();

Console.WriteLine("Enter Column:");
var column = Console.ReadLine();
Console.WriteLine();

Console.WriteLine("Enter Value:");
var value = Console.ReadLine();
Console.WriteLine();

if (table == null || column == null || value == null)
    return;

var sqlQuery = $"Select * from {table} where {column}={value}";

Console.WriteLine($"Processing {sqlQuery}...");
Console.WriteLine();

try
{
    string[] files = Directory.GetFiles(folderLocation);

    // Filter the files to keep only the .db3 files
    List<string> db3Files = files
        .Where(file => Path.GetExtension(file).Equals(".db3", StringComparison.OrdinalIgnoreCase)).ToList();

    ParseDb3Files.Parse(table, column, value, db3Files);
}
catch (Exception e)
{
    Console.WriteLine(e);
}
finally
{
    Console.WriteLine();
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}