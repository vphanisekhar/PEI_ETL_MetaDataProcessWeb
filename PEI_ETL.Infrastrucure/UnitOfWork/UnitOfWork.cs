using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Interfaces;
using PEI_ETL.Infrastrucure.Repository;

namespace PEI_ETL.Infrastrucure.UnitOfWork
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ETLDbContext _context;
        private readonly ILogger _logger;
        public IProjectRepository Projects { get; private set; }

        public IProductRepository Products { get; private set; }

        public IETLBatchSrcRepository ETLBatchSrc { get; private set; }

        public IETLBatchRepository ETLBatch { get; private set; }

        public IETLBatchSrcStepRepository ETLBatchSrcStep { get; private set; }

        public IETLBatchStepCfgRepository ETLBatchStepCfg { get; private set; }

        public IETLJobsRepository ETLJobs { get; private set; }

        public IETLBatchSrcStepCfgRepository ETLBatchSrcStepCfg { get; }

        public UnitOfWork(
            ETLDbContext context,
            ILoggerFactory logger
            )
        {
            _context = context;
            _logger = logger.CreateLogger("logs");

            Projects = new ProjectRepository(_context, _logger);

            Products = new ProductRepository(_context, _logger);

            ETLBatchSrc = new ETLBatchSRCRepository(_context, _logger);

            ETLBatch = new ETLBatchRepository(_context, _logger);

            ETLBatchSrcStep = new ETLBatchSrcStepRepository(_context, _logger);

            ETLBatchStepCfg = new ETLBatchStepCfgRepository(_context, _logger);

            ETLJobs = new ETLJobsRepository(_context, _logger);

            ETLBatchSrcStepCfg =  new  ETLBatchSrcStepCfgRepository(_context, _logger);
        }

        public async Task<int> CompletedAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }


}
