using CustomExceptionFun;
using System.Text.RegularExpressions;
using DeviceContext;
using MethodsInterfaces;
using Microsoft.EntityFrameworkCore;
using MainDatabaseContext;
namespace MethodsFuncs
{
    public class Methods : IMethods
    {
        private readonly string _uploadPath;

        public Methods()
        {
            _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFile");
        }

        public void CheckAndCreateFolderIfNotExist()
        {
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        public bool CheckFileExtension(string _fileName)
        {
            var Format = Path.GetExtension(_fileName);
            if (Format != ".csv")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<string> IsValidMacAddress(MainDatabase db, string mac)
        {
            List<string> AlreadyExistsMacs = new();
            string pattern = @"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$";
            var MatchMac = Regex.IsMatch(mac, pattern).ToString();

            //verificar no novo contexto
             var _checkMac = await db.DevicesToMain.FirstOrDefaultAsync(item => item.Mac == mac);
            if (_checkMac != null)
            {
                AlreadyExistsMacs.Add(mac);
                throw new MacAlreadyExistsException(mac); 
            }

            return MatchMac;
        }

    }
}