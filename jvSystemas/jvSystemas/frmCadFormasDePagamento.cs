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
    public partial class frmCadFormasDePagamento : Form
    {

        classMySQL mySql = new classMySQL();
        classTools tools = new classTools();

        public frmCadFormasDePagamento()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtFormaPagamento.Text.Length > 4)
            {
                string sql = string.Format("INSERT INTO formas_pagamento_tb (forma_pagamento, qtde_parcelas) VALUES ('{0}', '{1}')", txtFormaPagamento.Text, txtQtdeParcelas.Text);

                if (mySql.query(sql))
                {
                    txtFormaPagamento.Text = txtQtdeParcelas.Text = "";
                    txtFormaPagamento.Focus();
                }
            }
            else
            {
                MessageBox.Show("Você deve preencher ao menos a forma de pagamento!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCadFormasDePagamento_Load(object sender, EventArgs e)
        {

        }
    }
}
