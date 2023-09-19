using InmemoryDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReadCsvFuncs;

class Program
{
    public static void Main(string[] args)
    {
          var serviceProvider = new ServiceCollection()
            .AddDbContext<MacContext>(options =>
            options.UseInMemoryDatabase(databaseName: "InMemoryDatabase"))
            .BuildServiceProvider();

        var dbContext = serviceProvider.GetRequiredService<MacContext>();

        Task.Run(async () =>
            {
                ReadCsv read = new();
                await read.ReadCsvItens(dbContext);
            }).GetAwaiter().GetResult();

    }
}
