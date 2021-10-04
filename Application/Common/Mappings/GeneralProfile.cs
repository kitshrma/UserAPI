using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetUserById;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateUserCommand, User>();
            CreateMap<User, UserDTO>().ReverseMap();

        }
    }
}
