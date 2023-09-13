using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using DeviceCsv.Model;


var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    PrepareHeaderForMatch = args => args.Header.ToLower(),
};
using var reader = new StreamReader("Data/FileToRead.csv");



using (var csv = new CsvReader(reader, config))
{
    var records = csv.GetRecords<Device>();

    foreach (var device in records )
    {
        Console.WriteLine($"Model: {device.Model}\nMac: {device.Mac}\nProblem: {device.Problem}\nRemoteAcess: {device.RemoteAcess}\nDescription: {device.Data}\n\r");
        
    }
}




