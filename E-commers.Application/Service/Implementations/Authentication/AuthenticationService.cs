using AutoMapper;
using E_commers.Application.DTOS;
using E_commers.Application.DTOS.Identity;
using E_commers.Application.Service.Authantication;
using E_commers.Application.Service.Interfaces.Logging;
using E_commers.Application.Validations.Authentication;
using E_commers.Domain.Identity;
using E_commers.Domain.Interface.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commers.Application.Service.Implementations.Authentication
{
    public class AuthenticationService(ITokenManagment _tokenManagment,
        IValidationService validationService
        ,IValidator<LoginUser> _loginUserValidator,
        IValidator<CreateUser> _CreateUserValidator,
        IMapper _mapper,
        IUserManagment _userManagment,
        IRoleManagment _roleManagment,
        IAppLogger<AuthenticationService>_logger) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {
            var _validationResult = await validationService.ValidationAsync(user, _CreateUserValidator);
            if (!_validationResult.Success) return _validationResult;

            var mappedModel= _mapper.Map<AppUser>(user);
            mappedModel.UserName = user.Email;
            mappedModel.PasswordHash = user.Password;

            var result = await _userManagment.CreateUser(mappedModel);
            if (!result)
                return new ServiceResponse { Message = "email address might be already in use or unknown error" };

            var _user = await _userManagment.GetUserByEmail(user.Email);
            var users = await _userManagment.GetALLUsers();
            bool assingedResult = (bool)await _roleManagment.AddUserToRole(_user!, users!.Count() > 1 ? "User" : "Admin");
            if (!assingedResult)
            {
                int removeResult = await _userManagment.RemoveUserByEmail(_user.Email);
                if (removeResult <= 0)
                {
                    _logger.LogError(new Exception($"User with email as{_user.Email} failed to be remove as a result of role assinging issue"),"user could not be assigned role");
                    return new ServiceResponse { Message = "error occurred in create account" };
                }
            }
            return new ServiceResponse {Success = true , Message = "account Created" }; 

            //verify email

        }

        public  async Task<LoginResponse> LoginUser(LoginUser user)
        {
            var _validationResult = await validationService.ValidationAsync(user, _loginUserValidator);
            if (!_validationResult.Success)
                return new LoginResponse(Message: _validationResult.Message);
             var mapperModel = _mapper.Map<AppUser>(user);
            mapperModel.PasswordHash = user.Password;


            bool loginResult = await _userManagment.LoginUser(mapperModel);
            if (!loginResult)
                return new LoginResponse(Message: "email not found or invalid ");

            var _user = await _userManagment.GetUserByEmail(user.Email);
            var clamis = await _userManagment.GetUserClaim(_user!.Email);

            string jwtToken = _tokenManagment.GenerateToken(clamis);
            string RefreshToken = _tokenManagment.GetRefreshToken();

            int saveTokenResult = await _tokenManagment.AddRefreshToken(_user.Id, RefreshToken);
            return saveTokenResult <= 0 ? new LoginResponse(Message: "Internal error occourred while authenticating") :
                new LoginResponse(Success: true, Token: jwtToken, RefreshToken: RefreshToken);

        }

        public async Task<LoginResponse> ReviveToken(string refreshToken)
        {
            bool validationTokenResult = await _tokenManagment.ValidateRefreshToken(refreshToken);
            if (!validationTokenResult)
                return new LoginResponse(Message: "invalid token");

            string userId = await _tokenManagment.GetUserIdByRefreshToken(refreshToken);
            AppUser? user = await _userManagment.GetUserById(userId);
            var clamis = await _userManagment.GetUserClaim(user!.Email);
            string newJwtToken = _tokenManagment.GenerateToken(clamis);
            string newRefreshToken = _tokenManagment.GetRefreshToken();
            await _tokenManagment.UpdateRefreshToken(userId, newRefreshToken);
            return new LoginResponse(Success: true, Token: newJwtToken, RefreshToken: newRefreshToken);

              
        }
    }
}
