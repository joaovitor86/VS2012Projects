using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace jvSystemas
{
    public partial class frmLogin : Form
    {

        #region declarações
        Classes.classMySQL mySql = new Classes.classMySQL();
        #endregion

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            btnCancelar_Click(null, null);
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Length > 4 && txtSenha.Text.Length > 4)
            {
                string sql = string.Format
                (
                    "SELECT * FROM usuarios_tb WHERE usuario='{0}' AND senha='{1}' AND ativo='s' LIMIT 1",
                    txtUsuario.Text,
                    txtSenha.Text
                );

                MySqlDataAdapter myDa = new MySqlDataAdapter(sql, mySql.getConexao());
                DataTable myDt = new DataTable();

                myDa.Fill(myDt);

                if (myDt.Rows.Count > 0)
                {
                    Classes.classSession.DadosUsuario = myDt.Rows;
                    this.Hide();
                    new frmPrincipal().ShowDialog();
                }
                else
                {
                    MessageBox.Show("Dados inválidos!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;
            }
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;
            }
        }
    }
}
