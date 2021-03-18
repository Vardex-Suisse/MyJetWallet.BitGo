using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MyJetWallet.BitGo.Models
{
    [DataContract]
    public class Error
    {
        [JsonProperty("name", Required = Required.Default), DataMember(Order = 1)]
        public string Code { get; set; }
        
        [JsonProperty("error", Required = Required.Default), DataMember(Order = 2)]
        public string ErrorMessage { get; set; }
        
        [JsonProperty("requestId", Required = Required.Default), DataMember(Order = 3)]
        public string RequestId { get; set; }

        [JsonProperty("message", Required = Required.Default), DataMember(Order = 4)]
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
