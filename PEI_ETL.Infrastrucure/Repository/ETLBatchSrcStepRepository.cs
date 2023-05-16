using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;

namespace PEI_ETL.Infrastrucure.Repository
{
    public class ETLBatchSrcStepRepository : GenericRepository<ETLBatchSrcStep>, IETLBatchSrcStepRepository
    {
        public ETLBatchSrcStepRepository(ETLDbContext context, ILogger logger) : base(context, logger)
        {

        }
    
    }
}
