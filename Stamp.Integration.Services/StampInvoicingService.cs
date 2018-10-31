using Newtonsoft.Json;
using Stamp.Integration.Services.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Stamp.Integration.Services
{
    public class StampInvoicingService : IStampInvoicingService
    {
        private readonly Uri _baseUri;
        private static readonly HttpClient HttpClient = new HttpClient();

        public StampInvoicingService(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task<InvoiceDraftCreationViewModel> UploadInvoiceDraft(InvoiceDraftInputModel inputModel, string accessToken)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await HttpClient.PostAsJsonAsync(new Uri(_baseUri, "api/i/invoices/draft"), inputModel)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<InvoiceDraftCreationViewModel>(responseContent);
        }

        public async Task<InvoiceStatusViewModel> GetInvoiceStatus(Guid invoiceId, string accessToken)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await HttpClient.GetAsync(new Uri(_baseUri, $"api/i/invoices/{invoiceId}/status"))
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<InvoiceStatusViewModel>(responseContent);
        }

        public async Task<InvoiceCustomerViewModel> GetCustomerData(Guid invoiceId, string accessToken)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await HttpClient.GetAsync(new Uri(_baseUri, $"api/i/invoices/{invoiceId}/customer"))
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<InvoiceCustomerViewModel>(responseContent);
        }

        public async Task<InvoicePdfUrlViewModel> GetInvoicePdf(Guid invoiceId, string accessToken)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await HttpClient.GetAsync(new Uri(_baseUri, $"api/invoices/{invoiceId}/pdf"))
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<InvoicePdfUrlViewModel>(responseContent);
        }

        public async Task<InvoicePdfUrlViewModel> GetInvoiceNotePdf(Guid invoiceId, string accessToken)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await HttpClient.GetAsync(new Uri(_baseUri, $"api/invoices/{invoiceId}/note/pdf"))
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<InvoicePdfUrlViewModel>(responseContent);
        }

        public async Task<InvoiceOtelloCommunicationIdViewModel> GetOtelloCoomunicationId(Guid invoiceId,
            string accessToken)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await HttpClient.GetAsync(new Uri(_baseUri, $"api/invoices/{invoiceId}/crf0"))
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<InvoiceOtelloCommunicationIdViewModel>(responseContent);
        }

        public async Task CancelInvoice(Guid invoiceId, InvoiceNoteNumberInputeModel inputModel, string accessToken)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await HttpClient.PostAsJsonAsync(new Uri(_baseUri, $"api/invoices/{invoiceId}/cancel"), inputModel)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        public async Task SetDebitNoteNumber(Guid invoiceId, InvoiceNoteNumberInputeModel inputModel, string accessToken)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await HttpClient.PostAsJsonAsync(new Uri(_baseUri, $"api/invoices/{invoiceId}/note/debit/number"), inputModel)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }
    }
}