using IMDBApp.Model;
using IMDBApp.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IMDBApp.Pages
{
    public class CreateTitleModel : PageModel
    {
        [BindProperty]
        public Title CreateTitle { get; set; }
        [BindProperty]
        public List<TitleType> TitleTypes {  get; set; }
        public void OnGet()
        {
            TitleTypes = IMDBdal.GetTitleTypes();
        }
        public IActionResult OnPost() 
        {
            IMDBdal.InsertTitle(CreateTitle);
            return RedirectToPage("Index");
        }
    }
}
