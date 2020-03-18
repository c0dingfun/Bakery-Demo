using System;
using System.Collections.Generic;
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
        public OrderModel(BakeryContext db)=> this.db = db;

        // HH: This attribute ensures that the property is included in the **model 
        // binding** process, which results in values passed as part of an HTTP request 
        // being mapped to 
        // 1) PageModel properties and 
        // 2) handler method parameters. 
        // By default, model binding only works for values passed in a POST request. 
        // The Order page is reached by clicking a link on the home page, which results in 
        // a GET request. You must add SupportsGet = true to opt in to model binding on 
        // GET requests.
        [BindProperty(SupportsGet=true)]

        // route-parameter: order/3
        // query string order?id=3

        public int Id { get; set; }
        public Product Product { get; set; }

        public async Task OnGetAsync()=> Product=await db.Products.FindAsync(Id);
    }
}
