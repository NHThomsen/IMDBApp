using IMDBApp.Model;
using IMDBApp.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IMDBApp.Pages
{
    public class EditTitleModel : PageModel
    {
        [BindProperty]
        public Title EditTitle { get; set; }
        public IActionResult OnGet(string tconst)
        {
            Title EditTitle = IMDBdal.DoesTitleExist(tconst);
            if (EditTitle == null)
            {
                return RedirectToPage("Index");
            }
            System.Diagnostics.Debug.WriteLine(EditTitle.PrimaryTitle);
            return Page();
        }
        public IActionResult OnPost()
        {
            IMDBdal.UpdateTitle(EditTitle);
            return RedirectToPage("Index");
        }
    }
}
