using GoodToHave.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkysFormsDemo.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty] public string Name { get; set; }
        [BindProperty] public string Color { get; set; }
        [BindProperty] public decimal Price { get; set; }
        [BindProperty] public int PopularityPercent { get; set; }
        [BindProperty] public string Ean13 { get; set; }

        public void OnGet(int id)
        {
            var product = _context.Products.First(e => e.Id == id);
            Name = product.Name;
            Price = product.Price;
            Color = product.Color;
            Ean13 = product.Ean13;
            PopularityPercent = product.PopularityPercent;
        }

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var product = _context.Products.First(e => e.Id == id);
                product.Name = Name;
                product.Price = Price;
                product.Color = Color;
                product.Ean13 = Ean13;
                product.PopularityPercent = PopularityPercent;
                _context.SaveChanges();
                return RedirectToPage("Index");
            }

            return Page();
        }

    }
}
