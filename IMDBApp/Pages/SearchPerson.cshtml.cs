using IMDBApp.Model;
using IMDBApp.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IMDBApp.Pages
{
    public class SearchPersonModel : PageModel
    {
        [BindProperty]
        public string searchString { get; set; }

        public List<Name> SearchResults { get; set; }
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            SearchResults = IMDBdal.SearchPerson(searchString);
        }
    }
}
