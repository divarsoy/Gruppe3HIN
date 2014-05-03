<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="Fremdriftsdiagram.aspx.cs" Inherits="SysUt14Gr03.Fremdriftsdiagram" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Fremdriftsdiagram</h1>
    <asp:DropDownList ID="ddlFaser" runat="server" OnSelectedIndexChanged="ddlFaser_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <asp:Panel ID="Chart" runat="server"></asp:Panel>
    <asp:Chart ID="ChartHolder" runat="server" Width="800px" BackColor="LightGray" Height="350px">
        <Series>
            <asp:Series ChartType="Line" Name="Brukte tid" YValuesPerPoint="2"></asp:Series>
            <asp:Series ChartArea="ChartArea1" ChartType="Line" Name="Estimert tid" YValuesPerPoint="2"></asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
                <AxisY Title="Estimert tid - brukt tid"></AxisY>
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    <asp:Label ID="hei" runat="server"></asp:Label>
</asp:Content>

