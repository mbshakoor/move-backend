using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckApp.Dtos;
using TruckApp.Models;

namespace TruckApp.Data
{
    public interface IUserRepository
    {
        Task<User> Register(UserForRegisterDto userForRegisterDto);
        Task<string> AssignUserGroup(UserForUserGroupForDto userForUserGroupForDto);
        Task<string> AssignBranch(UserForBranchForDto userForBranchForDto);
        Task<User> Login(UserForLoginDto user);
        Task<bool> UserExists(string username);
        Task<string> DeactiveUser(UserForDeactiveDto userForDeactiveDto);
        Task<string> GetUsers();
        Task<string> ResetPassword(string Username, string OldPassword, string NewPassword);
        Task<string> ValidateOTP(string Username, string OTP);
        Task<string> ForgotPassword(string Username);
        Task<string> ResetPasswordWithoutOldPassword(string Username, string NewPassword);
    }
}
