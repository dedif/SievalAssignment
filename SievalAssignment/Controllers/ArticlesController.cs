using Microsoft.AspNetCore.Mvc;
using SievalAssignment.DAL;
using SievalAssignment.Models;

namespace SievalAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesDataAccessLayer _dataAccessLayer;

        public ArticlesController(IArticlesDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            IEnumerable<Article> articles;
            try
            {
                articles = await _dataAccessLayer.GetArticles();
            }
            catch (Exception exception) when (exception.Message.Equals("Articles not found"))
            {
                return NotFound();
            }
            return Ok(articles);
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            Article article;
            try
            {
                article = await _dataAccessLayer.GetArticle(id);
            }
            catch (Exception exception) when (exception.Message.Equals("Article not found"))
            {
                return NotFound();
            }
            return Ok(article);
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult> PutArticle(Article article)
        {
            try
            {
                await _dataAccessLayer.UpdateArticle(article);
            }
            catch (Exception exception) when (exception.Message.Equals("Article not found"))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            try
            {
                await _dataAccessLayer.InsertArticle(article);
            }
            catch (Exception exception) when (exception.Message.Equals("Entity set 'AssignmentContext.Articles'  is null."))
            {
                return Problem(exception.Message);
            }

            return CreatedAtAction("GetArticle", new { id = article.Id }, article);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            try
            {
                await _dataAccessLayer.DeleteArticle(id);
            }
            catch (Exception exception) when (exception.Message.Equals("Article not found"))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
