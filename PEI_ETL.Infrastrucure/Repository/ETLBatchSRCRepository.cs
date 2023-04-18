using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEI_ETL.Infrastrucure.Repository
{
    internal class ETLBatchSRCRepository : GenericRepository<ETLBatchSrc>, IETLBatchSrcRepository
    {
        public ETLBatchSRCRepository(ETLDbContext context, ILogger logger) : base(context, logger)
        {

        }
    
    }
}
