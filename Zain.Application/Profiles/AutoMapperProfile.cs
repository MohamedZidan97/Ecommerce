using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zain.Domain.Entities;
using static Zain.Application.Features.Users.ProductDto;

namespace _VC.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Product
            CreateMap<Product, ProductFormDto>().ReverseMap();
            CreateMap<Product, ProductQueryDto>().ReverseMap();
            CreateMap<Product, ProductCommendDto>().ReverseMap();



            #region Account 
            // Register
            //  CreateMap<AccountGeneralResponse, AccountRegisterRequest>().ReverseMap();
            #endregion
        }
    }
}
