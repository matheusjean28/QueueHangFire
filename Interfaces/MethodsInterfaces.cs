
namespace MethodsInterfaces
{
    public interface IMethods
    {
        void CheckAndCreateFolderIfNotExist();
        public bool CheckFileExtension(string _fileName);
        // IAsyncEnumerable<string> CheckIfMacAlreadyExists(DeviceDb db, string _macDevice);
    }

}