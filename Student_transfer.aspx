<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Student_transfer.aspx.cs" Inherits="Student_transfer"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <script src="notify-master/js/notify.js"></script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="col mt-2">
        <div class="card">
            <div class="card-header bg-primary text-white" style="background: linear-gradient(to right,#0078bc 1%,#00beda 100%, transparent); border-radius: 7px">

                <h3><b>Student Transfer</b></h3>

            </div>
            <div class="card-body" style="display: inline; padding-left: 0px; padding-right: 0px; padding-bottom: 0px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="col-md-12">

                            <div class="row ">

                                <div class="col-md-2">
                                    <label for="ddlmedium" style="color: black; float: left">Medium</label>
                                    <div class="input-field">
                                        <asp:DropDownList ID="ddlmedium" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlmedium_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-md-2">
                                    <label for="ddlclass" style="color: black; float: left">Class</label>
                                    <div class="input-field">
                                        <asp:DropDownList ID="ddlclass" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2" id="prom" runat="server">
                                    <label for="Year to be Promoted" style="color: black; float: left">
                                        Promoted to Class
                                                <asp:Label runat="server" ID="lblyearnxt" Style="display: none"></asp:Label></label>
                                    <div class="input-field">
                                        <asp:TextBox ID="transferyear" runat="server" data-toggle="tooltip" ReadOnly="true" data-placement="bottom" title="Standard for which Student is transfered" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <label for="ddltoyear" style="color: black; float: left">To Year</label>
                                    <div class="input-field">
                                        <asp:DropDownList ID="ddltoyear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddltoyear_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2" id="cancel" runat="server" style="padding-top: 30px; font-weight: bold">


                                    <asp:CheckBox ID="canceltransfer" AutoPostBack="true" OnCheckedChanged="canceltransfer_CheckedChanged" runat="server" placeholder="Click to transferthe student in previous standard" Text="Cancel Transfer"></asp:CheckBox>
                                </div>

                            </div>

                        </div>
                        <div class="row" style="margin-top: 10px">
                            <%-- GridView --%>
                            <div class="col-md-12">
                                 <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                <div class="card-panel" runat="server" id="grid_card" style="background-color: white; margin-left: 0px; margin-right: 0px;">
                                   
                                            <div class="well well-lg" runat="server" id="grid_show" style="height:450px; overflow-y:scroll" >
                                                <asp:GridView ID="grid1"  runat="server" Font-Size="12pt"
                                                    Style="text-align: center; width: 100%; border-color: white;" AutoGenerateColumns="False" CssClass="table table-container table-bordered mygrid" HeaderStyle-CssClass="FixedHeader">




                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />


                                                    <RowStyle HorizontalAlign="Center"></RowStyle>

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Serial No" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="srno" runat="server" Text='<%#Container.DataItemIndex+1%>' AutoPostBack="true"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="SELECT" HeaderStyle-Width="80px" ItemStyle-Width="80px">

                                                            <ItemTemplate>

                                                                <asp:CheckBox ID="chksel" runat="server" AutoPostBack="true" OnCheckedChanged="chksel_CheckedChanged" />

                                                            </ItemTemplate>
                                                            <HeaderTemplate>

                                                                <asp:CheckBox ID="chkall" runat="server" AutoPostBack="true" Style="margin-top: 2px" OnCheckedChanged="chkall_CheckedChanged" />
                                                                <asp:Label runat="server" Style="margin-left: 5px">SELECT</asp:Label>


                                                            </HeaderTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="STUDENT ID" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstud_id" runat="server" Text='<%# Eval("Student_id") %>' Style="margin: 9px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="STUDENT NAME" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("Student_Name") %>' Style="margin: 9px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        
                                                        <asp:TemplateField HeaderText="Division" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldiv" runat="server" Text='<%# Eval("Division") %>' Style="margin: 9px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="Division Id" HeaderStyle-Width="200px" ItemStyle-Width="200px" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldivid" runat="server" Text='<%# Eval("division_id") %>' Style="margin: 9px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="Group Name" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgrp" runat="server" Text='<%# Eval("GroupName") %>' Style="margin: 9px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="Group Id" HeaderStyle-Width="200px" ItemStyle-Width="200px" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgroupid" runat="server" Text='<%# Eval("Groupid") %>' Style="margin: 9px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="CHECK" HeaderStyle-Width="100px" ItemStyle-Width="100px" Visible="false">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblchk" runat="server" Style="margin: 9px;"></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                            </div>


                                       
                                </div>
                                             </ContentTemplate>
                                    </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-12">

                            <div class="row">
                                <div class="col-md-3 ">
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnsave" OnClick="btnsave_Click" Text="Promote" runat="server" CssClass="btn btn-success form-control" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btndetain" OnClick="btndetain_Click" Text="Detain" runat="server" CssClass="btn btn-success form-control" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="Clear" OnClick="Clear_Click" Text="Clear" runat="server" CssClass="btn btn-success form-control" />
                                </div>
                                <div class="col-md-3" id="err" runat="server" visible="false">
                                    <asp:LinkButton data-toggle="modal" data-target="#modal_scholar" ID="btn_error" CssClass="notification" runat="server">
                                        <span>Not Transferable Student</span>
                                        <span class="badge" id="badge" runat="server"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <br />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal_scholar">
        <div class="modal-dialog" style="width: 500px">

            <!-- Modal content-->
            <div class="modal-content">

                <div class="modal-header">

                    <h4 class="modal-title">Not Transferable Student</h4>
                </div>

                <div class="modal-body">
                    <asp:UpdatePanel ID="upd" runat="server">
                        <ContentTemplate>
                            <div class="well well-lg" runat="server" id="Div1" style="overflow: scroll; height: 450px; width: 100%; overflow-x: scroll;">
                                <asp:GridView ID="grid2" runat="server" Font-Size="12pt"
                                    Style="text-align: center; border: 2px solid; border-color: black; width: 100%" AutoGenerateColumns="False" CssClass="table table-hover table-striped table-bordered">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle HorizontalAlign="Center"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Serial No">
                                            <ItemTemplate>
                                                <asp:Label ID="srno" runat="server" Text='<%#Container.DataItemIndex+1%>' AutoPostBack="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Student Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblid" runat="server" Text='<%# Eval("Student_id")%>' Style="margin: 9px;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Student Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("Student_Name")%>' Style="margin: 9px;"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                                <br />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
            <div class="modal-footer">
            </div>


        </div>

    </div>
    <script>
        function sticky() {
            $(".sticky-header").floatThead({ top: 50 });
        }

    </script>
    <script type='text/javascript'>
        xAddEventListener(window, 'load',
            function sticky() { new xTableHeaderFixed('grid1', 'table-container', 0); }, false);
    </script>

</asp:Content>

