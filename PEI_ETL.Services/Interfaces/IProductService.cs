using PEI_ETL.Core.Entities;
using PEI_ETL.Services.DTO;

namespace PEI_ETL.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetailsDTO>> GetProductAsync();
        Task<bool> InsertAsync(ProductDetailsDTO productDetailsDTO);

        Task<bool> UpdateProduct(ProductDetails productDetails);

        Task<bool> DeleteProduct(int productId);

    }
}
