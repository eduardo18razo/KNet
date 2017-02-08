<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AltaEncuesta.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.AltaEncuesta" %>
<asp:UpdatePanel ID="upEncuesta" runat="server">
    <ContentTemplate>
        <header id="panelAlerta" runat="server" visible="false">
            <div class="alert alert-danger">
                <div>
                    <div style="float: left">
                        <asp:Image runat="server" ImageUrl="~/Images/error.jpg" />
                    </div>
                    <div style="float: left">
                        <h3>Error</h3>
                    </div>
                    <div class="clearfix clear-fix" />
                </div>
                <hr />
                <asp:Repeater runat="server" ID="rptErrorGrupoUsuario">
                    <ItemTemplate>
                        <ul>
                            <li><%# Container.DataItem %></li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </header>
        <div class="panel panel-primary">
            <div class="panel-heading">
                Agregar Encuesta
            </div>
            <div class="panel-body">
                <asp:HiddenField runat="server" ID="dfIdGrupo" />
                <asp:HiddenField runat="server" ID="dfModalPadre" />
                <div class="form-group">
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Tipo de Encuesta</label>
                        <asp:DropDownList runat="server" ID="ddlTipoEncuesta" CssClass="DropSelect" OnSelectedIndexChanged="ddlTipoEncuesta_OnSelectedIndexChanged" AutoPostBack="True" />
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Nombre de la encuesta</label>
                        <asp:TextBox runat="server" ID="txtDescripcionEncuesta" placeholder="DESCRIPCION" CssClass="form-control obligatorio" />
                    </div>

                    <div class="form-group" style="display: none">
                        <asp:CheckBox runat="server" Text="Ponderacion" ID="chkPonderacion" />
                    </div>


                    <div class="panel panel-primary" runat="server">
                        <div class="panel-heading">
                            Preguntas
                        </div>
                        <div class="panel-body">
                            <asp:TextBox ID="txtIdPregunta" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                            <div class="form-inline">
                                <br />
                                <div class="form-inline">

                                    <asp:Label ID="Label4" runat="server" Text="Pregunta" class="col-sm-2 control-label izquierda"></asp:Label>
                                    <asp:TextBox ID="txtPregunta" runat="server" CssClass="form-control obligatorio"></asp:TextBox>
                                </div>
                                <div class="form-inline margen-arriba">
                                    <asp:Label ID="Label5" runat="server" Text="Ponderacion" class="col-sm-2 control-label izquierda"></asp:Label>
                                    <asp:TextBox ID="txtPonderacion" runat="server" CssClass="form-control obligatorio"></asp:TextBox>
                                </div>
                                <div class="form-inline margen-arriba">
                                    *La ponderación de las preguntas debe sumar 100
                                </div>
                            </div>
                            <asp:Button ID="btnAddPregunta" runat="server" CssClass="btn btn-success" Text="Agregar" OnClick="btnAddPregunta_OnClick" />
                            <br />
                            <br />

                            <asp:Repeater runat="server" ID="rptPreguntas">
                                <HeaderTemplate>
                                    <div class="row">
                                        <div class="col-xs-6 col-sm-3">Pregunta</div>
                                        <div class="col-xs-6 col-sm-3">Ponderacion</div>
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="row">
                                        <div class="margen-arriba">

                                            <div class="col-xs-6 col-md-3">
                                                <asp:Label runat="server" ID="lblPregunta" Text='<%# Eval("Pregunta") %>'></asp:Label>
                                            </div>
                                            <div class="col-xs-5 col-md-3">
                                                <asp:Label runat="server" ID="lblPonderacion"><%# Eval("Ponderacion") %></asp:Label>
                                            </div>
                                            <asp:LinkButton runat="server" Text="Editar" OnClick="OnClick" CommandArgument='<%# Eval("Id") %>'>

                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div class="row">
                                        <div class="col-xs-6 col-md-3">
                                            <asp:Label runat="server" ID="lblPregunta" Text="Total"></asp:Label>
                                        </div>
                                        <div class="col-xs-5 col-md-3">
                                            <asp:Label runat="server" ID="lblTotal"><%# Eval("Ponderacion") %></asp:Label>
                                        </div>
                                    </div>

                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-footer" style="text-align: center">
                <asp:Button ID="btnGuardarEncuesta" runat="server" CssClass="btn btn-lg btn-success" Text="Guardar" OnClick="btnGuardarEncuesta_OnClick" />
                <asp:Button ID="btnLimpiarEncuesta" runat="server" CssClass="btn btn-lg btn-danger" Text="Limpiar" OnClick="btnLimpiarEncuesta_OnClick" />
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-lg btn-danger" Text="Cancelar" OnClick="btnCancelar_OnClick" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
