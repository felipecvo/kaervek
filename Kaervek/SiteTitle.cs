namespace Kaervek {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Web.UI;
  using System.Web;

  public class SiteTitle : Control, INamingContainer {
    public string CssClass { get; set; }

    protected override void Render(HtmlTextWriter writer) {
      var tagName = HttpContext.Current.Request.Path == ResolveUrl("~/") ? HtmlTextWriterTag.H1 : HtmlTextWriterTag.H2;
      if (!string.IsNullOrEmpty(CssClass)) writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);
      writer.RenderBeginTag(tagName);
      this.RenderChildren(writer);
      writer.RenderEndTag();
    }
  }
}
