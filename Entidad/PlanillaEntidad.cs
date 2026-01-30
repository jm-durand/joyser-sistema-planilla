using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class PlanillaEntidad
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public string MesPeriodo { get; set; }
        public string AnoPeriodo { get; set; }
        public double DiasLaborados { get; set; }
        public double DiasDominical { get; set; }
        public double AsignacionFamiliar { get; set; }
        public double Reintegro { get; set; }
        public double Bonificacion { get; set; }
        public double OtrosIngresos { get; set; }
        public double HorasExtraSimple { get; set; }
        public double HorasExtra60 { get; set; }
        public double HorasExtra100 { get; set; }
        public double Buc { get; set; }
        public double Pasajes { get; set; }
        public double Vacacional { get; set; }
        public double VacacionesTruncas { get; set; }
        public double Gratificacion { get; set; }
        public double Ley29351 { get; set; }
        public double Liquidacion { get; set; }
        public double OtrosBeneficios { get; set; }
        public double BonifacionExtraSalud { get; set; }
        public double BonificacionExtraPension { get; set; }
        public double Snp { get; set; }
        public double AporteObligatorio { get; set; }
        public double ComisionFlujo { get; set; }
        public double ComisionMixta { get; set; }
        public double PrimaSeguro { get; set; }
        public double AporteComplementario { get; set; }
        public double Conafovicer { get; set; }
        public double AporteSindical { get; set; }
        public double EsSaludVida { get; set; }
        public double Renta5taCategoria { get; set; }
        public double Eps { get; set; }
        public double Prestamos { get; set; }
        public double OtrosDescuentos { get; set; }
        public double EsSalud { get; set; }
        public double AporteComplementarioAFP { get; set; }
        public double SctrSalud { get; set; }
        public double SctrPension { get; set; }
        public double TotalIngresos { get; set; }
        public double TotalBeneficios { get; set; }
        public double TotalDescuentos { get; set; }
        public double TotalAporteEmpresa { get; set; }
        public double TotalPagarTrabajador { get; set; }
        public double TotalCostoTrabajador { get; set; }
        public double TotalPagarEventual { get; set; }
        public int FlagActivo { get; set; }
        public int CodUsuarioRegistro { get; set; }
        public int CodUsuarioModificacion { get; set; }
        public int CodTrabajador { get; set; }
        public int CodPlanillaConstruccion { get; set; }
        public int CodPlanillaEventual { get; set; }
        public int CodPlanillaEmpleados { get; set; }
        public int CodServicio { get; set; }
        public string Cantidad { get; set; }
        public int CodProyectoPlanilla { get; set; }
    }
}
