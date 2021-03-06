using GoodToHave.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkysFormsDemo.Services;
using SkysFormsDemo.ViewModels;

namespace SkysFormsDemo.Pages.Person
{
    public class IndexModel : PageModel
    {
        //private readonly IPersonService _personService;
        private readonly ApplicationDbContext _context;
        public List<PersonRowViewModel> Persons { get; set; }


        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGetFetchInfo(int id)
        {
            var person = _context.Person.Include(e => e.OwnedCars).First(e => e.Id == id);
            return new JsonResult(new { 
                namn = person.Name, 
                antalBilar = person.OwnedCars.Count });

        }

        public void OnGet()
        {
            Persons = _context.Person.Select(r => new PersonRowViewModel
            {
                City = r.City,
                Id = r.Id,
                Name = r.Name,
                Email = r.Email
            }).ToList();
        }
    }
}