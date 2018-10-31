using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Stamp.Integration.Services.Models
{
    public class InvoiceDraftInputModel
    {
        public string MerchantInvoiceId { get; set; }
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset CreatedOn { get; set; }

        public IEnumerable<InvoiceItemDraftInputModel> Items { get; set; }

        public decimal Discount { get; set; }
    }
}