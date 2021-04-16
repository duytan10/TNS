using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TNS.Models;

namespace TNS.Controllers
{
    public class HomeController : Controller
    {
        private static IMongoCollection<Test> testCollection;
        private static List<Test> testList;

        public HomeController()
        {
            Mongo mongo = new Mongo();
            testCollection = mongo.db.GetCollection<Test>("test");
            testList = testCollection.Find(new BsonDocument()).ToList();
        }

        // GET: Home
        public ActionResult Index()
        {
            return View(testList);
        }

        // GET: Home/Details/5
        public ActionResult Details(string id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(Test test)
        {
            try
            {
                testCollection.InsertOneAsync(test);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(string id)
        {
            ObjectId oId = new ObjectId(id);
            Test test = testCollection.Find(item => item.Id == oId).FirstOrDefault();

            return View(test);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Test test)
        {
            try
            {
                test.Id = new ObjectId(id);

                testCollection.FindOneAndUpdateAsync(
                    Builders<Test>.Filter.Eq("Id", test.Id),
                    Builders<Test>.Update
                        .Set("Title", test.Title)
                        .Set("Price", test.Price)
                        .Set("Details", test.Details)
                );

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
