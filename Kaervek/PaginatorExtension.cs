namespace Kaervek {
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.Mvc;

  public static class PaginatorExtension {
    public static List<T> AsPaginator<T>(this IQueryable<T> source, ApplicationController controller) {
      var total = source.Count();

      var result = source.Skip(controller.SkipItems).Take(controller.ItemsPerPage).ToList();

      var paginator = new Paginator() {
        CurrentPage = controller.CurrentPage,
        ItemsPerPage = controller.ItemsPerPage,
        Total = total
      };

      controller.ViewData["paginator"] = paginator;

      return result;
    }

    public static string Paginator(this HtmlHelper html, string action) {
      var paginator = html.ViewData["paginator"] as Paginator;

      var url = UrlHelper.GenerateUrl(null, action,
          html.ViewContext.Controller.ControllerContext.RouteData.Values["controller"].ToString(),
          null, html.RouteCollection, html.ViewContext.RequestContext, false);

      return paginator.ToHtml(url);
    }
  }
}
