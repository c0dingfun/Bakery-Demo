using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Data;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bakery.Pages
{
    // The PageModel acts as a combined page Controller and View Model.
    // As a Controller: its role is to process information from a request,
    // and then prepare a model for the view (the ViewModel).
    // There is a one-to-one mapping between PageModels and Content Pages (the view)
    // so that PageModel itself is the ViewModel

    // Info from the request (URL) is processed within **handler** methods.
    // This PageModel hs one handler method - OnGetAsync, which is executed by
    // convention as a result of HTTP requests made with the GET verb.
    public class IndexModel : PageModel
    {
        // private readonly ILogger<IndexModel> _logger;

        // public IndexModel(ILogger<IndexModel> logger)
        // {
        //     _logger = logger;
        // }

        // public void OnGet()
        // {

        // }


        private readonly BakeryContext db;
        // ctor
        public IndexModel(BakeryContext db) => this.db = db;
        public List<Product> Products { get; set; } = new List<Product>();
        
        public Product FeaturedProduct { get; set; }
        public async Task OnGetAsync()
        {
            Products = await db.Products.ToListAsync();
            FeaturedProduct = Products.ElementAt(new Random().Next(Products.Count));
        }
    }
}
