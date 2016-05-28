using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceArbolAcceso;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;

namespace KiiniHelp.General
{
    public partial class FrmIncidentes : Page
    {
        readonly ServiceArbolAccesoClient _servicioArbolAcceso = new ServiceArbolAccesoClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack && Session["UserData"] != null)
                {
                    List<ArbolAcceso> lstArboles = _servicioArbolAcceso.ObtenerArblodesAccesoByGruposUsuario(((Usuario)Session["UserData"]).Id, (int)BusinessVariables.EnumTipoArbol.Incidentes).Distinct().ToList();
                    foreach (ArbolAcceso arbol in lstArboles.OrderBy(o => o.IdNivel1).ThenBy(o => o.IdNivel2).ThenBy(o => o.IdNivel3).ThenBy(o => o.IdNivel4).ThenBy(o => o.IdNivel5).ThenBy(o => o.IdNivel6).ThenBy(o => o.IdNivel7).Distinct())
                    {
                        TreeNode nivel1 = new TreeNode(arbol.Nivel1.Descripcion, arbol.Id.ToString());
                        if (arbol.Nivel2 != null)
                        {
                            TreeNode nivel2 = new TreeNode(arbol.Nivel2.Descripcion, arbol.Id.ToString());
                            nivel1.ChildNodes.Add(nivel2);
                            if (arbol.Nivel3 != null)
                            {
                                TreeNode nivel3 = new TreeNode(arbol.Nivel3.Descripcion, arbol.Id.ToString());
                                nivel2.ChildNodes.Add(nivel3);
                                if (arbol.Nivel4 != null)
                                {
                                    TreeNode nivel4 = new TreeNode(arbol.Nivel4.Descripcion, arbol.Id.ToString());
                                    nivel3.ChildNodes.Add(nivel4);
                                    if (arbol.Nivel5 != null)
                                    {
                                        TreeNode nivel5 = new TreeNode(arbol.Nivel5.Descripcion, arbol.Id.ToString());
                                        nivel4.ChildNodes.Add(nivel5);
                                        if (arbol.Nivel6 != null)
                                        {
                                            TreeNode nivel6 = new TreeNode(arbol.Nivel6.Descripcion, arbol.Id.ToString());
                                            nivel5.ChildNodes.Add(nivel6);
                                            if (arbol.Nivel7 != null)
                                            {
                                                TreeNode nivel7 = new TreeNode(arbol.Nivel7.Descripcion, arbol.Id.ToString());
                                                nivel6.ChildNodes.Add(nivel7);
                                                tvArbol.Nodes.Add(nivel1);
                                            }
                                        }
                                    }
                                }
                            }
                            
                        }
                        tvArbol.Nodes.Add(nivel1);


                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}