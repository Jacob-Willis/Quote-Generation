namespace QuoteGeneration.Models
{
    public class QuoteClass
    {
        public int id { get; set; }
        public string author { get; set; }
        public string quote { get; set; }
        public string genre { get; set; }
        public int number_of_calls { get; set; }
        public bool enabled { get; set; }
        public string created_at { get; set; }

        public override string ToString()
        {
            return $"{author} - \"{quote}\"";
        }
    }

    public class QuotesResponse
    {
        public List<QuoteClass> Quotes { get; set; }
    }

    public class SavedQuotesService
    {
        public List<QuoteClass> Quotes { get; set; } = new List<QuoteClass>();
    }

    public class InsertQuoteResponse
    {
        public QuoteClass insert_Quotes_one { get; set; }
    }
}
