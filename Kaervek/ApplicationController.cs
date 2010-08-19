namespace Kaervek {
  using System.Web.Mvc;

  public class ApplicationController : Controller {
    public ApplicationController() {
      ItemsPerPage = 10;
    }

    /// <summary>
    /// Return the number of items that must be skipped, because paging.
    /// </summary>
    public int SkipItems {
      get {
        return (CurrentPage - 1) * ItemsPerPage;
      }
    }

    /// <summary>
    /// Indicate the number of items per page, if paging is required.
    /// The default value is 10.
    /// </summary>
    public int ItemsPerPage { get; set; }

    /// <summary>
    /// Current page number, retrieved from QueryString
    /// </summary>
    public int CurrentPage {
      get {
        int page;
        int.TryParse(Request.QueryString["page"], out page);
        if (page == 0) page = 1;
        ViewData["CurrentPage"] = page;
        return page;
      }
    }
  }
}
