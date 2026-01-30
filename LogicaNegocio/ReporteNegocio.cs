using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;

namespace LogicaNegocio
{
    public class ReporteNegocio
    {
        ReporteDatos reporteDatos = new ReporteDatos();
        public DataTable GenerarReportePlanilla(string FechaInicial, string FechaFinal, string MesPeriodo, string AnoPeriodo, int CodTipoPlanilla, int CodTipoBusqueda)
        {
            return reporteDatos.GenerarReportePlanilla(FechaInicial, FechaFinal, MesPeriodo, AnoPeriodo, CodTipoPlanilla, CodTipoBusqueda);
        }
        public DataTable GenerarReportePlanillaEventuales(string FechaInicial, string FechaFinal, string MesPeriodo, string AnoPeriodo, int CodTipoBusqueda)
        {
            return reporteDatos.GenerarReportePlanillaEventuales(FechaInicial, FechaFinal, MesPeriodo, AnoPeriodo, CodTipoBusqueda);
        }
        public DataTable GenerarReportePlanillaEmpleados(string FechaInicial, string FechaFinal, string MesPeriodo, string AnoPeriodo, int CodTipoBusqueda)
        {
            return reporteDatos.GenerarReportePlanillaEmpleados(FechaInicial, FechaFinal, MesPeriodo, AnoPeriodo, CodTipoBusqueda);
        }
        public DataTable GenerarReportePlanillaDestajo(string FechaInicial, string FechaFinal, string MesPeriodo, string AnoPeriodo, int CodTipoBusqueda)
        {
            return reporteDatos.GenerarReportePlanillaDestajo(FechaInicial, FechaFinal, MesPeriodo, AnoPeriodo, CodTipoBusqueda);
        }
        public DataTable ObtenerPlanillasTrabajadorPorFecha(DateTime FechaInicial, DateTime FechaFinal, int CodTrabajador)
        {
            return reporteDatos.ObtenerPlanillasTrabajadorPorFecha(FechaInicial, FechaFinal, CodTrabajador);
        }
        public DataTable ObtenerPlanillasTrabajadorReporteIndividual(DateTime FechaInicial, DateTime FechaFinal, int CodTrabajador, int CodTipoPlanilla)
        {
            return reporteDatos.ObtenerPlanillasTrabajadorReporteIndividual(FechaInicial, FechaFinal, CodTrabajador, CodTipoPlanilla);
        }
        public DataTable ObtenerReporteBoletaPagoIndividual(int CodPlanilla)
        {
            return reporteDatos.ObtenerReporteBoletaPagoIndividual(CodPlanilla);
        }
        public DataTable ObtenerReporteBoletaPagoIndividualEmpleados(int CodPlanilla)
        {
            return reporteDatos.ObtenerReporteBoletaPagoIndividualEmpleados(CodPlanilla);
        }
        public DataTable ObtenerReporteReciboEventualIndividual(int CodPlanilla)
        {
            return reporteDatos.ObtenerReporteReciboEventualIndividual(CodPlanilla);
        }
        public DataTable ObtenerReporteBoletaPagoMasivo(DateTime FechaInicial, DateTime FechaFinal)
        {
            return reporteDatos.ObtenerReporteBoletaPagoMasivo(FechaInicial, FechaFinal);
        }
        public DataTable ObtenerReporteBoletaPagoMasivoEventuales(DateTime FechaInicial, DateTime FechaFinal)
        {
            return reporteDatos.ObtenerReporteBoletaPagoMasivoEventuales(FechaInicial, FechaFinal);
        }
        public DataTable ObtenerReporteBoletaPagoMasivoDestajo(DateTime FechaInicial, DateTime FechaFinal)
        {
            return reporteDatos.ObtenerReporteBoletaPagoMasivoDestajo(FechaInicial, FechaFinal);
        }
        public DataTable ObtenerReporteBoletaPagoMasivoEmpleados(DateTime FechaInicial, DateTime FechaFinal)
        {
            return reporteDatos.ObtenerReporteBoletaPagoMasivoEmpleados(FechaInicial, FechaFinal);
        }
        public DataTable ObtenerDatosGenerales()
        {
            return reporteDatos.ObtenerDatosGenerales();
        }
        public DataTable ObtenerReporteBoletaPagoIndividualEventuales(int CodPlanilla)
        {
            return reporteDatos.ObtenerReporteBoletaPagoIndividualEventuales(CodPlanilla);
        }
        public DataTable ObtenerReporteBoletaPagoPlanillaEventuales(int CodPlanilla)
        {
            return reporteDatos.ObtenerReporteBoletaPagoPlanillaEventuales(CodPlanilla);
        }
        public DataTable GenerarReportePagoCheques(string CodEstado, string CodTipoPago, string FechaInicio, string FechaFin, string CodProyectoPlanilla)
        {
            return reporteDatos.GenerarReportePagoCheques(CodEstado, CodTipoPago, FechaInicio, FechaFin, CodProyectoPlanilla);
        }
    }
}
