using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;

namespace PEI_ETL.Infrastrucure.Repository
{
    public class ETLBatchStepRepository : GenericRepository<ETLBatchStep>, IETLBatchStepRepository
    {
        public ETLBatchStepRepository(ETLDbContext context, ILogger logger) : base(context, logger)
        {

        }
    
    }
}
