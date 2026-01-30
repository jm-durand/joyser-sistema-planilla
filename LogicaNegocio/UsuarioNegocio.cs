using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using AccesoDatos;

namespace LogicaNegocio
{
    public class UsuarioNegocio
    {
        UsuarioDatos usuarioDatos = new UsuarioDatos();
        public DataTable Login(UsuarioEntidad usuarioEntidad)
        {
            return usuarioDatos.Login(usuarioEntidad);
        }
        public DataTable BuscarUsuarioInterno(string TextoBuscar)
        {
            return usuarioDatos.BuscarUsuarioInterno(TextoBuscar);
        }
        public DataTable CargarPerfilAcceso()
        {
            return usuarioDatos.CargarPerfilAcceso();
        }
        public String RegistrarUsuarioInterno(UsuarioEntidad usuarioEntidad)
        {
            return usuarioDatos.RegistrarUsuarioInterno(usuarioEntidad);
        }
        public String ActualizarUsuarioInterno(UsuarioEntidad usuarioEntidad, int FlagCambiarContrasena)
        {
            return usuarioDatos.ActualizarUsuarioInterno(usuarioEntidad, FlagCambiarContrasena);
        }
        public DataTable CargarDatosUsuarioInterno(int codUsuario)
        {
            return usuarioDatos.CargarDatosUsuarioInterno(codUsuario);
        }
    }
}
