using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Net;

namespace PRFT1
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGridData();
            }
        }
        private bool IsNumeric(string valor)
        {
            return valor.All(char.IsNumber);
        }
        private void LoadGridData()
        {
            // Create a new produtos.
            DataTable produtos = new DataTable("Produtos");

            // Create the columns.
            produtos.Columns.Add(new DataColumn("ID", typeof(int)));
            produtos.Columns.Add(new DataColumn("Nome", typeof(string)));
            produtos.Columns.Add(new DataColumn("Quantidade", typeof(int)));
            produtos.Columns.Add(new DataColumn("Categoria", typeof(string)));
            produtos.Columns.Add(new DataColumn("Medida de Volume", typeof(string)));
            produtos.Columns.Add(new DataColumn("Preço", typeof(string)));

            //Add data to the new produtos.
            DataTable dt = new DataTable();
            produtos.Rows.Add(101, "Jack Daniels", 7, "Whisky", "1 L", "R$" + 100.00);
            produtos.Rows.Add(102, "Jack Daniels Fire", 12, "Whisky", "1 L", "R$" + 153.00);
            produtos.Rows.Add(103, "Jack Daniels Honey", 2, "Whisky", "1 L", "R$" + 192.00);
            produtos.Rows.Add(104, "Jack Daniels Green Apple", 22, "Whisky", "1 L", "R$" + 315.00);
            produtos.Rows.Add(105, "Red Label ", 13, "Whisky", "1 L", "R$" + 85.00);
            produtos.Rows.Add(106, "Green Label ", 19, "Whisky", "1 L", "R$" + 188.00);
            produtos.Rows.Add(107, "Black Label ", 23, "Whisky", "1 L", "R$" + 224.00);
            produtos.Rows.Add(108, "Blue Label ", 1, "Whisky", "1 L", "R$" + 416.00);
            produtos.Rows.Add(109, "Platinum Label ", 9, "Whisky", "1 L", "R$" + 760.00);
            produtos.Rows.Add(110, "Golden Label ", 22, "Whisky", "1 L", "R$" + 329.00);
            produtos.Rows.Add(111, "Smirnoff", 7, "Vodka", "1 L", "R$" + 100.00);
            produtos.Rows.Add(112, "Smirnoff Ice", 12, "Vodka", "1 L", "R$" + 153.00);
            produtos.Rows.Add(113, "Smirnoff Melancia", 2, "Vodka", "1 L", "R$" + 192.00);
            produtos.Rows.Add(114, "Smirnoff Maracuja", 22, "Vodka", "1 L", "R$" + 315.00);
            produtos.Rows.Add(115, "Vibe", 13, "Energetico", "2 L", "R$" + 85.00);
            produtos.Rows.Add(116, "One", 19, "Energetico", "2 L", "R$" + 188.00);
            produtos.Rows.Add(117, "RedBull", 23, "Energetico", "275 ml", "R$" + 224.00);
            produtos.Rows.Add(118, "Monster", 1, "Energetico", "700 ml", "R$" + 416.00);
            produtos.Rows.Add(119, "Gelo de Maracujá", 9, "Suco", "200 ml", "R$" + 760.00);
            produtos.Rows.Add(120, "Gelo de Coco", 22, "Suco", "200 ml", "R$" + 329.00);

            //Persist the produtos in the Session object.
            Session["TaskTable"] = produtos;

            //Bind the GridView control to the data source.
            Gv.DataSource = Session["TaskTable"];
            Gv.DataBind();
            int linhas = Gv.Rows.Count;
            lbl.Text = "Linhas Atuais: " + linhas;
        }
        private void ReLoadGridData()
        {
            DataTable dt = Session["TaskTable"] as DataTable;
            Gv.DataSource = dt;
            Gv.DataBind();
            int linhas = Gv.Rows.Count;
            lbl.Text = "Linhas Atuais: " + linhas;
        }
        protected void TaskGridView_Sorting(object sender, GridViewSortEventArgs e)
        {

            //Retrieve the produtos from the session object.
            DataTable dt = Session["TaskTable"] as DataTable;

            if (dt != null)
            {

                //Sort the data.
                DataView dvUsers = new DataView(dt);
                dvUsers.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                DataTable dvt = new DataTable();
                dvt = dvUsers.ToTable();
                Session["TaskTable"] = dvt;
                Gv.DataSource = dvUsers;
                Gv.DataBind();
            }
        }
        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }
        protected void Gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv.PageIndex = e.NewPageIndex;
            ReLoadGridData();
        }
        public async Task<Produtos> CadastrarAPI(Produtos dados)
        {
            Produtos list = new Produtos();
            using (var Http = new HttpClient())
            {
                var myContent = JsonConvert.SerializeObject(dados);
                HttpRequestMessage _client = new HttpRequestMessage();
                _client.RequestUri = new System.Uri("https://localhost:44373/api/Produto");
                _client.Method = HttpMethod.Post;
                _client.Content = new StringContent(myContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await Http.SendAsync(_client);
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        list = await response.Content.ReadAsAsync<Produtos>();
                        Response.Write("<script>alert('Sucesso!');</script>");
                    }
                    catch (Exception ex)
                    {
                        var exception = ex;
                        Response.Write($"<script>alert('Erro:{ex}');</script>");
                    }
                }
                else
                {
                    var errorcontent = await response.Content.ReadAsStringAsync();
                    Response.Write($"<script>alert('Falha de Acesso: {errorcontent}');</script>");
                }
                return list;
            }
        }
        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["TaskTable"] as DataTable;
            DataView dvUsers = new DataView(dt);
            string col = ddl.SelectedValue;
            if (ddl.SelectedValue == "Nome")
            {
                dvUsers.RowFilter = col + " LIKE '%" + txtPesquisa.Text + "%' ";
                DataTable dvt = new DataTable();
                dvt = dvUsers.ToTable();
                Session["TaskTable"] = dvt;
                Gv.DataSource = dvUsers;
                Gv.DataBind();
                int linhas = Gv.Rows.Count;
                lbl.Text = "Linhas Atuais: " + linhas;
            }
            else
            {
                dvUsers.RowFilter = col + " = " + txtPesquisa.Text;
                DataTable dvt = new DataTable();
                dvt = dvUsers.ToTable();
                Session["TaskTable"] = dvt;
                Gv.DataSource = dvUsers;
                Gv.DataBind();
                int linhas = Gv.Rows.Count;
                lbl.Text = "Linhas Atuais: " + linhas;
                Gv.AllowPaging = true;
            }
        }
        protected void btnListaCompleta_Click(object sender, EventArgs e)
        {
            Gv.AllowPaging = false;
            ReLoadGridData();
        }
        protected void btnRedefinir_Click(object sender, EventArgs e)
        {
            Gv.AllowPaging = true;
            LoadGridData();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            if (chk_ID.Checked == true || chk_NOME.Checked == chk_QUANTIDADE.Checked == true
                || chk_MEDIDA.Checked == true || chk_PRECO.Checked == true)
            {
                var something = "";
                if (chk_ID.Checked == true)
                {
                    if (something == "") { something += " ID >=" + i_ID.Text + " AND ID <=" + f_ID.Text + " "; }
                    else { something += "AND ID >=" + i_ID.Text + " AND ID <=" + f_ID.Text + ""; }
                }
                if (chk_NOME.Checked == true)
                {
                    if (something == "") { something += " NOME LIKE '%" + i_NOME.Text + "%' "; }
                    else { something += "AND NOME LIKE '%" + i_NOME.Text + "%' "; }
                }
                if (chk_QUANTIDADE.Checked == true)
                {
                    if (something == "") { something += " QUANTIDADE >=" + i_QUANTIDADE.Text + " AND QUANTIDADE <=" + f_QUANTIDADE.Text + " "; }
                    else { something += "AND QUANTIDADE >=" + i_QUANTIDADE.Text + " AND QUANTIDADE <=" + f_QUANTIDADE.Text + ""; }
                }
                if (chk_MEDIDA.Checked == true)
                {
                    if (something == "") { something += " MEDIDA >=" + i_MEDIDA.Text + " AND MEDIDA <=" + f_MEDIDA.Text + " "; }
                    else { something += "AND MEDIDA >=" + i_MEDIDA.Text + " AND MEDIDA <=" + f_MEDIDA.Text + ""; }
                }
                if (chk_PRECO.Checked == true)
                {
                    if (something == "") { something += " PRECO >=" + i_PRECO.Text + " AND PRECO <=" + f_PRECO.Text + " "; }
                    else { something += "AND PRECO >=" + i_PRECO.Text + " AND PRECO <=" + f_PRECO.Text + ""; }
                }
                DataTable dt = Session["TaskTable"] as DataTable;
                DataView dvUsers = new DataView(dt);
                dvUsers.RowFilter = something;
                //dvUsers.RowFilter = "ID >=" + i_ID.Text + " AND ID <=" + f_ID.Text
                //+ " AND Nome LIKE '%" + i_NOME.Text + "%'";
                DataTable dvt = new DataTable();
                dvt = dvUsers.ToTable();
                Session["TaskTable"] = dvt;
                Gv.DataSource = dvUsers;
                Gv.DataBind();
                int linhas = Gv.Rows.Count;
                lbl.Text = "Linhas Atuais: " + linhas;
            }
        }
        protected void btnInserir_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(txt_ID.Text);
            string nome = txt_NOME.Text;
            int quantidade = Int32.Parse(txt_QUANTIDADE.Text);
            decimal medida = decimal.Parse(txt_MEDIDA.Text);
            decimal preco = decimal.Parse(txt_PRECO.Text);
            Produtos dados = new Produtos()
            {
                Nome = txt_NOME.Text,
                Quantidade = txt_QUANTIDADE.Text,
                Medida = txt_MEDIDA.Text,
                Preco = txt_PRECO.Text
            };
            Task.Run(async () =>
            {
                await CadastrarAPI(dados);
            }).Wait();
        }

        protected void chk_ID_CheckedChanged(object sender, EventArgs e)
        {
            i_ID.Enabled = true;
            f_ID.Enabled = true;
        }

        protected void chk_NOME_CheckedChanged(object sender, EventArgs e)
        {
            i_NOME.Enabled = true;
        }

        protected void chk_QUANTIDADE_CheckedChanged(object sender, EventArgs e)
        {
            i_QUANTIDADE.Enabled = true;
            f_QUANTIDADE.Enabled = true;
        }

        protected void chk_MEDIDA_CheckedChanged(object sender, EventArgs e)
        {
            i_MEDIDA.Enabled = true;
            f_MEDIDA.Enabled = true;
        }

        protected void chk_PRECO_CheckedChanged(object sender, EventArgs e)
        {
            i_PRECO.Enabled = true;
            f_PRECO.Enabled = true;
        }
    }
}