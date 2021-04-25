using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TNS.Models;

namespace TNS.Controllers
{
    public class ProductController : Controller
    {
        private static IMongoCollection<Product> productCollection;
        private static List<Product> productList;

        public ProductController()
        {
            Mongo mongo = new Mongo();
            productCollection = mongo.db.GetCollection<Product>("products");
            productList = productCollection.Find(new BsonDocument()).ToList();
        }

        public ActionResult Index(string id)
        {
            Product product = productList.Find(item => item.Id.ToString().CompareTo(id) == 0);

            return View(product);
        }
    }
}
