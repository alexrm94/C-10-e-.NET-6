using Microsoft.AspNetCore.Mvc.RazorPages; // PageModel
using Packt.Shared; // NorthwindContext
using Microsoft.AspNetCore.Mvc; // [BindProperty], IActionResult
namespace Northwind.Web.Pages;

public class SuppliersModel : PageModel
{
    [BindProperty]
    public Supplier? Supplier { get; set; }
    public IActionResult OnPost()
    {
        if ((Supplier is not null) && ModelState.IsValid)
        {
            db.Suppliers.Add(Supplier);
            db.SaveChanges();
            return RedirectToPage("/suppliers");
        }
        else
        {
            return Page(); // return to original page
        }
    }

    private NorthwindContext db;
    public SuppliersModel(NorthwindContext injectedContext)
    {
        db = injectedContext;

    }
    public IEnumerable<object>? Suppliers { get; set; }
    public void OnGet()
    {
        ViewData["Title"] = "Northwind B2B - Suppliers";

        /*
        Suppliers = new[]
        {
        "Alpha Co", "Beta Limited", "Gamma Corp"
        };
        */

        Suppliers = db.Suppliers
            .OrderBy(c => c.Country)
            .ThenBy(c => c.CompanyName);


    }

}