using PEI_ETL.Services.DTO;

namespace PEI_ETL.Services.Interfaces
{
    public interface IETLBatchSrcStepCfgService
    {
        Task<IEnumerable<ETLBatchSrcStepCfgDTO>> GetETLBatchSrcStepCfgAsync();
        Task<bool> InsertAsync(ETLBatchSrcStepCfgDTO eTLBatchSrcStepCfgDTO);

        Task<bool> UpdateETLBatchSrcStepCfg(ETLBatchSrcStepCfgDTO eTLBatchSrcStepCfgDTO);

        Task<bool> DeleteETLBatchSrcStepCfg(int Id);
    }
}
