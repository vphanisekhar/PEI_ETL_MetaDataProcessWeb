using AutoMapper;
using PEI_ETL.Core.Entities;
using PEI_ETL.Core.Interfaces;
using PEI_ETL.Services.DTO;

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

        public async Task<bool> DeleteProduct(int productId)
        {
            if (productId > 0)
            {
                var product = await _unitOfWork.Products.GetById(productId);
                //if (productDetails != null)
                //{
                //    //_unitOfWork.Products.Remove(productDetails);
                //    return true;
                //    //var result = _unitOfWork.Save();

                //    //if (result > 0)
                //    //    return true;
                //    //else
                //    //    return false;
                //}

                if (product != null)
                {
                    product.IsDeleted = true;


                    _unitOfWork.Products.Upsert(product);
                    return true;

                    //var result = _unitOfWork.Save();

                    //if (result > 0)
                    //    return true;
                    //else
                    //    return false;
                }
            }
            return false;
        }

        public async Task<ProductDetails> GetProductById(int productId)
        {
            if (productId > 0)
            {
                var productDetails = await _unitOfWork.Products.GetById(productId);
                if (productDetails != null)
                {
                    return productDetails;
                }
            }
            return null;
        }

        public async Task<bool> UpdateProduct(ProductDetails productDetails)
        {
            if (productDetails != null)
            {
                var product = await _unitOfWork.Products.GetById(productDetails.Id);
                if (product != null)
                {
                    product.ProductName = productDetails.ProductName;
                    product.ProductDescription = productDetails.ProductDescription;
                    product.ProductPrice = productDetails.ProductPrice;
                    product.ProductStock = productDetails.ProductStock;

                     _unitOfWork.Products.Upsert(product);
                    return true;

                    //var result = _unitOfWork.Save();

                    //if (result > 0)
                    //    return true;
                    //else
                    //    return false;
                }
            }
            return false;
        }

        public async Task<int> CompletedAsync()
        {
            return await _unitOfWork.CompletedAsync();
        }
    }
}
