using System.Net;
using System.Text.Json.Serialization;

namespace App.Application;

    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public List<string>? ErrorMessage { get; set; }
        [JsonIgnore]
        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
        [JsonIgnore]
        public bool IsFailure => !IsSuccess;
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        [JsonIgnore]
        public string UrlAsCreated { get; set; }

        //static factory methods for creating instances of ServiceResult<T>
        public static ServiceResult<T> Success(T data, HttpStatusCode httpStatusCode=HttpStatusCode.OK)
        {
            return new ServiceResult<T>
            {
                Data = data, 
                StatusCode = httpStatusCode
            };
        }

        public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
        {
            return new ServiceResult<T>
            {
                Data = data,
                StatusCode = HttpStatusCode.Created,
                UrlAsCreated = urlAsCreated
            };
        }

    public static ServiceResult<T> Failure(List<string> errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                ErrorMessage = errorMessage,
                StatusCode = httpStatusCode
            };
        }

        public static ServiceResult<T> Failure(string errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                ErrorMessage = [errorMessage],
                StatusCode = httpStatusCode
            };
        }
    }

    public class ServiceResult
    {
        public List<string>? ErrorMessage { get; set; }
        [JsonIgnore]
        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
        [JsonIgnore]
        public bool IsFailure => !IsSuccess;
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        //static factory methods for creating instances of ServiceResult
        public static ServiceResult Success(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new ServiceResult
            {
                StatusCode = httpStatusCode
            };
        }
        public static ServiceResult Failure(List<string> errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                ErrorMessage = errorMessage,
                StatusCode = httpStatusCode
            };
        }

        public static ServiceResult Failure(string errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                ErrorMessage = [errorMessage],
                StatusCode = httpStatusCode
            };
        }
    }