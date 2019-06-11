using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FontaineVerificationProject.Dtos;
using FontaineVerificationProject.Models;
using AutoMapper;

namespace FontaineVerificationProject.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<Verification, VerificationDto>();
            //CreateMap<Sale, SaleDto>();
            CreateMap<LoginDto, User>();
            CreateMap<RegisterDto, User>();        
        }
    }
}
