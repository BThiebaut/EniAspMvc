using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace EniProjetMvc.Extensions
{
    public static class HtmlExtension
    {
        private static readonly string formGroup = "<div class=\"form-group\">";
        private static readonly string colMd10 = "<div class=\"col-md-10\">";
        private static readonly string endDiv = "</div>";

        public static MvcHtmlString WidgetFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var sb = new StringBuilder();

            sb.Append(formGroup);
            sb.Append(html.LabelFor(expression, htmlAttributes: new { @class = "control-label col-md-2" }));
            sb.Append(colMd10);
            sb.Append(html.EditorFor(expression, new { htmlAttributes = new { @class = "form-control" } }));
            sb.Append(html.ValidationMessageFor(expression, "", new { @class = "text-danger" }));
            sb.Append(endDiv);
            sb.Append(endDiv);

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString DateWidgetFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var sb = new StringBuilder();

            sb.Append(formGroup);
            sb.Append(html.LabelFor(expression, htmlAttributes: new { @class = "control-label col-md-2" }));
            sb.Append(colMd10);
            sb.Append(html.EditorFor(expression, new { htmlAttributes = new { @class = "form-control datepicker" } }));
            sb.Append(html.ValidationMessageFor(expression, "", new { @class = "text-danger" }));
            sb.Append(endDiv);
            sb.Append(endDiv);

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString TimeWidgetFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var sb = new StringBuilder();

            sb.Append(formGroup);
            sb.Append(html.LabelFor(expression, htmlAttributes: new { @class = "control-label col-md-2" }));
            sb.Append(colMd10);
            sb.Append(html.EditorFor(expression, new { htmlAttributes = new { @class = "form-control timepicker readonly"} }));
            sb.Append(endDiv);
            sb.Append(endDiv);

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString DropDownWidgetFor<TModel, TValueLabel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValueLabel>> expressionLabel, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList)
        {
            var sb = new StringBuilder();

            sb.Append(formGroup);
            sb.Append(html.LabelFor(expressionLabel, htmlAttributes: new { @class = "control-label col-md-2" }));
            sb.Append(colMd10);
            sb.Append(html.DropDownListFor(expression, selectList));
            sb.Append(html.ValidationMessageFor(expression, "", new { @class = "text-danger" }));
            sb.Append(endDiv);
            sb.Append(endDiv);

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString SubmitWidget<TModel>(this HtmlHelper<TModel> html, string label, string cssButtonClass = "default")
        {
            var sb = new StringBuilder();

            sb.Append(formGroup);
            sb.Append("<div class=\"col-md-offset-2 col-md-10\">");
            sb.Append("<input type =\"submit\" value=\""+ label +"\" class=\"btn btn-"+ cssButtonClass +"\" />");
            sb.Append(endDiv);
            sb.Append(endDiv);
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString ModeWidgetLabels<TModel>(this HtmlHelper<TModel> html, bool mode)
        {
            var strLabel = mode ? "Ajouter" : "Modifier";
            return MvcHtmlString.Create(strLabel);
        }

    }
}
