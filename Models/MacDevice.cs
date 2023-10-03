namespace MacDeviceModels
{
    public class MacDevice
    {

        public int Id;
        public string Model { get; set; } = string.Empty;
        public string Mac { get; set; } = string.Empty;
        public bool Problem { get; set; } = false;
        public bool RemoteAcess { get; set; } = false;

    }
}