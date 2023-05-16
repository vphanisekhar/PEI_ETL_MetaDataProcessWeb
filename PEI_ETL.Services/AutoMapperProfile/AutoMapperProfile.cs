using AutoMapper;
using PEI_ETL.Core.Entities;
using PEI_ETL.Services.DTO;

namespace PEI_ETL.Services.AutoMapperProfile
{

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProjectDTO, Project>().ReverseMap();


            CreateMap<ProductDetailsDTO, ProductDetails>().ReverseMap();

            CreateMap<ETLBatchSrcDTO, ETLBatchSrc>().ReverseMap();

            CreateMap<ETLBatchDTO, ETLBatch>().ReverseMap();


            CreateMap<ETLBatchSrcStepDTO, ETLBatchSrcStep>().ReverseMap();
        }
    }
}
