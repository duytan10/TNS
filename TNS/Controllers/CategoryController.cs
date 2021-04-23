using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TNS.Models;

namespace TNS.Controllers
{
    public class CategoryController : Controller
    {
        private static IMongoCollection<Category> categoryCollection;
        private static List<Category> categoryList;

        private static IMongoCollection<Product> productCollection;
        private static List<Product> productList;

        public CategoryController()
        {
            Mongo mongo = new Mongo();
            categoryCollection = mongo.db.GetCollection<Category>("category_test");
            productCollection = mongo.db.GetCollection<Product>("products_test");
            categoryList = categoryCollection.Find(new BsonDocument()).ToList();
        }

        // GET: Category
        public ActionResult Index(string id)
        {
            List<ObjectId> objectIds = categoryList.Find(item => item.Id.ToString().CompareTo(id) == 0).ProductIds;

            List<Product> relatedProducts = GetRelatedproducts(objectIds);

            Category category = categoryList.Find(item => item.Id.ToString().CompareTo(id) == 0);

            ViewBag.Name = category.Name;

            return View(relatedProducts);
        }

        private List<Product> GetRelatedproducts(List<ObjectId> objectIds)
        {
            HashSet<ObjectId> objectIdsHS = new HashSet<ObjectId>(objectIds.Select(objectId => objectId));

            List<Product> productList = productCollection.Find(new BsonDocument()).ToList();

            List<Product> relatedProducts = new List<Product>();

            foreach (var product in productList)
            {
                if (objectIdsHS.Contains(product.Id))
                {
                    relatedProducts.Add(product);
                }
            }

            return relatedProducts;
        }
    }
}
