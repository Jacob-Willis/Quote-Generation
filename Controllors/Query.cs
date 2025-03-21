using QuoteGeneration.Models;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace QuoteGeneration.Controllors
{
    public class Query
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "https://quote-generator.hasura.app/v1/graphql";
        private const string endpointUrl = "https://quote-generator.hasura.app/api/rest/Quotes";
        private const string adminSecret = "UKPB83diNF4rArzOf9nmCDBpad1lHEF5Buf0j6Dj7jsCxzDBGWLnOA1V4VDjJMln";

        public Query(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetQuotesAsync()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-hasura-admin-secret", adminSecret);

            var response = await _httpClient.GetAsync(endpointUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            } else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<QuoteClass> AddQuoteAsync(string author, string quote)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-hasura-admin-secret", adminSecret);
            var newQuote = new
            {
                payloadObject = new
                {
                    author = author,
                    quote = quote,
                    number_of_calls = 0
                }
            };
            var json = JsonSerializer.Serialize(newQuote, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            });
            var response = await _httpClient.PostAsync(endpointUrl, new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Deserialize using InsertQuoteResponse
                var deserializedResponse = JsonSerializer.Deserialize<InsertQuoteResponse>(jsonResponse, options);

                // Return the deserialized QuoteClass object
                return deserializedResponse?.insert_Quotes_one;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<string> UpdateQuoteAsync(QuoteClass quote)
        {
            string _endpointUrl = $"{endpointUrl}/{quote.id}";
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-hasura-admin-secret", adminSecret);
            var newQuote = new
            {
                payloadObject = new
                {
                    author = quote.author,
                    quote = quote.quote,
                    number_of_calls = quote.number_of_calls,
                    enabled = quote.enabled
                }
            };
            var json = JsonSerializer.Serialize(newQuote, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            });
            var response = await _httpClient.PostAsync(_endpointUrl, new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}
