using Login.Controllers;
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
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly IConfiguration _config;
        public UserGroupRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> AddUserGroup(UserGroupForAddDto userGroupForInsertDto)
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(userGroupForInsertDto), Encoding.UTF8, "application/json");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.PostAsync(UserAPIURL + "api/userGroups/AddUserGroup", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;
        }

        public async Task<string> DeleteUserGroup(UserGroupForIdDto userGroupForIdDto)
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.DeleteAsync(UserAPIURL + "api/userGroups/deleteusergroup/" + userGroupForIdDto.id))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;
        }

        public async Task<string> GetUserGroup()
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.GetAsync(UserAPIURL + "api/userGroups/getusergroup"))
                {

                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;
        }

        public async Task<string> GetUserGroupById(UserGroupForIdDto userGroupForIdDto)
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(userGroupForIdDto), Encoding.UTF8, "application/json");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.GetAsync(UserAPIURL + "api/userGroups/getusergroupbyid/" + userGroupForIdDto.id))
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

        public async Task<string> UpdateUserGroup(UserGroupForUpdateDto userGroupForUpdateDto)
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(userGroupForUpdateDto), Encoding.UTF8, "application/json");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.PutAsync(UserAPIURL + "api/userGroups/updateusergroup", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;
        }
    }
}
