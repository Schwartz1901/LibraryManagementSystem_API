﻿using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.Business.Dtos.AuthDtos;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Concrete
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration configuration;

        public AuthRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJwtToken(UserEntity user) { }
    }
}
