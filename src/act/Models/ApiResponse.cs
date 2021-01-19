namespace act.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse(T payload)
        {
            Payload = payload;
        }

        public T Payload { get; set; }
    }
}