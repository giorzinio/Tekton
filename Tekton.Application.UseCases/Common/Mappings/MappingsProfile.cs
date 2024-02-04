using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekon.Application.DTO;
using Tekton.Domain.Entities;

namespace Tekton.Application.UseCases.Common.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
