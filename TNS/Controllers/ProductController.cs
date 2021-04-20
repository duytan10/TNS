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
        private static IMongoCollection<Category> categoryCollection;
        private static List<Category> categoryList;

        public ProductController()
        {
            Mongo mongo = new Mongo();
            categoryCollection = mongo.db.GetCollection<Category>("category");
            categoryList = categoryCollection.Find(new BsonDocument()).ToList();
        }

        // GET: Product
        public ActionResult Index()
        {
            return View(categoryList);
        }

        // GET: Product/Details/5
        public ActionResult Details(string id)
        {
            Category category = categoryList.Find(item => item.Id.ToString().CompareTo(id) == 0);

            return View(category);
        }
    }
}
