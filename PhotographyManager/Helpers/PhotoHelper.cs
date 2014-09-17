using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotographyManager.Web.Helpers
{
    public class PhotoHelper
    {
        public static HtmlString Radio(string name, bool isChecked, string text)
        {
            return new HtmlString(text+"<br><input type='radio' name='" + name + "'" + (isChecked==true?"checked":"") + "><br>");
        }
    }
}