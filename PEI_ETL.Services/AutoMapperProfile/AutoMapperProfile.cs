using AutoMapper;
using PEI_ETL.Core.Entities;
using PEI_ETL.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDemo.Core.Models;

namespace PEI_ETL.Services.AutoMapperProfile
{

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProjectDTO, Project>().ReverseMap();


            CreateMap<ProductDetailsDTO, ProductDetails>().ReverseMap();
        }
    }
}
