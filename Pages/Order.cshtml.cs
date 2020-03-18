using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                var emailBody = 
                    $@"<p>Thank you, we have received your order for {OrderQuantity} unit(s) of {Product.Name}!</p>
                    <p>Your email address is <br/> {OrderShipping.Replace("\n", "<br/>")}</p>
                    Your total is ${Product.Price * OrderQuantity}.<br/>
                    We will contact you if we have questions about your order. Thank you!<br/>
                    <br/> <br/> <br/> 

                    HH <br/>
                    Manager, Fifth Coffee, LLC";
                
                using (var smtp = new SmtpClient())
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtp.PickupDirectoryLocation = @"c:\mailpickup";
                    var message = new MailMessage();
                    message.To.Add(OrderEmail);
                    message.Subject="Fifth Coffee - Your New Order";
                    message.Body = emailBody;
                    message.IsBodyHtml = true;
                    message.From = new MailAddress("sales@fifthcoffee.com");
                    await smtp.SendMailAsync(message);
                }

                // url: https://stackoverflow.com/questions/32260/sending-email-in-net-through-gmail
                // HH: try to send email via gmail.com
                // using (MailMessage mail = new MailMessage())
                // {
                //     mail.From = new MailAddress("xxxx@domain.com");
                //     mail.To.Add(OrderEmail);
                //     mail.Subject ="Fifth Coffee - Your New Order";
                //     mail.Body = emailBody;
                //     mail.IsBodyHtml = true;
                //     // mail.Attachments.Add(new Attachment("C:\\file.zip"));

                //     using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                //     {
                //         smtp.Credentials = 
                //             new NetworkCredential("xxxx@domain.com", "password");
                //         smtp.EnableSsl = true;
                //         await smtp.SendMailAsync(mail);
                //     }
                // }

                return RedirectToPage("OrderSuccess");
            }
            return Page();
        }
    }
}
