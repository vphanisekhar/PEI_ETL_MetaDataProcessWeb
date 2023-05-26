using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;

namespace PEI_ETL.Infrastrucure.Repository
{
    public class ETLBatchSrcStepCfgRepository : GenericRepository<ETLBatchSrcStepCfg>, IETLBatchSrcStepCfgRepository
    {
        public ETLBatchSrcStepCfgRepository(ETLDbContext context, ILogger logger) : base(context, logger)
        {

        }
    
    }
}
