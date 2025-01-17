<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frm_group_allocation.aspx.cs" Inherits="frm_group_allocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
         .FixedHeader {
            position: sticky;
            font-weight: bold;
            top: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="card">
        <div class="card-header text-white" style="background: linear-gradient(to right, #6f03bf 1%, #363695 100%) !important; border-radius: 7px;">
            <h3>Group Allocation</h3>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="up_row" runat="server">
                <ContentTemplate>
                    <div class="row">



                        <div class="col-md-9">
                            <div class="row">
                                <div class="col-md-9">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label runat="server" ID="lblmedium" Text="Medium" Style="font-weight: bold"></asp:Label><br />
                                            <asp:DropDownList runat="server" ID="ddlmedium" CssClass="form-control" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label runat="server" ID="lblstandard" Text="Standard" Style="font-weight: bold"></asp:Label><br />
                                            <asp:DropDownList runat="server" ID="ddlstandard" CssClass="form-control" OnSelectedIndexChanged="ddlstandard_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label runat="server" ID="lblgroup" Text="Group" Style="font-weight: bold"></asp:Label><br />
                                            <asp:DropDownList runat="server" ID="ddl_group" CssClass="form-control" OnSelectedIndexChanged="ddl_group_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label runat="server" ID="lbldivision" Text="Division" Style="font-weight: bold"></asp:Label><br />
                                            <asp:DropDownList runat="server" ID="ddl_division" CssClass="form-control" OnSelectedIndexChanged="ddl_division_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="well well-lg" style="padding-top: 0px">
                                        <b>Sort By:</b><br />
                                        <div class="row" style="padding-top: 4px; padding-bottom: 4px">
                                            <div class="col-lg-6">
                                                <asp:RadioButton runat="server" ID="rdoname" GroupName="grpallo" Checked="true" Text="Name" OnCheckedChanged="rdoname_CheckedChanged" AutoPostBack="true" />
                                            </div>
                                            <div class="col-lg-6 ">
                                                <asp:RadioButton runat="server" ID="rdorolldiv" GroupName="grpallo" Checked="false" Text="Roll No./Division" OnCheckedChanged="rdorolldiv_CheckedChanged" AutoPostBack="true" />
                                            </div>
                                        </div>
                                    </div>

                                </div>


                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="row">

                                <div class="col-md-4">
                                    <br />
                                    <asp:Button runat="server" ID="btn_save" Text="Save" CssClass="btn btn-success form-control" OnClick="btn_save_Click" />
                                </div>
                                <div class="col-md-4">
                                    <br />
                                    <asp:Button runat="server" ID="btn_edit" Text="Edit" CssClass="btn btn-success form-control" OnClick="btn_edit_Click" />
                                </div>
                                <div class="col-md-4">
                                    <br />
                                    <asp:Button runat="server" ID="btn_clear" Text="Clear" CssClass="btn btn-success form-control" OnClick="btn_clear_Click" />
                                </div>
                            </div>
                        </div>
                    </div>




                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="up_grid" runat="server">
                <ContentTemplate>
                    <center>
                        <div class="row" style="padding-top: 30px;">
                     <div id="divgroupmst" runat="server"  style="width: 100%; word-wrap: break-word; max-height:450px;overflow:auto">
                        <asp:GridView runat="server" ID="gv_grp_mst" CssClass="table table-bordered table-hover mygrid"  AutoGenerateColumns="false" HeaderStyle-CssClass="FixedHeader">
                            <Columns>
                               <asp:TemplateField  HeaderText="Select" >
                                   <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="chkallgroup" OnCheckedChanged="chkallgroup_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                   </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkgroup" ></asp:CheckBox>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
                               
                                <asp:TemplateField  HeaderText="Student ID">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_studid" Text='<% #Bind("student_id")%>'  ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle  />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Student Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbl_name" Text='<% #Bind("Name")%>' ></asp:Label>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Roll No">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblroll" Text='<% #Bind("Roll_no")%>' ></asp:Label>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Division">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbldivision" Text='<% #Bind("division_name")%>' ></asp:Label>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 </Columns>
                        </asp:GridView>
                    </div>
               </div>
                        </center>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


</asp:Content>

