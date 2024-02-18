<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pass.aspx.cs" Inherits="WebPhim.Pass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="text" runat="server" id="txtPass" placeholder="Nhập password" />
            <input type="button" runat="server" id="btnPass" value="Login" onserverclick="btnPass_Click" />
        </div>
    </form>
</body>
</html>
