using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GoodToHave.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace SkysFormsDemo.Pages.Person
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        [StringLength(100)]
        public string Name { get; set; }


        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public decimal Salary { get; set; }

        [Range(0, 8, ErrorMessage = "Du måste ha mellan 0 och 8 bilar dummer")]
        public int CarCount { get; set; }

        public string City { get; set; }
        public string Email { get; set; }

        public DateTime Datum { get; set; }

        public int CountryId { get; set; }

        public List<SelectListItem> AllCountries { get; set; }


        public EditModel(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void OnGet(int personId)
        {
            //Set properties från databas
            var person = _context.Person.Include(p=>p.Country).First(e => e.Id == personId);

            _mapper.Map( person, this);


            //Name = person.Name;
            //StreetAddress = person.StreetAddress;
            //PostalCode = person.PostalCode;
            //Salary = person.Salary;
            //City = person.City;
            //Email = person.Email;
            //CarCount = person.CarCount;
            //CountryId = person.Country.Id;
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


        public IActionResult OnPost(int personId)
        {
            if (ModelState.IsValid)
            {
                var person = _context.Person.First(e => e.Id == personId);
                person.Email = Email;
                person.CarCount = CarCount;
                person.City = City;
                person.Name = Name;
                person.StreetAddress = StreetAddress;
                person.PostalCode = PostalCode;
                person.Salary = Salary;

                person.Country = _context.Countries.First(e => e.Id == CountryId);

                _context.SaveChanges();

                //Spara i databas
                return RedirectToPage("Index");
            }
            //Visa felen och rita om formuläret
            SetAllCountries();
            return Page();

        }



    }
}
