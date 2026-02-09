namespace Prueba.Web.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string[] Errors { get; set; } = Array.Empty<string>();
        public T? Data { get; set; }
    }
}
