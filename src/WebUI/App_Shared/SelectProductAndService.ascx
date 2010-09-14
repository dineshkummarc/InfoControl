﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectProductAndService.ascx.cs"
    Inherits="InfoControl.Web.UI.SelectProductAndService" %>
<table>
    <tr>
        <td>
            Nome do produto/Serviço:<br />
            <asp:TextBox ID="txtItem" runat="server" Width="250px" CssClass="cDynDat11" MaxLength="128"
                plugin="autocomplete"
                servicepath='/InfoControl/InfoControl/SearchService.svc/SearchProductAndService'
                options="{max: 10}"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valtxtItem" ControlToValidate="txtItem" runat="server"
                ErrorMessage="&nbsp;&nbsp;&nbsp;" Display="Dynamic"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;
            <p style="font-size: 7pt; color: gray">
                Dica: Digite parte do texto, que o completará automaticamente!</p>
        </td>
    </tr>
</table>
<input id="<%=this.ClientID %>" type="text" value="<%=ViewState["CustomerId"] %>"
    style="display: none" />

