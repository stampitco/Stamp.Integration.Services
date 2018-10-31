using Stamp.Integration.Services.Models;
using System.Threading.Tasks;

namespace Stamp.Integration.Services
{
    public interface IStampMerchantService
    {
        Task<MerchantAccessTokenViewModel> GetAccessToken(MerchantLoginInputModel inputModel);
    }
}