<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.Master" AutoEventWireup="true" CodeBehind="OpprettProsjekt.aspx.cs" Inherits="SysUt14Gr03.OpprettProsjekt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
       <h2>Opprett Prosjekt</h2>
       <br />

        <div class="form-group">
            <label for="inputProsjektnavn" class="col-sm-1 control-label">Prosjektnavn</label>
            <div class="col-sm-3">
            <asp:TextBox ID="tbProsjektnavn" CssClass="form-control" runat="server" ></asp:TextBox>
                </div>
        </div>
        <div class="clearfix"></div>

        <div class="form-group">
            <label for="valgProsjektleder" class="col-sm-1 control-label">Prosjektleder</label>
            <div class="col-sm-3">
            <asp:DropDownList ID="ddlBrukere" CssClass="form-control" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Selected = "True" Text = "Velg Prosjektleder" Value = "0"></asp:ListItem>
            </asp:DropDownList>
                </div>
        </div>
        <div class="clearfix"></div>
   
        <div class="form-group">
            <label for="valgTeam" class="col-sm-1 control-label">Team</label>
            <div class="col-sm-3">
            <asp:DropDownList ID="dropTeam" CssClass="form-control" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Selected = "True" Text = "Velg Team" Value = "0"></asp:ListItem>
            </asp:DropDownList>
                </div>
        </div>
        <div class="clearfix"></div>
        <br />
        <div class="form-group">
        <div class="col-sm-5">
        <asp:Calendar ID="cal" Width="400px" BorderColor="Blue" ShowGridLines="true" NextMonthText="Neste" PrevMonthText="Forrige" TodayDayStyle-BackColor="Blue" TodayDayStyle-ForeColor="White" WeekendDayStyle-ForeColor="Red" runat="server"  ></asp:Calendar>
            </div>
            </div>
         <div class="clearfix"></div>
        
        <br />
        
        <div class="form-group">
            <label for="inputStartdato" class="col-sm-1 control-label">Startdato</label>
            <div class="col-sm-2">
            <asp:TextBox ID="tbStart" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
            <asp:Button ID="btnStart" CssClass="btn btn-primary" Text="Sett Startdato" OnClick="btnStart_Click" runat="server"  />
                </div>
        </div>
         <div class="clearfix"></div>
        <br />
        <div class="form-group">
            <label for="inputSluttdato" class="col-sm-1 control-label">Sluttdato</label>
            <div class="col-sm-2">
            <asp:TextBox ID="tbSlutt" CssClass="form-control" runat="server" ></asp:TextBox>
            </div>
            <div class="col-sm-1">
            <asp:Button ID="btnSlutt" CssClass="btn btn-primary" Text="Sett Sluttdato" OnClick="btnSlutt_Click" runat="server"/>
                </div>
               
        </div>
         <div class="clearfix"></div>
        <br />
        
    <asp:Button ID="btnLagre" CssClass="btn btn-primary" runat="server" Text="Lagre Prosjekt" OnClick="btnLagre_Click" Width="300px" />
        <br /><br />
   <p> <asp:Label ID="lblFeil" class="control-label" runat="server" Visible="False"></asp:Label></p>

    </div>
</asp:Content>
