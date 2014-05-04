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
            <asp:DropDownList ID="dropTeam" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TeamDropDown_Change" AppendDataBoundItems="true">
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
            <asp:TextBox ID="tbStart" Placeholder="DD.MM.YYYY" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
            <asp:Button ID="btnStart" CssClass="btn btn-primary" Text="Sett Startdato" OnClick="btnDato_Click" runat="server"  />
                </div>
        </div>
         <div class="clearfix"></div>

        <div class="form-group">
            <label for="inputSluttdato" class="col-sm-1 control-label">Sluttdato</label>
            <div class="col-sm-2">
                <asp:TextBox ID="tbSlutt" Placeholder="DD.MM.YYYY" CssClass="form-control" runat="server" ></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:Button ID="btnSlutt" CssClass="btn btn-primary" Text="Sett Sluttdato" OnClick="btnDato_Click" runat="server"/>
            </div>          
        </div>
         <div class="clearfix"></div>
         <asp:CompareValidator id="CompareValidator1" runat="server" Display="Dynamic" Type="Date" Operator="DataTypeCheck" ControlToValidate="tbStart"
                ErrorMessage="Du må taste inn en gyldig dato" CssClass="clearfix feilMelding">
         </asp:CompareValidator>
         <asp:CompareValidator id="CompareValidator2" runat="server" Display="Dynamic" Type="Date" Operator="DataTypeCheck" ControlToValidate="tbSlutt"
                ErrorMessage="Du må taste inn en gyldig dato" CssClass="clearfix feilMelding">
         </asp:CompareValidator>
        <br />
        <h2>Faser</h2>
        <div class="clearfix"></div>
        <div class="form-group">
            <label for="faseNavn" class="col-sm-1 control-label">Fasenavn</label>
            <div class="col-sm-2">
                <asp:TextBox ID="tbFasenavn" CssClass="form-control" runat="server" ></asp:TextBox>
            </div>     
        </div>
        <div class="clearfix"></div>
        <div class="form-group">
            <label for="faseLeder" class="col-sm-1 control-label">Faseleder</label>
            <div class="col-sm-2">
                <asp:DropDownList ID="ddFaseLeder" AutoPostBack="true" runat="server" CssClass="form-control">
                    <asp:ListItem Selected = "True" Text = "Velg Faseleder" Value = "0"></asp:ListItem>
                </asp:DropDownList>
            </div>     
        </div>
         <div class="clearfix"></div>
        <div class="form-group">
            <label for="tbFaseStart" class="col-sm-1 control-label">Startdato</label>
            <div class="col-sm-2">
                <asp:TextBox ID="tbFaseStart" Placeholder="DD.MM.YYYY" CssClass="form-control" runat="server" ></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:Button ID="btnFaseStart" CssClass="btn btn-primary" Text="Sett Startdato" OnClick="btnDato_Click" runat="server"/>
            </div>          
        </div>
         <div class="clearfix"></div>
 
        <div class="form-group">
            <label for="tbFaseSlutt" class="col-sm-1 control-label">Sluttdato</label>
            <div class="col-sm-2">
                <asp:TextBox ID="tbFaseSlutt" Placeholder="DD.MM.YYYY" CssClass="form-control" runat="server" ></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:Button ID="btnFaseSlutt" CssClass="btn btn-primary" Text="Sett Sluttdato" OnClick="btnDato_Click" runat="server"/>
            </div>          
        </div>
         <div class="clearfix"></div>

         <asp:CompareValidator id="CompareValidator3" runat="server" Display="Dynamic" Type="Date" Operator="DataTypeCheck" ControlToValidate="tbFaseStart"
                ErrorMessage="Du må taste inn en gyldig dato" CssClass="feilMelding clearfix">
         </asp:CompareValidator>
         <asp:CompareValidator id="CompareValidator4" runat="server" Display="Dynamic" Type="Date" Operator="DataTypeCheck" ControlToValidate="tbFaseSlutt"
                ErrorMessage="Du må taste inn en gyldig dato" CssClass="feilMelding clearfix">
         </asp:CompareValidator>
        <br />
        </div>
        <asp:Label ID="lblFaseFeil" CssClass="feilMelding" runat="server" Text="Alle feltene under faser må fylles ut!" Visible="False"></asp:Label>
            <div class="clearfix"></div>
        <asp:Button ID="btnLeggTilFase" CssClass="btn btn-success" runat="server" Text="Legg til Fase" OnClick="btnLeggTilFase_Click" Width="300px" />
        <div class="clearfix"></div>
        <br />

        <asp:GridView ID="gvFaser" CssClass="table" DataKeyNames="bruker_id" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false"
            onRowDeleting="FaserOnRowDeleting" OnRowDataBound="FaserEditRowDataBound" OnRowEditing="FaserRowEditing" OnRowCancelingEdit="FaserRowCancelingEdit" OnRowUpdating="FaserRowUpdating">
                <Columns>
                    <asp:BoundField DataField="Fasenavn" HeaderText="Fasenavn" />
                    <asp:TemplateField HeaderText="Faseleder">
                        <ItemTemplate>
                            <asp:Label ID="lblFaseleder" runat="server" Text='<%#Bind("Faseleder")%>'></asp:label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddFaselederEdit" OnSelectedIndexChanged="ddFaselederEdit_SelectedIndexChanged" AutoPostBack="true" runat="server" ></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start">
                        <ItemTemplate>
                            <asp:Label ID="lblFaseStartEdit" runat="server" Text='<%#Bind("Start") %>'></asp:label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbFaseStartEdit" runat="server" Text='<%#Bind("Start") %>'></asp:TextBox>
                            <asp:CompareValidator id="CompareValidator5" runat="server" Display="Dynamic" Type="Date" Operator="DataTypeCheck" ControlToValidate="tbFaseStartEdit"
                                ErrorMessage="<br />Du må taste inn en gyldig dato" CssClass="feilMelding">
                            </asp:CompareValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Slutt">
                        <ItemTemplate>
                            <asp:Label ID="lblFaseSluttEdit" runat="server" Text='<%#Bind("Slutt") %>'></asp:label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="tbFaseSluttEdit" runat="server" Text='<%#Bind("Slutt") %>'></asp:TextBox>
                            <asp:CompareValidator id="CompareValidator6" runat="server" Display="Dynamic" Type="Date" Operator="DataTypeCheck" ControlToValidate="tbFaseSluttEdit"
                                ErrorMessage="<br />Du må taste inn en gyldig dato" CssClass="feilMelding">
                            </asp:CompareValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="True" ButtonType="Link" />
                    <asp:CommandField ShowDeleteButton="True" ButtonType="Link" />
                </Columns>
        </asp:GridView>


        <asp:Button ID="btnLagre" CssClass="btn btn-primary" runat="server" Text="Lagre Prosjekt" OnClick="btnLagre_Click" Width="300px" />
        <div class="clearfix"></div>
</asp:Content>
