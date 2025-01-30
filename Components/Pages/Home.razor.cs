using Microsoft.AspNetCore.Components;
using Quote_Generation.Components.Models;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace Quote_Generation.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        public List<QuoteClass> QuoteList { get; set; }
        public string Author { get; set; }
        public string Quote { get; set; }

        public Home()
        {
            QuoteList = new List<QuoteClass>();
            Author = "New instance";
            Quote = "Click button to generate a new quote";
        }

        protected override async Task OnInitializedAsync()
        {
            QuoteList = await Http.GetFromJsonAsync<List<QuoteClass>>("Resources/quotes.json");
        }

        protected void GenerateQuote()
        {
            if (QuoteList != null && QuoteList.Count > 0)
            {
                Random random = new();
                int i = random.Next(QuoteList.Count);

                Author = QuoteList[i].Author;
                Quote = QuoteList[i].Quote;
            }
        }
    }
}
