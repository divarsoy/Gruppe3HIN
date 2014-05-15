<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.Master" CodeBehind="AdministrasjonAvOppgave.aspx.cs" Inherits="SysUt14Gr03.AdministrasjonAvOppgave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class ="container">
    
    
    <h2>Administrering av Oppgave</h2>
    <br />
    <label>Oppgaver</label>
    <asp:DropDownList ID="ddlOppgaverIProsjekt" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOppgaverIProsjekt_SelectedIndexChanged" Width="300px"></asp:DropDownList>
    <br />
    <div class="form-group">
        <div class="row">
            <div style="display: inline-block" class="col-md-4">
                <label>Oppgave ID</label>
                <asp:TextBox ID="tbID" CssClass="form-control" runat="server" Width="300px"></asp:TextBox>
                <label>Tittel</label>
                <asp:TextBox ID="tbTittel" CssClass="form-control" runat="server" Width="300px"></asp:TextBox>
                <label>Beskrivelse av oppgave</label>
                <asp:TextBox ID="tbBeskrivelse" CssClass="form-control" runat="server" Height="100px" Width="300px" Wrap="true" TextMode="MultiLine"></asp:TextBox>
                <label>Krav</label>
                <asp:TextBox ID="tbKrav" CssClass="form-control" runat="server" Width="300px"></asp:TextBox>
                <label>Estimering</label>
                <asp:TextBox ID="TbEstimering" CssClass="form-control" runat="server" Width="300px"></asp:TextBox>
                <label>Brukt tid</label>
                <asp:TextBox ID="tbBruktTid" CssClass="form-control" runat="server" OnTextChanged="tbBruktTid_TextChanged" Width="300px"></asp:TextBox>
                <label>Gjenstående tid</label>
                <asp:TextBox ID="tbRemainingTime" CssClass="form-control" runat="server" Width="300px"></asp:TextBox>
                <label>Aktiv</label>
                <asp:CheckBox ID="cbAktiv" runat="server"/>

                <br /><br /><br />

                <label>Status</label><br />
                <asp:DropDownList ID="ddlStatus" style="display: inline-block" CssClass="form-control" runat="server" Width="300px"></asp:DropDownList>
                <br />
                <label>Prioritet</label>
                <asp:DropDownList ID="ddlPrioritet" style="display: inline-block" CssClass="form-control" runat="server" Width="300px"></asp:DropDownList>
                <label>Prosjekt</label>
                <asp:DropDownList ID="ddlProsjekt" style="display: inline-block" CssClass="form-control" runat="server" Width="300px"></asp:DropDownList>
            </div>

            <div class="col-md-4 col-md-offset-1">
                <asp:Calendar Width="300px" ID="cal" BorderColor="Blue" ShowGridLines="true" NextMonthText="Neste" PrevMonthText="Forrige" TodayDayStyle-BackColor="Blue" TodayDayStyle-ForeColor="White" WeekendDayStyle-ForeColor="Red" runat="server" ></asp:Calendar>
                <br />
                <label>Startdato</label>
                <asp:TextBox ID="tbTidsfristStart" CssClass="form-control" runat="server" ReadOnly="true" Width="300px"></asp:TextBox>
                <label>Sluttdato</label>
                <asp:TextBox ID="tbTidsfristSlutt" CssClass="form-control" runat="server" Width="300px" ></asp:TextBox>
                <asp:CompareValidator id="CompareValidator6" runat="server" Display="Dynamic" Type="Date" Operator="DataTypeCheck" ControlToValidate="tbTidsfristSlutt"
                    ErrorMessage="<br />Du må taste inn en gyldig dato" CssClass="feilMelding"></asp:CompareValidator>
                <br />
                <asp:Button ID="btnSlutt" CssClass="btn btn-primary" Text="Sett Sluttdato" OnClick="btnSlutt_Click" runat="server" Width="112px" Height="30px" />

                <br /><br /><br />

                <label>Tilgjengelige Brukere</label>
                <asp:DropDownList ID="ddlBrukere" CssClass="form-control" runat="server" Width="300px"></asp:DropDownList>
                <asp:ListBox ID="lbBrukere" CssClass="form-control" runat="server" Height="100px" Width="300px" Enabled="True"></asp:ListBox>
                <br />
                <asp:Button ID="btnLeggTilBrukere" CssClass="btn btn-primary" Text="Legg Til Brukere" runat="server" OnClick="btnLeggTilBrukere_Click" />
                <asp:Button ID="btnSlettBrukere" CssClass="btn btn-warning" Text="Ta bort Bruker" runat="server" OnClick="btnSlettBrukere_Click" />
            </div>
        </div>
    </div>
    <br />
    <asp:Button ID="btnEndre" runat="server" OnClick="btnEndre_Click" Text="Endre Oppgave" CssClass="btn btn-primary"/>


</div>
</asp:Content>