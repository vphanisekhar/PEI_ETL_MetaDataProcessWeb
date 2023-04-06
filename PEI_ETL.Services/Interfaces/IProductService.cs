using PEI_ETL.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEI_ETL.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetailsDTO>> GetProductAsync();
        Task<bool> InsertAsync(ProductDetailsDTO productDetailsDTO);
        
    }
}
