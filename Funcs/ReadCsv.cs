using MacToDatabaseModel;
using System.Text.RegularExpressions;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using DeviceCsv.Model;
using Read.Interfaces;

namespace ReadCsvFuncs
{
    public class ReadCsv : IRead
    {
        private readonly string folderName = "Temp";
        private readonly string folderPath = Directory.GetCurrentDirectory();

        public async Task<IEnumerable<MacToDatabase>> ReadCsvItens()
        {
            var _folderPath = folderPath;
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
            _folderPath = Path.GetFullPath(folderName);


            List<MacToDatabase> macList = new();


            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
            using var reader = new StreamReader("Data/FileToRead.csv");


            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecordsAsync<Device>();

            await foreach (var device in records)
            {
                MacToDatabase deviceItem = new();
                static bool IsValidMacAddress(string mac)
                {
                    string pattern = @"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$";
                    return Regex.IsMatch(mac, pattern);
                }
                if (IsValidMacAddress(device.Mac))
                {
                    deviceItem.Mac = device.Mac;
                }
                else
                {
                    string errorMessage = $"[Error Occurred at {DateTime.Now}] - Invalid Model: {device.Model}, MAC: {device.Mac}";

                    await File.AppendAllTextAsync(Path.Combine(_folderPath, "Error.csv"), errorMessage);
                    throw new Exception($"the item {device.Id} has an error, please check FileLog");
                }

                if (device.Model.Length <= 0 || device.Model.Length >= 99)
                {
                    string errorMessage = $"[Error Occurred at {DateTime.Now}] - Invalid Model: {device.Model}, MAC: {device.Mac}";

                    await File.AppendAllTextAsync(Path.Combine(_folderPath, "Error.csv"), errorMessage);
                    throw new Exception($"the item {device.Model} has an error, please check FileLog");
                }
                else
                {
                    deviceItem.Model = device.Model;
                }
                macList.Add(deviceItem);



            }
            foreach (var item in macList)
            {
                Console.WriteLine(item.Mac);

            }
            return macList;
        }
    }
};






