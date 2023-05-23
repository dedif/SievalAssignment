using Microsoft.AspNetCore.Mvc;
using Moq;
using SievalAssignment.DAL;
using SievalAssignment.Models;

namespace SievalAssignment.Controllers.Tests
{
    [TestClass()]
    public class ArticlesControllerTests
    {
        [TestClass]
        public class GetArticles
        {
            [TestMethod]
            public async Task ReturnsOkResult_WhenArticlesAreFound()
            {
                // Arrange
                var mockList = new List<Article>();
                var mockDataAccessLayer = new Mock<IArticlesDataAccessLayer>();
                mockDataAccessLayer.Setup(dal => dal.GetArticles()).ReturnsAsync(mockList);
                var controller = new ArticlesController(mockDataAccessLayer.Object);

                // Act
                var result = await controller.GetArticles();

                // Assert
                Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
                var okObjectResult = result.Result as OkObjectResult;
                Assert.AreEqual(mockList, okObjectResult.Value);
            }

            [TestMethod]
            public async Task ReturnsNotFoundResult_WhenArticlesAreNotFound()
            {
                // Arrange
                var mockDataAccessLayer = new Mock<IArticlesDataAccessLayer>();
                mockDataAccessLayer.Setup(dal => dal.GetArticles()).ThrowsAsync(new Exception("Articles not found"));
                var controller = new ArticlesController(mockDataAccessLayer.Object);

                // Act
                var result = await controller.GetArticles();

                // Assert
                Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            }
        }

        [TestClass]
        public class GetArticle
        {
            [TestMethod]
            public async Task ReturnsOk_WhenArticleFound()
            {
                // Arrange
                var mockArticle = new Article(0, "x", "x", 0);
                var mockDataAccessLayer = new Mock<IArticlesDataAccessLayer>();
                mockDataAccessLayer.Setup(dal => dal.GetArticle(It.IsAny<int>())).ReturnsAsync(mockArticle);
                var controller = new ArticlesController(mockDataAccessLayer.Object);

                // Act
                var result = await controller.GetArticle(1);
                var result2 = result.Result;

                // Assert
                Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
                var okObjectResult = result.Result as OkObjectResult;
                Assert.AreEqual(mockArticle, okObjectResult.Value);
            }

            [TestMethod]
            public async Task ReturnsNotFound_WhenArticleNotFound()
            {
                // Arrange
                var mockDataAccessLayer = new Mock<IArticlesDataAccessLayer>();
                mockDataAccessLayer.Setup(dal => dal.GetArticle(It.IsAny<int>())).Throws(new Exception("Article not found"));
                var controller = new ArticlesController(mockDataAccessLayer.Object);

                // Act
                var result = await controller.GetArticle(1);

                // Assert
                Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            }
        }

        [TestClass]
        public class PutArticle
        {
            [TestMethod]
            public async Task ReturnsNoContent_WhenArticleExists()
            {
                // Arrange
                var mockDataAccessLayer = new Mock<IArticlesDataAccessLayer>();
                mockDataAccessLayer.Setup(dal => dal.UpdateArticle(It.IsAny<Article>())).Returns(Task.CompletedTask);
                var controller = new ArticlesController(mockDataAccessLayer.Object);
                var article = new Article(0, "x", "x", 0);

                // Act
                var result = await controller.PutArticle(article);

                // Assert
                Assert.IsInstanceOfType(result, typeof(NoContentResult));
            }

            [TestMethod]
            public async Task ReturnsNotFound_WhenArticleDoesNotExist()
            {
                // Arrange
                var mockDataAccessLayer = new Mock<IArticlesDataAccessLayer>();
                mockDataAccessLayer.Setup(dal => dal.UpdateArticle(It.IsAny<Article>())).Throws(new Exception("Article not found"));
                var controller = new ArticlesController(mockDataAccessLayer.Object);
                var article = new Article(0, "x", "x", 0);

                // Act
                var result = await controller.PutArticle(article);

                // Assert
                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

        [TestClass]
        public class PostArticle
        {
            [TestMethod]
            public async Task ShouldReturnCreatedAtActionResult_WhenPostIsSuccessful()
            {
                // Arrange
                var mockDataAccessLayer = new Mock<IArticlesDataAccessLayer>();
                var controller = new ArticlesController(mockDataAccessLayer.Object);
                var article = new Article(0, "x", "x", 0);

                // Act
                var result = await controller.PostArticle(article);

                // Assert
                Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
            }

            [TestMethod]
            public async Task ShouldReturnProblemResult_WhenDatabaseCannotBeFound()
            {
                // Arrange
                var mockDataAccessLayer = new Mock<IArticlesDataAccessLayer>();
                mockDataAccessLayer.Setup(x => x.InsertArticle(It.IsAny<Article>())).Throws(new Exception("Entity set 'AssignmentContext.Articles'  is null."));
                var controller = new ArticlesController(mockDataAccessLayer.Object);
                var article = new Article(0, "x", "x", 0);

                // Act
                var result = await controller.PostArticle(article);

                // Assert
                var objectResult = result.Result as ObjectResult;
                Assert.IsInstanceOfType(objectResult.Value, typeof(ProblemDetails));
            }
        }

        [TestClass]
        public class DeleteArticle
        {
            // Unit Test
            [TestMethod]
            public async Task ReturnsNoContent_WhenArticleExists()
            {
                // Arrange
                var mockDataAccessLayer = new Mock<IArticlesDataAccessLayer>();
                mockDataAccessLayer.Setup(dal => dal.DeleteArticle(It.IsAny<int>())).Returns(Task.CompletedTask);
                var controller = new ArticlesController(mockDataAccessLayer.Object);

                // Act
                var result = await controller.DeleteArticle(1);

                // Assert
                Assert.IsInstanceOfType(result, typeof(NoContentResult));
            }

            [TestMethod]
            public async Task ReturnsNotFound_WhenArticleDoesNotExist()
            {
                // Arrange
                var mockDataAccessLayer = new Mock<IArticlesDataAccessLayer>();
                mockDataAccessLayer.Setup(dal => dal.DeleteArticle(It.IsAny<int>())).Throws(new Exception("Article not found"));
                var controller = new ArticlesController(mockDataAccessLayer.Object);

                // Act
                var result = await controller.DeleteArticle(1);

                // Assert
                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }
    }
}