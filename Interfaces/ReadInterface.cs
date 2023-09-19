using MacToDatabaseModel;
using InmemoryDb;

namespace Read.Interfaces
{
    public interface IRead
    {
        
         Task<IEnumerable<MacToDatabase>>ReadCsvItens(MacContext db );
};


}