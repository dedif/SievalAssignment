using Microsoft.EntityFrameworkCore;
using SievalAssignment.Models;

namespace SievalAssignment.DAL
{
    public class ArticlesDataAccessLayer : IArticlesDataAccessLayer
    {
        private readonly AssignmentContext _context;

        public ArticlesDataAccessLayer(AssignmentContext context)
        {
            _context = context;
        }

        public bool ArticleExists(int id)
        {
            return (_context.Articles?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task DeleteArticle(int id)
        {
            if (_context.Articles == null)
            {
                throw new Exception("Article not found");
            }
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                throw new Exception("Article not found");
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }

        public async Task<Article> GetArticle(int id)
        {
            if (_context.Articles == null)
            {
                throw new Exception("Article not found");
            }
            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                throw new Exception("Article not found");
            }

            return article;
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            if (_context.Articles == null)
            {
                throw new Exception("Articles not found");
            }
            return await _context.Articles.ToListAsync();
        }

        public async Task<Article> InsertArticle(Article article)
        {
            if (_context.Articles == null)
            {
                throw new Exception("Entity set 'AssignmentContext.Articles'  is null.");
            }
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return article;
        }

        public async Task UpdateArticle(Article article)
        {
            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(article.Id))
                {
                    throw new Exception("Article not found");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
