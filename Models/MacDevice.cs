using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace MacDeviceModels
{
    public class MacDevice
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Model { get; set; } = string.Empty;
        public string Mac { get; set; } = string.Empty;

        [BooleanTrueValues]
        public bool Problem { get; set; }
        [BooleanTrueValues]

        public bool RemoteAccess { get; set; }
    }
}