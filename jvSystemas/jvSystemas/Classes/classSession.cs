using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace jvSystemas.Classes
{
    class classSession
    {
        private static DataRowCollection dados_usuario;
        public static DataRowCollection DadosUsuario
        {
            get { return dados_usuario; }
            set { dados_usuario = value; }
        }
    }
}
