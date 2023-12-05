using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;

using Xamarin.Forms;
using Testapp.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using ModernHttpClient;

namespace Testapp.Services
{
    public class RestService : IRestService
    {
        HttpClient client;

        public List<Module> Modules { get; private set; }
        public List<Question> Questions { get; private set; }

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<List<Module>> GetDataModuleAsync()
        {
            Modules = new List<Module>();

            try
            {
                var url = Constants.url + "/api/modules/all/";
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                Modules = JsonConvert.DeserializeObject<List<Module>>(json);
            } catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return Modules;
        }

        public async Task<List<Question>> GetDataQuestionAsync()
        {
            Questions = new List<Question>();

            try
            {
                var url = Constants.url + "/api/questions/all/";
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                Questions = JsonConvert.DeserializeObject<List<Question>>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return Questions;
        }
    }
}