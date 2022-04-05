using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkysFormsDemo.Data;

namespace SkysFormsDemo.Pages.Person
{
    [BindProperties]
    public class NewModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [StringLength(100)]
        public string Name { get; set; }


        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public decimal Salary { get; set; }

        [Range(0,8,ErrorMessage = "Du m�ste ha mellan 0 och 8 bilar dummer")]
        public int CarCount { get; set; }

        public string City { get; set; }
        public string Email { get; set; }

        public DateTime Datum { get; set; }

        public int CountryId { get; set; }

        public List<SelectListItem> AllCountries { get; set; }



        public NewModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            SetAllCountries();
        }

        public void SetAllCountries()
        {
            AllCountries = _context.Countries.Select(country => new SelectListItem
            {
                Text = country.Name,
                Value = country.Id.ToString()
            }).ToList();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var person = new Data.Person();
                person.Email = Email;
                person.CarCount = CarCount;
                person.City = City;
                person.Name = Name;
                person.StreetAddress = StreetAddress;
                person.PostalCode = PostalCode;
                person.Salary = Salary;

                person.Country = _context.Countries.First(e=>e.Id == CountryId);

                _context.Person.Add(person);
                _context.SaveChanges();

                //Spara i databas
                return RedirectToPage("Index");
            }

            //Visa felen och rita om formul�ret
            SetAllCountries();
            return Page();

        }

        // OnPost
    }
}
