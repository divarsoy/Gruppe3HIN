﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="DefaultUtvikler.aspx.cs" Inherits="SysUt14Gr03.BrukerForside" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="col-md-12">
            <h1>Forside for Brukere</h1>
        </div>
    <div class="col-md-4">
        <h3>Velkommen <%: Fornavn %>!</h3>
        <h4>Velg Prosjekt:</h4>
        <asp:ListBox ID="ListBoxProsjekt" runat="server" Rows="1"></asp:ListBox>
        <asp:Button ID="btnVelgProsjekt" runat="server" Text="Velg Prosjekt" OnClick="btnVelgProsjekt_Click" />        
    </div>
    <div class="col-md-4"></div>
    <div class="kolonner col-md-4">
        <p><asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px">
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
            <TodayDayStyle BackColor="#CCCCCC" />
            </asp:Calendar>
        </p>
    </div>
    <div class="kolonner col-md-3">
        <p>
            <asp:Image ID="Image1" runat="server" Height="197px" ImageUrl="~/Pictures/blomst.jpg" Width="284px" />
        </p>
    </div>
    <div class="kolonner col-md-3">
        <p>Navn: Ola Per</p>
        <p>Rolle: ProsjektLeder</p>
        <p>Epost: sysut14gr03@gmail.com</p>
    </div>
    <div class="kant col-md-6">
        <p class="redText">Du har 1 ny melding</p>
        <p>Emne: velkommen</p>
        <p>Melding: Velkommen som ny bruker!</p>
    </div>
    <div class="kolonner col-md-10">
        <h1>Mer kommer etterhvert! :-)</h1>
        
    </div>
    <div class="kolonner col-md-2">
       
    </div>
    <p>...</p>
        </div>
</asp:Content>