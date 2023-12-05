using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Testapp.Models;

namespace Testapp.Services
{
    public interface IRestService
    {
        Task<List<Module>> GetDataModuleAsync();
        Task<List<Question>> GetDataQuestionAsync();
    }
}
