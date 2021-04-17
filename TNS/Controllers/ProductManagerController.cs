using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TNS.Models;

namespace TNS.Controllers
{
    public class ProductManagerController : Controller
    {
        private static IMongoCollection<Product> productCollection;
        private static List<Product> productList;

        public ProductManagerController()
        {
            Mongo mongo = new Mongo();
            productCollection = mongo.db.GetCollection<Product>("products");
            productList = productCollection.Find(new BsonDocument()).ToList();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            return View(productList);
        }

        // GET: ProductManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductManager/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                productCollection.InsertOneAsync(product);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductManager/Edit/5
        public ActionResult Edit(string id)
        {
            Product product = productList.Find(item => item.Id.ToString().CompareTo(id) == 0);

            return View(product);
        }

        // POST: ProductManager/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Product product)
        {
            try
            {
                product.Id = new ObjectId(id);

                productCollection.FindOneAndUpdateAsync(
                    Builders<Product>.Filter.Eq("Id", product.Id),
                    Builders<Product>.Update
                        .Set("Title", product.Title)
                        .Set("ShortDescription", product.ShortDescription)
                        .Set("DiscountPercent", product.DiscountPercent)
                        .Set("Details", product.Details)
                        .Set("GuaganteeMonth", product.GuaganteeMonth)
                        .Set("Image", product.Image)
                        .Set("Type", product.Type)
                );

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductManager/Delete/5
        public ActionResult Delete(string id)
        {
            Product product = productList.Find(item => item.Id.ToString().CompareTo(id) == 0);

            return View(product);
        }

        // POST: ProductManager/Delete/5
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
    }
}
