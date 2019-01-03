<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forms.aspx.cs" Inherits="Control.Net.demo.Forms" %>
<%@ Register Assembly="Control.Web" Namespace="Control.Web" TagPrefix="Joint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>表单控件测试</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="renderer" content="webkit|ie-comp|ie-stand" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <link type="text/css" rel="stylesheet" href="/layui/css/layui.css" />
    <script type="text/javascript" src="/common/lib/jquery-1.12.3.min.js"></script>
    <script type="text/javascript" src="/common/lib/layer.min.js"></script>
    <script type="text/javascript" src="/common/js/Joint-Common.js"></script>
</head>
<body>
    <form id="form1" class="layui-form" onsubmit="return false;" onkeydown="if(event.keyCode==13)return false;" runat="server">
        <table>
            <tr>
                <td><Joint:JointTextField ID="c_Name" IsRequired="true" runat="server" LabelText="姓名" /></td>
                <td><Joint:JointTextField ID="c_UserName" IsRequired="true" runat="server" LabelText="用户名" /></td>
            </tr>
            <tr>
                <td><Joint:JointTextField ID="c_Pwd" IsRequired="true" runat="server" LabelText="密码" PassWord="true" /></td>
                <td><Joint:JointCombobox ID="c_Sel" runat="server" IsQuickFilter="false" LabelText="民族" ValueList="1[#]1|汉族^2|满族^3|回族" /></td>
            </tr>
            <tr>
                <td><Joint:JointCheckBox ID="c_Sex" Checked="true" runat="server" LabelText="性别1" ShowText="男士|女士" /> </td>
                <td><Joint:JointCheckBox ID="c_Sex2" Checked="true" runat="server" Skins="checkbox" LabelText="男士" ShowText="" /> </td>
            </tr>
            <tr>
                <td><Joint:JointDateField ID="c_Date" runat="server" LabelText="日期" /></td>
                <td><Joint:JointNumericText ID="c_Number" runat="server" LabelText="数字" ShowSpinner="true" IsFloat="false" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <Joint:JointTextArea ID="c_Area" Height="70px" Width="690px" IsRequired="true" runat="server" LabelText="多行文本" />
                </td>
            </tr>
            <tr>
                <td colspan="2"><Joint:JointCheckBoxGroup ID="c_CheckG1" Width="690px" DefaultValue="1,3,5" ColumnsNumber="6" runat="server" LabelText="民族" ValueList="1[#]1|汉族^2|满族^3|回族^4|金族^5|木族^6|土族" /></td>
            </tr>
            <tr>
                <td colspan="2"><Joint:JointCheckBoxGroup ID="c_CheckG2" Width="690px" DefaultValue="2,4,6" Skins="switch" ColumnsNumber="6" runat="server" LabelText="民族" ValueList="1[#]1|汉族^2|满族^3|回族^4|金族^5|木族^6|土族" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
