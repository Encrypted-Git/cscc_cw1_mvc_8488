using _8488_cw1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _8488_cw1.Controllers
{
    public class BookController : Controller
    {
        public string proxy_endpoint = "http://ec2-3-129-195-159.us-east-2.compute.amazonaws.com";
        // GET: BookController
        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();

            var response = await client.GetAsync(proxy_endpoint + "/api/Books");
            
            var content = await response.Content.ReadAsStringAsync();
            
            var books = JsonConvert.DeserializeObject<List<Book>>(content);
            
            return View(books);
        }

        // GET: BookController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Book book = new Book();

            var client = new HttpClient();
            
            var response = await client.GetAsync(proxy_endpoint + "/api/Books/" + id);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
            
                book = JsonConvert.DeserializeObject<Book>(content);
            }
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Book collection)
        {
            try
            {
                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(collection);

                var data = new StringContent(json, Encoding.UTF8, "application/json");
                
                await client.PostAsync(proxy_endpoint + "/api/Books", data);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Book book = new Book();

            var client = new HttpClient();

            var response = await client.GetAsync(proxy_endpoint + "/api/Books/" + id);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                book = JsonConvert.DeserializeObject<Book>(content);
            }
            return View(book);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Book collection)
        {
            try
            {
                var client = new HttpClient();
                
                collection.BookId = id;
                
                var json = JsonConvert.SerializeObject(collection);


                var data = new StringContent(json, Encoding.UTF8, "application/json");

                await client.PutAsync(proxy_endpoint+"/api/Books/"+id, data);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Book book = new Book();

            var client = new HttpClient();

            var response = await client.GetAsync(proxy_endpoint + "/api/Books/" + id);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                book = JsonConvert.DeserializeObject<Book>(content);
            }
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var client = new HttpClient();
                
                await client.DeleteAsync(proxy_endpoint + "/api/Books/" + id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
