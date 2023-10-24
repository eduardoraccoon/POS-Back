using POS.Application.Commons.Bases;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Dtos.Provider.Response;

namespace POS.Application.Interfaces
{
    public interface IProviderApplication
    {
        Task<BaseResponse<IEnumerable<ProviderResponseDto>>> ListProviders();
        Task<BaseResponse<ProviderResponseDto>> ProviderById(int providerId);
        Task<BaseResponse<bool>> RegisterProvider(ProviderRequestDto requestDto);
        Task<BaseResponse<bool>> EditProvider(int providerId, ProviderRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveProvider(int providerId);
    }
}
