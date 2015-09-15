using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using jvSystemas.Classes;

namespace jvSystemas
{
    public partial class frmPrincipal : Form
    {

        classMySQL mysql = new classMySQL();
        classTools tools = new classTools();

        public frmPrincipal()
        {
            InitializeComponent();
        }

        #region desabilitar o fechar do form
        const int MF_BYPOSITION = 0x400;
        [DllImport("User32")]
        private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("User32")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("User32")]
        private static extern int GetMenuItemCount(IntPtr hWnd);
        #endregion

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            //Desabilita o botão Fechar (X)
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuItemCount = GetMenuItemCount(hMenu);
            RemoveMenu(hMenu, MenuItemCount - 1, MF_BYPOSITION);
            //Desabilita o botão Fechar (X)

            lblUsuarioLogado.Text = Classes.classSession.DadosUsuario[0]["nome"].ToString();
            lblLoginLogado.Text = Classes.classSession.DadosUsuario[0]["usuario"].ToString();
            lblIpLogado.Text = tools.LocalIPAddress();
            lblMaquinaLogada.Text = tools.HostName();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Finalizar o sistema?", this.Text, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        #region Métodos customizados
        private void abreFormFilho(Form form)
        {
            foreach (Form formFilho in this.MdiChildren)
            {
                if (formFilho.GetType() == form.GetType())
                {
                    formFilho.Focus();
                    return;
                }
            }
            form.MdiParent = this;
            form.Show();
        }
        #endregion

        private void formasDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abreFormFilho(new frmCadFormasDePagamento());
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            abreFormFilho(new frmListaFormaDePagamento());
        }
    }
}
