using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkysFormsDemo.Pages
{
    //[BindProperties]
    public class RegisterModel : PageModel
    {
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
        [Range(18,50,ErrorMessage = "Ange en �lder mellan 18 och 50")]
        public int Age { get; set; }

//        public string ImportantMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToPage("/Index"); //Vi �r inloggade
            }

//            ImportantMessage = "Se till att skriva in  r�tt dummer";
            return Page();
        }

    }
}
