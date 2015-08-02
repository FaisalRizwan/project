<%@ page title="Home page" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="_Default, App_Web_md5xekke" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <table id="Table1" height="177px" width="490px" >
        <tr>
            <td>
                Name :
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqName" ControlToValidate="txtName" Display="Static"
                    ForeColor="Red" ErrorMessage="*" runat="server" />
                <asp:RegularExpressionValidator ID="regexpName" runat="server" ErrorMessage="Expression does not validate."
                    ControlToValidate="txtName" ValidationExpression="^[a-zA-Z'.\s]{1,40}$" />
            </td>
        </tr>
        <tr>
            <td>
                Phone :
            </td>
            <td>
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqPhone" ControlToValidate="txtPhone" Display="Static"
                    ForeColor="Red" ErrorMessage="*" runat="server" />
                <asp:RegularExpressionValidator ID="regPhone" runat="server" ControlToValidate="txtPhone"
                    ValidationExpression="[0-9]{11}" Text="Enter a valid phone number" />
            </td>
        </tr>
        <tr>
            <td>
                Image :
            </td>
            <td>
                <asp:FileUpload ID="fbFileUpload" runat="server" />
                <asp:RequiredFieldValidator ID="rqFileUpload" ControlToValidate="fbFileUpload" Display="Static"
                    ErrorMessage="*" ForeColor="Red" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnSearch_Click" Width="126px" />
            </td>
        </tr>
        <table>
</asp:Content>
