using System;
using System.Web.UI;

namespace KiiniHelp
{
    public partial class FrmEncuesta : Page
    {
        public int? IdTicket
        {
            get
            {
                int result = 0;
                if (hfIdTicket.Value != string.Empty)
                    result = Convert.ToInt32(hfIdTicket.Value);
                else
                    result = (int)Session["IdTicketTicket"];
                return result;
            }
            set
            {
                if (hfIdTicket != null)
                {
                    hfIdTicket.Value = value.ToString();
                    Session.Remove("IdTicketTicket");
                }
                else
                    Session["IdTicketTicket"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            UcEncuestaCaptura.OnCancelarModal += UcEncuestaCaptura_OnCancelarModal;
        }

        void UcEncuestaCaptura_OnCancelarModal()
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            IdTicket = Convert.ToInt32(Request.QueryString["IdTicket"]);

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
    }
}