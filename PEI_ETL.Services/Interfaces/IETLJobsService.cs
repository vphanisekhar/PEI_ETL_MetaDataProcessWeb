using PEI_ETL.Services.DTO;

namespace PEI_ETL.Services.Interfaces
{
    public interface IETLJobsService
    {
        Task<IEnumerable<ETLJobsDTO>> GetETLJobsAsync();
        Task<bool> InsertAsync(ETLJobsDTO eTLJobsDTO);

        Task<bool> UpdateETLJobs(ETLJobsDTO eTLJobsDTO);

        Task<bool> DeleteETLJobs(int Id);
    }
}
