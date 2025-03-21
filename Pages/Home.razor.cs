using Microsoft.AspNetCore.Components;
using QuoteGeneration.Models;
using System.Net.Http;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace QuoteGeneration.Pages
{
    public partial class Home : ComponentBase
    {
        private QuotesResponse QuotesResponse { get; set; }
        public string Author { get; set; }
        public string Quote { get; set; }
        private bool IsLoading { get; set; } = false;
        private string LoadingMessage = "Loading . . .";

        public Home()
        {
            Author = "New instance";
            Quote = "Click button to generate a new quote";
        }

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            StateHasChanged();
            await Task.Delay(100);
            if (SavedQuotesService.Quotes.Count == 0)
            {
                try
                {
                    var json = await QueryService.GetQuotesAsync();
                    QuotesResponse = JsonSerializer.Deserialize<QuotesResponse>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    SavedQuotesService.Quotes.AddRange(QuotesResponse.Quotes);

                    await GenerateQuote();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } else
            {
                await GenerateQuote();
            }
        }

        private async Task GenerateQuote()
        {
            IsLoading = true;
            LoadingMessage = "Loading . . .";
            StateHasChanged();
            await Task.Delay(100);

            if (SavedQuotesService.Quotes != null && SavedQuotesService.Quotes.Count > 0)
            {
                var quotes = SavedQuotesService.Quotes.Where(q => q.enabled)
                                                      .OrderBy(q => q.number_of_calls)
                                                      .Take(10)
                                                      .ToList();

                LoadingMessage = "Displaying quote . . .";
                StateHasChanged();
                await Task.Delay(100);

                Random random = new();
                int i = random.Next(quotes.Count);

                Author = quotes[i].author;
                Quote = quotes[i].quote;
                var updatedQuote = SavedQuotesService.Quotes.FirstOrDefault(q => q.id == quotes[i].id);
                updatedQuote.number_of_calls++;
                await QueryService.UpdateQuoteAsync(updatedQuote);
            }

            IsLoading = false;
            LoadingMessage = "Loading . . .";
            StateHasChanged();
            await Task.Delay(100);
        }
    }
}
