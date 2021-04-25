using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Web.Mvc;
using TNS.Models;

namespace TNS.Controllers
{
    public class HomeController : Controller
    {
        private static IMongoCollection<Category> categoryCollection;
        private static List<Category> categoryList;

        public HomeController()
        {
            Mongo mongo = new Mongo();
            categoryCollection = mongo.db.GetCollection<Category>("categories");
            categoryList = categoryCollection.Find(new BsonDocument()).ToList();
        }

        // GET: Home
        public ActionResult Index()
        {
            return View(categoryList);
        }
    }
}
