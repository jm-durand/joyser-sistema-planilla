using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class Conexion
    {
        public string GetConexion()
        {
            return ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
        }
    }
}
