using ReadCsvFuncs;

class Program
{
    public static void Main(string[] args)
    {
        Task.Run(async () =>
            {
                ReadCsv read = new();
                await read.ReadCsvItens();
            }).GetAwaiter().GetResult();

    }
}
