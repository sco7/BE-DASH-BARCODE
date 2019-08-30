using FontaineVerificationProject.Dtos;
using FontaineVerificationProject.Models;
using AutoMapper;

namespace FontaineVerificationProject.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LoginDto, User>();
            CreateMap<RegisterDto, User>();        
        }
    }
}
