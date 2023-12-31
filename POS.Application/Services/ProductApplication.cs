﻿using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Product.Request;
using POS.Application.Dtos.Product.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.Product;
using POS.Domain.Entities;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    public class ProductApplication : IProductApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ProductValidator _validationRules;

        public ProductApplication(IUnitOfWork unitOfWork, IMapper mapper, ProductValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public async Task<BaseResponse<IEnumerable<ProductResponseDto>>> ListProducts()
        {
            var response = new BaseResponse<IEnumerable<ProductResponseDto>>();
            var products = await _unitOfWork.Product.GetAllAsync();

            if (products is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<ProductResponseDto>>> GetAllProducts()
        {
            var response = new BaseResponse<IEnumerable<ProductResponseDto>>();
            var products = await _unitOfWork.Product.GetAllProducts();

            if (products is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<ProductResponseDto>> ProductById(int productId)
        {
            var response = new BaseResponse<ProductResponseDto>();
            var product = await _unitOfWork.Product.GetByIdAsync(productId);

            if (product is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<ProductResponseDto>(product);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterProduct(ProductRequestDto requestDto)
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

            var product = _mapper.Map<Product>(requestDto);
            response.Data = await _unitOfWork.Product.RegisterAsync(product);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> EditProduct(int productId, ProductRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            var productEdit = await ProductById(productId);

            if (productEdit is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            var product = _mapper.Map<Product>(requestDto);
            product.Id = productId;
            response.Data = await _unitOfWork.Product.EditAsync(product);

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

        public async Task<BaseResponse<bool>> RemoveProduct(int productId)
        {
            var response = new BaseResponse<bool>();
            var product = await ProductById(productId);

            if (product.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            response.Data = await _unitOfWork.Product.RemoveAsync(productId);

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
