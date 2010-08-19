namespace Kaervek {
  using System;
  using System.Collections.Generic;
  using System.Web.Mvc;
  using System.Web.Mvc.Html;

  public static class HtmlExtension {
    public static MvcHtmlString MenuLink(this HtmlHelper html, string text, string action, string controller) {
      return MenuLink(html, text, action, controller, null);
    }

    public static MvcHtmlString MenuLink(this HtmlHelper html, string text, string action, string controller, params string[] subcontrollers) {
      var name = html.ViewContext.Controller.ToString();
      name = name.Substring(name.LastIndexOf('.') + 1).Replace("Controller", "");
      var controllers = new List<string>();
      controllers.Add(controller);
      if (subcontrollers != null) {
        controllers.AddRange(subcontrollers);
      }
      var active = controllers.Find(x => x == name) != null;
      var attributes = new { @class = active ? "active" : "" };

      return html.ActionLink(text, action, controller, null, attributes);
    }

    public static MvcHtmlString SubMenuLink(this HtmlHelper html, string text, string action, string controller) {
      var name = html.ViewContext.Controller.ToString();
      name = name.Substring(name.LastIndexOf('.') + 1).Replace("Controller", "");
      var actionName = html.ViewContext.Controller.ControllerContext.RouteData.Values["action"] as string;
      var active = (controller == name) && (action == actionName);
      var attributes = new { @class = active ? "active" : "" };

      return html.ActionLink(text, action, controller, null, attributes);
    }

    private static string jsIncludeTag = "<script src=\"{0}\" type=\"text/javascript\"></script>";
    public static string Javascripts(this HtmlHelper html, params string[] files) {
      var js = new List<string>();
      var urlHelper = new UrlHelper(html.ViewContext.RequestContext);

      foreach (var file in files) {
        var name = file.ToLower();
        if (!name.EndsWith(".js")) name += ".js";
        if (!name.StartsWith("/")) name = "/" + name;
        name = "~/scripts" + name;
        name = urlHelper.Content(name);

        js.Add(string.Format(jsIncludeTag, name));
      }

      return string.Join(Environment.NewLine, js.ToArray());
    }

    private static string cssIncludeTag = "<link href=\"{0}\" type=\"text/css\" rel=\"stylesheet\" />";
    public static string StyleSheetLinkTag(this HtmlHelper html, params string[] cssFiles) {
      var css = new List<string>();
      var urlHelper = new UrlHelper(html.ViewContext.RequestContext);

      foreach (var cssFile in cssFiles) {
        var name = cssFile.ToLower();
        if (!name.EndsWith(".css")) name += ".css";
        if (!name.StartsWith("/")) name = "/" + name;
        name = "~/content" + name;
        name = urlHelper.Content(name);

        css.Add(string.Format(cssIncludeTag, name));
      }

      return string.Join(Environment.NewLine, css.ToArray());
    }

    public static string Cycle(this HtmlHelper html, params string[] items) {
      string key = string.Format("cycle_index_{0}", string.Join("_", items));
      var context = html.ViewContext.HttpContext;
      int index = Convert.ToInt32(context.Items[key]);
      context.Items[key] = index + 1;
      return items[index % items.Length];
    }
  }
}
