using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDemo.Core.Models;

namespace PEI_ETL.Infrastrucure.Repository
{

    public class ProductRepository : GenericRepository<ProductDetails>, IProductRepository
    {
        public ProductRepository(ETLDbContext context, ILogger logger) : base(context, logger)
        {

        }
    }
}
