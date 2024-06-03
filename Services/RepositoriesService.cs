using MyPortfolio.Models;
using System.Text.Json;

namespace MyPortfolio.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        public async Task<List<MyPortfolioRepository>> GetMyRepositories()
        {
            string jsonData = await GetGithubRepositoriesJson();
            List<MyPortfolioRepository> MyRepos = ParseRepositoriesJson(jsonData);
            
            return MyRepos;
        }

        private List<MyPortfolioRepository> ParseRepositoriesJson(string json)
        {
            List<MyPortfolioRepository> MyRepos = [];
            using (JsonDocument document = JsonDocument.Parse(json))
            {
                foreach (JsonElement repo in document.RootElement.EnumerateArray())
                {
                    MyRepos.Add(new MyPortfolioRepository
                    {
                        Id = repo.GetProperty("id").GetUInt32(),
                        Name = repo.GetProperty("name").GetString() ?? string.Empty,
                        Description = repo.GetProperty("description").GetString() ?? string.Empty,
                        Url = repo.GetProperty("html_url").GetString() ?? string.Empty,
                        Created = DateOnly.FromDateTime(repo.GetProperty("created_at").GetDateTime()),
                        Updated = DateOnly.FromDateTime(repo.GetProperty("updated_at").GetDateTime()),
                    });
                }
            }
            return MyRepos;
        }

        private async Task<string> GetGithubRepositoriesJson()
        {
            string json = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://api.github.com/users/JorgeBlasco/repos");
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                }
            }
            return json;
        }

    }
}
