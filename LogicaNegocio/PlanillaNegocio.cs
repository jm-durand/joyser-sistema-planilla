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
    public class PlanillaNegocio
    {
        PlanillaDatos planillaDatos = new PlanillaDatos();
        public DataTable CargarTipoPlanilla()
        {
            return planillaDatos.CargarTipoPlanilla();
        }
        public DataTable CargarDatosTrabajadorPlanilla(int CodTrabajador, int CodTipoPlanilla)
        {
            return planillaDatos.CargarDatosTrabajadorPlanilla(CodTrabajador, CodTipoPlanilla);
        }
        public DataTable CargarDatosTrabajadorEventualPlanilla(int CodTrabajador)
        {
            return planillaDatos.CargarDatosTrabajadorEventualPlanilla(CodTrabajador);
        }
        public DataTable CargarDatosServicio(int CodServicio)
        {
            return planillaDatos.CargarDatosServicio(CodServicio);
        }
        public DataTable ObtenerParametrosIngresoPlanilla(int CodTrabajador, int cantDiasLaborados, int CodTipoPlanilla)
        {
            return planillaDatos.ObtenerParametrosIngresoPlanilla(CodTrabajador, cantDiasLaborados, CodTipoPlanilla);      
        }
        public DataTable ObtenerParametrosBeneficiosPlanilla(int CodTrabajador, int cantDiasLaborados, int Periodo, int CodTipoPlanilla)
        {
            return planillaDatos.ObtenerParametrosBeneficiosPlanilla(CodTrabajador, cantDiasLaborados, Periodo, CodTipoPlanilla);
        }
        public DataTable ObtenerParametrosBeneficiosExtrasPlanilla(int CodTrabajador, int cantDiasLaborados, int CodTipoPlanilla)
        {
            return planillaDatos.ObtenerParametrosBeneficiosExtrasPlanilla(CodTrabajador, cantDiasLaborados, CodTipoPlanilla);
        }
        public DataTable ObtenerParametrosBeneficiosEspecialePlanilla(int CodTrabajador, int cantDiasAfectos, int Periodo, int CodTipoPlanilla)
        {
            return planillaDatos.ObtenerParametrosBeneficiosEspecialePlanilla(CodTrabajador, cantDiasAfectos, Periodo, CodTipoPlanilla);
        }
        public DataTable ObtenerParametrosDescuentosPlanilla(int CodTrabajador, int cantDiasLaborados, int cantDiasDominical, double TotalAfecto, int CodTipoPlanilla)
        {
            return planillaDatos.ObtenerParametrosDescuentosPlanilla(CodTrabajador, cantDiasLaborados, cantDiasDominical, TotalAfecto, CodTipoPlanilla);
        }
        public DataTable ObtenerParametrosAportacionesEmpleadorPlanilla(int CodTrabajador, int cantDiasLaborados, double TotalAfecto, int CodTipoPlanilla)
        {
            return planillaDatos.ObtenerParametrosAportacionesEmpleadorPlanilla(CodTrabajador, cantDiasLaborados, TotalAfecto, CodTipoPlanilla);
        }
        public String RegistrarPlanillaConstruccion(PlanillaEntidad planillaEntidad)
        {
            return planillaDatos.RegistrarPlanillaConstruccion(planillaEntidad);
        }
        public String RegistrarPlanillaEmpleados(PlanillaEntidad planillaEntidad)
        {
            return planillaDatos.RegistrarPlanillaEmpleados(planillaEntidad);
        }
        public String ActualizarPlanillaConstruccion(PlanillaEntidad planillaEntidad)
        {
            return planillaDatos.ActualizarPlanillaConstruccion(planillaEntidad);
        }
        public String ActualizarPlanillaEmpleados(PlanillaEntidad planillaEntidad)
        {
            return planillaDatos.ActualizarPlanillaEmpleados(planillaEntidad);
        }
        public String RegistrarPlanillaEventuales(PlanillaEntidad planillaEntidad)
        {
            return planillaDatos.RegistrarPlanillaEventuales(planillaEntidad);
        }
        public String ActualizarPlanillaEventuales(PlanillaEntidad planillaEntidad)
        {
            return planillaDatos.ActualizarPlanillaEventuales(planillaEntidad);
        }
        public DataTable ObtenerPlanillasReciente(string FechaInicio, string FechaFin)
        {
            return planillaDatos.ObtenerPlanillasReciente(FechaInicio, FechaFin);
        }
        public DataTable ObtenerPlanillasEventualesReciente(string FechaInicio, string FechaFin)
        {
            return planillaDatos.ObtenerPlanillasEventualesReciente(FechaInicio, FechaFin);
        }
        public DataTable ObtenerPlanillasDestajoRecientes(string FechaInicio, string FechaFin)
        {
            return planillaDatos.ObtenerPlanillasDestajoRecientes(FechaInicio, FechaFin);
        }
        public DataTable ObtenerPlanillasEmpleadosReciente(string FechaInicio, string FechaFin)
        {
            return planillaDatos.ObtenerPlanillasEmpleadosReciente(FechaInicio, FechaFin);
        }
        public DataTable ObtenerDatosPlanilla(int codPlanilla)
        {
            return planillaDatos.ObtenerDatosPlanilla(codPlanilla);
        }
        public DataTable ObtenerDatosPlanillaEventuales(int codPlanilla)
        {
            return planillaDatos.ObtenerDatosPlanillaEventuales(codPlanilla);
        }
        public DataTable ObtenerDatosPlanillaEmpleados(int codPlanilla)
        {
            return planillaDatos.ObtenerDatosPlanillaEmpleados(codPlanilla);
        }
        public String RegistrarPlanillaConstruccionEventuales(PlanillaEntidad planillaEntidad)
        {
            return planillaDatos.RegistrarPlanillaConstruccionEventuales(planillaEntidad);
        }
        public String ActualizarPlanillaConstruccionEventuales(PlanillaEntidad planillaEntidad)
        {
            return planillaDatos.ActualizarPlanillaConstruccionEventuales(planillaEntidad);
        }
        public DataTable ObtenerDatosPlanillaConstruccionEventuales(int codPlanilla)
        {
            return planillaDatos.ObtenerDatosPlanillaConstruccionEventuales(codPlanilla);
        }
    }
}
