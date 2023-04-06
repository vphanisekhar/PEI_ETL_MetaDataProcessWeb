using AutoMapper;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;
using PEI_ETL.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDemo.Core.Models;

namespace PEI_ETL.Services.Service
{

    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDetailsDTO>> GetProductAsync()
        {
            var products = await _unitOfWork.Products.GetAll();

            return _mapper.Map<IEnumerable<ProductDetailsDTO>>(products);
        }

        public async Task<bool> InsertAsync(ProductDetailsDTO productDetailsDTO)
        {
            var project = _mapper.Map<ProductDetails>(productDetailsDTO);
            return await _unitOfWork.Products.Add(project);
        }

        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }
    }
}
