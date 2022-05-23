using Login.Dtos;
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
    public class BranchRepository : IBranchRepository
    {
        private readonly IConfiguration _config;
        public BranchRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> AddBranch(BranchForAddDto branchForInsertDto)
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(branchForInsertDto), Encoding.UTF8, "application/json");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.PostAsync(UserAPIURL + "api/branches/AddBranch", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;
        }

        public async Task<string> DeleteBranch(BranchForIdDto branchForIdDto)
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.DeleteAsync(UserAPIURL + "api/branches/deletebranch/"+branchForIdDto.Id))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;
        }

        public async Task<string> GetBranch()
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.GetAsync(UserAPIURL + "api/branches/getbranch"))
                {

                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;
        }

        public async Task<string> GetBranchById(BranchForIdDto branchForIdDto)
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(branchForIdDto), Encoding.UTF8, "application/json");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.GetAsync(UserAPIURL + "api/branches/getbranchbyid/" + branchForIdDto.Id))
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

        public async Task<string> UpdateBranch(BranchForUpdateDto branchForUpdateDto)
        {
            string apiResponse = "";
            User receivedUser = new User();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Key", "Secret@123");
                StringContent content = new StringContent(JsonConvert.SerializeObject(branchForUpdateDto), Encoding.UTF8, "application/json");
                string UserAPIURL = _config.GetConnectionString("UserAPIUrl");
                using (var response = await httpClient.PutAsync(UserAPIURL + "api/branches/updatebranch", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;
        }
    }
}
