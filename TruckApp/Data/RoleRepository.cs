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
    public class RoleRepository : IRoleRepository
    {
        private readonly IConfiguration _config;
        public RoleRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> AddRole(RoleForInsertDto roleForInsertDto)
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(roleForInsertDto), Encoding.UTF8, "application/json");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.PostAsync(UserAPIURL + "api/roles/AddRole", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;
        }

        public async Task<string> DeleteRole(RoleForIdDto roleForIdDto)
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.DeleteAsync(UserAPIURL + "api/roles/deleterole/"+roleForIdDto.id))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;
        }

        public async Task<string> GetRole()
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.GetAsync(UserAPIURL + "api/roles/getrole"))
                {

                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;
        }

        public async Task<string> GetRoleById(RoleForIdDto roleForIdDto)
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(roleForIdDto), Encoding.UTF8, "application/json");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.GetAsync(UserAPIURL + "api/roles/getrolebyid/" + roleForIdDto.id))
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

        public async Task<string> UpdateRole(RoleForUpdateDto roleForUpdateDto)
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(roleForUpdateDto), Encoding.UTF8, "application/json");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.PutAsync(UserAPIURL + "api/roles/updaterole", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;
        }
    }
}
