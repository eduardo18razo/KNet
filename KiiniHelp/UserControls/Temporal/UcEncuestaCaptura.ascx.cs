﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceEncuesta;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Operacion;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Temporal
{
    public partial class UcEncuestaCaptura : UserControl, IControllerModal
    {
        readonly ServiceEncuestaClient _servicioEncuesta = new ServiceEncuestaClient();
        private List<Control> _lstControles;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Control myControl = GetPostBackControl(Page);

            if ((myControl != null))
            {
                if ((myControl.ClientID == "btnAddTextBox"))
                {

                }
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _lstControles = new List<Control>();
            int? idEncuesta = ((FrmEncuesta)Page).IdTicket;
            if (idEncuesta != null) IdTicket = (int)idEncuesta;
            Encuesta encuesta = _servicioEncuesta.ObtenerEncuestaByIdTicket(IdTicket);
            if (encuesta != null)
            {
                lblDescripcionMascara.Text = encuesta.Descripcion;
                PintaControles(encuesta.EncuestaPregunta, encuesta.IdTipoEncuesta);
                Session["EncuestaActiva"] = encuesta;
            }
        }

        public static Control GetPostBackControl(Page thePage)
        {
            Control myControl = null;
            string ctrlName = thePage.Request.Params.Get("__EVENTTARGET");
            if (((ctrlName != null) & (ctrlName != string.Empty)))
            {
                myControl = thePage.FindControl(ctrlName);
            }
            else
            {
                foreach (string item in thePage.Request.Form)
                {
                    Control c = thePage.FindControl(item);
                    if (((c) is Button))
                    {
                        myControl = c;
                    }
                }

            }
            return myControl;
        }

        public int IdTicket
        {
            get { return Convert.ToInt32(hfIdEncuesta.Value); }
            set { hfIdEncuesta.Value = value.ToString(); }
        }

        public void ValidaMascaraCaptura()
        {
            try
            {
                Encuesta encuesta = (Encuesta)Session["EncuestaActiva"];
                switch (encuesta.IdTipoEncuesta)
                {
                    case (int)BusinessVariables.EnumTipoEncuesta.Logica:
                        break;
                    case (int)BusinessVariables.EnumTipoEncuesta.Calificacion:
                        foreach (EncuestaPregunta pregunta in encuesta.EncuestaPregunta)
                        {
                            HtmlGenericControl divControl = (HtmlGenericControl)divControles.FindControl("createDiv" + pregunta.Id);
                            if (divControl != null)
                            {
                                HtmlGenericControl divGrupo = (HtmlGenericControl)divControl.FindControl("createDivs" + pregunta.Id);
                                if (divGrupo != null)
                                {
                                    if (!divGrupo.Controls.Cast<Control>().Any(control => ((RadioButton)control).Checked))
                                    {
                                        throw new Exception("Debe contestar todas las pregunats");
                                    }
                                }
                            }
                        }
                        break;
                    case (int)BusinessVariables.EnumTipoEncuesta.OpcionMultiple:
                        break;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RespuestaEncuesta> ObtenerCapturaMascara()
        {
            List<RespuestaEncuesta> lstCamposCapturados;
            try
            {
                Encuesta encuesta = (Encuesta)Session["EncuestaActiva"];
                lstCamposCapturados = new List<RespuestaEncuesta>();
                switch (encuesta.IdTipoEncuesta)
                {
                    case (int)BusinessVariables.EnumTipoEncuesta.Logica:
                        foreach (EncuestaPregunta pregunta in encuesta.EncuestaPregunta)
                        {
                            HtmlGenericControl divControl = (HtmlGenericControl)divControles.FindControl("createDiv" + pregunta.Id);
                            if (divControl != null)
                            {
                                HtmlGenericControl divGrupo = (HtmlGenericControl)divControl.FindControl("createDivs" + pregunta.Id);
                                if (divGrupo != null)
                                {
                                    for (int i = 0; i < 2; i++)
                                    {
                                        RadioButton rbtn = (RadioButton)divGrupo.FindControl("rbtn" + pregunta.Id + i);
                                        if (rbtn.Checked)
                                        {
                                            lstCamposCapturados.Add(new RespuestaEncuesta
                                            {
                                                IdEncuesta = encuesta.Id,
                                                IdTicket = IdTicket,
                                                IdPregunta = pregunta.Id,
                                                ValorRespuesta = i == 0 ? 0 : 1,
                                                Ponderacion = i == 0 ? 0 : pregunta.Ponderacion

                                            });
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case (int)BusinessVariables.EnumTipoEncuesta.Calificacion:
                        foreach (EncuestaPregunta pregunta in encuesta.EncuestaPregunta)
                        {
                            HtmlGenericControl divControl = (HtmlGenericControl)divControles.FindControl("createDiv" + pregunta.Id);
                            if (divControl != null)
                            {
                                HtmlGenericControl divGrupo = (HtmlGenericControl)divControl.FindControl("createDivs" + pregunta.Id);
                                if (divGrupo != null)
                                {
                                    for (int i = 0; i < 10; i++)
                                    {
                                        RadioButton rbtn = (RadioButton)divGrupo.FindControl("rbtn" + pregunta.Id + i);
                                        if (rbtn.Checked)
                                        {
                                            lstCamposCapturados.Add(new RespuestaEncuesta
                                            {
                                                IdEncuesta = encuesta.Id,
                                                IdTicket = IdTicket,
                                                IdPregunta = pregunta.Id,
                                                ValorRespuesta = i,
                                                Ponderacion = ((i + 1) * 10),

                                            });
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case (int)BusinessVariables.EnumTipoEncuesta.OpcionMultiple:
                        foreach (EncuestaPregunta pregunta in encuesta.EncuestaPregunta)
                        {
                            HtmlGenericControl divControl = (HtmlGenericControl)divControles.FindControl("createDiv" + pregunta.Id);
                            if (divControl != null)
                            {
                                HtmlGenericControl divGrupo = (HtmlGenericControl)divControl.FindControl("createDivs" + pregunta.Id);
                                if (divGrupo != null)
                                {
                                    for (int i = 1; i < 6; i++)
                                    {
                                        RadioButton rbtn = (RadioButton)divGrupo.FindControl("rbtn" + pregunta.Id + i);

                                        if (rbtn.Checked)
                                        {
                                            lstCamposCapturados.Add(new RespuestaEncuesta
                                            {
                                                IdEncuesta = encuesta.Id,
                                                IdTicket = IdTicket,
                                                IdPregunta = pregunta.Id,
                                                ValorRespuesta = i,
                                                Ponderacion = (pregunta.Ponderacion / 5) * i

                                            });
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lstCamposCapturados;
        }

        public void PintaControles(List<EncuestaPregunta> lstControles, int tipoEncuesta)
        {
            try
            {
                switch (tipoEncuesta)
                {
                    case (int)BusinessVariables.EnumTipoEncuesta.Logica:
                        foreach (EncuestaPregunta pregunta in lstControles)
                        {
                            HtmlGenericControl createDiv = new HtmlGenericControl("DIV") { ID = "createDiv" + pregunta.Id };
                            createDiv.Attributes["class"] = "form-group";
                            Label lbl = new Label { Text = pregunta.Pregunta, CssClass = "control-label" };
                            createDiv.Controls.Add(lbl);
                            divControles.Controls.Add(createDiv);
                            createDiv = new HtmlGenericControl("DIV") { ID = "createDivs" + pregunta.Id };
                            createDiv.Attributes["class"] = "form-group";

                            RadioButton rbNo = new RadioButton();
                            rbNo.ID = "rbtn" + pregunta.Id + "0";
                            rbNo.Text = "No";
                            rbNo.GroupName = "EncuestaLogica" + pregunta.Id;
                            rbNo.Style.Add("padding", "10px");
                            createDiv.Controls.Add(rbNo);
                            RadioButton rbSi = new RadioButton();
                            rbSi.ID = "rbtn" + pregunta.Id + "1";
                            rbSi.Text = "SI";
                            rbSi.GroupName = "EncuestaLogica" + pregunta.Id;
                            rbSi.Style.Add("padding", "10px");
                            createDiv.Controls.Add(rbSi);

                            divControles.Controls.Add(createDiv);
                        }
                        break;
                    case (int)BusinessVariables.EnumTipoEncuesta.Calificacion:
                        foreach (EncuestaPregunta pregunta in lstControles)
                        {
                            HtmlGenericControl createDiv = new HtmlGenericControl("DIV") { ID = "createDiv" + pregunta.Id };
                            createDiv.Attributes["class"] = "form-group";
                            Label lbl = new Label { Text = pregunta.Pregunta, CssClass = "control-label" };
                            createDiv.Controls.Add(lbl);
                            divControles.Controls.Add(createDiv);
                            createDiv = new HtmlGenericControl("DIV") { ID = "createDivs" + pregunta.Id };
                            createDiv.Attributes["class"] = "form-group";
                            for (int i = 0; i < 10; i++)
                            {

                                RadioButton rb = new RadioButton();
                                rb.ID = "rbtn" + pregunta.Id + i;
                                rb.Text = i.ToString();
                                rb.GroupName = "EncuestaCalificacion" + pregunta.Id;
                                rb.Style.Add("padding", "10px");
                                createDiv.Controls.Add(rb);
                            }
                            divControles.Controls.Add(createDiv);
                        }
                        break;
                    case (int)BusinessVariables.EnumTipoEncuesta.OpcionMultiple:
                        foreach (EncuestaPregunta pregunta in lstControles)
                        {
                            HtmlGenericControl createDiv = new HtmlGenericControl("DIV") { ID = "createDiv" + pregunta.Id };
                            createDiv.Attributes["class"] = "form-group";
                            Label lbl = new Label { Text = pregunta.Pregunta, CssClass = "control-label" };
                            createDiv.Controls.Add(lbl);
                            divControles.Controls.Add(createDiv);
                            createDiv = new HtmlGenericControl("DIV") { ID = "createDivs" + pregunta.Id };
                            createDiv.Attributes["class"] = "form-group";

                            RadioButton rbtnMala = new RadioButton();
                            rbtnMala.ID = "rbtn" + pregunta.Id + "1";
                            rbtnMala.Text = "PESIMO";
                            rbtnMala.GroupName = "EncuestaMultiple" + pregunta.Id;
                            rbtnMala.Style.Add("padding", "10px");
                            createDiv.Controls.Add(rbtnMala);

                            RadioButton rbtnRegular = new RadioButton();
                            rbtnRegular.ID = "rbtn" + pregunta.Id + "2";
                            rbtnRegular.Text = "MALO";
                            rbtnRegular.GroupName = "EncuestaMultiple" + pregunta.Id;
                            rbtnRegular.Style.Add("padding", "10px");
                            createDiv.Controls.Add(rbtnRegular);

                            RadioButton rbtnBuena = new RadioButton();
                            rbtnBuena.ID = "rbtn" + pregunta.Id + "3";
                            rbtnBuena.Text = "REGULAR";
                            rbtnBuena.GroupName = "EncuestaLogica" + pregunta.Id;
                            rbtnBuena.Style.Add("padding", "10px");
                            createDiv.Controls.Add(rbtnBuena);

                            RadioButton rbtnMuyBuena = new RadioButton();
                            rbtnMuyBuena.ID = "rbtn" + pregunta.Id + "4";
                            rbtnMuyBuena.Text = "BUENO";
                            rbtnMuyBuena.GroupName = "EncuestaLogica" + pregunta.Id;
                            rbtnMuyBuena.Style.Add("padding", "10px");
                            createDiv.Controls.Add(rbtnMuyBuena);

                            RadioButton rbtnExelente = new RadioButton();
                            rbtnExelente.ID = "rbtn" + pregunta.Id + "5";
                            rbtnExelente.Text = "EXCELENTE";
                            rbtnExelente.GroupName = "EncuestaLogica" + pregunta.Id;
                            rbtnExelente.Style.Add("padding", "10px");
                            createDiv.Controls.Add(rbtnExelente);
                            divControles.Controls.Add(createDiv);
                        }
                        break;
                }

                upEncuestas.Update();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            try
            {
                ValidaMascaraCaptura();
                ObtenerCapturaMascara();
                _servicioEncuesta.ContestaEncuesta(ObtenerCapturaMascara());
                if (OnAceptarModal != null)
                    OnAceptarModal();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (OnCancelarModal != null)
                    OnCancelarModal();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
    }
}