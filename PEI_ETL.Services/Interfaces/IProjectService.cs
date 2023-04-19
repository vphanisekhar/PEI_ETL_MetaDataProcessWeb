using PEI_ETL.Services.DTO;

namespace PEI_ETL.Services.Interfaces
{
    public interface IProjectService
    {

        Task<IEnumerable<ProjectDTO>> GetProjectAsync();
        Task<bool> InsertAsync(ProjectDTO projectDTO);
    }
}
