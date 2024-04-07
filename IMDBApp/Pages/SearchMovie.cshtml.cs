using IMDBApp.Model;
using IMDBApp.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IMDBApp.Pages
{
    public class SearchMovieModel : PageModel
    {
        [BindProperty]
        public string searchString { get; set; }
        [BindProperty]
        public List<Title> SearchResults {  get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public void OnPost()
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                SearchResults = IMDBdal.SearchMovieTitle(searchString);
            }
        }
    }
}
