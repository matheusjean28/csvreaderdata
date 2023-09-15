using System.ComponentModel.DataAnnotations;
using MacToDatabaseInterface.Interface;

namespace MacToDatabaseModel
{
    public class MacToDatabase : IMacToDatabase
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Model { get; set; } = string.Empty;
        public string Mac { get; set; } = string.Empty;
        public bool Problem { get; set; }
        public bool RemoteAccess { get; set; }

        
    }
}