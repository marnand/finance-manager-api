﻿using FinanceManager.Model;
using FinanceManager.Model.Control;
using FinanceManager.Model.DTO;
using FinanceManager.Repository.Interfaces;
using FinanceManager.Service.Extensions;
using FinanceManager.Service.Interfaces;
using System.Net;

namespace FinanceManager.Service;

internal class UserService(JWTToken jwtToken, IUserRepository userRepository) : IUserService
{
    private readonly JWTToken _jwtToken = jwtToken;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<int>> Create(CreateUserDTO user)
    {
        var result = User.Create(user.Name, user.Email, user.Pass);
        if (!result.IsSuccess)
            return Result<int>.ResultError(result.Error!.Message, result.Error.StatusCode);
        
        var passHash = PasswordHasher.HashPassword(user.Pass);
        result.Data!.SetPasswordHash(passHash);
        return Result<int>.Success(await _userRepository.Create(result.Data));
    }

    public async Task<Result<string>> Login(LoginDTO login)
    {
        var dbUser = await _userRepository.GetByEmail(login.Email); 
        if (dbUser is null) return Result<string>.ResultError("Usuário não encontrado!", HttpStatusCode.NotFound);        

        var (isValid, message) = PasswordHasher.VerifyPassword(login.Password, dbUser.Password);
        if (!isValid) return Result<string>.ResultError(message, HttpStatusCode.Unauthorized);

        var token = _jwtToken.GenerateToken(dbUser.Id.ToString(), dbUser.Email);
        return Result<string>.Success(token);
    }

    public async Task<Result<IEnumerable<User>>> Get(int id)
    {
        var users = await _userRepository.Get(id);
        return Result<IEnumerable<User>>.Success(users);
    }
}
