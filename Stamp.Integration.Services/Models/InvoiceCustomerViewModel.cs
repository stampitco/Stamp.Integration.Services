using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Stamp.Integration.Services.Models
{
    public class InvoiceCustomerViewModel
    {
        public string Fullname { get; set; }
        public string IdentityDocumentId { get; set; }
        public string IdentityDocumentIssuingCountryIso2Code { get; set; }
        public string ResidencyCountryIso2Code { get; set; }
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime DateOfBirth { get; set; }
        public DocumentType DocumentType { get; set; }
    }
}