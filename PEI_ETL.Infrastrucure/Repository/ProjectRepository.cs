using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;

namespace PEI_ETL.Infrastrucure.Repository
{

    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ETLDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
