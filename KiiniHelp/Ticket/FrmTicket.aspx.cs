using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KiiniHelp.Ticket
{
    public partial class FrmTicket : System.Web.UI.Page
    {
        public int IdMascara
        {
            get
            {
                int result = 0;
                if (hfIdMascara.Value != string.Empty)
                    result = Convert.ToInt32(hfIdMascara.Value);
                else
                    result = (int) Session["IDTicket"];
                return result;
            }
            set
            {
                if (hfIdMascara != null)
                    hfIdMascara.Value = value.ToString();
                else
                    Session["IDTicket"] = value;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["IDTicket"] = 1;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            int idMascara = 1;
            //int idMascara = Convert.ToInt32(Request.QueryString["MascaraId"]);
            IdMascara = idMascara;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idMascara = 1;
                //int idMascara = Convert.ToInt32(Request.QueryString["MascaraId"]);
                IdMascara = idMascara;
            }
        }
    }
}