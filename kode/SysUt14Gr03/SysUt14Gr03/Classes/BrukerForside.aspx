<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BrukerForside.aspx.cs" Inherits="SysUt14Gr03.BrukerForside.aspx" %>


<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>


<!DOCTYPE html>


<asp:Content ID="Content1" runat="server" contentplaceholderid="MainContent">
    <p>
        Velkommen! :-)</p>



    <asp:Panel ID="Panel1" runat="server" Height="287px" Width="858px">
        <asp:Button ID="Button1" runat="server" Text="Ny" />
        <asp:Button ID="Button2" runat="server" Text="Button" />
        <asp:Button ID="Button3" runat="server" Text="Button" />
        <asp:Button ID="Button4" runat="server" Text="Button" />
        <asp:Chart ID="Chart1" runat="server">
            <series>
                <asp:Series Name="Series1">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
        </asp:Chart>

    </asp:Panel>



</asp:Content>