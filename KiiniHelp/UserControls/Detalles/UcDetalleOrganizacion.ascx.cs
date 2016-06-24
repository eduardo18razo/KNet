using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceOrganizacion;
using KiiniNet.Entities.Cat.Operacion;

namespace KiiniHelp.UserControls.Detalles
{
    public partial class UcDetalleOrganizacion : System.Web.UI.UserControl
    {
        public int IdOrganizacion
        {
            set
            {
                using (Organizacion ub = new ServiceOrganizacionClient().ObtenerOrganizacionUsuario(value))
                {
                    if (ub == null) return;
                    if (ub.Holding != null)
                        lblPais.Text = ub.Holding.Descripcion;
                    if (ub.Compania != null)
                        lblCampus.Text = ub.Compania.Descripcion;
                    if (ub.Direccion != null)
                        lblTorre.Text = ub.Direccion.Descripcion;
                    if (ub.SubDireccion != null)
                        lblPiso.Text = ub.SubDireccion.Descripcion;
                    if (ub.Gerencia != null)
                        lblZona.Text = ub.Gerencia.Descripcion;
                    if (ub.SubGerencia != null)
                        lblSubZona.Text = ub.SubGerencia.Descripcion;
                    if (ub.Jefatura != null)
                        lblsite.Text = ub.Jefatura.Descripcion;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}