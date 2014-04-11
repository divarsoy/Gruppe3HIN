<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="ManuellTimeregistrering.aspx.cs" Inherits="SysUt14Gr03.ManuellTimeregistrering" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
      <asp:Label ID="lblTittel" runat="server"></asp:Label>
    </h1>
    <asp:Label ID="Label1" runat="server" Text="Registrer timer for..." Visible="false"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlDag" runat="server" Visible="false" CssClass="dropdown-header"></asp:DropDownList>

    <br />
    <asp:Label ID="Label2" runat="server" Text="Starttidspunkt:" Visible="false"></asp:Label>
    <asp:TextBox ID="txtStart" runat="server" TextMode="Time" Visible="false" CssClass="form-control-static"></asp:TextBox>
    <asp:Label ID="Label3" runat="server" Text="Sluttidspunkt:" Visible="false"></asp:Label>
    <asp:TextBox ID="txtSlutt" runat="server" TextMode="Time" Visible="false"></asp:TextBox>
    <br />
    <asp:Button ID="btnAddPause" runat="server" CssClass="btn btn-primary" Text="Legg til pause" OnClick="btnAddPause_Click" Visible="false"/>
    <br />
    <asp:Panel ID="pnlPauser" runat="server">
    </asp:Panel>
    <script type = "text/javascript">
        function Confirm() {

            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Lagre timer?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <br />
    <asp:HiddenField ID="lagreTime" runat="server" value="" />
    <asp:HiddenField ID="infoField" runat="server" value="" />
    <asp:Button ID="btnLagre" runat="server" CssClass="btn btn-success" Text="Lagre timer" Visible="false" OnClick="btnLagre_Click" />

    <asp:Label ID="lblTest" runat="server" Visible="false"></asp:Label>


    </asp:Content>
