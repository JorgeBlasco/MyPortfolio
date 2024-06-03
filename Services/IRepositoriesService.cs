using MyPortfolio.Models;

namespace MyPortfolio.Services
{
    public interface IRepositoriesService
    {
        public Task<List<MyPortfolioRepository>> GetMyRepositories();
    }
}