using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace La27Barberia
{
    public class RestClient<T>
    {
        private HttpClient client;

        public RestClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Common.Host);
        }

        public async Task<T> GetAsync(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            T result = default(T);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<T>();
            }
            return result;
        }

        public async Task<List<T>> GetListAsync(string path)
        {
            List<T> result = new List<T>();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<List<T>>();
            }
            return result;
        }

        public async Task PostAsync(T t, string path)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync<T>(path, t);
            response.EnsureSuccessStatusCode();
        }

        public async Task<T> PutAsync(T t, string path)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync<T>(path, t);
            T result = default(T);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<T>();
            }
            return result;
        }

        public async Task DeleteAsync(string path)
        {
            HttpResponseMessage response = await client.DeleteAsync(path);
            response.EnsureSuccessStatusCode();
        }
    }
}
