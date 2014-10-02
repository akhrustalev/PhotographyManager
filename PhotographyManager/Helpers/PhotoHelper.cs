using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotographyManager.Web.Helpers
{
    public class PhotoHelper
    {
        public static HtmlString Radio(string name, bool isChecked,string text, string value)
        {
            return new HtmlString(text + "<br><input type='radio' value='" + value + "' name='" + name + "'" + (isChecked==true?"checked":"") + "><br>");
        }
    }
}