using System;
using CsvHelper;
using CsvHelper.Configuration;
using DeviceCsv.Model;
using ReadCsvFuncs;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    public  static async Task  Main(string[] args)
    {
        ReadCsv read = new();
        await read.ReadCsvItens();
    }
}
