using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Results
{
    public class OperationResult<T>
    {
        public bool Succeeded { get; private set; }
        public string? Message { get; private set; }
        public string[] Errors { get; private set; } = Array.Empty<string>();
        public T? Data { get; private set; }

        private OperationResult() { }

        public static OperationResult<T> Success(T data, string? message = null)
        {
            return new OperationResult<T>
            {
                Succeeded = true,
                Data = data,
                Message = message
            };
        }

        public static OperationResult<T> Failure(string message, params string[] errors)
        {
            return new OperationResult<T>
            {
                Succeeded = false,
                Message = message,
                Errors = errors.Length > 0 ? errors : new[] { message }
            };
        }

        public static OperationResult<T> Failure(string message, IEnumerable<string> errors)
        {
            return new OperationResult<T>
            {
                Succeeded = false,
                Message = message,
                Errors = errors.ToArray()
            };
        }
    }
}
