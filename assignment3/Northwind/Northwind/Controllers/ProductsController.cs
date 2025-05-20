using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Models;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers; 
using System.Text.Json;

namespace Northwind.Controllers;

public class ProductsController : Controller
{
    private readonly string baseUrl;
    private readonly string appJson;

    public ProductsController(IConfiguration config)
    {
        baseUrl = config.GetValue<string>("BaseUrl");
        appJson = config.GetValue<string>("AppJson");
    }

    private void ConfigClient(HttpClient client)
    {
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(appJson));
        client.BaseAddress = new Uri(baseUrl);
    }

    public async void GetCategoriesAsync() 
    {
        try
        {
            var categories = new List<Category>();

            using (var client = new HttpClient())
            {
                ConfigClient(client);
                var response = await client.GetAsync("categories");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<List<Category>>(json);
                }
                else
                {
                    ViewBag.ErrorMessage = response.StatusCode.ToString();
                }
            }
            ViewBag.Categories = new SelectList(categories, nameof(Category.categoryId), nameof(Category.categoryName));
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
        }
    }

    [AllowAnonymous]
    // GET: ProductsController
    public async Task<ActionResult> Index(int categoryId = 1)
    {
        GetCategoriesAsync();
        if (ViewBag.ErrorMessage != null)
        {
            return View("Error", new ErrorViewModel());
        }
        try
        {
            var products = new List<Product>();

            using (var client = new HttpClient())
            {
                ConfigClient(client);
                var response = await client.GetAsync("products/bycategory/" + categoryId);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(json);
                }
                else
                {
                    ViewBag.ErrorMessage = response.StatusCode.ToString();
                    return View("Error", new ErrorViewModel());
                }
            }

            return View(products);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View("Error", new ErrorViewModel());
        }
    }

    [Authorize]
    // GET: ProductsController/Details/5
    public async Task<ActionResult> Details(int id)
    {
        try
        {
            var product = new Product();

            using (var client = new HttpClient())
            {
                ConfigClient(client);
                var response = await client.GetAsync("products/" + id);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    product = JsonSerializer.Deserialize<Product>(json);
                }
                else
                {
                    ViewBag.ErrorMessage = $"Unable to find product with id={id}";
                    return View("Error", new ErrorViewModel());
                }
                try
                {
                    var categories = new List<Category>();

                    using (var clientC = new HttpClient())
                    {
                        ConfigClient(clientC);
                        var responseC = await clientC.GetAsync("categories");

                        if (response.IsSuccessStatusCode)
                        {
                            var json = await responseC.Content.ReadAsStringAsync();
                            categories = JsonSerializer.Deserialize<List<Category>>(json);
                        }
                        else
                        {
                            ViewBag.ErrorMessage = responseC.StatusCode.ToString();
                        }
                    }
                    for(int i = 0; i < categories.Count; i++)
                    {
                        if (categories[i].categoryId == product.categoryId)
                        {
                            ViewBag.CategoryName = categories[i].categoryName;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Error", new ErrorViewModel());
                }
            }
            return View(product);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View("Error", new ErrorViewModel());
        }
    }
}
