using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace jvSystemas.Classes
{
    class classMySQL
    {
        private string HOST = "localhost";
        private string USER = "root";
        private string PASS = "vertrigo";
        private string BASE = "db_informatica";
        private string conStr = "server={0};user id={1};password={2};database={3};charset=utf8;";
        private MySqlConnection myCon;

        /// <summary>
        /// 
        /// </summary>
        public classMySQL()
        {
            this.getConexao();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MySqlConnection getConexao()
        {
            string conexao = string.Format
            (
                conStr,
                this.HOST,
                this.USER,
                this.PASS,
                this.BASE
            );

            myCon = new MySqlConnection(conexao);

            if (myCon.State == ConnectionState.Closed)
            {
                try
                {
                    myCon.Open();
                }
                catch (MySqlException myex)
                {
                    System.Windows.Forms.MessageBox.Show(myex.Message.ToString(), "Erro de execução", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }

            return myCon;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool query(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, this.getConexao());

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable getDados(string sql)
        {
            MySqlDataAdapter myDa = new MySqlDataAdapter(sql, this.getConexao());
            DataTable myDt = new DataTable();

            myDa.Fill(myDt);

            return myDt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="caminho"></param>
        /// <returns></returns>
        public bool BackUp(string caminho)
        {
            using (MySqlConnection conn = new MySqlConnection(String.Format(this.conStr, this.HOST, this.USER, this.PASS, this.BASE)))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        try
                        {
                            mb.ExportToFile(caminho);
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                        finally
                        {
                            conn.Close();
                        }

                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="var"></param>
        /// <returns></returns>
        public string getVar(string var)
        {
            string retorno = null;

            switch (var)
            {
                case "HOST":
                    retorno = this.HOST;
                    break;

                case "USER":
                    retorno = this.USER;
                    break;

                case "PASS":
                    retorno = this.USER;
                    break;

                case "BASE":
                    retorno = this.BASE;
                    break;
            }

            return retorno;
        }
    }
}
