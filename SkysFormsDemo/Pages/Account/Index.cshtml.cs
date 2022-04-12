using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkysFormsDemo.Data;
using SkysFormsDemo.Services;

namespace SkysFormsDemo.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<KontoViewModel> Konton { get; set; }

        public class KontoViewModel
        {
            public int Id { get; set; }       
            public string AccountNo { get; set; }
            public decimal Balance { get; set; }
        }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Konton = _context.Accounts.Select(r => new KontoViewModel
            {
                Id = r.Id,
                AccountNo = r.AccountNo,
                Balance = r.Balance
            }).ToList();
        }
    }
}