using PEI_ETL.Core.Entities;
using PEI_ETL.Services.DTO;

namespace PEI_ETL.Services.Interfaces
{
    public interface IETLBatchService
    {
        Task<IEnumerable<ETLBatchDTO>> GetETLBatchAsync();
        Task<bool> InsertAsync(ETLBatchDTO eTLBatchDTO);

        Task<bool> UpdateETLBatch(ETLBatchDTO eTLBatch);

        Task<bool> DeleteETLBatch(int Id);
    }
}
