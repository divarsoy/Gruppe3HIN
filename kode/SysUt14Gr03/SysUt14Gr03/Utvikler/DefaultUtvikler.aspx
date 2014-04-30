<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="DefaultUtvikler.aspx.cs" Inherits="SysUt14Gr03.BrukerForside" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
      <div class="container">
        <span class="glyphicon glyphicon-pencil"></span>  
        <h1>Utviklersiden</h1>
        <p>Start med å velge et prosjekt nedenfor. Deretter kan du velge oppgaver fra menyen ovenfor.</p>
      </div>
    </div>

    <div class="container">
      <div class="row">
        <div class="col-md-4">
          <h2>Velg Prosjekt</h2>
          <asp:ListBox ID="ListBoxProsjekt" runat="server" Rows="1">
                <asp:ListItem Selected = "True" Text = "Velg Prosjekt" Value = "0"></asp:ListItem>
          </asp:ListBox>
          <asp:Button ID="btnVelgProsjekt" runat="server" Text="Velg Prosjekt" OnClick="btnVelgProsjekt_Click" />
        </div>
        <div class="col-md-4">
          <h2>Valgt prosjekt</h2>
            <asp:Label ID="lblValgtProsjekt" runat="server" Text=""></asp:Label> 
        </div>
        <div class="col-md-4">
          <h2>Kalender</h2>
          <asp:Calendar ID="Calendar2" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px">
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
            <TodayDayStyle BackColor="#CCCCCC" />
            </asp:Calendar>
        </div>
      </div>
    </div>
</asp:Content>
