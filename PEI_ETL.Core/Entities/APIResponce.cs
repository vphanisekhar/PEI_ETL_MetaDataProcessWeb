namespace PEI_ETL.Core.Entities
{
    public class APIResponce
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Result { get; set; }
    }
}
