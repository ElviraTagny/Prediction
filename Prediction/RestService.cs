using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Prediction
{
    public class RestService : IRestService
    {
        HttpClient client;
        String RestUrl = "http://swdcfreza838:8080/api/score-kernixes/";

        public List<Profile> Items { get; private set; }

        public RestService()
        {
            //var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
            //var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public async Task<List<Profile>> RefreshDataAsync()
        {
            Items = new List<Profile>();
            var uri = new Uri(string.Format(RestUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Profile>>(content);
                }
            }
            catch (Exception ex)
            {
                Items = null;
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<Profile> GetProfileAsync(String id){
            Profile item = new Profile();
            var uri = new Uri(string.Format(RestUrl + id, string.Empty));
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<Profile>(content);
                }
            }
            catch (Exception ex)
            {
                item = null;
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return item;
        }

        public async Task<bool> SaveProfileAsync(Profile item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(RestUrl, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(item);
                //json.Replace(":0,", string.Empty); //removing id attribute
                //json.Replace("\\\"id\\\"", string.Empty); //removing id attribute
                json = json.Replace("\"id\":0,", string.Empty); //removing id attribute
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    //Debug.WriteLine(@"Profile successfully saved.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return false;
        }

        public async Task<bool> DeleteProfileAsync(string id)
        {
            var uri = new Uri(string.Format(RestUrl + id, string.Empty));

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"Profile successfully deleted.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return false;
        }

        public async Task<String> GetScoreSignaletique(String id)
        {
            Profile item = new Profile();
            var uri = new Uri(string.Format(RestUrl + "score-si/" + id, string.Empty));
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<Profile>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                return @"ERROR { " + ex.Message + " }";
            }
            return item.predictionValue;
        }

        public async Task<String> GetPredictionML(String id)
        {
            Profile item = new Profile();
            var uri = new Uri(string.Format(RestUrl + "score-ml/" + id, string.Empty));
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<Profile>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                return @"ERROR { " + ex.Message + " }";
            }
            return item.predictionValue;
        }
    }
}