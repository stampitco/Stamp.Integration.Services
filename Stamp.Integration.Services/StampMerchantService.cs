using Newtonsoft.Json;
using Stamp.Integration.Services.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Stamp.Integration.Services
{
    public class StampMerchantService : IStampMerchantService
    {
        private readonly Uri _baseUri;
        private static readonly HttpClient HttpClient = new HttpClient();

        public StampMerchantService(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task<MerchantAccessTokenViewModel> GetAccessToken(MerchantLoginInputModel inputModel)
        {
            var response = await HttpClient.PostAsJsonAsync(new Uri(_baseUri, "api/auth/signin"), inputModel)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<MerchantAccessTokenViewModel>(responseContent);
        }
    }
}
