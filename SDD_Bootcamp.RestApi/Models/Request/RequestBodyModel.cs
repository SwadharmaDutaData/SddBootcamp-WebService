namespace SDD_Bootcamp.RestApi.Models.Request
{
    public class RequestBodyModel<T>
    {
        public string OperationType { get; set; }
        public string OperationDesc { get; set; }
        public T Request { get; set; }
    }
}
