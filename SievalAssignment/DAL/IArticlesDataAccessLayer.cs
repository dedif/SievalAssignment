using SievalAssignment.Models;

namespace SievalAssignment.DAL
{
    public interface IArticlesDataAccessLayer
    {
        public Task<IEnumerable<Article>> GetArticles();

        public Task<Article> GetArticle(int id);

        public Task UpdateArticle(Article article);

        public Task<Article> InsertArticle(Article article);

        public Task DeleteArticle(int id);

        protected bool ArticleExists(int id);
    }
}
