using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using jvSystemas.Classes;

namespace jvSystemas
{
    public partial class frmListaFormaDePagamento : Form
    {

        classMySQL mySql = new classMySQL();
        classTools tools = new classTools();
        private int _id = 0;

        public frmListaFormaDePagamento()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _id = 0;
            txtFormaPagamento.Text = txtQtdeParcelas.Text = "";
            gerenciaCampos(0);
        }

        private void frmListaFormaDePagamento_Load(object sender, EventArgs e)
        {
            carregaDados();
        }

        private void carregaDados()
        {
            string sql = "SELECT * FROM formas_pagamento_tb";
            DataTable dt = mySql.getDados(sql);
            dgvDados.Rows.Clear();
            int j = 0;
            foreach (DataRow rows in dt.Rows)
            {
                dgvDados.Rows.Add(rows["id"], rows["forma_pagamento"], rows["qtde_parcelas"]);
                j++;
            }
        }

        private void dgvDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgvDados.SelectedRows[0];

                object id = row.Cells[0].Value;
                object forma = row.Cells[1].Value;
                object parcela = row.Cells[2].Value;
                
                _id = Convert.ToInt32(id.ToString());
                txtFormaPagamento.Text = forma.ToString();
                txtQtdeParcelas.Text = parcela.ToString();

                gerenciaCampos(1);
            }
            catch { }
        }

        #region métodos customizados
        private void gerenciaCampos(int x)
        {
            if (x == 0)//bloqueia
            {
                txtFormaPagamento.Text = txtQtdeParcelas.Text = "";
                groupBox1.Enabled = false;
                btnSalvar.Enabled = false;
            }
            else if (x == 1)//desbloqueia
            {
                groupBox1.Enabled = true;
                btnSalvar.Enabled = true;
            }
        }
        #endregion

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            carregaDados();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (this._id > 0)
            {
                string sql = string.Format
                (
                    "UPDATE formas_pagamento_tb SET forma_pagamento='{0}', qtde_parcela='{1}' WHERE id={2}",
                    txtFormaPagamento.Text,
                    txtQtdeParcelas.Text,
                    this._id
                );

                mySql.query(sql);
                carregaDados();
                gerenciaCampos(0);
            }
        }
    }
}
