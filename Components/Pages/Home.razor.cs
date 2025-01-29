using Microsoft.AspNetCore.Components;

namespace Quote_Generation.Components.Pages
{
    public partial class Home : ComponentBase
    {
        public string Author { get; set; } = "Creed";
        public string Quote { get; set; } = "When Pam gets Michael's old chair, I get Pam's old chair. Then I'll have two chairs. Only one to go.";

        private List<(string Author, string Quote)> QuotesList = new List<(string, string)>()
        {
            ("Michael Scott", "I'm not superstitious, but I am a little stitious."),
            ("Jim Halpert", "Bears. Beets. Battlestar Galactica."),
            ("Pam Beesly", "I feel God in this Chili's tonight."),
            ("Dwight Schrute", "Identity theft is not a joke, Jim! Millions of families suffer every year."),
            ("Dwight Schrute", "Whenever I'm about to do something, I think, 'Would an idiot do that?' And if they would, I do not do that thing."),
            ("Creed", "When Pam gets Michael's old chair, I get Pam's old chair. Then I'll have two chairs. Only one to go."),
            // Add more quotes as desired
        };

        protected override void OnInitialized()
        {
            Random random = new Random();
            int i = random.Next(QuotesList.Count);

            Author = QuotesList[i].Author;
            Quote = QuotesList[i].Quote;
        }

        protected void GenerateQuote()
        {
            Random random = new Random();
            int i = random.Next(QuotesList.Count);

            Author = QuotesList[i].Author;
            Quote = QuotesList[i].Quote;
        }
    }
}
