using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using ActionFilterAndHandler.Models;

namespace ActionFilterAndHandler.Controllers.Tests
{
    [TestClass()]
    public class ProductsControllerTests
    {
        [TestMethod()]
        public void GetProductsTest()
        {
            //Assert.Fail();
            var expected = 1;

            var client = new RestClient("http://localhost:12784/api/Products/1");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("Host", "localhost:12784");
            request.AddHeader("Postman-Token", "39b88df7-7bff-4713-9f0c-2fc5fd1f2a12,fd902b17-4b7a-4c1e-a291-4eca7dfb5885");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.15.0");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var  response = client.Execute<Product>(request);

            // AreEqual 比較 A (expected) 與 B (response.Data.ProductID)
            Assert.AreEqual(expected, response.Data.ProductID);
        }

        [TestMethod()]
        public void GetProductTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PutProductTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PATCHProductTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PostProductTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteProductTest()
        {
            Assert.Fail();
        }
    }
}