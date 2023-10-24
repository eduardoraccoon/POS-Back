using AutoMapper;
using FluentValidation;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Category.Response;
using POS.Application.Dtos.Client.Request;
using POS.Application.Dtos.Client.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.Client;
using POS.Domain.Entities;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    public class ClientApplication : IClientApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ClientValidator _clientValidator;

        public ClientApplication(IUnitOfWork unitOfWork, IMapper mapper, ClientValidator clientValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _clientValidator = clientValidator;
        }

        public async Task<BaseResponse<IEnumerable<ClientResponseDto>>> ListClients()
        {
            var response = new BaseResponse<IEnumerable<ClientResponseDto>>();
            var clients = await _unitOfWork.Client.GetAllAsync();

            if (clients is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<ClientResponseDto>>(clients);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<ClientResponseDto>> ClientById(int clientId)
        {
            var response = new BaseResponse<ClientResponseDto>();
            var client = await _unitOfWork.Client.GetByIdAsync(clientId);

            if (client is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<ClientResponseDto>(client);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterClient(ClientRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _clientValidator.ValidateAsync(requestDto);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
                return response;
            }

            var client = _mapper.Map<Client>(requestDto);
            response.Data = await _unitOfWork.Client.RegisterAsync(client);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> EditClient(int clientId, ClientRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var clientEdit = await ClientById(clientId);

            if (clientEdit is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            var client = _mapper.Map<Client>(requestDto);
            client.Id = clientId;
            response.Data = await _unitOfWork.Client.EditAsync(client);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemoveClient(int clientId)
        {
            var response = new BaseResponse<bool>();
            var client = await ClientById(clientId);

            if (client.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            response.Data = await _unitOfWork.Client.RemoveAsync(clientId);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }
    }
}
