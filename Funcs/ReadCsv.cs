using System.IO;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using DeviceCsv.Model;
using System.Collections;

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
                    string item = $"Model: {device.Model}\nMac: {device.Mac}\nProblem: {device.Problem}\nRemoteAcess: {device.RemoteAccess}\nDescription: {device.Data}\n\r";
                    Devices.Add(item);
                   await File.AppendAllTextAsync(Path.Combine(_folderPath, "Files.csv"),item);
                }

            }
            return Devices;
        }
    }
};






