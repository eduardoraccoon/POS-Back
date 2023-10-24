using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Dtos.Provider.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.Provider;
using POS.Domain.Entities;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    internal class ProviderApplication : IProviderApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ProviderValidator _validationRules;

        public ProviderApplication(IUnitOfWork unitOfWork, IMapper mapper, ProviderValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public async Task<BaseResponse<IEnumerable<ProviderResponseDto>>> ListProviders()
        {
            var response = new BaseResponse<IEnumerable<ProviderResponseDto>>();
            var providers = await _unitOfWork.Provider.GetAllAsync();

            if (providers is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<ProviderResponseDto>>(providers);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<ProviderResponseDto>> ProviderById(int providerId)
        {
            var response = new BaseResponse<ProviderResponseDto>();
            var provider = await _unitOfWork.Provider.GetByIdAsync(providerId);

            if (provider is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<ProviderResponseDto>(provider);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterProvider(ProviderRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var validationResult = await _validationRules.ValidateAsync(requestDto);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
                return response;
            }

            var provider = _mapper.Map<Provider>(requestDto);
            response.Data = await _unitOfWork.Provider.RegisterAsync(provider);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> EditProvider(int providerId, ProviderRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var providerEdit = await ProviderById(providerId);

            if (providerEdit is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            var provider = _mapper.Map<Provider>(requestDto);
            provider.Id = providerId;
            response.Data = await _unitOfWork.Provider.EditAsync(provider);

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

        public async Task<BaseResponse<bool>> RemoveProvider(int providerId)
        {
            var response = new BaseResponse<bool>();
            var provider = await ProviderById(providerId);

            if (provider.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            response.Data = await _unitOfWork.Provider.RemoveAsync(providerId);

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
