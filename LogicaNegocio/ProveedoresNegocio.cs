using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidad;

namespace LogicaNegocio
{
    public class ProveedoresNegocio
    {
        ProveedoresDatos proveedoresDatos = new ProveedoresDatos();
        public DataTable BuscarProveedores(string TextoBuscar)
        {
            return proveedoresDatos.BuscarProveedores(TextoBuscar);
        }
        public DataTable CargarDatosProveedor(int CodProveedor)
        {
            return proveedoresDatos.CargarDatosProveedor(CodProveedor);
        }
        public DataTable CargarRubro()
        {
            return proveedoresDatos.CargarRubro();
        }
        public String RegistrarProveedor(ProveedoresEntidad proveedoresEntidad)
        {
            return proveedoresDatos.RegistrarProveedor(proveedoresEntidad);
        }
        public String ActualizarProveedor(ProveedoresEntidad proveedoresEntidad)
        {
            return proveedoresDatos.ActualizarProveedor(proveedoresEntidad);
        }
        public string EliminarProveedor(int CodProveedor, int CodUsuario)
        {
            return proveedoresDatos.EliminarProveedor(CodProveedor, CodUsuario);
        }
        public DataTable ObtenerListadoProveedores(int CodProyecto)
        {
            return proveedoresDatos.ObtenerListadoProveedores(CodProyecto);
        }
        public DataTable CargarProveedores()
        {
            return proveedoresDatos.CargarProveedores();
        }
        public DataTable CargarProyectosPagarProveedores(int CodProveedor)
        {
            return proveedoresDatos.CargarProyectosPagarProveedores(CodProveedor);
        }
        public String RegistrarPagoProveedoresPorCheque(ProveedoresEntidad proveedoresEntidad)
        {
            return proveedoresDatos.RegistrarPagoProveedoresPorCheque(proveedoresEntidad);
        }
        public String RegistrarPagoProveedoresSinProyectoPorCheque(ProveedoresEntidad proveedoresEntidad)
        {
            return proveedoresDatos.RegistrarPagoProveedoresSinProyectoPorCheque(proveedoresEntidad);
        }
        public DataTable ObtenerProyectosProveedor(int CodProveedor)
        {
            return proveedoresDatos.ObtenerProyectosProveedor(CodProveedor);
        }
        public DataTable ObtenerPagosRealizados(int CodProveedor, int CodProyecto)
        {
            return proveedoresDatos.ObtenerPagosRealizados(CodProveedor, CodProyecto);
        }
        public DataTable ObtenerDatosPagoProveedor(int CodPagoProveedor)
        {
            return proveedoresDatos.ObtenerDatosPagoProveedor(CodPagoProveedor);
        }
        public DataTable ObtenerDatosProveedorProyectoPago(int CodProveedor, int CodProyecto)
        {
            return proveedoresDatos.ObtenerDatosProveedorProyectoPago(CodProveedor, CodProyecto);
        }
        public String RegistrarPagoProveedor(ProveedoresEntidad proveedoresEntidad)
        {
            return proveedoresDatos.RegistrarPagoProveedor(proveedoresEntidad);
        }
        public String ModificarPagoProveedor(ProveedoresEntidad proveedoresEntidad)
        {
            return proveedoresDatos.ModificarPagoProveedor(proveedoresEntidad);
        }
        public String EliminarPagoProveedores(ProveedoresEntidad proveedoresEntidad)
        {
            return proveedoresDatos.EliminarPagoProveedores(proveedoresEntidad);
        }
    }
}
