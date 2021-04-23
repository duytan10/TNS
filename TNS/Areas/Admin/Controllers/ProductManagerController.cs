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

        public ProductManagerController()
        {
            Mongo mongo = new Mongo();
            productCollection = mongo.db.GetCollection<Product>("products_test");
            productList = productCollection.Find(new BsonDocument()).ToList();

            categoryCollection = mongo.db.GetCollection<Category>("products_test");
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
                    InStock = product.InStock
                };

                productCollection.InsertOneAsync(productAdded);

                string categoryId = Request.Form["Category"];
                ObjectId oId = new ObjectId(categoryId);
                //List<ObjectId> productIds = new List<ObjectId>
                //{
                //    productIds.Add(oPId);
                //};
                //categoryCollection.FindOneAndUpdateAsync(
                //    Builders<Category>.Filter.Eq("Id", oId),
                //    Builders<Category>.Update
                //        .Set("ProductIds", product.Id));

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
