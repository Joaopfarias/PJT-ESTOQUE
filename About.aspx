<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="PRFT1.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .pagination-ys {
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a,
                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a,
                .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a,
                .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover,
                .pagination-ys table > tbody > tr > td > span:hover,
                .pagination-ys table > tbody > tr > td > a:focus,
                .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }
    </style>
    <br />

    <div class="container">
        <asp:DropDownList runat="server" ID="ddl" CssClass="btn btn-default">
            <asp:ListItem Text="ID" Value="ID"></asp:ListItem>
            <asp:ListItem Text="Nome" Value="Nome"></asp:ListItem>
            <asp:ListItem Text="Quantidade" Value="Quantidade"></asp:ListItem>
            <asp:ListItem Text="Categoria" Value="Categoria"></asp:ListItem>
            <asp:ListItem Text="Medida de Volume" Value="Medida de Volume"></asp:ListItem>
            <asp:ListItem Text="Preço" Value="Preço"></asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="txtPesquisa" class="form-control col-md-3" runat="server"></asp:TextBox>
        <asp:Button runat="server" CssClass="btn btn-default" ID="btnPesquisa" OnClick="btnPesquisa_Click" Text="🔎" />
        <asp:Button runat="server" CssClass="btn btn-default" ID="btnListaCompleta" OnClick="btnListaCompleta_Click" Text="Ver Lista Completa" />
        <asp:Button runat="server" CssClass="btn btn-default" ID="btnRedefinir" OnClick="btnRedefinir_Click" Text="Redefinir Filtros" />
        <button class="btn btn-default" type="button" id="btnBusca" data-toggle="collapse" data-target="#collapseBusca" aria-expanded="false" aria-controls="collapseBusca">
            Busca Avançada</button>
        <button class="btn btn-default" type="button" id="btnInsert" data-toggle="modal" data-target="#exampleModal" data-whatever="@getbootstrap">
            Inserir</button><br />
        <br />
        <div class="modal fade modal-xl" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Tela para inserção de dados na tabela</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <asp:Panel runat="server" CssClass="form-row">
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" placeholder="ID" class="form-control" ID="txt_ID"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" placeholder="Nome" class="form-control" ID="txt_NOME"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" placeholder="Quantidade" class="form-control" ID="txt_QUANTIDADE"></asp:TextBox>
                                </div>
                            </asp:Panel>
                        </div>
                        <br />
                        <div class="form-group">
                            <asp:Panel runat="server" CssClass="form-row">
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" placeholder="Medida de Volume" class="form-control" ID="txt_MEDIDA"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" placeholder="Preço" class="form-control" ID="txt_PRECO"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:Button runat="server" class="btn" Text="Inserir" ID="btnInserir" OnClick="btnInserir_Click" />
                                </div>
                            </asp:Panel>
                        </div>
                        <br />
                        <br />
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <button type="button" class="btn btn-primary">Inserir</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="collapse" id="collapseBusca">
            <div class="card card-body">
                <div class="container">
                    <div class="container">
                        <asp:Panel runat="server" CssClass="form-row">
                            <div class="col-md-2">
                                <asp:CheckBox runat="server" class="form-check-input" ID="chk_ID" OnCheckedChanged="chk_ID_CheckedChanged" />
                                <asp:Label runat="server" Text="ID"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:CheckBox runat="server" class="form-check-input" ID="chk_NOME" OnCheckedChanged="chk_NOME_CheckedChanged" />
                                <asp:Label runat="server" Text="Nome"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:CheckBox runat="server" class="form-check-input" ID="chk_QUANTIDADE" OnCheckedChanged="chk_QUANTIDADE_CheckedChanged" />
                                <asp:Label runat="server" Text="Quantidade"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:CheckBox runat="server" class="form-check-input" ID="chk_MEDIDA" OnCheckedChanged="chk_MEDIDA_CheckedChanged" />
                                <asp:Label runat="server" Text="Medida de Volume"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:CheckBox runat="server" class="form-check-input" ID="chk_PRECO" OnCheckedChanged="chk_PRECO_CheckedChanged" />
                                <asp:Label runat="server" Text="Preço"></asp:Label>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="container">
                        <asp:Panel runat="server" CssClass="form-row">
                            <div class="col-md-2">
                                <asp:TextBox runat="server" class="form-control" ID="i_ID" placeholder="De" Enabled="true"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" class="form-control" ID="i_NOME" placeholder="Nome" Enabled="true"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" class="form-control" ID="i_QUANTIDADE" placeholder="De" Enabled="true"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" class="form-control" ID="i_MEDIDA" placeholder="De" Enabled="true"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" class="form-control" ID="i_PRECO" placeholder="De" Enabled="true"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <span></span>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="container">
                        <asp:Panel runat="server" CssClass="form-row">
                            <div class="col-md-2">
                                <asp:TextBox runat="server" class="form-control" ID="f_ID" placeholder=" Até" Enabled="true"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" class="form-control" ID="f_QUANTIDADE" placeholder=" Até" Enabled="true"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" class="form-control" ID="f_MEDIDA" placeholder=" Até" Enabled="true"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" class="form-control" ID="f_PRECO" placeholder=" Até" Enabled="true"></asp:TextBox>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <br />
        </div>
        <asp:Label runat="server" class="form-control" ID="lbl"></asp:Label><br />
    </div>
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:GridView runat="server" ClientIDMode="AutoID" AllowSorting="True" ID="Gv"
                class="table table-hover" BackColor="White" BorderColor="#CCCCCC"
                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                GridLines="Horizontal" AllowPaging="true" OnPageIndexChanging="Gv_PageIndexChanging"
                OnSorting="TaskGridView_Sorting">
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
                <AlternatingRowStyle BackColor="#DFDEDE" />
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
