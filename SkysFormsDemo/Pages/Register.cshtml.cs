using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkysFormsDemo.Pages
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        [Required(ErrorMessage = "Ange en epost tack")]
        [EmailAddress(ErrorMessage = "Ange en epostaddress")]
        public string Email { get; set; }


        [Compare(nameof(Email), ErrorMessage = "Ange samma som ovan")]
        public string EmailAgain { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Ange samma password igen")]
        public string PasswordAgain { get; set; }


        [Range(18,50,ErrorMessage = "Ange en ålder mellan 18 och 50")]
        public int Age { get; set; }

        public string ImportantMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToPage("/Index"); //Vi är inloggade
            }

            ImportantMessage = "Se till att skriva in  rätt dummer";
            return Page();
        }

    }
}
