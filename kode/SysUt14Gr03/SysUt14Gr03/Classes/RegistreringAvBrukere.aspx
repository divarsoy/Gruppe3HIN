<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistreringAvBrukere.aspx.cs" Inherits="SysUt14Gr03.Classes.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 193px; width: 250px">
    <form id="form1" runat="server">
        <p>
            &nbsp;</p>
        <p>
            Etternavn<asp:TextBox ID="tb_reg_etternavn" runat="server" OnTextChanged="TextBox1_TextChanged" Width="170px"></asp:TextBox>
        </p>
        Fornavn<asp:TextBox ID="tb_reg_fornavn" runat="server" OnTextChanged="tb_reg_fornavn_TextChanged" Width="168px"></asp:TextBox>
        <p>
            Epost<asp:TextBox ID="tb_reg_epost" runat="server" OnTextChanged="tb_reg_epost_TextChanged" Width="168px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
        <p style="margin-left: 40px">
            <asp:Button ID="bt_adm_reg" runat="server" OnClick="Button1_Click" Text="Registrer Bruker" />
        </p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
