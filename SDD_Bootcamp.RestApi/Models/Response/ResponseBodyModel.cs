using SDD_Bootcamp.RestApi.ApiHelpers;

namespace SDD_Bootcamp.RestApi.Models.Response
{
    public class ResponseBodyModel<T> : ErrorTemplateModel
    {
        public string ResponseType { get; set; }
        public string ResponseMessage { get; set; }
        public T ResponseBody { get; set; }
    }
}
