using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Interfaces;
using PEI_ETL.Infrastrucure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEI_ETL.Infrastrucure.UnitOfWork
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ETLDbContext _context;
        private readonly ILogger _logger;
        public IProjectRepository Projects { get; private set; }

        public IProductRepository Products { get; private set; }

        public UnitOfWork(
            ETLDbContext context,
            ILoggerFactory logger
            )
        {
            _context = context;
            _logger = logger.CreateLogger("logs");

            Projects = new ProjectRepository(_context, _logger);

            Products =new ProductRepository(_context, _logger);
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
