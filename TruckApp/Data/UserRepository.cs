using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TruckApp.Dtos;
using TruckApp.Models;

namespace TruckApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;
        public UserRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> DeactiveUser(UserForDeactiveDto userForDeactiveDto)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(userForDeactiveDto), Encoding.UTF8, "application/json");
                string UserAPIUrl = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.PostAsync(UserAPIUrl+"api/users/deactive", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    //try
                    //{
                    //    receivedUser = JsonConvert.DeserializeObject<User>(apiResponse);
                    //}
                    //catch (Exception ex)
                    //{
                    //    return receivedUser;
                    //}
                }
            }
            return apiResponse;
        }

        public Task<bool> UserExists(string Username)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Login(UserForLoginDto user)
        {
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                string UserAPIUrl = _config.GetConnectionString("UserAPIUrl"); 
                using (var response = await httpClient.PostAsync(UserAPIUrl+"api/users/login", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    try
                    {
                        receivedUser = JsonConvert.DeserializeObject<User>(apiResponse);
                    }
                    catch (Exception ex)
                    {
                        return receivedUser;
                    }
                }
            }
            return receivedUser;

        }

        public async Task<User> Register(UserForRegisterDto userForRegisterDto)
        {
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(userForRegisterDto), Encoding.UTF8, "application/json");
                string UserAPIUrl = _config.GetConnectionString("UserAPIUrl"); 
                using (var response = await httpClient.PostAsync(UserAPIUrl+"api/users/register", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    try
                    {
                        receivedUser = JsonConvert.DeserializeObject<User>(apiResponse);
                    }
                    catch (Exception ex)
                    {
                        return receivedUser;
                    }
                }
            }
            return receivedUser;
        }
        public async Task<string> AssignUserGroup(UserForUserGroupForDto userForUserGroupForDto)
        {
            User receivedUser = new User();
            string apiResponse = "[]";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(userForUserGroupForDto), Encoding.UTF8, "application/json");
                string UserAPIUrl = _config.GetConnectionString("UserAPIUrl"); 
                using (var response = await httpClient.PutAsync(UserAPIUrl+"api/users/assignusergroup", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;

        }
        public async Task<string> AssignBranch(UserForBranchForDto userForBranchForDto)
        {
            User receivedUser = new User();
            string apiResponse = "[]";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(userForBranchForDto), Encoding.UTF8, "application/json");
                string UserAPIUrl = _config.GetConnectionString("UserAPIUrl"); 
                using (var response = await httpClient.PutAsync(UserAPIUrl+"api/users/assignbranch", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;

        }

        public async Task<string> GetUsers()
        {
            //User receivedUser = new User();
            string user = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");

                string UserAPIUrl = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.GetAsync(UserAPIUrl + "api/users/getusers"))
                {
                    user = await response.Content.ReadAsStringAsync();
                    //try
                    //{
                    //    receivedUser = JsonConvert.DeserializeObject<User>(apiResponse);
                    //}
                    //catch (Exception ex)
                    //{
                    //    return receivedUser;
                    //}
                }
            }
            return user;
        }
        public async Task<string> ValidateOTP(string Username, string OTP)
        {
            //User receivedUser = new User();
            string user = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");

                string UserAPIUrl = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.GetAsync(UserAPIUrl + "api/users/ValidateOTP?Username="+Username+"&OTP="+OTP))
                {
                    user = await response.Content.ReadAsStringAsync();
                }
            }
            return user;
        }
        public async Task<string> ResetPassword(string Username, string OldPassword, string NewPassword)
        {
            User receivedUser = new User();
            string apiResponse = "[]";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                string UserAPIUrl = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.GetAsync(UserAPIUrl + "api/users/resetpassword?Username=" + Username + "&OldPassword=" + OldPassword + "&NewPassword=" + NewPassword))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;

        }
        public async Task<string> ForgotPassword(string Username)
        {
            User receivedUser = new User();
            string apiResponse = "[]";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                string UserAPIUrl = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.GetAsync(UserAPIUrl + "api/users/forgotpassword?Username=" + Username))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;

        }
        public async Task<string> ResetPasswordWithoutOldPassword(string Username, string NewPassword)
        {
            User receivedUser = new User();
            string apiResponse = "[]";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                string UserAPIUrl = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.GetAsync(UserAPIUrl + "api/users/resetpasswordwithoutoldpassword?Username=" + Username+"&NewPassword="+NewPassword))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;

        }

    }
}
