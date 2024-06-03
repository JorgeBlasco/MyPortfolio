namespace MyPortfolio.Models
{
    public class MyPortfolioRepository
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public DateOnly Created { get; set; }
        public DateOnly Updated { get; set; }
    }
}
