using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkysFormsDemo.Data;

namespace SkysFormsDemo.Pages.Person
{
    public class ProffsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<PersonViewModel> Persons { get; set; }

        public class PersonViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public string Email { get; set; }
        }

        public ProffsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Persons = _context.Person.Where(a=>a.Name.StartsWith("A")).Select(r => new PersonViewModel
            {
                City = r.City,
                Id = r.Id,
                Name = r.Name,
                Email = r.Email
            }).ToList();

        }
    }
}
