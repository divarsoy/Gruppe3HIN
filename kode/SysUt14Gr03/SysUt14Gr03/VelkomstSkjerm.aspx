<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VelkomstSkjerm.aspx.cs" Inherits="SysUt14Gr03.VelkomstSkjerm" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="MainContent">
   
    <p>

    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Søk etter medlemmer i din gruppe:&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

    </p>
    <p>
        &nbsp;</p>
            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="1030px">
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Top" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                <TodayDayStyle BackColor="#CCCCCC"/>
            </asp:Calendar>
    <div>
        <asp:Panel ID="Panel1" runat="server" Height="537px" style="text-align: center">

             <asp:Panel ID="Meldinger" runat="server" Height="200px" Width="327px">
                 <asp:Button ID="Button1" runat="server" Text="Button" />
             </asp:Panel>
            <asp:Panel ID="Notifikasjon" runat="server" Height="200px" Width="530px" style="text-align: center">
                 <asp:Button ID="Button2" runat="server" Text="Button" />
             </asp:Panel>
            <asp:Panel ID="Oppgaver" runat="server" Height="200px" Width="439px">
                 <asp:Button ID="Button3" runat="server" Text="Button" />
             </asp:Panel>
        </asp:Panel>
    </div>
    
 
</asp:Content>


