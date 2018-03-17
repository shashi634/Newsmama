<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SubAdmin.aspx.cs" Inherits="NewsCaptain.Admin.SubAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <section class="content-header">
        <h1>Dashboard
           
            <small>Control panel</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Dashboard</li>
        </ol>
    </section>
    <section class="content">
        <div class="row">
            <div class="col-sm-3">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Create Sub Admin</h3>
                        <span class="label label-primary pull-right"><i class="fa fa-bell "></i></span>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                            <div class="form-group">
                                <asp:TextBox ID="TextBox1" runat="server" class="form-control" placeholder="Sub Admin Name"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="TextBox2" runat="server" class="form-control" placeholder="Email Id"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:TextBox ID="TextBox3" runat="server" class="form-control" placeholder="Mobile No."></asp:TextBox>
                            </div>
                        
                        <div class="form-group">
                            <asp:DropDownList ID="ddl_usertype" class="form-control" runat="server" placeholder="User Type">
                                <asp:ListItem Value="1">Admin</asp:ListItem>
                                <asp:ListItem Selected="True" Value="2">Writer</asp:ListItem>
                                <asp:ListItem Value="3">Editor</asp:ListItem>
                            </asp:DropDownList>
                            </div>

                        <div class="form-group">
                            <asp:Button ID="Button1" CssClass="btn btn-group" OnClientClick="overlay" OnClick="Button1_OnClick" runat="server" Text="Register"  />
                            <asp:Button ID="Button2" CssClass="btn btn-group" OnClientClick="overlay" OnClick="Button2_OnClick" runat="server" Text="Update"  />
                            </div>
                        <div class="form-group">
                            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    </div>
        </div>

        
            <div class="col-sm-9">
                <div class="box box-danger">
                    <div class="box-header with-border">
                        <h3 class="box-title">Sub Admin Details</h3>
                        <span class="label label-danger pull-right"><i class="fa fa-eye"></i></span>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="form-group">
                            <asp:GridView ID="SubAdminTable" AutoGenerateColumns="false" runat="server"
                                 onrowdeleting="subAdmin_RowDeleting"
                                 CssClass="table table-striped table-bordered table-hover" RowStyle-BorderColor="White" PageSize="10"
                                 OnPageIndexChanging="postList_PageIndexChanging" 
                                 AllowPaging="true"
                                 >
                                 <Columns>
                                     <asp:TemplateField HeaderStyle-BorderColor="White">
                                         <HeaderTemplate> Sr. No.</HeaderTemplate>
                                         <ItemTemplate>
                                             <%#Container.DataItemIndex+1 %>
                                             <asp:Label ID="h" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                 </Columns>
                                 <Columns>
                                     <asp:TemplateField HeaderStyle-BorderColor="White">
                                         <HeaderTemplate> Name</HeaderTemplate>
                                         <ItemTemplate>
                                             <%#Eval("Name") %>
                                         </ItemTemplate>
                                         
                                     </asp:TemplateField>
                                 </Columns>
                                 <Columns>
                                     <asp:TemplateField HeaderStyle-BorderColor="White">
                                         <HeaderTemplate > Mobile No.</HeaderTemplate>
                                         <ItemTemplate>
                                              <%#Eval("MobileNo") %>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                 </Columns>
                                <Columns>
                                     <asp:TemplateField HeaderStyle-BorderColor="White">
                                         <HeaderTemplate > EmailId.</HeaderTemplate>
                                         <ItemTemplate>
                                              <%#Eval("EmailId") %>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                 </Columns>
                                <Columns>
                                     <asp:TemplateField HeaderStyle-BorderColor="White">
                                         <HeaderTemplate > Type</HeaderTemplate>
                                         <ItemTemplate>
                                              <%#Eval("UserType") %>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                 </Columns>
                                <Columns>
                                     <asp:TemplateField HeaderStyle-BorderColor="White">
                                         <HeaderTemplate > Status</HeaderTemplate>
                                         <ItemTemplate>
                                              <%#Eval("isActive") %>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                 </Columns>
                                <Columns>
                                     <asp:TemplateField HeaderStyle-BorderColor="White">
                                         <HeaderTemplate > Registration Date</HeaderTemplate>
                                         <ItemTemplate>
                                              <%#Eval("RegistrationDate") %>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                 </Columns>
                                 <Columns>
                                     <asp:TemplateField HeaderStyle-BorderColor="White">
                                         <HeaderTemplate> </HeaderTemplate>
                                         <ItemTemplate>
                                             <a href='<%#"SubAdmin.aspx?C="+DataBinder.Eval(Container.DataItem,"publicid") %>'><span class="glyphicon glyphicon-edit"></span></a>
                                            <asp:LinkButton ID="LinkButton2"  runat="server" CommandName="Delete" placeholder="Edit"><span class="glyphicon glyphicon-trash"></span></asp:LinkButton>
                                         </ItemTemplate>
                                        
                                     </asp:TemplateField>
                                 </Columns>
                             </asp:GridView>
                        </div>
                       

                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box -->
            </div>
        </div>
    </section>
        <script type="text/javascript">
         $(document).ready(function () {
                 
             function toggle_password(target) {
                 var d = document;
                 var tag = d.getElementById(target);
                 var tag2 = d.getElementById("showhide");

                 if (tag2.innerHTML == 'Show') {
                     tag.setAttribute('type', 'text');
                     tag2.innerHTML = 'Hide';

                 } else {
                     tag.setAttribute('type', 'password');
                     tag2.innerHTML = 'Show';
                 }
             }
             
         });
       
    </script>
</asp:Content>
