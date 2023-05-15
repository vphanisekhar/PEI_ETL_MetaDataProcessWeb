using PEI_ETL.Core.Entities;
using PEI_ETL.Services.DTO;

namespace PEI_ETL.Services.Interfaces
{
    public interface IETLBatchStepService
    {
        Task<IEnumerable<ETLBatchStepDTO>> GetETLBatchStepAsync();
        Task<bool> InsertAsync(ETLBatchStepDTO eTLBatchStepDTO);

        Task<bool> UpdateETLBatchStep(ETLBatchStepDTO eTLBatchStepDTO);

        Task<bool> DeleteETLBatchStep(int Id);
    }
}
