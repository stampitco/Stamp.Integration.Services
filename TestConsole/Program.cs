using Stamp.Integration.Services;
using Stamp.Integration.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        static async Task MainAsync()
        {
            var stampMerchantPortalUrl = "REPLACE_WITH_MERCHANT_PORTAL_URL";
            var stampMerchantApiUri = new Uri("REPLACE_WITH_MERCHANT_API_URL");
            var stampInvoicingApiUri = new Uri("REPLACE_WITH_INVOICING_API_URL");

            var email = "REPLACE_WITH_EMAIL";
            var password = "REPLACE_WITH_PASSWORD";
            

            var stampMerchantService = new StampMerchantService(stampMerchantApiUri);
            var stampInvoicingService = new StampInvoicingService(stampInvoicingApiUri);

            try
            {
                var accessToken = await stampMerchantService.GetAccessToken(new MerchantLoginInputModel()
                {
                    Email = email,
                    Password = password

                }).ConfigureAwait(false);

                Console.WriteLine("[Access Token] {0}", accessToken.AccessToken);

                var uploadInvoiceDraft = await stampInvoicingService
                    .UploadInvoiceDraft(new InvoiceDraftInputModel()
                    {
                        CreatedOn = DateTimeOffset.UtcNow,
                        Discount = 0,
                        MerchantInvoiceId = $"FAT-{DateTime.Now.Ticks}",
                        Items = new List<InvoiceItemDraftInputModel>()
                        {
                            new InvoiceItemDraftInputModel()
                            {
                                // min 5 chars
                                Description = "Borsa in pelle nera",
                                ItemCode = "IT555A",
                                Quantity = 1,
                                UnitPriceWithVat = 1899.99m,
                                VatRate = 22
                            }
                        }
                    }, accessToken.AccessToken).ConfigureAwait(false);


                Console.WriteLine("[Upload Invoice Draft] DraftId: {0}", uploadInvoiceDraft.DraftId);
                Console.WriteLine("[Upload Invoice Draft] InvoiceId: {0}", uploadInvoiceDraft.InvoiceId);

                // Open browser with Stamp Merchant Portal

                Process.Start($"{stampMerchantPortalUrl}/draft-invoice/{uploadInvoiceDraft.DraftId}");

                Console.WriteLine(
                    "Search the customer in Stamp Merchant Portal using the phone number provided. Once the invoice has been saved, press any key to continue...");

                Console.ReadLine();

                var customerData = await stampInvoicingService
                    .GetCustomerData(uploadInvoiceDraft.InvoiceId, accessToken.AccessToken).ConfigureAwait(false);

                Console.WriteLine("[Invoice customer data] Name: {0} Identity document id: {1}", customerData.Fullname,
                    customerData.IdentityDocumentId);

                var invoiceStatus = await stampInvoicingService
                    .GetInvoiceStatus(uploadInvoiceDraft.InvoiceId, accessToken.AccessToken).ConfigureAwait(false);

                Console.WriteLine("[Invoice Status] {0}", invoiceStatus.Status);

                var otelloCommunicationId = await stampInvoicingService
                    .GetOtelloCoomunicationId(uploadInvoiceDraft.InvoiceId, accessToken.AccessToken).ConfigureAwait(false);

                Console.WriteLine("[Otello Communication Id] {0}", otelloCommunicationId.OtelloCommunicationId);

                await stampInvoicingService.CancelInvoice(uploadInvoiceDraft.InvoiceId,
                    new InvoiceNoteNumberInputeModel()
                    {
                        InvoiceNoteNumber = $"NOT-{DateTime.Now.Ticks}"
                    }, accessToken.AccessToken).ConfigureAwait(false);

                Console.WriteLine("[Cancel invoice] Succeeded");

                Process.Start($"{stampMerchantPortalUrl}/view-invoice/{uploadInvoiceDraft.InvoiceId}");

                Console.WriteLine("Opening Stamp Merchant Portal to check invoice status. Press any key to continue...");
                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine("[Error] {0}", ex);
            }
        }
    }
}
