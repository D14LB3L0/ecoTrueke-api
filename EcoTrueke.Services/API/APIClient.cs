using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace EcoTrueke.Services.API
{
    public class APIClient
    {
        private readonly HttpClient httpClient;
        public const string GET = "GET";
        public const string POST = "POST";
        public const string PUT = "PUT";
        public const string PATCH = "PATCH";
        public const string DELETE = "DELETE";

        public APIClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            httpClient.DefaultRequestHeaders.Clear();
        }

        public async Task<APIResponse> Get(string url, AuthorizationHeader authorizationHeader, string parameters = null)
        {
            return await Call(GET, url, authorizationHeader, parameters);
        }

        public async Task<APIResponse> Post(string url, AuthorizationHeader authorizationHeader, string parameters = null, List<IFormFile> files = null, IFormCollection values = null)
        {
            return await Call(POST, url, authorizationHeader, parameters, files, values);
        }

        public async Task<APIResponse> PostFormUrlEncoded(string url, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(parameters))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    HttpResponseMessage response = await httpClient.PostAsync(url, content);

                    string sResponse = await response.Content.ReadAsStringAsync();
                    return new APIResponse(response.StatusCode, sResponse);
                }
            }
        }

        public async Task<APIResponse> Put(string url, AuthorizationHeader authorizationHeader, string parameters = null, List<IFormFile> files = null, IFormCollection values = null)
        {
            return await Call(PUT, url, authorizationHeader, parameters, files, values);
        }

        public async Task<APIResponse> Patch(string url, AuthorizationHeader authorizationHeader, string parameters = null, List<IFormFile> files = null, IFormCollection values = null)
        {
            return await Call(PATCH, url, authorizationHeader, parameters, files, values);
        }

        public async Task<APIResponse> Delete(string url, AuthorizationHeader authorizationHeader, string parameters = null)
        {
            return await Call(DELETE, url, authorizationHeader/*, parameters*/);
        }

        private async Task<APIResponse> Call(string method, string url, AuthorizationHeader authorizationHeader, string parameters = null, List<IFormFile> files = null, IFormCollection values = null)
        {
            HttpContent content = null;

            if ((files == null || files.Count == 0) && (values == null || values.Count == 0) && method != GET && method != DELETE)
                content = BuildBody(parameters);
            else if ((files != null && files.Count > 0) || (values != null && values.Count > 0))
                content = BuildFormData(files, values);
            else if (parameters != null)
                url = $"{url}{BuildQuery(parameters)}";

            if (authorizationHeader != null)
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationHeader.Type, authorizationHeader.AuthenticationString);

            using HttpResponseMessage response =
                method == GET ? await httpClient.GetAsync(url) : (
                method == POST ? await httpClient.PostAsync(url, content) : (
                method == PUT ? await httpClient.PutAsync(url, content) : (
                method == PATCH ? await httpClient.PatchAsync(url, content) : (
                method == DELETE ? await httpClient.DeleteAsync(url) : null))));
            string sResponse = await response.Content.ReadAsStringAsync();
            return new APIResponse(response.StatusCode, sResponse);
        }

        private HttpContent BuildBody(string parameters)
        {
            return parameters != null ? new StringContent(parameters, Encoding.UTF8, "application/json") : null;
        }

        private HttpContent BuildFormData(List<IFormFile> files, IFormCollection values)
        {
            var multipartContent = new MultipartFormDataContent();

            if (files != null)
            {
                foreach (var file in files)
                {
                    multipartContent.Add(new StreamContent(file.OpenReadStream()), file.Name, file.FileName);
                }
            }

            if (values != null)
            {
                foreach (var value in values)
                {
                    multipartContent.Add(new StringContent(value.Value), value.Key);
                }
            }

            return multipartContent;
        }

        private string BuildQuery(string parameters)
        {
            var jObject = (JObject)JsonConvert.DeserializeObject(parameters);
            var dic = new Dictionary<string, string>();
            var uri = "?";
            var keyValue = jObject.GetEnumerator();

            bool isTrue = !string.IsNullOrWhiteSpace(parameters);

            while (isTrue)
            {
                if (keyValue.Current.Key != null)
                {
                    if (!string.IsNullOrWhiteSpace(keyValue.Current.Value.ToString()))
                    {
                        dic.Add(keyValue.Current.Key, keyValue.Current.Value.ToString());
                    }
                }
                isTrue = keyValue.MoveNext();
            }

            foreach (var item in dic)
            {
                if (item.Value.StartsWith("["))
                {
                    var strings = item.Value.Replace("[", "").Replace("]", "").Replace("\n  \"", "").Replace("\"", "").Replace("\n", "").Split(",");

                    foreach (var s in strings)
                    {
                        //borrar este comentario
                        uri += $"{item.Key}={s.ToString().Trim()}&";
                    }
                }
                else
                {
                    string itemValue = item.Value == "&" ? "%26" : item.Value;
                    uri += $"{item.Key}={itemValue}&";
                }
            }
            if (uri != "?")
                uri = uri.Remove(uri.LastIndexOf("&"));

            return uri;
        }
    }
}
