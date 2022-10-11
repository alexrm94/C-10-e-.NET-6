using Microsoft.AspNetCore.Mvc;
using Northwind.Mvc.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Packt.Shared; // NorthwindContext
using Northwind.Common;

namespace Northwind.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext db;
        private readonly IHttpClientFactory clientFactory;

        public HomeController(ILogger<HomeController> logger,
        NorthwindContext injectedContext,
        IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            db = injectedContext;
            clientFactory = httpClientFactory;
        }
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
        // public IActionResult Index() //sincrono
        public async Task<IActionResult> Index()// assincrono
        {
            _logger.LogError("This is a serious error (not really!)");
            _logger.LogWarning("This is your first warning!");
            _logger.LogWarning("Second warning!");
            _logger.LogInformation("I am in the Index method of the HomeController.");

            /*
            Categories: db.Categories.ToList(),
            Products: db.Products.ToList()
            //sincrono
             */

            HomeIndexViewModel model = new
            (
            VisitorCount: (new Random()).Next(1, 1001),
            Categories: await db.Categories.ToListAsync(),
            Products: await db.Products.ToListAsync()

            );

            try
            {
                HttpClient client = clientFactory.CreateClient(
                    name: "Minimal.WebApi");
                HttpRequestMessage request = new(
                method: HttpMethod.Get, requestUri: "api/weather");
                HttpResponseMessage response = await client.SendAsync(request);
                ViewData["weather"] = await response.Content
                .ReadFromJsonAsync<WeatherForecast[]>();
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"The Minimal.WebApi service is not responding. Exception: { ex.Message}");
            ViewData["weather"] = Enumerable.Empty<WeatherForecast>().ToArray();
            }



            return View(model); // pass model to view
        }
        [Route("private")]
        [Authorize(Roles = "Administrators")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // public IActionResult ProductDetail(int? id)//sincrono
        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("You must pass a product ID in the route, for example, / Home / ProductDetail / 21");
            }
            //Product? model = db.Products.SingleOrDefault(p => p.ProductId == id);//sincrono
            Product? model = await db.Products.SingleOrDefaultAsync(p => p.ProductId == id);
            if (model == null)
            {
                return NotFound($"ProductId {id} not found.");
            }
            return View(model); // pass model to view and then return result
        }


        public IActionResult ModelBinding()
        {
            return View(); // the page with a form to submit
        }
        [HttpPost]
        public IActionResult ModelBinding(Thing thing)
        {
            // return View(thing); // show the model bound thing

            HomeModelBindingViewModel model = new(
            thing,
            !ModelState.IsValid,
            ModelState.Values
            .SelectMany(state => state.Errors)
            .Select(error => error.ErrorMessage)
);
            return View(model);
        }

        public IActionResult ProductsThatCostMoreThan(decimal? price)
        {
            if (!price.HasValue)
            {
                return BadRequest("You must pass a product price in the query string, for example, / Home / ProductsThatCostMoreThan ? price = 50");
            }
            IEnumerable<Product> model = db.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Where(p => p.UnitPrice > price);
            /*
             Pra filtrar por um valor específico sem permitir unidades zeradas de estoque
            .Where(p => p.UnitPrice >= price)
            .Where(p => p.UnitPrice <= price)
            .Where(p => p.UnitsInStock != 0);
            */

            if (!model.Any())
            {
                return NotFound(
                $"No products cost more than {price:C}.");
            }
            ViewData["MaxPrice"] = price.Value.ToString("C");
            return View(model); // pass model to view
        }

        public async Task<IActionResult> Customers(string country)
        {
            string uri;

            if (string.IsNullOrEmpty(country))
            {
                ViewData["Title"] = "All Customers Worldwide";
                uri = "api/customers/";
            }
            else
            {
                ViewData["Title"] = $"Customers in {country}";
                uri = $"api/customers/?country={country}";
            }

            HttpClient client = clientFactory.CreateClient(
              name: "Northwind.WebApi");

            HttpRequestMessage request = new(
              method: HttpMethod.Get, requestUri: uri);

            HttpResponseMessage response = await client.SendAsync(request);

            IEnumerable<Customer>? model = await response.Content
              .ReadFromJsonAsync<IEnumerable<Customer>>();

            return View(model);
        }



    }

}

