using PEI_ETL.Services.DTO;

namespace PEI_ETL.Services.Interfaces
{
    public interface IETLBatchSrcService
    {
        Task<IEnumerable<ETLBatchSrcDTO>> GetETLBatchSrcAsync();
        Task<bool> InsertAsync(ETLBatchSrcDTO eTLBatchSrcDTO);

        Task<bool> UpdateETLBatchSrc(ETLBatchSrcDTO eTLBatchSrcDTO);

        Task<bool> DeleteETLBatchSrc(int Id);
    }
}
