using POS.Application.Commons.Bases;
using POS.Application.Dtos.Client.Request;
using POS.Application.Dtos.Client.Response;

namespace POS.Application.Interfaces
{
    public interface IClientApplication
    {
        Task<BaseResponse<IEnumerable<ClientResponseDto>>> ListClients();
        Task<BaseResponse<ClientResponseDto>> ClientById(int clientId);
        Task<BaseResponse<bool>> RegisterClient(ClientRequestDto requestDto);
        Task<BaseResponse<bool>> EditClient(int clientId, ClientRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveClient(int clientId);
    }
}
