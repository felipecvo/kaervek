namespace Kaervek {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Paginator {
        public int Total { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages {
            get {
                var pages = Total / ItemsPerPage;
                if (Total % ItemsPerPage > 0) {
                    pages++;
                }
                return pages;
            }
        }
        public string ToHtml(string url) {
            var builder = new StringBuilder("<div class=\"paging\"><ul>");

            if (CurrentPage - 1 > 0) {
                if (CurrentPage == 2) {
                    builder.AppendFormat("<li><a href=\"{0}\">&lt; anterior</a></li>", url);
                } else {
                    builder.AppendFormat("<li><a href=\"{0}?page={1}\">&lt; anterior</a></li>", url, CurrentPage - 1);
                }
            } else {
                builder.Append("<li class=\"nolink\">&lt; anterior</li>");
            }

            for (int i = 1; i <= TotalPages; i++) {
                if (i == CurrentPage) {
                    builder.AppendFormat("<li class=\"current\">{0}</li>", i);
                } else if (i == 1) {
                    builder.AppendFormat("<li><a href=\"{0}\">{1}</a></li>", url);
                } else {
                    builder.AppendFormat("<li><a href=\"{0}?page={1}\">{1}</a></li>", url, i);
                }
            }

            if (CurrentPage + 1 <= TotalPages) {
                builder.AppendFormat("<li><a href=\"{0}?page={1}\">próximo &gt;</a></li>", url, CurrentPage + 1);
            } else {
                builder.Append("<li class=\"nolink\">próximo &gt;</li>");
            }

            builder.Append("</ul></div>");

            return builder.ToString();
        }
    }
}
