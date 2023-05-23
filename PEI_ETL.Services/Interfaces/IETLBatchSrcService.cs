using PEI_ETL.Core.Entities;
using PEI_ETL.Services.DTO;

namespace PEI_ETL.Services.Interfaces
{
    public interface IETLBatchStepCfgService
    {
        Task<IEnumerable<ETLBatchStepCfgDTO>> GetETLBatchStepCfgAsync();
        Task<bool> InsertAsync(ETLBatchStepCfgDTO eTLBatchStepCfgDTO);

        Task<bool> UpdateETLBatchStepCfg(ETLBatchStepCfgDTO eTLBatchStepCfgDTO);

        Task<bool> DeleteETLBatchStepCfg(int Id);

        Task<IEnumerable<ETLBatchStepCfgDTO>> GetETLBatchStepCfgFilterAsync(string batchName, string batchStep);


    }
}
