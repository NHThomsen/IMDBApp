using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IMDBApp.Model;
using IMDBApp.SQL;

namespace IMDBApp.Pages
{
    public class CreateNameModel : PageModel
    {
        [BindProperty]
        public Name InsertName { get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPost() 
        {
            IMDBdal.InsertName(InsertName);
            return RedirectToPage("Index");
        }
    }
}
