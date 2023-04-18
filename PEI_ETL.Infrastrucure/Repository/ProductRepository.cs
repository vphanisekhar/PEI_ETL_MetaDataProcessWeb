using Microsoft.Extensions.Logging;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;


namespace PEI_ETL.Infrastrucure.Repository
{

    public class ProductRepository : GenericRepository<ProductDetails>, IProductRepository
    {
        public ProductRepository(ETLDbContext context, ILogger logger) : base(context, logger)
        {

        }
    }
}
