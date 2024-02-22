namespace Db3Parser;

using System;
using System.Collections.Generic;
using System.Data.SQLite;

class ParseDb3Files
{
    public static void Parse(string table, string column, string value, List<string> dbPaths)
    {
        Console.WriteLine("The following db3 files match the criteria:");
        Console.WriteLine();
        
        foreach (string dbPath in dbPaths)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();
                
                var query = $"Select * from {table} where NetworkName='Telecom Network' and {column}={value}";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var mapServiceUrl = reader.GetString(reader.GetOrdinal("MapServiceUrl"));
                            var networkId = reader.GetInt32(reader.GetOrdinal("NetworkId"));

                            Console.WriteLine($"{Path.GetFileName(dbPath)} has {column}: {value} for Telecom Network on map service: {mapServiceUrl}");
                        }
                    }
                }
            }
        }
    }
}
