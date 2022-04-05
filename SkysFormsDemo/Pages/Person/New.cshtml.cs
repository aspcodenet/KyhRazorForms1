using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkysFormsDemo.Data;

namespace SkysFormsDemo.Pages.Person
{
    public class NewModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public decimal Salary { get; set; }

        public int CarCount { get; set; }
        public string City { get; set; }
        public string Email { get; set; }

        public DateTime Datum { get; set; }



        public NewModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        // OnPost
    }
}
