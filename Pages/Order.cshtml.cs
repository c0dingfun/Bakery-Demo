using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Data;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakery.Pages
{
    public class OrderModel : PageModel
    {
        // public void OnGet() { }

        private BakeryContext db;
        public OrderModel(BakeryContext db) => this.db = db;

        // HH: This attribute ensures that the property is included in the **model 
        // binding** process, which results in values passed as part of an HTTP request 
        // being mapped to 
        // 1) PageModel properties and 
        // 2) handler method parameters. 
        // By default, model binding only works for values passed in a POST request. 
        // The Order page is reached by clicking a link on the home page, which results in 
        // a GET request. You must add SupportsGet = true to opt in to model binding on 
        // GET requests.
        [BindProperty(SupportsGet = true)]

        // route-parameter: order/3
        // query string order?id=3

        public int Id { get; set; }
        public Product Product { get; set; }

        [BindProperty, EmailAddress, Required, Display(Name = "Your Email Address")]
        public string OrderEmail { get; set; }

        [BindProperty, Required(ErrorMessage = "Please Supply a shipping address"), Display(Name = "Shipping Address")]
        public string OrderShipping { get; set; }

        [BindProperty, Display(Name = "Quantity")]
        public int OrderQuantity { get; set; } = 1;

        public async Task OnGetAsync() => Product = await db.Products.FindAsync(Id);

        public async Task<IActionResult> OnPostAsync()
        {
            Product = await db.Products.FindAsync(Id);
            if (ModelState.IsValid)
            {
                return RedirectToPage("OrderSuccess");
            }
            return Page();
        }
    }
}
