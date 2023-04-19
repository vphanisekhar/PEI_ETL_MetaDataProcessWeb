using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;

namespace PEI_ETL.Infrastrucure.Repository
{
    public class ETLBatchSRCRepository : GenericRepository<ETLBatchSrc>, IETLBatchSrcRepository
    {
        public ETLBatchSRCRepository(ETLDbContext context, ILogger logger) : base(context, logger)
        {

        }
    
    }
}
