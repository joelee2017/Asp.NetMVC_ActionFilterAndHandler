using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Tracing;
using ActionFilterAndHandler.Filter;
using ActionFilterAndHandler.Formatter;
using ActionFilterAndHandler.Models;

namespace ActionFilterAndHandler.Controllers
{
    [JwtAuthorize]
    public class ProductsController : ApiController
    {
        private Northwind db = new Northwind();

        //GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            //ProductCsvFormatter productCsvFormatter = new ProductCsvFormatter();
            //productCsvFormatter.
            var response = Request.CreateResponse(HttpStatusCode.OK, value: "value");
            //return db.Products;
            return db.Products;
        }

        //Tracing API Action
        //public IQueryable<Product> GetProducts()
        //{
        //    Configuration.Services.GetTraceWriter().Info(Request, "Products", "Get the list of products.");
        //    return db.Products;
        //}

        //測試使用 HttpResponseMessage 類別
        //public HttpResponseMessage GetProducts()
        //{
        //    var response = Request.CreateResponse(HttpStatusCode.OK, value: "Hi!!");
        //    //return db.Products;
        //    return response;
        //}

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductID)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PATCHProduct(int id, Product PatchData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != PatchData.ProductID)
            {
                return BadRequest();
            }

            Product product = db.Products.Find(id);

            if(product == null)
            {
                return BadRequest();
            }

            product.ProductName = PatchData.ProductName;
            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.ProductID }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductID == id) > 0;
        }
    }
}