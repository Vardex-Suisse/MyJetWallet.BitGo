using System;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models
{
    public class Error
    {
        [JsonProperty("name", Required = Required.Default)]
        public string Code { get; set; }
        
        [JsonProperty("error", Required = Required.Default)]
        public string ErrorMessage { get; set; }
        
        [JsonProperty("requestId", Required = Required.Default)]
        public string RequestId { get; set; }

        [JsonProperty("message", Required = Required.Default)]
        public string Message { get; set; }

        internal Error()
        {
        }
        
        internal Error(string code, string message)
        {
            this.Code = code;
            this.ErrorMessage = message;
        }

        internal Error(string code, string message, string requestId)
        {
            this.Code = code;
            this.ErrorMessage = message;
            this.RequestId = requestId;
        }

        public override string ToString()
        {
            return $"[{this.Code}] {this.ErrorMessage}";
        }
    }

    public class ArgumentError : Error
    {
        public ArgumentError(string message) : base("Invalid Parameter", message) { }
    }

    public class BitGoErrorException : Exception
    {
        public Error Error { get; set; }

        public BitGoErrorException(Error error) : base($"Error response from BitGo: [{error?.Code}] {error?.ErrorMessage}")
        {
            Error = error;
        }
    }
}
