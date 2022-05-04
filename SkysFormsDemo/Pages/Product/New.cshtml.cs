using GoodToHave.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkysFormsDemo.Pages.Product
{
    public class NewModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NewModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty] public string Name { get; set; }
        [BindProperty] public string Color { get; set; }
        [BindProperty] public decimal Price { get; set; }
        [BindProperty] public int PopularityPercent { get; set; }
        [BindProperty] public string Ean13 { get; set; }


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var product = new GoodToHave.Data.Product
                {
                    Name = Name,
                    Price = Price,
                    Color = Color,
                    Created = DateTime.UtcNow,
                    Ean13 = Ean13,
                    LastBought = DateTime.MinValue,
                    PopularityPercent = PopularityPercent
                };
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
