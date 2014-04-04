<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="HistorikkStattestikk.aspx.cs" Inherits="SysUt14Gr03.HistorikkStattestikk" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="MainContent">
    <h1>Historikk/Statestikk</h1>

    <div class="kolonner col-md-8">
        <p>Her har du historikk/statestikk om deg selv fra din opprettelse av brukeren.(kan komme bedre forklaring)</p>

        <p><asp:Button ID="tempButton" runat="server" Text="Muligens bruke" /></p>
        
    </div>
    <div class="kolonner col-md-4">
        <p>
            <asp:Panel ID="Graph" runat="server">
                <a>skal komme en graf her for kor mye som han har gjort, ant timer eller ant oppgaver som er blitt ferdig eller begge.</a>
                <asp:Chart ID="MonhtlyGraph" runat="server">
                    <Series>
                        <asp:Series Name="Oppgaver gjort"></asp:Series>
                        <asp:Series Name="Tid brukt"></asp:Series>
                        <asp:Series Name="muligens komme mer?"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </asp:Panel>
        </p>
    </div>
    <div class="kolonner col-md-3">
        <p>
            <asp:Image ID="Image1" runat="server" Height="197px" ImageUrl="~/Pictures/profilbilde.jpg" Width="284px" />
            <asp:Button ID="btnUserInfo" runat="server" OnClick="btn_nav_changeUser_Click" Text="Endre bruker informasjon" />
        </p>
    </div>
    <div class="kolonner col-md-6">
        <h3>statestikk om deg selv</h3>
        <asp:ListView ID="lvStatistics" runat="server" SelectMethod="lvStatistics_GetData"></asp:ListView>
    </div>
    <div class="kant col-md-6">
        <h3>Historikk om deg selv</h3>
        <asp:ListView ID="lvHistory" runat="server" SelectMethod="lvHistory_GetData"></asp:ListView>
    </div>
    <div class="kolonner col-md-10">
        <h3>Din aktivitet</h3>
        <asp:ListView ID="lvActivity" runat="server" SelectMethod="lvActivity_GetData"></asp:ListView>
        <asp:ListBox ID="Project" runat="server" OnSelectedIndexChanged="Projects_SelectedIndexChanged" ItemType="SysUt14Gr03.Models.Prosjekt"></asp:ListBox>
        <asp:ListBox ID="Meeting" runat="server" OnSelectedIndexChanged="Meetings_SelectedIndexChanged" ItemType="SysUt14Gr03.Models.Moete"></asp:ListBox>
        <asp:ListBox ID="Team" runat="server" OnSelectedIndexChanged="Team_SelectedIndexChanged" ItemType="SysUt14Gr03.Models.Team"></asp:ListBox>
    </div>
    <div class="kolonner col-md-2">
       <a>muligheter for mer</a>
    </div>
</asp:Content>



