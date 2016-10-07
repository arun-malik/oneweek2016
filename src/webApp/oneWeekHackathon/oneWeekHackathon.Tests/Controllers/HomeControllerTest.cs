using Microsoft.VisualStudio.TestTools.UnitTesting;
using oneWeekHackathon.Controllers.Web;
using System.Web.Mvc;

namespace oneWeekHackathon.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
