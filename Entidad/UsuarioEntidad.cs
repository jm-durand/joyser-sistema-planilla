using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class UsuarioEntidad
    {
        public string Usuario { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int CodPerfilAcceso { get; set; }
        public int Estado { get; set; }
        public string Contrasena { get; set; }
        public string RepiteContrasena { get; set; }
        public int CodUsuario { get; set; }
    }
}
