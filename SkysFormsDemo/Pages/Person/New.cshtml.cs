using System.ComponentModel.DataAnnotations;
using GoodToHave.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

        [Range(0,8,ErrorMessage = "Du måste ha mellan 0 och 8 bilar dummer")]
        public int CarCount { get; set; }

        [Required]
        [MaxLength(30)]
        public string City { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        public bool IsCool { get; set; }

        [MaxLength(1000)]
        public string Cv { get; set; }


        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Ange ett LAND dummer")]
        public string CountryId { get; set; }

        public List<SelectListItem> AllCountries { get; set; }

        public List<SelectListItem> AllPositions{ get; set; }

        public PlayerPosition SelectedPosition { get; set; }


        public NewModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Datum = DateTime.Now;
            //SelectedPosition = PlayerPosition.Forward;
            SetAllLists();
        }

        private void SetAllLists()
        {
            SetAllCountries();
            SetAllPositions();
        }


        public void SetAllPositions()
        {
            AllPositions = Enum.GetValues<PlayerPosition>().Select(p => new SelectListItem
            {
                Text = p.ToString(),
                Value = p.ToString()
            }).ToList();
        }

        public void SetAllCountries()
        {
            AllCountries = _context.Countries.Select(country => new SelectListItem
            {
                Text = country.Name,
                Value = country.Id.ToString()
            }).ToList();
            AllCountries.Insert(0,new SelectListItem
            {
                Value = "",
                Text = "Please select a country"
            });
        }

        public bool IsValidPostNummer(string inmatat)
        {
            //fråga mot databasen om inmatat 
            return inmatat.StartsWith("2");
        }

        public IActionResult OnPost()
        {
            if (IsValidPostNummer(PostalCode) == false)
            {
                ModelState.AddModelError(nameof(PostalCode),"Det är inte valid postnummer");
            }
            if (ModelState.IsValid)
            {
                var person = new GoodToHave.Data.Person();
                person.Email = Email;
                person.CarCount = CarCount;
                person.City = City;
                person.Name = Name;
                person.StreetAddress = StreetAddress;
                person.PostalCode = PostalCode;
                person.Salary = Salary;

                person.Country = _context.Countries.First(e=>e.Id.ToString() == CountryId);

                _context.Person.Add(person);
                _context.SaveChanges();

                //Spara i databas
                return RedirectToPage("Index");
            }

            //Visa felen och rita om formuläret
            SetAllLists();

            return Page();

        }

        // OnPost
    }
}
