using IMDBApp.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IMDBApp.Pages
{
    public class DeleteTitleModel : PageModel
    {
        public IActionResult OnGet(string tconst)
        {
            IMDBdal.DeleteTitle(tconst);
            return RedirectToPage("Index");
        }
    }
}
