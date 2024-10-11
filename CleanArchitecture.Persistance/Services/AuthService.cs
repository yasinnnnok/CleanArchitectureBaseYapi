﻿using AutoMapper;
using CleanArchitecture.Application.Absractions;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistance.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(UserManager<User> userManager, IMapper mapper, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
        }

        public async Task<LoginCommandResponse> CreateTokenByRefreshTokenAsync(CreateNewTokenRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) throw new Exception("Kullanıcı bulunamadı");

            if (user.RefreshToken!= request.RefreshToken)
            {
                throw new Exception("Refresh token geçerli deği!");
            }

            if (user.RefreshTokenExpires < DateTime.Now)
                throw new Exception("Refresh Token süresi dolmuş.");

            LoginCommandResponse response = await _jwtProvider.CreateTokenAsync(user);
            return response;
        }

        public async Task<LoginCommandResponse> LoginAsync(LoginCommand request, CancellationToken cancellationToken)
        {
            User user = await _userManager.Users.Where(
                p => p.UserName == request.userNameOrEmail || p.Email == request.userNameOrEmail).FirstOrDefaultAsync(cancellationToken);

            if (user == null) throw new Exception("Kullanıcı bulunamadı");

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (result)
            {
                LoginCommandResponse response = await _jwtProvider.CreateTokenAsync(user);
                return response;
            }
            throw new Exception("Şifreyi yanlış girdiniz1");
        }

        public async Task RegisterAsync(RegisterCommand request)
        {
            User user = _mapper.Map<User>(request);
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

        }
    }
}
