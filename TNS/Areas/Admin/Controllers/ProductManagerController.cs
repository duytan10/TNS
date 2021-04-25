using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TNS.Models;

namespace TNS.Areas.Admin.Controllers
{
    public class ProductManagerController : Controller
    {
        private static IMongoCollection<Product> productCollection;
        private static List<Product> productList;

        private static IMongoCollection<Category> categoryCollection;
        private static List<Category> categoryList;

        public ProductManagerController()
        {
            Mongo mongo = new Mongo();
            productCollection = mongo.db.GetCollection<Product>("products");
            productList = productCollection.Find(new BsonDocument()).ToList();

            categoryCollection = mongo.db.GetCollection<Category>("categories");
            categoryList = categoryCollection.Find(new BsonDocument()).ToList();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Delete(string id)
        {
            Product product = productList.Find(item => item.Id.ToString().CompareTo(id) == 0);

            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                Product product = productList.Find(item => item.Id.ToString().CompareTo(id) == 0);
                productCollection.DeleteOneAsync(Builders<Product>.Filter.Eq("Id", product.Id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public List<Category> GetCategories()
        {
            return categoryList;
        }
    }
}
