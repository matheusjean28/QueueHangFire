namespace CsvSerializeDataViewModels
{
    public class CsvSerializeDataDto
    {
          public int Id {get;set;}
        public string FileName {get;set;} = string.Empty;
        public required byte[] Data {get;set;} 
    }
}