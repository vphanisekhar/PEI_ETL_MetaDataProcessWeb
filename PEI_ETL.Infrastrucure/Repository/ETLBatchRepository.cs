using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;

namespace PEI_ETL.Infrastrucure.Repository
{
    public class ETLBatchRepository : GenericRepository<ETLBatch>, IETLBatchRepository
    {
        public ETLBatchRepository(ETLDbContext context, ILogger logger) : base(context, logger)
        {

        }
    
    }
}
