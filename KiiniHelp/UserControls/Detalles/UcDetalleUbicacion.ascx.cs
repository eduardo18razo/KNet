using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.ServiceUbicacion;
using KiiniNet.Entities.Cat.Operacion;

namespace KiiniHelp.UserControls.Detalles
{
    public partial class UcDetalleUbicacion : UserControl
    {
        private List<string> _lstError = new List<string>();

        public int IdUbicacion
        {
            set
            {
                using (Ubicacion ub = new ServiceUbicacionClient().ObtenerUbicacionUsuario(value))
                {
                    if (ub == null) return;
                    if (ub.Pais != null)
                        lblPais.Text = ub.Pais.Descripcion;
                    if (ub.Campus != null)
                        lblCampus.Text = ub.Campus.Descripcion;
                    if (ub.Torre != null)
                        lblTorre.Text = ub.Torre.Descripcion;
                    if (ub.Piso != null)
                        lblPiso.Text = ub.Piso.Descripcion;
                    if (ub.Zona != null)
                        lblZona.Text = ub.Zona.Descripcion;
                    if (ub.SubZona != null)
                        lblSubZona.Text = ub.SubZona.Descripcion;
                    if (ub.SiteRack != null)
                        lblsite.Text = ub.SiteRack.Descripcion;
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}