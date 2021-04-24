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
            productCollection = mongo.db.GetCollection<Product>("products_test");
            productList = productCollection.Find(new BsonDocument()).ToList();

            categoryCollection = mongo.db.GetCollection<Category>("category_test");
            categoryList = categoryCollection.Find(new BsonDocument()).ToList();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            Product lastProduct = productList.Last();
            Category category = categoryList.Find(item => item.Id.CompareTo(lastProduct.CategoryId) == 0);

            ObjectId[] productIds = category.ProductIds.ToArray();
            int pos = Array.IndexOf(productIds, lastProduct.Id);

            if (pos < 0)
            {
                category.ProductIds.Add(lastProduct.Id);
                categoryCollection.FindOneAndUpdateAsync(
                Builders<Category>.Filter.Eq("Id", lastProduct.CategoryId),
                Builders<Category>.Update
                    .Set("ProductIds", category.ProductIds)
                );
            }
            
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
                //Get Status
                Status statusOption = (Status)Enum.Parse(typeof(Status), HttpContext.Request.Form["status"]);

                //Get Details
                Details detailText = new Details();
                ProductDetailType textType = (ProductDetailType)Enum.Parse(typeof(ProductDetailType), HttpContext.Request.Form["textType"]);
                detailText.Type = textType;
                detailText.Content = HttpContext.Request.Form["textContent"];
                Details detailImage = new Details();
                ProductDetailType textImage = (ProductDetailType)Enum.Parse(typeof(ProductDetailType), HttpContext.Request.Form["imageType"]);
                detailImage.Type = textImage;
                detailImage.Content = HttpContext.Request.Form["imageContent"];
                List<Details> details = new List<Details>
                {
                    detailText,
                    detailImage
                };

                //Get Short description
                string des = Request.Form["ShortDescriptions"];
                string[] desList = des.Split(new char[] { '|' });
                List<string> descriptions = new List<string>();
                foreach (string s in desList)
                {
                    descriptions.Add(s);
                }

                string imgs = Request.Form["Images"];
                string[] imgList = imgs.Split(new char[] { '|' });
                List<string> images = new List<string>();
                foreach (string s in imgList)
                {
                    images.Add(s);
                }

                //Get Category Id
                ObjectId categoryId = new ObjectId(Request.Form["Category"]);

                Product productAdded = new Product
                {
                    Title = product.Title,
                    ShortDescriptions = descriptions,
                    Price = product.Price,
                    DiscountPercent = product.DiscountPercent,
                    Status = statusOption,
                    Details = details,
                    GuaranteeMonth = product.GuaranteeMonth,
                    Images = images,
                    Type = product.Type,
                    Brand = product.Brand,
                    InStock = product.InStock,
                    CategoryId = categoryId
                };

                productCollection.InsertOneAsync(productAdded);

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

                Status statusOption = (Status)Enum.Parse(typeof(Status), HttpContext.Request.Form["status"]);

                Details detailText = new Details();
                ProductDetailType textType = (ProductDetailType)Enum.Parse(typeof(ProductDetailType), HttpContext.Request.Form["textType"]);
                detailText.Type = textType;
                detailText.Content = HttpContext.Request.Form["textContent"];
                Details detailImage = new Details();
                ProductDetailType textImage = (ProductDetailType)Enum.Parse(typeof(ProductDetailType), HttpContext.Request.Form["imageType"]);
                detailImage.Type = textImage;
                detailImage.Content = HttpContext.Request.Form["imageContent"];
                List<Details> details = new List<Details>
                {
                    detailText,
                    detailImage
                };

                string des = Request.Form["ShortDescriptions"];
                string[] desList = des.Split(new char[] { '|' });
                List<string> descriptions = new List<string>();
                foreach (string s in desList)
                {
                    descriptions.Add(s);
                }

                string imgs = Request.Form["Images"];
                string[] imgList = imgs.Split(new char[] { '|' });
                List<string> images = new List<string>();
                foreach (string s in imgList)
                {
                    images.Add(s);
                }

                productCollection.FindOneAndUpdateAsync(
                    Builders<Product>.Filter.Eq("Id", product.Id),
                    Builders<Product>.Update
                        .Set("Title", product.Title)
                        .Set("ShortDescriptions", descriptions)
                        .Set("Price", product.Price)
                        .Set("DiscountPercent", product.DiscountPercent)
                        .Set("Status", statusOption)
                        .Set("Details", details)
                        .Set("GuaranteeMonth", product.GuaranteeMonth)
                        .Set("Images", images)
                        .Set("Type", product.Type)
                        .Set("Brand", product.Brand)
                        .Set("InStock", product.InStock)
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
