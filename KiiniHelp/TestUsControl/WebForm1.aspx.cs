using System;
using System.Web.UI;

namespace KiiniHelp.TestUsControl
{
    public partial class WebForm1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //(Usuario)Session["UserData"]
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}