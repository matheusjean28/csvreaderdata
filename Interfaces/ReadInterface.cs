using MacToDatabaseModel;
namespace Read.Interfaces
{
    public interface IRead
    {
        
         Task<IEnumerable<MacToDatabase>>ReadCsvItens();
};


}