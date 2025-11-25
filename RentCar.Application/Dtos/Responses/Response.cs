namespace RentCar.Application.Dtos.Responses
{
    public enum ResponseStatus
    {
        Success,
        Conflict,
        NotFound,
        ValidationError,
        NotAllowed
    }

    public abstract class Response<T> where T : class
    {
        public IEnumerable<T>? Data { get; set; } = null;
        
        public ResponseStatus Status { get; set; }

        public string? Message { get; set; }
    }
}
