using System;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using KinniNet.Business.Utils;
using Microsoft.Ajax.Utilities;

namespace KiiniHelp.Funciones
{
    public static class Metodos
    {
        public static class Strings
        {
            public static string CastToEnum(string input)
            {
                if (String.IsNullOrEmpty(input))
                    throw new ArgumentException("Cadena vacia");
                string[] cadena = input.Trim().Split(' ');
                input = string.Empty;
                foreach (string word  in cadena)
                {
                    input += word.First().ToString().ToUpper() + word.Trim().Substring(1);
                }
                
                return input;
            }
        }

        public static class Enumeradores
        {
            public static T GetStringEnum<T>(string cadena)
            {
                return (T) Enum.Parse(typeof (T), Strings.CastToEnum(cadena.ToLower()));
            }
            public static T GetValueEnumFromString<T>(string cadena)
            {
                return (T)Enum.Parse(typeof(T), Strings.CastToEnum(cadena.ToLower()));
            }
        }
        public static void LimpiarCombo(DropDownList ddl)
        {
            try
            {
                ddl.Items.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void LimpiarRadioList(RadioButtonList rbtnlst)
        {
            try
            {
                rbtnlst.Items.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void LlenaComboCatalogo(DropDownList ddl, object datos)
        {
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = datos;
                ddl.DataTextField = "Descripcion";
                ddl.DataValueField = "Id";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static void LlenaListBoxCatalogo(CheckBoxList chkLbx, object datos)
        {
            try
            {
                if (chkLbx.DataSource != null)
                {
                    chkLbx.DataSource = null;
                    chkLbx.DataBind();
                }
                chkLbx.DataSource = datos;
                chkLbx.DataTextField = "Descripcion";
                chkLbx.DataValueField = "Id";
                chkLbx.DataBind();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static void LlenaListBoxCatalogo(RadioButtonList rbtnLbx, object datos)
        {
            try
            {
                if (rbtnLbx.DataSource != null)
                {
                    rbtnLbx.DataSource = null;
                    rbtnLbx.DataBind();
                }
                rbtnLbx.DataSource = datos;
                rbtnLbx.DataTextField = "Descripcion";
                rbtnLbx.DataValueField = "Id";
                rbtnLbx.DataBind();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static void FiltraCombo(DropDownList ddlFiltro, DropDownList ddlLlenar, object source)
        {
            try
            {
                ddlLlenar.Items.Clear();
                if (ddlFiltro.SelectedValue != BusinessVariables.ComboBoxCatalogo.Value.ToString())
                {
                    ddlLlenar.Enabled = true;
                    LlenaComboCatalogo(ddlLlenar, source);
                }
                else
                {
                    ddlLlenar.DataSource = null;
                    ddlLlenar.DataBind();
                }

                ddlLlenar.Enabled = ddlLlenar.DataSource != null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool ValidaCapturaCatalogoCampus(int idTipoUsuario, string descripcion, int idColonia, string calle, string noExt, string noInt)
        {
            StringBuilder sb = new StringBuilder();
            if (idTipoUsuario == BusinessVariables.ComboBoxCatalogo.Value)
                sb.AppendLine("Tipo de usuario es un campo obligatorio.<br>");
            if (descripcion == string.Empty)
                sb.AppendLine("Descripción es un campo obligatorio.<br>");
            if (idColonia == BusinessVariables.ComboBoxCatalogo.Value)
                sb.AppendLine("Colonia es un campo obligatorio.<br>");
            if (calle == string.Empty)
                sb.AppendLine("Calle es un campo obligatorio.<br>");
            if (noExt == string.Empty)
                sb.AppendLine("Número Exterior es un campo obligatorio.<br>");
            //if (noInt == string.Empty)
            //    sb.AppendLine("Número Interior es un campo obligatorio.<br>");
            if (sb.ToString() != string.Empty)
                throw new Exception(sb.ToString());
            return true;
        }

        public static bool ValidaCapturaCatalogo(string descripcion)
        {
            StringBuilder sb = new StringBuilder();
            if (descripcion == string.Empty)
                sb.AppendLine("Descripción es un campo obligatorio.<br>");
            if (sb.ToString() != string.Empty)
                throw new Exception(sb.ToString());
            return true;
        }

        public static bool HashPermission()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }
    }
}