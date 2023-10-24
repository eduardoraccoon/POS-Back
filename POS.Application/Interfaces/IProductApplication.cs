using POS.Application.Commons.Bases;
using POS.Application.Dtos.Product.Request;
using POS.Application.Dtos.Product.Response;

namespace POS.Application.Interfaces
{
    public interface IProductApplication
    {
        Task<BaseResponse<IEnumerable<ProductResponseDto>>> ListProducts();
        Task<BaseResponse<IEnumerable<ProductResponseDto>>> GetAllProducts();
        Task<BaseResponse<ProductResponseDto>> ProductById(int productId);
        Task<BaseResponse<bool>> RegisterProduct(ProductRequestDto requestDto);
        Task<BaseResponse<bool>> EditProduct(int productId, ProductRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveProduct(int productId);
    }
}
