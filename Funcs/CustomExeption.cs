namespace CustomExceptionFun
{
    public class MacAlreadyExistsException : Exception
    {
        public MacAlreadyExistsException(string mac)
            : base($"MAC Already Exists: {mac}")
        {
        }
    }

}