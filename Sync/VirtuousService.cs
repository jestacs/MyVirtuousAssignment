using RestSharp;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sync
{
    /// <summary>
    /// API Docs found at https://docs.virtuoussoftware.com/
    /// </summary>
    internal class VirtuousService
    {
        private readonly RestClient _restClient;

        public VirtuousService(IConfiguration configuration) 
        {
            var apiBaseUrl = configuration.GetValue("VirtuousApiBaseUrl");
            var apiKey = configuration.GetValue("VirtuousApiKey");

            var options = new RestClientOptions(apiBaseUrl)
            {
                Authenticator = new RestSharp.Authenticators.OAuth2.OAuth2AuthorizationRequestHeaderAuthenticator(apiKey)
            };

            _restClient = new RestClient(options);
        }

        public async Task<PagedResult<AbbreviatedContact>> GetContactsAsync(int skip, int take)
        {
            var request = new RestRequest("/api/Contact/Query", Method.Post);
            request.AddQueryParameter("Skip", skip);
            request.AddQueryParameter("Take", take);

            List<Conditions> conditions = new List<Conditions>();
            conditions.Add(new Conditions { Parameter = "Address", Operator = "Like", Value = "AZ", SecondaryValue = "", Values = new List<string>() });
            var body = new ContactQueryRequest();
            //body.Groups = conditions;

            request.AddJsonBody(body);

            var response = await _restClient.ExecutePostAsync<PagedResult<AbbreviatedContact>>(request);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var contacts = JsonSerializer.Deserialize<PagedResult<AbbreviatedContact>>(response.Content, options);
            return contacts;
        }
    }
}
