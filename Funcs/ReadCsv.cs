using MacToDatabaseModel;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using DeviceCsv.Model;
using System.Collections;
using System.Diagnostics;

namespace ReadCsvFuncs
{
    public class ReadCsv
    {
        private readonly string folderName = "Temp";
        private readonly string folderPath = Directory.GetCurrentDirectory();

        public async Task<IEnumerable<string>> ReadCsvItens()
        {
            var _folderPath = folderPath;
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
            _folderPath = Path.GetFullPath(folderName);



            List<string> Devices = new();
            List<MacToDatabase> MacToDatabases = new();
            MacToDatabase deviceToList = new();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
            using var reader = new StreamReader("Data/FileToRead.csv");



            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<Device>();

                foreach (var device in records)
                {
                    static bool IsValidMacAddress(string mac)
                    {
                        string pattern = @"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$";
                        return Regex.IsMatch(mac, pattern);
                    }
                    string item = $"Model: {device.Model}\nMac: {device.Mac}\nProblem: {device.Problem}\nRemoteAcess: {device.RemoteAccess}\nDescription: {device.Data}\n\r";

                    if (device.Model.Length <= 0 || device.Model.Length >= 99)
                    {
                        Console.WriteLine("is an invalid model");
                        Console.WriteLine(device.Model);
                        throw new Exception($"the item {device.Model} has an error, please check FileLog");
                    }
                    else
                    {
                        Console.WriteLine("is a valid model");
                        deviceToList.Model = device.Model;
                    }




                    if (IsValidMacAddress(device.Mac))
                    {
                        Console.WriteLine("O endereço MAC é válido.");
                        deviceToList.Mac = device.Mac;
                    }
                    else
                    {
                        throw new Exception($"the item {device.Id} has an error, please check FileLog");
                    }

                    // if (device.Problem )
                    // {
                    //     Console.WriteLine("is an invalid model");
                    //     Console.WriteLine(device.Problem);
                    //     throw new Exception($"the item {device.Problem} has an error, please check FileLog");
                    // }
                    // else
                    // {
                    //     deviceToList.Problem = device.Problem;
                    // }

                    // if (device.RemoteAccess != false || device.RemoteAccess != true)
                    // {
                    //     Console.WriteLine("is an invalid model");
                    //     Console.WriteLine(device.RemoteAccess);
                    //     throw new Exception($"the item {device.RemoteAccess} has an error, please check FileLog");
                    // }
                    // else
                    // {
                    //     deviceToList.RemoteAccess = device.RemoteAccess;
                    // }



                    Devices.Add(item);
                    await File.AppendAllTextAsync(Path.Combine(_folderPath, "Files.csv"), item);
                }

            }
            return Devices;
        }
    }
};






