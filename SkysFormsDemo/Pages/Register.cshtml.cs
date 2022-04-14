using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkysFormsDemo.Data;
using SkysFormsDemo.Infrastructure.Validation;

namespace SkysFormsDemo.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        [Required]
        [Range(1900,2030)]
        [GoodHockeyYear(ErrorMessage = "Vi vann inte vm då ")]
        public int Year { get; set; }


        [BindProperty]
        [Required(ErrorMessage = "Ange en epost tack")]
        [EmailAddress(ErrorMessage = "Ange en epostaddress")]
        public string Email { get; set; }


        [BindProperty]
        [Required(ErrorMessage = "sdasdadsa")]
        [Compare(nameof(Email), ErrorMessage = "Ange samma som ovan")]
        public string EmailAgain { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [Compare(nameof(Password), ErrorMessage = "Ange samma password igen")]
        [DataType(DataType.Password)]
        public string PasswordAgain { get; set; }


        [BindProperty]
        [Range(18,50,ErrorMessage = "Ange en ålder mellan 18 och 50")]
        public int Age { get; set; }

        //public string ImportantMessage { get; set; }

        public List<SelectListItem> Countries { get; set; }

        [BindProperty]
        [Required]
        public string CountryCode { get; set; }


        [BindProperty]
        [Required]
        public string Capital { get; set; }


        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(string test = "", string ?test2="")
        {
            FillDropdowns();
        }

        private void FillDropdowns()
        {
            Countries = new List<SelectListItem>();
            Countries.Add(new SelectListItem("Sverige", "SE"));
            Countries.Add(new SelectListItem("Norge", "NO"));
            Countries.Add(new SelectListItem("Finland", "FI"));
        }


        public IActionResult OnPost()
        {

          


            if (CountryCode == "SE")
            {
                if (Capital.ToLower() != "stockholm")
                    ModelState.AddModelError(nameof(Capital),"Ange rätt huvudstad för Sverige");
            }
            if (CountryCode == "FI")
            {
                if (Capital.ToLower() != "helsingfors")
                    ModelState.AddModelError(nameof(Capital), "Ange rätt huvudstad för Finland");
            }
            if (CountryCode == "NO")
            {
                if (Capital.ToLower() != "oslo")
                    ModelState.AddModelError(nameof(Capital), "Ange rätt huvudstad för oslo");
            }

            if (ModelState.IsValid)
            {
                if (_context.Person.Any(e => e.Email == Email))
                {
                    ModelState.AddModelError(nameof(Email),"Du har redan ett konto");
                }
            }



            if (ModelState.IsValid)
            {
                return RedirectToPage("/Index"); //Vi är inloggade
            }

            //            ImportantMessage = "Se till att skriva in  rätt dummer";
            FillDropdowns();
            return Page();
        }

    }
}
