using Stamp.Integration.Services.Models;
using System;
using System.Threading.Tasks;

namespace Stamp.Integration.Services
{
    public interface IStampInvoicingService
    {
        Task CancelInvoice(Guid invoiceId, InvoiceNoteNumberInputeModel inputModel, string accessToken);
        Task<InvoiceCustomerViewModel> GetCustomerData(Guid invoiceId, string accessToken);
        Task<InvoicePdfUrlViewModel> GetInvoiceNotePdf(Guid invoiceId, string accessToken);
        Task<InvoicePdfUrlViewModel> GetInvoicePdf(Guid invoiceId, string accessToken);
        Task<InvoiceStatusViewModel> GetInvoiceStatus(Guid invoiceId, string accessToken);
        Task<InvoiceOtelloCommunicationIdViewModel> GetOtelloCoomunicationId(Guid invoiceId, string accessToken);
        Task SetDebitNoteNumber(Guid invoiceId, InvoiceNoteNumberInputeModel inputModel, string accessToken);
        Task<InvoiceDraftCreationViewModel> UploadInvoiceDraft(InvoiceDraftInputModel inputModel, string accessToken);
    }
}