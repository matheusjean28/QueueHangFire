namespace CsvFileModels
{
    public class CsvFile 
    {
        public int Id {get;set;}
        public string Name {get;set;} = string.Empty;
        public required byte[] Data {get;set;} 
    }   
}