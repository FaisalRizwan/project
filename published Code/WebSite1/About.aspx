<%@ page title="Dettagli sull'organizzazione" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="About, App_Web_md5xekke" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Contact Information</h2>
    <h2>
        &nbsp;<asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
        <asp:TextBox ID="txtName" runat="server" Style="margin-left: 14px"></asp:TextBox>
        &nbsp;
        <asp:Label ID="Label2" runat="server" Text="Phone"></asp:Label>
        &nbsp;<asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
    </h2>
    <p>
        &nbsp;</p>
    <asp:GridView ID="dgvContactDetails" DataKeyNames="contactid,name" runat="server"
        AutoGenerateColumns="False" CssClass="Gridview" HeaderStyle-BackColor="#61A6F8"
        ShowFooter="True" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White"
        OnRowCancelingEdit="dgvContactDetails_RowCancelingEdit" OnRowDeleting="dgvContactDetails_RowDeleting"
        OnRowEditing="dgvContactDetails_RowEditing" OnRowUpdating="dgvContactDetails_RowUpdating"
        OnRowCommand="dgvContactDetails_RowCommand" OnRowDataBound="dgvContactDetails_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/update.jpg"
                        ToolTip="Update" Height="20px" Width="20px" />
                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/Cancel.jpg"
                        ToolTip="Cancel" Height="20px" Width="20px" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit.jpg"
                        ToolTip="Edit" Height="20px" Width="20px" />
                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server"
                        ImageUrl="~/Images/delete.jpg" ToolTip="Delete" Height="20px" Width="20px" />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/AddNewitem.jpg"
                        CommandName="AddNew" Width="30px" Height="30px" ToolTip="Add new User" ValidationGroup="validaiton" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID">
                <EditItemTemplate>
                    <asp:Label ID="lbleditContactId" runat="server" Text='<%#Eval("contactid") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblitemContactId" runat="server" Text='<%#Eval("contactid") %>' />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtftrContactId" runat="server"  MaxLength="3"/>
                    <asp:RequiredFieldValidator ID="rfvContactId" runat="server" ControlToValidate="txtftrContactId"
                        Text="*" ValidationGroup="validaiton"  ForeColor="Red" />
                        <asp:RegularExpressionValidator ID="regid" runat="server" ControlToValidate="txtftrContactId"
                        ValidationExpression="[0-9]" Text="Enter a valid phone number" />
                </FooterTemplate>
                <ItemStyle Width="100px" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <EditItemTemplate>
                    <asp:TextBox ID="txtName" runat="server" Text='<%#Eval("name") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("name") %>' />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtftrName" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtftrName"
                        Text="*" ValidationGroup="validaiton"   ForeColor="Red"/>
                        <asp:RegularExpressionValidator ID="regexpName" runat="server" ErrorMessage="Expression does not validate."
                    ControlToValidate="txtftrName" ValidationExpression="^[a-zA-Z'.\s]{1,40}$" />
                </FooterTemplate>
                <ItemStyle Width="150px" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phone">
                <EditItemTemplate>
                    <asp:TextBox ID="txtPhone" runat="server" Text='<%#Eval("phone") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPhone" runat="server" Text='<%#Eval("phone") %>' />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtftrPhone" runat="server" />
                    
                     <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtftrPhone"
                            Text="*" ValidationGroup="validaiton"  ForeColor="Red" />
                            <asp:RegularExpressionValidator ID="regPhone" runat="server" ControlToValidate="txtftrPhone"
                        ValidationExpression="[0-9]{11}" Text="Enter a valid phone number" />
                </FooterTemplate>
                <ItemStyle Width="100px" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Photo">
                <%-- <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("image") %>'></asp:TextBox>
                    </EditItemTemplate>--%>
                <FooterTemplate>
                    <asp:FileUpload ID="fbFileUpload" runat="server" />
                    <%--<asp:RequiredFieldValidator ID="rqName" ControlToValidate="fbFileUpload" Display="Static"
                        ErrorMessage="Required" runat="server" ForeColor="Red" />--%>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("image") %>' />
                </ItemTemplate>
                <ControlStyle Height="100px" Width="100px" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White"></HeaderStyle>
    </asp:GridView>
</asp:Content>
