using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkysFormsDemo.Data;

namespace SkysFormsDemo.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<ProductViewModel> Items { get; set; }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Ean { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }

        public void OnGet()
        {
            Items = _context.Products.Select(e => new ProductViewModel
            {
                Name = e.Name,
                Ean = e.Ean13,
                Id = e.Id,
                Price = e.Price
            }).ToList();
        }
    }
}
