using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkysFormsDemo.Data;
using SkysFormsDemo.ViewModels;


namespace SkysFormsDemo.Pages.Person
{
    public class ProffsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<PersonRowViewModel> Persons { get; set; }

        public ProffsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Persons = _context.Person.Where(a=>a.Name.StartsWith("A")).Select(r => new PersonRowViewModel
            {
                City = r.City,
                Id = r.Id,
                Name = r.Name,
                Email = r.Email
            }).ToList();

        }
    }
}
