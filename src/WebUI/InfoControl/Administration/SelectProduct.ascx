﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="App_Shared_SelectProduct"
    CodeBehind="SelectProduct.ascx.cs" %>
<table>
    <tr>
        <td>
            Código de barras, código do produto ou parte do nome:<br />
            <asp:TextBox ID="txtProduct" runat="server" Width="250px" CssClass="cDynDat11"
                plugin="autocomplete"
                servicepath='/InfoControl/InfoControl/SearchService.svc/SearchProduct'
                options="{max: 10}"
                MaxLength="100"> </asp:TextBox>
            <%--
             <asp:TextBox ID="txtCustomer" runat="server" Width="250px" CssClass="cDynDat11" AutoPostBack="True"
                OnTextChanged="txtCustomer_TextChanged"
                plugin="autocomplete"
                servicepath='/InfoControl/InfoControl/SearchService.svc/SearchProduct'
                options="{max: 10}"
                MaxLength="100"></asp:TextBox>--%>
            
            <asp:RequiredFieldValidator ID="valProduct" ControlToValidate="txtProduct" runat="server"
                ErrorMessage="&nbsp;&nbsp;&nbsp;" Display="Dynamic"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;
            <p style="font-size: 7pt; color: gray">
                Dica: Digite parte do texto, que o completará automaticamente!</p>
        </td>
    </tr>
</table>
<input id="<%=this.ClientID %>" type="text" value="<%=ViewState["CustomerId"] %>"
    style="display: none" />
<%--<ajaxToolkit:AutoCompleteExtender ID="completetxtProduct" runat="server" CompletionInterval="1000"
    CompletionSetCount="20" MinimumPrefixLength="3" ServiceMethod="SearchProduct"
    ServicePath="~/Controller/SearchService" TargetControlID="txtProduct" BehaviorID="completetxtProduct">
</ajaxToolkit:AutoCompleteExtender>--%>
