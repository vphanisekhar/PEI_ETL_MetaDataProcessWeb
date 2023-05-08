using PEI_ETL.Core.Entities;
using PEI_ETL.Services.DTO;

namespace PEI_ETL.Services.Interfaces
{
    public interface IETLBatchSrcService
    {
        Task<IEnumerable<ProductDetailsDTO>> GetETLBatchSrcAsync();
        Task<bool> InsertAsync(ProductDetailsDTO productDetailsDTO);

        Task<bool> UpdateETLBatchSrc(ETLBatchSrc eTLBatchSrc);

        Task<bool> DeleteETLBatchSrc(ETLBatchSrc eTLBatchSrc);
    }
}
