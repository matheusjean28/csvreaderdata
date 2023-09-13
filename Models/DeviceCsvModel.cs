using CsvHelper.Configuration.Attributes;

namespace DeviceCsv.Model
{
    public class Device
    {

        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Mac { get; set; } = string.Empty;

        
        public string Data { get; set; } =  string.Empty;
        
        [BooleanTrueValues("false")]
        [BooleanFalseValues("true")]
        public bool RemoteAcess;

        [BooleanTrueValues("false")]
        [BooleanFalseValues("true")]
        public bool Problem;
    }
}