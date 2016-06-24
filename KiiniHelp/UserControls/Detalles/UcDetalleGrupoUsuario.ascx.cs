using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceGrupoUsuario;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniHelp.UserControls.Detalles
{
    public partial class UcDetalleGrupoUsuario : UserControl
    {
        public int IdUsuario
        {
            set
            {
                rptUserGroups.DataSource = new ServiceGrupoUsuarioClient().ObtenerGruposDeUsuario(value);
                rptUserGroups.DataBind();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rptUserGroups_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                //if (((GrupoUsuario) e.Item.DataItem).SubGrupoUsuario.Count > 0)
                //{
                //    Repeater rpt = (Repeater) e.Item.FindControl("rptSubRol");
                //    rpt.DataSource = ((GrupoUsuario) e.Item.DataItem).SubGrupoUsuario;
                //    rpt.DataBind();
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}