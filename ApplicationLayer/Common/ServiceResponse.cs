
namespace Application.Common
{
    public class ServiceResponse<T>
    {
        public List<T> values { get; set; } = new List<T>();
        public bool status { get; set; } = false;
        public string Message { get; set; } = string.Empty;

        public ServiceResponse(bool status, string Message)
        {
            this.status = status;
            this.Message = Message;
        }
        public ServiceResponse(bool status,string Message , List<T> values) :this(status,Message)
        {   
            this.values = values;
        }
    }
}
