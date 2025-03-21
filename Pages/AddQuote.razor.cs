using Microsoft.AspNetCore.Components;
using QuoteGeneration.Controllors;
using QuoteGeneration.Models;

namespace QuoteGeneration.Pages
{
    public partial class AddQuote : ComponentBase
    {
        private string Author { get; set; } = string.Empty;
        private string Quote { get; set; } = string.Empty;
        private bool IsLoading { get; set; } = false;
        private string LoadingMessage = "Loading . . .";

        private async void AddNewQuote()
        {
            // Have loading to check quote is exists
            IsLoading = true;
            LoadingMessage = "Checking if quote exists...";
                StateHasChanged();
                await Task.Delay(1000);

            if (SavedQuotesService.Quotes.Exists
               (q => 
                    q       .quote.ToLower().Replace(" ", "")?.Replace(".","") == 
                            Quote.ToLower().Replace(" ", "")?.Replace(".","")
               ))
            {
                LoadingMessage = "Quote already exists!";
                StateHasChanged();
                await Task.Delay(1000);
                LoadingMessage = "Loading . . .";
                IsLoading = false;
                StateHasChanged();
                return;
            } else if (Quote == "" || Author == "")
            {
                LoadingMessage = "Missing data!";
                StateHasChanged();
                await Task.Delay(1000);
                LoadingMessage = "Loading . . .";
                IsLoading = false;
                StateHasChanged();
                return;
            }

            LoadingMessage = "Adding new quote...";
            StateHasChanged();
            await Task.Delay(1000);
            var newQuote = await QueryService.AddQuoteAsync(Author, Quote);
            SavedQuotesService.Quotes.Add(newQuote);

            // Have loading animation here
            Author = string.Empty;
            Quote = string.Empty;

            IsLoading = false;
            LoadingMessage = "Loading . . .";
            StateHasChanged();
        }
    }
}
