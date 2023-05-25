using PEI_ETL.Services.DTO;

namespace PEI_ETL.Services.Interfaces
{
    public interface IETLBatchSrcStepService
    {
        Task<IEnumerable<ETLBatchSrcStepDTO>> GetETLBatchSrcStepAsync();
        Task<bool> InsertAsync(ETLBatchSrcStepDTO eTLBatchStepDTO);

        Task<bool> UpdateETLBatchSrcStep(ETLBatchSrcStepDTO eTLBatchStepDTO);

        Task<bool> DeleteETLBatchSrcStep(int Id);

        Task<IEnumerable<ETLBatchSrcStepDTO>> GetETLBatchSrcStepFilterAsync(string batchName, string sourceId);
    }
}
