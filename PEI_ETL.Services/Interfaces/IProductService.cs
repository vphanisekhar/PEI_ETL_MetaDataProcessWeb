using PEI_ETL.Services.DTO;

namespace PEI_ETL.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetailsDTO>> GetProductAsync();
        Task<bool> InsertAsync(ProductDetailsDTO productDetailsDTO);
        
    }
}
