<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebPhim.Home" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="./Style.css">
    <title>Xem phim - Nghe nhạc - ...</title>
</head>
<body>
    <form runat="server">
        <div id="header">
            <h1 id="name">KienDev2003</h1>
            <div id="radContent">
                <li style="display: inline-block; font-size: 30px;">
                    <label>
                        <asp:RadioButton ID="radPhimChieuRap" runat="server" OnCheckedChanged="radPhimChieuRap_Click" AutoPostBack="true" />
                        Phim Chiếu Rạp
                    </label>
                </li>
                <li style="display: inline-block; font-size: 30px;">
                    <label>
                        <asp:RadioButton ID="radPhim18" runat="server" OnCheckedChanged="radPhim18_Click" AutoPostBack="true"/>
                        Phim Giải Trí
                    </label>
                </li>
            </div>
            <div id="formTimKiem">
                <input style="font-size: 20px;" type="text" id="txtTk" placeholder="Nhập tên phim" runat="server">
                <input style="font-size: 15px;" type="button" id="btnTk" value="Tìm kiếm" runat="server" onserverclick="btnTimKiem_Click">
            </div>
        </div>
        <div>
            <ul style="float:left" class="list-movies" id="ul_list_phim" runat="server">
                
            </ul>
        </div>
        <div id="footer" style="width: 60%; margin: auto;">
            <center>
                <a href="https://www.facebook.com/phamss.kienss.7" target="_blank">Copyright © 2023 | KienMobile</a>
                <a style="padding-left: 10px;" class="fa fa-phone" href="tel:+84982450165">Hotline: 0982.450.165</a>
            </center>
        </div>
    </form>
</body>
</html>
