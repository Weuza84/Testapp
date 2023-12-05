using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Testapp.Models;

namespace Testapp.Services
{
    public class ApiManager
    {
        IRestService restService;

        public ApiManager(IRestService service)
        {
            restService = service;
        }

        public Task<List<Module>> GetModulesAsync()
        {
            return restService.GetDataModuleAsync();
        }

        public Task<List<Question>> GetQuestionsAsync()
        {
            return restService.GetDataQuestionAsync();
        }
    }
}
