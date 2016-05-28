<%@ Page Title="" Language="C#" MasterPageFile="~/Usuarios.Master" AutoEventWireup="true" CodeBehind="FrmConsultas.aspx.cs" Inherits="KiiniHelp.General.FrmConsultas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        /*.rootNode {
            font-size: 18px;
            padding: 5px;
            color: #000;
            border: 5px;
            width: 100%;
            padding-left: 5px
        }

            .rootNode :hover {
                color: #FFF;
            }

        .treeNode {
            color: #000;
            font: 14px Arial, Sans-Serif;
            width: 100%;
            padding-left: 5px
        }

            .treeNode :hover {
                background-color: #0B0B3B;
                color: #FFF;
            }



        .leafNode {
            padding: 4px;
            color: #000;
            width: 100%;
            padding-left: 5px;
            padding-left: 5px
        }

            .leafNode :hover {
                background-color: #0B0B3B;
                color: #FFF;
            }

        .selectNode {
            font-weight: bold;
            background-color: #FFF;
            color: #000;
            width: 100%;
            padding-left: 15px;
        }

            .selectNode :hover {
                color: #000;
            }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TreeView runat="server" ID="tvArbol" Style="width: 350px" ExpandImageUrl="../Images/plus.png" CollapseImageUrl="../Images/minus.png" OnTreeNodeCheckChanged="tvArbol_OnTreeNodeCheckChanged" OnSelectedNodeChanged="tvArbol_OnSelectedNodeChanged">
        <LeafNodeStyle CssClass="leafNode" />
        <NodeStyle CssClass="treeNode" />
        <RootNodeStyle CssClass="rootNode" />
        <SelectedNodeStyle CssClass="selectNode" />
    </asp:TreeView>
</asp:Content>
