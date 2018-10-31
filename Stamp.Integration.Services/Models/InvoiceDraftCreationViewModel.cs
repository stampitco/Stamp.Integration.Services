using System;

namespace Stamp.Integration.Services.Models
{
    public class InvoiceDraftCreationViewModel
    {
        public Guid InvoiceId { get; set; }
        public Guid DraftId { get; set; }
    }
}