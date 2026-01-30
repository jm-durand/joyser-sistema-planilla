USE master
GO
DROP DATABASE SistemaPlanilla
GO
CREATE DATABASE SistemaPlanilla
GO
USE SistemaPlanilla
GO
CREATE TABLE TipoPlanilla(
CodTipoPlanilla INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DescPlanilla VARCHAR(100),
FlagActivo BIT
)
GO
CREATE TABLE ErrorProcedure(
CodError INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Mensaje VARCHAR(MAX),
NombreProcedure VARCHAR(200),
LineaError VARCHAR(800),
FechaError DATETIME
)
GO
CREATE TABLE TipoDocumentoIdentidad(
CodTipDocumentoIdent INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DescTipDocumentoIdent VARCHAR(100),
FlagActivo BIT
)
GO
CREATE TABLE EstadoCivil(
CodEstadoCivil INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DescEstadoCivil VARCHAR(100),
)
GO
CREATE TABLE PerfilAcceso(
CodPerfilAcceso INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DescPerfilAcceso VARCHAR(100),
FlagActivo BIT
)
GO
CREATE TABLE Banco(
CodBanco INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DescBanco VARCHAR(100),
FlagActivo BIT
)
GO
CREATE TABLE TipoAportacion(
CodTipoAportacion INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DescTipoAportacion VARCHAR(100),
AO NUMERIC(19,8) NOT NULL,
CO NUMERIC(19,8) NOT NULL,
PS NUMERIC(19,8) NOT NULL,
AC NUMERIC(19,8) NOT NULL,
MI NUMERIC(19,8) NOT NULL,
FlagActivo BIT
)
GO
CREATE TABLE LaborTrabajo(
CodLaborTrabajo INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DescLaborTrabajo VARCHAR(100),
FlagActivo BIT
)
GO
CREATE TABLE Cargo(
CodCargo INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DescCargo VARCHAR(100),
FlagActivo BIT
)
GO
CREATE TABLE Usuario(
CodUsuario INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Usuario VARCHAR(50) NOT NULL,
Contrasena VARCHAR(20) NULL,
Nombres VARCHAR(100) NOT NULL,
Apellidos VARCHAR(100) NOT NULL,
CodPerfilAcceso INT FOREIGN KEY REFERENCES PerfilAcceso(CodPerfilAcceso), 
FlagActivo BIT
)
GO
CREATE TABLE TipoAsignacion(
CodTipoAsignacion INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DescTipoAsignacion VARCHAR(100) NOT NULL,
)
GO
CREATE TABLE TipoParametro(
CodTipoParametro INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DescTipoParametro VARCHAR(100) NOT NULL,
)
CREATE TABLE UnidadMedidaParametro(
CodUnidadMedidaParametro INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DescUnidadMedidaParametro VARCHAR(100) NOT NULL,
)
GO
CREATE TABLE Asignacion(
CodAsignacion INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DescAsignacion VARCHAR(100) NOT NULL,
CodTipoAsignacion INT FOREIGN KEY REFERENCES TipoAsignacion(CodTipoAsignacion),
FlagActivo BIT
)
GO
--CREATE TABLE PerfilPlanilla(
--CodPerfilPlanilla INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
--DescPerfilPlanilla VARCHAR(100) NOT NULL,
--Jornal NUMERIC(14,2),
--FlagBUC BIT,
--PercentBUC NUMERIC(14,2),
--FlagMovilidad BIT,
--DiaMovilidad NUMERIC(14,2),
--FlagDominical BIT,
--DiasDominical INT,
--FlagBonifLey29351 BIT,
--PercentBonifLey29351 NUMERIC(14,2),
--FlagVacaciones BIT,
--PercentVacaciones NUMERIC(14,2),
--FlagCTS BIT,
--PercentCTS NUMERIC(14,2),
--FlagGratificacion BIT,
--SemestreGratificacion CHAR(1),
--FlagHoraExtra60 BIT,
--FlagHoraExtra100 BIT,
--FlagAsigEscolar BIT,
--PercentAsigEscolar NUMERIC(14,2),
--FlagONP BIT,
--PercentONP NUMERIC(14,2),
--FlagAFP BIT,
--CodAFP INT FOREIGN KEY REFERENCES AFPS(CodAFP),
--FlagTardardanzaAcum BIT,
--FlagEsSalud BIT,
--PercentEsSalud NUMERIC(14,2),
--FlagSCTR BIT,
--PercentSCTR NUMERIC(14,2),
--FlagComplAFP BIT,
--PercentComplAFP NUMERIC(14,2),
--FlagActivo BIT,
--FechaRegistro DATETIME NOT NULL,
--CodUsuarioRegistro INT FOREIGN KEY REFERENCES Usuario(CodUsuario) NOT NULL,
--FechaModificacion DATETIME NULL,
--CodUsuarioModificacion INT FOREIGN KEY REFERENCES Usuario(CodUsuario) NULL
--)
CREATE TABLE PerfilPlanilla(
CodPerfilPlanilla INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DescPerfilPlanilla VARCHAR(100) NOT NULL,
Jornal NUMERIC(14,2),
FlagActivo BIT,
FechaRegistro DATETIME NOT NULL,
CodUsuarioRegistro INT FOREIGN KEY REFERENCES Usuario(CodUsuario) NOT NULL,
FechaModificacion DATETIME NULL,
CodUsuarioModificacion INT FOREIGN KEY REFERENCES Usuario(CodUsuario) NULL
)
GO
CREATE TABLE Parametro(
CodParametro INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
DescParametro VARCHAR(100) NOT NULL,
CodTipoParametro INT FOREIGN KEY REFERENCES TipoParametro(CodTipoParametro),
CodUnidadMedidaParametro INT FOREIGN KEY REFERENCES UnidadMedidaParametro(CodUnidadMedidaParametro),
FlagActivo BIT
)
GO
CREATE TABLE PerfilPlanillaParametro(
CodPerfilPlanillaParametro INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
CodPerfilPlanilla INT FOREIGN KEY REFERENCES PerfilPlanilla(CodPerfilPlanilla) NOT NULL,
CodParametro INT FOREIGN KEY REFERENCES Parametro(CodParametro) NOT NULL, 
CampoParametro NUMERIC(14,2) NULL,
FlagActivo BIT
)
GO
CREATE TABLE Trabajador(
CodTrabajador INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
CodTipDocumentoIdent INT FOREIGN KEY REFERENCES TipoDocumentoIdentidad(CodTipDocumentoIdent) NOT NULL,
DocumentoIdentidad VARCHAR(50) NOt NULL,
Nombres VARCHAR(100) NOT NULL,
ApellidoPaterno VARCHAR(100) NOT NULL,
ApellidoMaterno VARCHAR(100) NULL,
FechaNacimiento DATE NULL,
Sexo CHAR(1) NULL,
CodEstadoCivil INT FOREIGN KEY REFERENCES EstadoCivil(CodEstadoCivil) NOT NULL,
CantidadHijos INT,
FechaIngreso DATE NOT NULL,
FechaCese DATE NULL,
HaberMensual NUMERIC(14,2) NOT NULL,
CodTipoAportacion INT FOREIGN KEY REFERENCES TipoAportacion(CodTipoAportacion) NULL,
NroCUSPP VARCHAR(100),
CodLaborTrabajo INT FOREIGN KEY REFERENCES LaborTrabajo(CodLaborTrabajo) NULL,
CodCargoTrabajo INT FOREIGN KEY REFERENCES Cargo(CodCargo) NULL,
CodFinanciera INT FOREIGN KEY REFERENCES Banco(CodBanco) NULL,
NroCuentaFinanciera VARCHAR(100) NULL,
NroCuentaCTS VARCHAR(100) NULL,
CodTipoPlanilla INT FOREIGN KEY REFERENCES TipoPlanilla(CodTipoPlanilla) NULL,
CodPerfilPlanilla INT FOREIGN KEY REFERENCES PerfilPlanilla(CodPerfilPlanilla) NULL,
RutaFoto VARCHAR(150) NULL,
FlagActivo BIT,
FechaRegistro DATETIME NOT NULL,
CodUsuarioRegistro INT FOREIGN KEY REFERENCES Usuario(CodUsuario) NOT NULL,
FechaModificacion DATETIME NULL,
CodUsuarioModificacion INT FOREIGN KEY REFERENCES Usuario(CodUsuario) NULL
)

CREATE TABLE PlanillaConstruccion(
CodPlanillaConstruccion INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
CodTrabajador INT FOREIGN KEY REFERENCES Trabajador(CodTrabajador) NOT NULL,
CodPerfilPlanilla INT FOREIGN KEY REFERENCES PerfilPlanilla(CodPerfilPlanilla) NOT NULL,
FechaInicio DATE,
FechaFin DATE,
PeriodoMes CHAR(2),
PeriodoAno VARCHAR(4),
DiasLaborados NUMERIC(14,2),
DiasDomingosFeriados NUMERIC(14,2),
AsignacionFamiliar NUMERIC(14,2),
Reintegro NUMERIC(14,2),
Bonificacion NUMERIC(14,2),
HorasSimple NUMERIC(14,2),
HorasExtras60 NUMERIC(14,2),
HorasExtras100 NUMERIC(14,2),
BUC NUMERIC(14,2),
Pasajes NUMERIC(14,2),
Vacacional NUMERIC(14,2),
Gratificacion NUMERIC(14,2),
Liquidacion NUMERIC(14,2),
BonificacionExtraSalud NUMERIC(14,2),
BonificacionExtraPension NUMERIC(14,2),
SNP NUMERIC(14,2),
AporteObligatorio NUMERIC(14,2),
ComisionFlujo NUMERIC(14,2),
ComisionMixta NUMERIC(14,2),
PrimaSeguro NUMERIC(14,2),
AporteComplementario NUMERIC(14,2),
Conafovicer NUMERIC(14,2),
AporteSindical NUMERIC(14,2),
EsSaludVida NUMERIC(14,2),
Renta5taCategoria NUMERIC(14,2),
EPS NUMERIC(14,2),
OtrosDctos NUMERIC(14,2),
EsSalud NUMERIC(14,2),
AportComplementarioAFP NUMERIC(14,2),
SCTRSalud NUMERIC(14,2),
SCTRPension NUMERIC(14,2), 
TotalIngresos NUMERIC(14,2), 
TotalBeneficios NUMERIC(14,2), 
TotalDescuentos NUMERIC(14,2), 
TotalAporteEmpresa NUMERIC(14,2),
TotalPagarTrabajador NUMERIC(14,2), 
TotalCostoTrabajador NUMERIC(14,2), 
FlagActivo BIT,
FechaRegistro DATETIME NOT NULL,
CodUsuarioRegistro INT FOREIGN KEY REFERENCES Usuario(CodUsuario) NOT NULL,
FechaModificacion DATETIME NULL,
CodUsuarioModificacion INT FOREIGN KEY REFERENCES Usuario(CodUsuario) NULL
)
---------------------------------------------------------
----PROCEDIMIENTOS---------------------------------------
---------------------------------------------------------

CREATE PROCEDURE PA_sel_CargarParametrosMantenimientoPerfilPlanilla
AS
SELECT para.CodParametro,para.DescParametro,tip.CodTipoParametro,unid.DescUnidadMedidaParametro FROM Parametro para 
INNER JOIN TipoParametro tip ON (tip.CodTipoParametro=para.CodTipoParametro) 
INNER JOIN UnidadMedidaParametro unid ON (unid.CodUnidadMedidaParametro=para.CodUnidadMedidaParametro)
WHERE para.FlagActivo=1

---------------------------------------------------------
CREATE PROCEDURE PA_ins_GuardarPerfilPlanilla
@NombrePerfil VARCHAR(100),
@Jornal NUMERIC(14,2),
@CodUsuarioRegistro INT,
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

IF NOT EXISTS(SELECT 1 FROM PerfilPlanilla WHERE DescPerfilPlanilla=@NombrePerfil AND Jornal=@Jornal)
BEGIN
	INSERT INTO PerfilPlanilla(DescPerfilPlanilla,Jornal,FlagActivo,FechaRegistro,CodUsuarioRegistro)
	VALUES(@NombrePerfil,@Jornal,1,GETDATE(),@CodUsuarioRegistro)

	DECLARE @CodigoAutoGenerado VARCHAR(10)
	SET @CodigoAutoGenerado=(SELECT CodPerfilPlanilla FROM PerfilPlanilla WHERE DescPerfilPlanilla=@NombrePerfil AND Jornal=@Jornal)

	SET @Mensaje='EXITO|'+@CodigoAutoGenerado

END
ELSE
BEGIN
	SET @Mensaje='REPETIDO'
END

COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 
---------------------------------------------------------
CREATE PROCEDURE PA_ins_GuardarParametroPerfilPlanilla
@CodPerfil INT,
@CodParametro INT,
@CampoParametro VARCHAR(10),
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

IF NOT EXISTS(SELECT 1 FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfil AND CodParametro=@CodParametro)
BEGIN
	INSERT INTO PerfilPlanillaParametro(CodPerfilPlanilla,CodParametro,CampoParametro,FlagActivo)
	VALUES(@CodPerfil,@CodParametro,@CampoParametro,1)

	SET @Mensaje='EXITO'

END

COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 
---------------------------------------------------------

CREATE PROCEDURE PA_sel_CargarPerfilesPlanilla
AS
SELECT CodPerfilPlanilla,DescPerfilPlanilla FROM PerfilPlanilla WHERE FlagActivo=1

---------------------------------------------------------
CREATE PROCEDURE PA_ins_GuardarTrabajador
@CodTipoDocumentoIdentidad INT,
@DocumentoIdentidad VARCHAR(20),
@Nombres VARCHAR(100),
@ApellidoPaterno VARCHAR(100),
@ApellidoMaterno VARCHAR(100),
@FechaNacimiento DATE,
@Sexo CHAR(1),
@CodEstadoCivil INT,
@FechaIngreso DATE,
@FechaCese DATE,
@HaberMensual NUMERIC(14,8),
@CodTipoAportacion INT,
@NroCuspp VARCHAR(50),
@CodTipoTrabajo INT,
@CodCargo INT,
@CodBanco INT,
@NumeroCuentaBanco VARCHAR(50),
@NumeroCuentaCTS VARCHAR(50),
@CodTipoPlanilla INT,
@CodPerfilPlanilla INT,
@CodUsuarioRegistro INT,
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

IF NOT EXISTS(SELECT 1 FROM Trabajador WHERE CodTipDocumentoIdent=@CodTipoDocumentoIdentidad AND DocumentoIdentidad=@DocumentoIdentidad AND Nombres=@Nombres AND ApellidoPaterno=@ApellidoPaterno) 
BEGIN
	INSERT INTO Trabajador(CodTipDocumentoIdent,DocumentoIdentidad,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,Sexo,CodEstadoCivil,
	FechaIngreso,FechaCese,HaberMensual,CodTipoAportacion,NroCUSPP,CodLaborTrabajo,CodCargoTrabajo,CodFinanciera,NroCuentaFinanciera,NroCuentaCTS,CodTipoPlanilla,CodPerfilPlanilla,FlagActivo,FechaRegistro,CodUsuarioRegistro)
	VALUES (@CodTipoDocumentoIdentidad,@DocumentoIdentidad,@Nombres,@ApellidoPaterno,@ApellidoMaterno,@FechaNacimiento,@Sexo,@CodEstadoCivil,
	@FechaIngreso,@FechaCese,@HaberMensual,@CodTipoAportacion,@NroCuspp,@CodTipoTrabajo,@CodCargo,@CodBanco,@NumeroCuentaBanco,@NumeroCuentaCTS,@CodTipoPlanilla,@CodPerfilPlanilla,1,GETDATE(),@CodUsuarioRegistro)

	SET @Mensaje='EXITO'
END
ELSE
BEGIN
	SET @Mensaje='REPETIDO'
END


COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 
---------------------------------------------------------
CREATE PROCEDURE PA_sel_CargarTipoPlanilla
AS
SELECT CodTipoPlanilla,DescPlanilla FROM TipoPlanilla WHERE FlagActivo=1
---------------------------------------------------------

ALTER PROCEDURE PA_gen_ObtenerListaTrabajadores
AS
SELECT CodTrabajador,DocumentoIdentidad+' - '+UPPER(ApellidoPaterno+' '+ApellidoMaterno)+', '+Nombres AS NombreTrabajador  
FROM Trabajador WHERE FlagActivo=1 AND CAST(FechaCese AS DATE)>CAST(GETDATE() AS DATE)
---------------------------------------------------------
ALTER PROCEDURE PA_sel_CargarDatosPlanillaTrabajador
@CodTrabajador INT
AS
SELECT carg.DescCargo,trab.HaberMensual,perf.Jornal,CAST((perf.Jornal)/8 AS NUMERIC(14,2)) AS HESimple,CAST((((perf.Jornal/8)*0.6)+(perf.Jornal/8)) AS NUMERIC(14,2)) AS HE60,CAST((((perf.Jornal/8)*1)+(perf.Jornal/8)) AS NUMERIC(14,2)) AS HE100 FROM Trabajador trab 
INNER JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo) 
LEFT JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trab.CodPerfilPlanilla)
WHERE trab.CodTrabajador=@CodTrabajador
---------------------------------------------------------

ALTER PROCEDURE PA_gen_ObtenerIngresosPlanillaTrabajador
@CodTrabajador INT,
@CantidadDias INT
AS

DECLARE @CodPerfilPlanilla INT
SET @CodPerfilPlanilla=(SELECT CodPerfilPlanilla FROM Trabajador WHERE CodTrabajador=@CodTrabajador)

SELECT DISTINCT CAST((perf.Jornal)*@CantidadDias AS NUMERIC(14,2)) AS TotalHaber,
CAST((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=5) AS NUMERIC(14,2)) AS BUCPorDia,
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=5)/100)*perf.Jornal*@CantidadDias AS NUMERIC(14,2)) AS BUCTotal, 
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=4)) AS NUMERIC(14,2)) AS PasajeDia,
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=4))*@CantidadDias AS NUMERIC(14,2)) AS PasajeTotal
FROM Trabajador trab 
INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trab.CodPerfilPlanilla)
INNER JOIN PerfilPlanillaParametro para ON (para.CodPerfilPlanilla=perf.CodPerfilPlanilla)
WHERE trab.CodTrabajador=@CodTrabajador

--------------------------------------------------------------
ALTER PROCEDURE PA_gen_ObtenerBeneficiosPlanillaTrabajador
@CodTrabajador INT,
@IntMesPeriodo INT,
@CantidadDias INT
AS

DECLARE @CodPerfilPlanilla INT
SET @CodPerfilPlanilla=(SELECT CodPerfilPlanilla FROM Trabajador WHERE CodTrabajador=@CodTrabajador)

SELECT DISTINCT CAST((perf.Jornal)*@CantidadDias AS NUMERIC(14,2)) AS TotalHaber,
CAST((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=3) AS NUMERIC(14,2)) AS VacacionalPorDia,
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=3)/100)*perf.Jornal*@CantidadDias AS NUMERIC(14,2)) AS VacacionalCTotal, 
CAST((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=15) AS NUMERIC(14,2)) AS LiquidadicionDia,
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=15)/100)*perf.Jornal*@CantidadDias AS NUMERIC(14,2)) AS LiquidadicionTotal,
CAST((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=(CASE WHEN @IntMesPeriodo BETWEEN 1 AND 7 THEN 21 ELSE 22 END)) AS NUMERIC(14,2)) AS GratificacionPorDia,
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=(CASE WHEN @IntMesPeriodo BETWEEN 1 AND 7 THEN 21 ELSE 22 END))/100)*perf.Jornal*@CantidadDias AS NUMERIC(14,2)) AS GratificacionTotal
FROM Trabajador trab 
INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trab.CodPerfilPlanilla)
INNER JOIN PerfilPlanillaParametro para ON (para.CodPerfilPlanilla=perf.CodPerfilPlanilla)
WHERE trab.CodTrabajador=@CodTrabajador

--------------------------------------------------------------
ALTER PROCEDURE PA_gen_ObtenerBeneficiosBonifExtraPlanillaTrabajador
@CodTrabajador INT,
@CantidadDias INT
AS

DECLARE @CodPerfilPlanilla INT
SET @CodPerfilPlanilla=(SELECT CodPerfilPlanilla FROM Trabajador WHERE CodTrabajador=@CodTrabajador)

SELECT DISTINCT CAST((perf.Jornal)*@CantidadDias AS NUMERIC(14,2)) AS TotalHaber,
CAST((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=17) AS NUMERIC(14,2)) AS BonifEsSaludPorDia,
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=17)/100)*perf.Jornal*@CantidadDias AS NUMERIC(14,2)) AS BonifEsSaludTotal, 
CAST((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=18) AS NUMERIC(14,2)) AS BonifPensionDia,
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=18)/100)*perf.Jornal*@CantidadDias AS NUMERIC(14,2)) AS BonifPensionTotal
FROM Trabajador trab 
INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trab.CodPerfilPlanilla)
INNER JOIN PerfilPlanillaParametro para ON (para.CodPerfilPlanilla=perf.CodPerfilPlanilla)
WHERE trab.CodTrabajador=@CodTrabajador

--------------------------------------------------------------

ALTER PROCEDURE PA_gen_ObtenerDescuentosPlanillaTrabajador
@CodTrabajador INT,
@TotalAfecto NUMERIC(14,2),
@CantidadDias INT,
@CantidadDiasDominical INT
AS

DECLARE @CodPerfilPlanilla INT
DECLARE @FlagSNP INT
DECLARE @CodTipoAportacion INT
SELECT @CodPerfilPlanilla=CodPerfilPlanilla,@CodTipoAportacion=CodTipoAportacion FROM Trabajador WHERE CodTrabajador=@CodTrabajador
SET @FlagSNP=(SELECT (CASE WHEN CodTipoAportacion=5 THEN 1 ELSE 0 END) FROM Trabajador WHERE CodTrabajador=@CodTrabajador)

SELECT DISTINCT CAST((perf.Jornal)*@CantidadDias AS NUMERIC(14,2)) AS TotalHaber,
CAST((CASE WHEN @FlagSNP=1 THEN (SELECT AO FROM TipoAportacion WHERE CodTipoAportacion=5) ELSE 0 END) AS NUMERIC(14,2)) AS ParametroSNP,
CAST(((CASE WHEN @FlagSNP=1 THEN (SELECT AO FROM TipoAportacion WHERE CodTipoAportacion=5) ELSE 0 END)/100)*(@TotalAfecto-(CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=4))*@CantidadDias AS NUMERIC(14,2)))) AS NUMERIC(14,2)) AS TotalSNP,
CAST((CASE WHEN @FlagSNP=0 THEN (SELECT AO FROM TipoAportacion WHERE CodTipoAportacion=@CodTipoAportacion) ELSE 0 END) AS NUMERIC(14,2)) AS ParametroAFPAporteObligatorio,
CAST(((CASE WHEN @FlagSNP=0 THEN (SELECT AO FROM TipoAportacion WHERE CodTipoAportacion=@CodTipoAportacion) ELSE 0 END)/100)*(@TotalAfecto-(CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=4))*@CantidadDias AS NUMERIC(14,2)))) AS NUMERIC(14,2)) AS TotalAFPAporteObligatorio,
CAST((CASE WHEN @FlagSNP=0 THEN (SELECT CO FROM TipoAportacion WHERE CodTipoAportacion=@CodTipoAportacion) ELSE 0 END) AS NUMERIC(14,2)) AS ParametroAFPComisionFlujo,
CAST(((CASE WHEN @FlagSNP=0 THEN (SELECT CO FROM TipoAportacion WHERE CodTipoAportacion=@CodTipoAportacion) ELSE 0 END)/100)*(@TotalAfecto-(CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=4))*@CantidadDias AS NUMERIC(14,2)))) AS NUMERIC(14,2)) AS TotalAFPComisionFlujo,
CAST((CASE WHEN @FlagSNP=0 THEN (SELECT PS FROM TipoAportacion WHERE CodTipoAportacion=@CodTipoAportacion) ELSE 0 END) AS NUMERIC(14,2)) AS ParametroAFPPrimaSeguro,
CAST(((CASE WHEN @FlagSNP=0 THEN (SELECT PS FROM TipoAportacion WHERE CodTipoAportacion=@CodTipoAportacion) ELSE 0 END)/100)*(@TotalAfecto-(CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=4))*@CantidadDias AS NUMERIC(14,2)))) AS NUMERIC(14,2)) AS TotalAFPPrimaSeguro,
CAST((CASE WHEN @FlagSNP=0 THEN (SELECT AC FROM TipoAportacion WHERE CodTipoAportacion=@CodTipoAportacion) ELSE 0 END) AS NUMERIC(14,2)) AS ParametroAFPAporteComplementario,
CAST(((CASE WHEN @FlagSNP=0 THEN (SELECT AC FROM TipoAportacion WHERE CodTipoAportacion=@CodTipoAportacion) ELSE 0 END)/100)*(@TotalAfecto-(CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=4))*@CantidadDias AS NUMERIC(14,2)))) AS NUMERIC(14,2)) AS TotalAFPAporteComplementario,
CAST((CASE WHEN @FlagSNP=0 THEN (SELECT MI FROM TipoAportacion WHERE CodTipoAportacion=@CodTipoAportacion) ELSE 0 END) AS NUMERIC(14,2)) AS ParametroAFPComisionMixta,
CAST(((CASE WHEN @FlagSNP=0 THEN (SELECT MI FROM TipoAportacion WHERE CodTipoAportacion=@CodTipoAportacion) ELSE 0 END)/100)*(@TotalAfecto-(CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=4))*@CantidadDias AS NUMERIC(14,2)))) AS NUMERIC(14,2)) AS TotalAFPComisionMixta,
CAST((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=11) AS NUMERIC(14,2)) AS ParametroConafovicer,
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=11)/100)*((perf.Jornal*@CantidadDias)+(perf.Jornal*@CantidadDiasDominical)) AS NUMERIC(14,2)) AS TotalConafovicer,
CAST((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=23) AS NUMERIC(14,2)) AS ParametroSindical,
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=23)/100)*@TotalAfecto AS NUMERIC(14,2)) AS TotalSindical,
CAST((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=24) AS NUMERIC(14,2)) AS ParametroEsSaludVida,
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=24)/100)*@TotalAfecto AS NUMERIC(14,2)) AS TotalEsSaludVida
FROM Trabajador trab 
INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trab.CodPerfilPlanilla)
INNER JOIN PerfilPlanillaParametro para ON (para.CodPerfilPlanilla=perf.CodPerfilPlanilla)
WHERE trab.CodTrabajador=@CodTrabajador

--------------------------------------------------------------
CREATE PROCEDURE PA_gen_ObtenerAportacionesEmpleadorPlanillaTrabajador
@CodTrabajador INT,
@TotalPagar NUMERIC(14,8),
@CantidadDias INT
AS

DECLARE @CodPerfilPlanilla INT
SET @CodPerfilPlanilla=(SELECT CodPerfilPlanilla FROM Trabajador WHERE CodTrabajador=@CodTrabajador)

SELECT DISTINCT CAST((perf.Jornal)*@CantidadDias AS NUMERIC(14,2)) AS TotalHaber,
CAST((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=1) AS NUMERIC(14,2)) AS ParametroEsSalud,
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=1)/100)*@TotalPagar AS NUMERIC(14,2)) AS TotalEsSalud, 
CAST((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=14) AS NUMERIC(14,2)) AS ParametroAporComplementarioAFP,
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=14)/100)*@TotalPagar AS NUMERIC(14,2)) AS TotalAporComplementarioAFP,
CAST((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=19) AS NUMERIC(14,2)) AS ParametroSCTRSalud,
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=19)/100)*@TotalPagar AS NUMERIC(14,2)) AS TotalSCTRSalud,
CAST((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=20) AS NUMERIC(14,2)) AS ParametroSCTRPension,
CAST(((SELECT CampoParametro FROM PerfilPlanillaParametro WHERE CodPerfilPlanilla=@CodPerfilPlanilla AND CodParametro=20)/100)*@TotalPagar AS NUMERIC(14,2)) AS TotalSCTRPension
FROM Trabajador trab 
INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trab.CodPerfilPlanilla)
INNER JOIN PerfilPlanillaParametro para ON (para.CodPerfilPlanilla=perf.CodPerfilPlanilla)
WHERE trab.CodTrabajador=@CodTrabajador
--------------------------------------------------------------
ALTER PROCEDURE PA_gen_ObtenerDatosTrabajadorBoletaPlanillaPrevio
@CodTrabajador INT
AS
SELECT trab.Nombres+' '+trab.ApellidoPaterno+' '+trab.ApellidoPaterno AS Nombres, trab.DocumentoIdentidad,car.DescCargo,bank.DescBanco,trab.NroCuentaFinanciera FROM Trabajador trab
INNER JOIN Banco bank ON (bank.CodBanco=trab.CodFinanciera)
INNER JOIN Cargo car ON (car.CodCargo=trab.CodCargoTrabajo)
WHERE trab.CodTrabajador=@CodTrabajador
--------------------------------------------------------------
ALTER PROCEDURE PA_sel_BuscarTrabajador
@TextoBuscar VARCHAR(200)                    
AS                      
 IF @TextoBuscar=''                              
 BEGIN                    
  SELECT TOP 10 trab.CodTrabajador,CONVERT(VARCHAR(10),trab.FechaIngreso,103) AS FechaIngreso,trab.Nombres+' '+trab.ApellidoPaterno+' '+trab.ApellidoMaterno+' - '+trab.DocumentoIdentidad AS Nombres,carg.DescCargo AS Cargo,pla.DescPlanilla AS Planilla,    
  perf.DescPerfilPlanilla,CASE WHEN CAST(trab.FechaCese AS DATE)>CAST(GETDATE() AS DATE) THEN '1' ELSE '0' END AS FlagVigente FROM Trabajador trab    
  INNER JOIN  TipoPlanilla pla ON (trab.CodTipoPlanilla=pla.CodTipoPlanilla)    
  INNER JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo)   
  INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trab.CodPerfilPlanilla) 
  ORDER BY trab.FechaIngreso DESC
 END                              
 ELSE                              
 BEGIN                              
SELECT TOP 10 trab.CodTrabajador,CONVERT(VARCHAR(10),trab.FechaIngreso,103) AS FechaIngreso,trab.Nombres+' '+trab.ApellidoPaterno+' '+trab.ApellidoMaterno+' - '+trab.DocumentoIdentidad AS Nombres,carg.DescCargo AS Cargo,pla.DescPlanilla AS Planilla,    
  perf.DescPerfilPlanilla,CASE WHEN CAST(trab.FechaCese AS DATE)>CAST(GETDATE() AS DATE) THEN '1' ELSE '0' END AS FlagVigente FROM Trabajador trab    
  INNER JOIN  TipoPlanilla pla ON (trab.CodTipoPlanilla=pla.CodTipoPlanilla)    
  INNER JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo) 
  INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trab.CodPerfilPlanilla)    
  WHERE Nombres+' '+ApellidoPaterno+' '+ApellidoMaterno+' '+DocumentoIdentidad+' '+carg.DescCargo+' '+pla.DescPlanilla+' ' LIKE '%'+@TextoBuscar+'%'    
  ORDER BY trab.FechaIngreso DESC                     
 END 
--------------------------------------------------------------
ALTER PROCEDURE PA_sel_BuscarPerfilPlanilla
@TextoBuscar VARCHAR(200)                    
AS                      
 IF @TextoBuscar=''                              
 BEGIN                    
  SELECT TOP 10 perf.CodPerfilPlanilla,CONVERT(VARCHAR(10),perf.FechaRegistro,103) AS FechaRegistro,perf.DescPerfilPlanilla AS Nombres,'S/. '+CAST(perf.Jornal AS VARCHAR(5)) AS Jornal,    
  CASE WHEN perf.FlagActivo=1 THEN '1' ELSE '0' END AS FlagActivo FROM PerfilPlanilla perf      
  ORDER BY FechaRegistro DESC
 END                              
 ELSE                              
 BEGIN                              
  SELECT TOP 10 perf.CodPerfilPlanilla,CONVERT(VARCHAR(10),perf.FechaRegistro,103) AS FechaRegistro,perf.DescPerfilPlanilla AS Nombres,'S/. '+CAST(perf.Jornal AS VARCHAR(5)) AS Jornal,    
  CASE WHEN perf.FlagActivo=1 THEN '1' ELSE '0' END AS FlagActivo FROM PerfilPlanilla perf      
  WHERE DescPerfilPlanilla LIKE '%'+@TextoBuscar+'%'    
  ORDER BY FechaRegistro DESC                    
 END 
--------------------------------------------------------------
ALTER PROCEDURE PA_ins_RegistrarPlanillaConstruccion
@CodTrabajador INT,
@FechaInicio DATE,
@FechaFin DATE,
@PeriodoMes CHAR(2),
@PeriodoAno VARCHAR(4),
@DiasLaborados NUMERIC(14,2),
@DiasDomingosFeriados NUMERIC(14,2),
@AsignacionFamiliar NUMERIC(14,2),
@Reintegro NUMERIC(14,2),
@Bonificacion NUMERIC(14,2),
@HorasExtraSimple NUMERIC(14,2),
@HorasExtras60 NUMERIC(14,2),
@HorasExtras100 NUMERIC(14,2),
@BUC NUMERIC(14,2),
@Pasajes NUMERIC(14,2),
@Vacacional NUMERIC(14,2),
@Gratificacion NUMERIC(14,2),
@Liquidacion NUMERIC(14,2),
@BonificacionExtraSalud NUMERIC(14,2),
@BonificacionExtraPension NUMERIC(14,2),
@SNP NUMERIC(14,2),
@AporteObligatorio NUMERIC(14,2),
@ComisionFlujo NUMERIC(14,2),
@ComisionMixta NUMERIC(14,2),
@PrimaSeguro NUMERIC(14,2),
@AporteComplementario NUMERIC(14,2),
@Conafovicer NUMERIC(14,2),
@AporteSindical NUMERIC(14,2),
@EsSaludVida NUMERIC(14,2),
@Renta5taCategoria NUMERIC(14,2),
@EPS NUMERIC(14,2),
@OtrosDctos NUMERIC(14,2),
@EsSalud NUMERIC(14,2),
@AportComplementarioAFP NUMERIC(14,2),
@SCTRSalud NUMERIC(14,2),
@SCTRPension NUMERIC(14,2), 
@TotalIngresos NUMERIC(14,2), 
@TotalBeneficios NUMERIC(14,2), 
@TotalDescuentos NUMERIC(14,2), 
@TotalAporteEmpresa NUMERIC(14,2),
@TotalPagarTrabajador NUMERIC(14,2), 
@TotalCostoTrabajador NUMERIC(14,2), 
@CodUsuarioRegistro INT,
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

DECLARE @CodPerfilPlanilla INT
SET @CodPerfilPlanilla=(SELECT CodPerfilPlanilla FROM Trabajador WHERE CodTrabajador=@CodTrabajador)

IF NOT EXISTS (SELECT 1 FROM Trabajador WHERE CAST(FechaCese AS DATE)<=CAST(GETDATE() AS DATE) AND CodTrabajador=@CodTrabajador)
BEGIN
	IF NOT EXISTS (SELECT 1 FROM PlanillaConstruccion WHERE CodTrabajador=@CodTrabajador AND CAST(@FechaInicio AS DATE) BETWEEN CAST(FechaInicio AS DATE) AND CAST(FechaFin AS DATE) AND FlagActivo=1)
	BEGIN	
		INSERT INTO PlanillaConstruccion(CodTrabajador,CodPerfilPlanilla,FechaInicio,FechaFin,PeriodoMes,PeriodoAno,DiasLaborados,DiasDomingosFeriados,AsignacionFamiliar,Reintegro,
		Bonificacion,HorasSimple,HorasExtras60,HorasExtras100,BUC,Pasajes,Vacacional,Gratificacion,Liquidacion,BonificacionExtraSalud,BonificacionExtraPension,SNP,AporteObligatorio,
		ComisionFlujo,ComisionMixta,PrimaSeguro,AporteComplementario,Conafovicer,AporteSindical,EsSaludVida,Renta5taCategoria,EPS,OtrosDctos,EsSalud,AportComplementarioAFP,SCTRSalud,SCTRPension,
		TotalIngresos,TotalBeneficios,TotalDescuentos,TotalAporteEmpresa,TotalPagarTrabajador,TotalCostoTrabajador,FlagActivo,FechaRegistro,CodUsuarioRegistro)
		VALUES (@CodTrabajador,@CodPerfilPlanilla,@FechaInicio,@FechaFin,@PeriodoMes,@PeriodoAno,@DiasLaborados,@DiasDomingosFeriados,@AsignacionFamiliar,@Reintegro,
		@Bonificacion,@HorasExtraSimple,@HorasExtras60,@HorasExtras100,@BUC,@Pasajes,@Vacacional,@Gratificacion,@Liquidacion,@BonificacionExtraSalud,@BonificacionExtraPension,@SNP,@AporteObligatorio,
		@ComisionFlujo,@ComisionMixta,@PrimaSeguro,@AporteComplementario,@Conafovicer,@AporteSindical,@EsSaludVida,@Renta5taCategoria,@EPS,@OtrosDctos,@EsSalud,@AportComplementarioAFP,@SCTRSalud,@SCTRPension,
		@TotalIngresos,@TotalBeneficios,@TotalDescuentos,@TotalAporteEmpresa,@TotalPagarTrabajador,@TotalCostoTrabajador,1,GETDATE(),@CodUsuarioRegistro)

		SET @Mensaje='EXITO'
	END
	ELSE
	BEGIN
		SET @Mensaje='REPETIDO'
	END
END
ELSE
BEGIN
	SET @Mensaje='CESADO'
END

COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 

--------------------------------------------------------------
ALTER PROCEDURE PA_sel_ObtenerPlanillaRecientes
AS
SELECT TOP 5 plani.CodPlanillaConstruccion,UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad,DATENAME(MONTH, DATEADD( MONTH , CAST(plani.PeriodoMes AS INT) , -1 )) AS MesPeriodo,CONVERT(VARCHAR,plani.FechaInicio,103) AS FechaInicio,CONVERT(VARCHAR,plani.FechaFin,103) AS FechaFin,FORMAT(trab.HaberMensual ,'#,0.00','es-PE') AS HaberMensual, plani.DiasLaborados,
perf.Jornal,FORMAT((perf.Jornal*plani.DiasLaborados) ,'#,0.00','es-PE') AS Basico,FORMAT((perf.Jornal*plani.DiasDomingosFeriados),'#,0.00','es-PE') AS TotalDominical,FORMAT(plani.TotalIngresos,'#,0.00','es-PE') AS TotalIngresos,
FORMAT(plani.TotalBeneficios,'#,0.00','es-PE') AS TotalBeneficios,FORMAT(plani.TotalDescuentos,'#,0.00','es-PE') AS TotalDescuentos,FORMAT(plani.TotalAporteEmpresa,'#,0.00','es-PE') AS TotalAporteEmpresa,FORMAT(plani.TotalPagarTrabajador,'#,0.00','es-PE') AS TotalPagarTrabajador,
FORMAT(plani.TotalCostoTrabajador,'#,0.00','es-PE') AS TotalCostoTrabajador
FROM PlanillaConstruccion plani 
INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trab.CodPerfilPlanilla)
WHERE plani.FlagActivo=1
ORDER BY plani.FechaRegistro DESC

--------------------------------------------------------------
ALTER PROCEDURE PA_sel_CargarDatosPlanilla
@CodPlanilla INT
AS

SELECT plani.PeriodoAno,plani.PeriodoMes,plani.FechaInicio,plani.FechaFin,plani.CodTrabajador,plani.DiasLaborados,plani.DiasDomingosFeriados,
plani.AsignacionFamiliar,plani.Reintegro,plani.Bonificacion,plani.BonificacionExtraSalud,plani.ComisionFlujo,plani.ComisionMixta,plani.Renta5taCategoria,
plani.EPS,plani.OtrosDctos 
FROM PlanillaConstruccion plani INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador)
WHERE plani.CodPlanillaConstruccion=@CodPlanilla
--------------------------------------------------------------
CREATE PROCEDURE PA_upd_ActualizarPlanillaConstruccion
@CodTrabajador INT,
@FechaInicio DATE,
@FechaFin DATE,
@PeriodoMes CHAR(2),
@PeriodoAno VARCHAR(4),
@DiasLaborados NUMERIC(14,2),
@DiasDomingosFeriados NUMERIC(14,2),
@AsignacionFamiliar NUMERIC(14,2),
@Reintegro NUMERIC(14,2),
@Bonificacion NUMERIC(14,2),
@HorasExtraSimple NUMERIC(14,2),
@HorasExtras60 NUMERIC(14,2),
@HorasExtras100 NUMERIC(14,2),
@BUC NUMERIC(14,2),
@Pasajes NUMERIC(14,2),
@Vacacional NUMERIC(14,2),
@Gratificacion NUMERIC(14,2),
@Liquidacion NUMERIC(14,2),
@BonificacionExtraSalud NUMERIC(14,2),
@BonificacionExtraPension NUMERIC(14,2),
@SNP NUMERIC(14,2),
@AporteObligatorio NUMERIC(14,2),
@ComisionFlujo NUMERIC(14,2),
@ComisionMixta NUMERIC(14,2),
@PrimaSeguro NUMERIC(14,2),
@AporteComplementario NUMERIC(14,2),
@Conafovicer NUMERIC(14,2),
@AporteSindical NUMERIC(14,2),
@EsSaludVida NUMERIC(14,2),
@Renta5taCategoria NUMERIC(14,2),
@EPS NUMERIC(14,2),
@OtrosDctos NUMERIC(14,2),
@EsSalud NUMERIC(14,2),
@AportComplementarioAFP NUMERIC(14,2),
@SCTRSalud NUMERIC(14,2),
@SCTRPension NUMERIC(14,2), 
@TotalIngresos NUMERIC(14,2), 
@TotalBeneficios NUMERIC(14,2), 
@TotalDescuentos NUMERIC(14,2), 
@TotalAporteEmpresa NUMERIC(14,2),
@TotalPagarTrabajador NUMERIC(14,2), 
@TotalCostoTrabajador NUMERIC(14,2), 
@CodUsuarioModificacion INT,
@CodPlanillaConstruccion INT,
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

DECLARE @CodPerfilPlanilla INT
SET @CodPerfilPlanilla=(SELECT CodPerfilPlanilla FROM Trabajador WHERE CodTrabajador=@CodTrabajador)

IF NOT EXISTS (SELECT 1 FROM Trabajador WHERE CAST(FechaCese AS DATE)<=CAST(GETDATE() AS DATE) AND CodTrabajador=@CodTrabajador)
BEGIN
	IF NOT EXISTS (SELECT 1 FROM PlanillaConstruccion WHERE CodTrabajador=@CodTrabajador AND CAST(@FechaInicio AS DATE) BETWEEN CAST(FechaInicio AS DATE) AND CAST(FechaFin AS DATE) AND FlagActivo=1 AND CodPlanillaConstruccion!=@CodPlanillaConstruccion)
	BEGIN
		UPDATE PlanillaConstruccion
		SET CodTrabajador=@CodTrabajador,CodPerfilPlanilla=@CodPerfilPlanilla,FechaInicio=@FechaInicio,FechaFin=@FechaFin,PeriodoMes=@PeriodoMes,PeriodoAno=@PeriodoAno,DiasLaborados=@DiasLaborados,DiasDomingosFeriados=@DiasDomingosFeriados,
		AsignacionFamiliar=@AsignacionFamiliar,Reintegro=@Reintegro,Bonificacion=@Bonificacion,HorasSimple=@HorasExtraSimple,HorasExtras60=@HorasExtras60,HorasExtras100=@HorasExtras100,BUC=@BUC,Pasajes=@Pasajes,Vacacional=@Vacacional,
		Gratificacion=@Gratificacion,Liquidacion=@Liquidacion,BonificacionExtraSalud=@BonificacionExtraSalud,BonificacionExtraPension=@BonificacionExtraPension,SNP=@SNP,AporteObligatorio=@AporteObligatorio,ComisionFlujo=@ComisionFlujo,ComisionMixta=@ComisionMixta,
		PrimaSeguro=@PrimaSeguro,AporteComplementario=@AporteComplementario,Conafovicer=@Conafovicer,AporteSindical=@AporteSindical,EsSaludVida=@EsSaludVida,Renta5taCategoria=@Renta5taCategoria,EPS=@EPS,OtrosDctos=@OtrosDctos,EsSalud=@EsSalud,
		AportComplementarioAFP=@AportComplementarioAFP,SCTRSalud=@SCTRSalud,SCTRPension=@SCTRPension,TotalIngresos=@TotalIngresos,TotalBeneficios=@TotalBeneficios,TotalDescuentos=@TotalDescuentos,TotalAporteEmpresa=@TotalAporteEmpresa,TotalPagarTrabajador=@TotalPagarTrabajador,
		TotalCostoTrabajador=@TotalCostoTrabajador,FlagActivo=1,FechaModificacion=GETDATE(),CodUsuarioModificacion=@CodUsuarioModificacion
		WHERE CodPlanillaConstruccion=@CodPlanillaConstruccion

		SET @Mensaje='EXITO'
	END
	ELSE
	BEGIN
		SET @Mensaje='REPETIDO'
	END
END
ELSE
BEGIN
	SET @Mensaje='CESADO'
END

COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 

--------------------------------------------------------------
ALTER PROCEDURE PA_gen_ObtenerListadoReportePlanilla
@FechaInicial DATE,
@FechaFinal DATE,
@AnoPeriodo CHAR(4),
@MesPeriodo CHAR(2),
@CodTipoBusqueda INT
AS
IF (@CodTipoBusqueda=1)
BEGIN	
	SELECT 
	ROW_NUMBER() OVER (ORDER BY CodPlanillaConstruccion DESC) [Nro],
	UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, carg.DescCargo AS [Status],
	perf.Jornal,plani.DiasLaborados,		
	CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,
	CAST((perf.Jornal*plani.DiasDomingosFeriados) AS NUMERIC(14,2)) AS Dominical,
	plani.Pasajes AS Pasajes,
	plani.BUC AS BUC,
	plani.AsignacionFamiliar AS [AsigFam],
	plani.Reintegro AS Reintegro,
	plani.Bonificacion AS Bonificacion,
	plani.HorasSimple AS [HESimples],
	plani.HorasExtras60 AS [HE60],
	plani.HorasExtras100 AS [HE100],
	plani.TotalIngresos AS [TotalIngresos],
	aport.DescTipoAportacion AS Afp,
	plani.SNP AS Snp,
	plani.AporteObligatorio AS [AporteOblig],
	plani.ComisionFlujo AS ComisionFlujo,
	plani.ComisionMixta AS ComisionMixta,
	plani.PrimaSeguro AS PrimaSeguro,
	plani.AporteComplementario AS [AporteComple],
	plani.Conafovicer AS Conafovicer,
	plani.AporteSindical AS AporteSindical,
	plani.EsSaludVida AS EsSaludVida,
	plani.Renta5taCategoria AS [Rentade5ta],
	plani.EPS AS EPS,
	plani.OtrosDctos AS [OtrosDescuent],
	plani.TotalDescuentos AS [TotalDescuentos],
	plani.Vacacional AS Vacaciones,
	plani.Gratificacion AS Gratificacion,
	plani.Liquidacion AS Liquidacion,
	plani.BonificacionExtraSalud AS [BonoExtSalud],
	plani.BonificacionExtraPension AS [BonoExtPension],
	plani.TotalBeneficios AS [TotalBeneficios],
	plani.EsSalud AS EsSalud,
	plani.AportComplementarioAFP AS [AporteComplemenAFP],
	plani.SCTRSalud AS [CstrSalud],
	plani.SCTRPension AS [CstrPension],
	plani.TotalAporteEmpresa AS [TotalEmpresa],	
	plani.TotalPagarTrabajador AS [TotalaPagar],
	plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
	CONVERT(VARCHAR,@FechaInicial,103) AS FechaInicial,CONVERT(VARCHAR,@FechaFinal,103) AS FechaFinal,DATENAME(MONTH,DATEADD(MONTH,CAST(@MesPeriodo AS INT),-1 )) AS MesPeriodo, @AnoPeriodo AS AnoPeriodo
	FROM PlanillaConstruccion plani 
	INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
	INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trab.CodPerfilPlanilla)
	INNER JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo)
	INNER JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
	WHERE plani.FlagActivo=1
	AND plani.PeriodoMes=@MesPeriodo AND plani.PeriodoAno=@AnoPeriodo
	ORDER BY plani.FechaRegistro DESC
END
ELSE IF (@CodTipoBusqueda=2)
BEGIN
		SELECT 
	ROW_NUMBER() OVER (ORDER BY CodPlanillaConstruccion DESC) [Nro],
	UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, carg.DescCargo AS [Status],
	perf.Jornal,plani.DiasLaborados,		
	CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,
	CAST((perf.Jornal*plani.DiasDomingosFeriados) AS NUMERIC(14,2)) AS Dominical,
	plani.Pasajes AS Pasajes,
	plani.BUC AS BUC,
	plani.AsignacionFamiliar AS [AsigFam],
	plani.Reintegro AS Reintegro,
	plani.Bonificacion AS Bonificacion,
	plani.HorasSimple AS [HESimples],
	plani.HorasExtras60 AS [HE60],
	plani.HorasExtras100 AS [HE100],
	plani.TotalIngresos AS [TotalIngresos],
	aport.DescTipoAportacion AS Afp,
	plani.SNP AS Snp,
	plani.AporteObligatorio AS [AporteOblig],
	plani.ComisionFlujo AS ComisionFlujo,
	plani.ComisionMixta AS ComisionMixta,
	plani.PrimaSeguro AS PrimaSeguro,
	plani.AporteComplementario AS [AporteComple],
	plani.Conafovicer AS Conafovicer,
	plani.AporteSindical AS AporteSindical,
	plani.EsSaludVida AS EsSaludVida,
	plani.Renta5taCategoria AS [Rentade5ta],
	plani.EPS AS EPS,
	plani.OtrosDctos AS [OtrosDescuent],
	plani.TotalDescuentos AS [TotalDescuentos],
	plani.Vacacional AS Vacaciones,
	plani.Gratificacion AS Gratificacion,
	plani.Liquidacion AS Liquidacion,
	plani.BonificacionExtraSalud AS [BonoExtSalud],
	plani.BonificacionExtraPension AS [BonoExtPension],
	plani.TotalBeneficios AS [TotalBeneficios],
	plani.EsSalud AS EsSalud,
	plani.AportComplementarioAFP AS [AporteComplemenAFP],
	plani.SCTRSalud AS [CstrSalud],
	plani.SCTRPension AS [CstrPension],
	plani.TotalAporteEmpresa AS [TotalEmpresa],	
	plani.TotalPagarTrabajador AS [TotalaPagar],
	plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
	CONVERT(VARCHAR,@FechaInicial,103) AS FechaInicial,CONVERT(VARCHAR,@FechaFinal,103) AS FechaFinal,DATENAME(MONTH,DATEADD(MONTH,CAST(@MesPeriodo AS INT),-1 )) AS MesPeriodo, @AnoPeriodo AS AnoPeriodo
	FROM PlanillaConstruccion plani 
	INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
	INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trab.CodPerfilPlanilla)
	INNER JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo)
	INNER JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
	WHERE plani.FlagActivo=1
	AND plani.FechaInicio BETWEEN @FechaInicial AND @FechaFinal
	ORDER BY plani.FechaRegistro DESC
END
--------------------------------------------------------------
ALTER PROCEDURE PA_gen_ObtenerDetallePlanillaTrabajador
@CodPlanilla INT
AS
	SELECT 
	ROW_NUMBER() OVER (ORDER BY CodPlanillaConstruccion DESC) [Nro],
	UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, carg.DescCargo AS [Status],
	perf.Jornal,plani.DiasLaborados,		
	CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,
	CAST((perf.Jornal*plani.DiasDomingosFeriados) AS NUMERIC(14,2)) AS Dominical,
	plani.Pasajes AS Pasajes,
	plani.BUC AS BUC,
	plani.AsignacionFamiliar AS [AsigFam],
	plani.Reintegro AS Reintegro,
	plani.Bonificacion AS Bonificacion,
	plani.HorasSimple AS [HESimples],
	plani.HorasExtras60 AS [HE60],
	plani.HorasExtras100 AS [HE100],
	plani.TotalIngresos AS [TotalIngresos],
	aport.DescTipoAportacion AS Afp,
	plani.SNP AS Snp,
	plani.AporteObligatorio AS [AporteOblig],
	plani.ComisionFlujo AS ComisionFlujo,
	plani.ComisionMixta AS ComisionMixta,
	plani.PrimaSeguro AS PrimaSeguro,
	plani.AporteComplementario AS [AporteComple],
	plani.Conafovicer AS Conafovicer,
	plani.AporteSindical AS AporteSindical,
	plani.EsSaludVida AS EsSaludVida,
	plani.Renta5taCategoria AS [Rentade5ta],
	plani.EPS AS EPS,
	plani.OtrosDctos AS [OtrosDescuent],
	plani.TotalDescuentos AS [TotalDescuentos],
	plani.Vacacional AS Vacaciones,
	plani.Gratificacion AS Gratificacion,
	plani.Liquidacion AS Liquidacion,
	plani.BonificacionExtraSalud AS [BonoExtSalud],
	plani.BonificacionExtraPension AS [BonoExtPension],
	plani.TotalBeneficios AS [TotalBeneficios],
	plani.EsSalud AS EsSalud,
	plani.AportComplementarioAFP AS [AporteComplemenAFP],
	plani.SCTRSalud AS [CstrSalud],
	plani.SCTRPension AS [CstrPension],
	plani.TotalAporteEmpresa AS [TotalEmpresa],	
	plani.TotalPagarTrabajador AS [TotalaPagar],
	plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
	CONVERT(VARCHAR,trab.FechaIngreso,103) AS FechaIngreso,
	CONVERT(VARCHAR,trab.FechaCese,103) AS FechaCese,
	trab.NroCUSPP,
	CONVERT(VARCHAR,FechaInicio,103) AS FechaInicial,CONVERT(VARCHAR,FechaFin,103) AS FechaFinal,DATENAME(MONTH,DATEADD(MONTH,CAST(PeriodoMes AS INT),-1 )) AS MesPeriodo, PeriodoAno AS AnoPeriodo	
	FROM PlanillaConstruccion plani 
	INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
	INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trab.CodPerfilPlanilla)
	INNER JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo)
	INNER JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
	WHERE plani.CodPlanillaConstruccion=@CodPlanilla
	

--------------------------------------------------------------

CREATE PROCEDURE PA_gen_ObtenerBoletasPagoMasivo
@FechaInicial DATE,
@FechaFinal DATE
AS
	SELECT 
	ROW_NUMBER() OVER (ORDER BY CodPlanillaConstruccion DESC) [Nro],
	UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, carg.DescCargo AS [Status],
	perf.Jornal,plani.DiasLaborados,		
	CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,
	CAST((perf.Jornal*plani.DiasDomingosFeriados) AS NUMERIC(14,2)) AS Dominical,
	plani.Pasajes AS Pasajes,
	plani.BUC AS BUC,
	plani.AsignacionFamiliar AS [AsigFam],
	plani.Reintegro AS Reintegro,
	plani.Bonificacion AS Bonificacion,
	plani.HorasSimple AS [HESimples],
	plani.HorasExtras60 AS [HE60],
	plani.HorasExtras100 AS [HE100],
	plani.TotalIngresos AS [TotalIngresos],
	aport.DescTipoAportacion AS Afp,
	plani.SNP AS Snp,
	plani.AporteObligatorio AS [AporteOblig],
	plani.ComisionFlujo AS ComisionFlujo,
	plani.ComisionMixta AS ComisionMixta,
	plani.PrimaSeguro AS PrimaSeguro,
	plani.AporteComplementario AS [AporteComple],
	plani.Conafovicer AS Conafovicer,
	plani.AporteSindical AS AporteSindical,
	plani.EsSaludVida AS EsSaludVida,
	plani.Renta5taCategoria AS [Rentade5ta],
	plani.EPS AS EPS,
	plani.OtrosDctos AS [OtrosDescuent],
	plani.TotalDescuentos AS [TotalDescuentos],
	plani.Vacacional AS Vacaciones,
	plani.Gratificacion AS Gratificacion,
	plani.Liquidacion AS Liquidacion,
	plani.BonificacionExtraSalud AS [BonoExtSalud],
	plani.BonificacionExtraPension AS [BonoExtPension],
	plani.TotalBeneficios AS [TotalBeneficios],
	plani.EsSalud AS EsSalud,
	plani.AportComplementarioAFP AS [AporteComplemenAFP],
	plani.SCTRSalud AS [CstrSalud],
	plani.SCTRPension AS [CstrPension],
	plani.TotalAporteEmpresa AS [TotalEmpresa],	
	plani.TotalPagarTrabajador AS [TotalaPagar],
	plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
	CONVERT(VARCHAR,trab.FechaIngreso,103) AS FechaIngreso,
	CONVERT(VARCHAR,trab.FechaCese,103) AS FechaCese,
	trab.NroCUSPP,
	CONVERT(VARCHAR,FechaInicio,103) AS FechaInicial,CONVERT(VARCHAR,FechaFin,103) AS FechaFinal,DATENAME(MONTH,DATEADD(MONTH,CAST(PeriodoMes AS INT),-1 )) AS MesPeriodo, PeriodoAno AS AnoPeriodo	
	FROM PlanillaConstruccion plani 
	INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
	INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trab.CodPerfilPlanilla)
	INNER JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo)
	INNER JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
	WHERE plani.FlagActivo=1 
	AND plani.FechaInicio BETWEEN @FechaInicial AND @FechaFinal

--------------------------------------------------------------

CREATE PROCEDURE [dbo].[PA_sel_Login]                
@Usuario VARCHAR(15),  
@Contrasena VARCHAR(30)               
AS              
IF EXISTS(SELECT 1 FROM Usuario WHERE Usuario=@Usuario)               
BEGIN 
	 IF EXISTS(SELECT 1 FROM Usuario WHERE Usuario=@Usuario  AND Contrasena=HASHBYTES('SHA2_512', @Contrasena))        
	 BEGIN        
		 SELECT CodUsuario,Usuario,FlagActivo,Nombres + ' ' + Apellidos AS NombresUsuario,CodPerfilAcceso FROM Usuario            
		 WHERE Usuario=@Usuario   
	 END        
	 ELSE        
	 BEGIN        
		 SELECT 'La contraseña ingresada es incorrecta'        
	 END               
END     
ELSE      
BEGIN      
 SELECT 'El usuario ingresado no se encuentra registrado'      
END  

--------------------------------------------------------------
ALTER PROCEDURE PA_sel_CargarDatosTrabajador
@CodTrabajador INT
AS

SELECT CodTipDocumentoIdent,DocumentoIdentidad,UPPER(Nombres),UPPER(ApellidoPaterno),UPPER(ApellidoMaterno),CONVERT(VARCHAR,FechaNacimiento,103) AS FechaNacimiento, CodEstadoCivil,
CONVERT(VARCHAR,FechaIngreso,103) AS FechaIngreso,CONVERT(VARCHAR,FechaCese,103) AS FechaCese,CASE WHEN Sexo='M' THEN 'Masculino' ELSE 'Femenino' END AS Sexo,HaberMensual,CodTipoAportacion,NroCUSPP,CodLaborTrabajo,CodCargoTrabajo,
CodFinanciera,NroCuentaFinanciera,NroCuentaCTS,CodTipoPlanilla,CodPerfilPlanilla,CodTrabajador
FROM Trabajador WHERE CodTrabajador=@CodTrabajador


--------------------------------------------------------------
CREATE PROCEDURE PA_sel_ObtenerLaborTrabajo
AS

SELECT CodLaborTrabajo,DescLaborTrabajo,FlagActivo FROM LaborTrabajo
--------------------------------------------------------------
CREATE PROCEDURE PA_ins_RegistrarLaborTrabajador
@DescripcionLabor VARCHAR(500),
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

IF NOT EXISTS(SELECT 1 FROM LaborTrabajo WHERE DescLaborTrabajo=@DescripcionLabor)
BEGIN
	INSERT INTO LaborTrabajo (DescLaborTrabajo,FlagActivo)
	VALUES (@DescripcionLabor,1)

	SET @Mensaje='EXITO'
END
ELSE
BEGIN
	SET @Mensaje='REPETIDO'
END

COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 

--------------------------------------------------------------

CREATE PROCEDURE PA_upd_ActualizarLaborTrabajador
@CodLaboTrabajador INT,
@DescripcionLabor VARCHAR(500),
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

IF NOT EXISTS(SELECT 1 FROM LaborTrabajo WHERE DescLaborTrabajo=@DescripcionLabor AND CodLaborTrabajo!=@CodLaboTrabajador)
BEGIN
	UPDATE LaborTrabajo
	SET DescLaborTrabajo=@DescripcionLabor
	WHERE CodLaborTrabajo=@CodLaboTrabajador

	SET @Mensaje='EXITO'
END
ELSE
BEGIN
	SET @Mensaje='REPETIDO'
END

COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 

--------------------------------------------------------------
CREATE PROCEDURE PA_sel_ObtenerDatosLaborTrabajo
@CodLaborTrabajo INT
AS

SELECT CodLaborTrabajo,DescLaborTrabajo FROM LaborTrabajo WHERE CodLaborTrabajo=@CodLaborTrabajo

--------------------------------------------------------------
ALTER PROCEDURE PA_sel_ObtenerPlanillasTrabajador
@CodTrabajador INT,
@FechaInicial DATE,
@FechaFinal DATE
AS

SELECT plani.CodPlanillaConstruccion,UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad,DATENAME(MONTH, DATEADD( MONTH , CAST(plani.PeriodoMes AS INT) , -1 )) AS MesPeriodo,CONVERT(VARCHAR,plani.FechaInicio,103) AS FechaInicio,CONVERT(VARCHAR,plani.FechaFin,103) AS FechaFin,FORMAT(trab.HaberMensual ,'#,0.00','es-PE') AS HaberMensual, plani.DiasLaborados,
perf.Jornal,FORMAT((perf.Jornal*plani.DiasLaborados) ,'#,0.00','es-PE') AS Basico,FORMAT((perf.Jornal*plani.DiasDomingosFeriados),'#,0.00','es-PE') AS TotalDominical,FORMAT(plani.TotalIngresos,'#,0.00','es-PE') AS TotalIngresos,
FORMAT(plani.TotalBeneficios,'#,0.00','es-PE') AS TotalBeneficios,FORMAT(plani.TotalDescuentos,'#,0.00','es-PE') AS TotalDescuentos,FORMAT(plani.TotalAporteEmpresa,'#,0.00','es-PE') AS TotalAporteEmpresa,FORMAT(plani.TotalPagarTrabajador,'#,0.00','es-PE') AS TotalPagarTrabajador,
FORMAT(plani.TotalCostoTrabajador,'#,0.00','es-PE') AS TotalCostoTrabajador
FROM PlanillaConstruccion plani 
INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trab.CodPerfilPlanilla)
WHERE plani.CodTrabajador=@CodTrabajador
AND CAST(plani.FechaRegistro AS DATE) BETWEEN @FechaInicial AND @FechaFinal
ORDER BY plani.FechaRegistro DESC

--------------------------------------------------------------
CREATE PROCEDURE PA_sel_CargarTrabajadoresReportes
AS
SELECT CodTrabajador,DocumentoIdentidad+' - '+UPPER(ApellidoPaterno+' '+ApellidoMaterno)+', '+Nombres AS NombreTrabajador  
FROM Trabajador
--------------------------------------------------------------

CREATE PROCEDURE PA_sel_CargarDatosGenerales
AS

SELECT 
(SELECT COUNT(*) FROM Trabajador) AS CantidadTrabajadores,
(SELECT COUNT(*) FROM Usuario) AS CantidadUsuarios,
(SELECT COUNT(*) FROM PerfilPlanilla) AS CantidadPerfiles,
(SELECT COUNT(*) FROM PlanillaConstruccion WHERE CAST(FechaRegistro AS DATE)=CAST(GETDATE() AS DATE)) AS CantidadPlanillaDia
--------------------------------------------------------------

CREATE PROCEDURE PA_sel_BuscarUsuarioInterno                      
@TextoBuscar VARCHAR(200)                    
AS                      
 IF @TextoBuscar=''                              
 BEGIN                    
  SELECT TOP 10 CodUsuario,Nombres+' '+Apellidos+' - '+Usuario AS Usuario,perfil.DescPerfilAcceso AS PerfilAcceso,    
  usuario.FlagActivo AS Estado FROM Usuario usuario    
  INNER JOIN PerfilAcceso perfil ON (usuario.CodPerfilAcceso=perfil.CodPerfilAcceso)    
  ORDER BY Nombres+' '+Apellidos    
 END                              
 ELSE                              
 BEGIN                              
  SELECT CodUsuario,Nombres+' '+Apellidos+' - '+Usuario AS Usuario,perfil.DescPerfilAcceso AS PerfilAcceso,    
  usuario.FlagActivo AS Estado FROM Usuario usuario    
  INNER JOIN PerfilAcceso perfil ON (usuario.CodPerfilAcceso=perfil.CodPerfilAcceso)    
  WHERE Nombres+' '+Apellidos+' '+Usuario+' '+perfil.DescPerfilAcceso LIKE '%'+@TextoBuscar+'%'    
  ORDER BY Nombres+' '+Apellidos                        
 END 

 --------------------------------------------------------------
 CREATE PROCEDURE PA_sel_CargarPerfilAcceso
 AS
 SELECT CodPerfilAcceso,DescPerfilAcceso FROM PerfilAcceso WHERE FlagActivo=1

--------------------------------------------------------------
ALTER PROCEDURE PA_ins_CrearUsuarioInterno
@Usuario VARCHAR(100),
@Nombres VARCHAR(150),
@ApellidoPaterno VARCHAR(150),
@ApellidoMaterno VARCHAR(150),
@CodPerfilAcceso INT,
@Estado INT,
@Contrasena VARCHAR(150),
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

IF NOT EXISTS(SELECT 1 FROM Usuario WHERE Usuario=@Usuario)
BEGIN
	INSERT INTO Usuario(Usuario,Nombres,Apellidos,CodPerfilAcceso,FlagActivo,Contrasena)
	VALUES(@Usuario,@Nombres,@ApellidoMaterno+' '+@ApellidoMaterno,@CodPerfilAcceso,@Estado,HASHBYTES('SHA2_512',@Contrasena))

	SET @Mensaje='EXITO'
END
ELSE
BEGIN
	SET @Mensaje='REPETIDO'
END

COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 
--------------------------------------------------------------
ALTER PROCEDURE PA_sel_CargarDatosUsuario
@CodUsuario INT
AS

SELECT Usuario,Nombres,Apellidos,CodPerfilAcceso,CAST(FlagActivo AS INT) FROM Usuario WHERE CodUsuario=@CodUsuario
--------------------------------------------------------------
CREATE PROCEDURE PA_upd_ActualizarUsuarioInterno
@CodUsuario INT,
@Usuario VARCHAR(100),
@Nombres VARCHAR(150),
@ApellidoPaterno VARCHAR(150),
@ApellidoMaterno VARCHAR(150),
@CodPerfilAcceso INT,
@Estado INT,
@Contrasena VARCHAR(150),
@FlagCambiarContrasena INT,
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

IF NOT EXISTS(SELECT 1 FROM Usuario WHERE Usuario=@Usuario AND CodUsuario!=@CodUsuario)
BEGIN
	IF(@FlagCambiarContrasena=1)
	BEGIN
		UPDATE Usuario
		SET Usuario=@Usuario,Nombres=@Nombres,Apellidos=@ApellidoPaterno+' '+@ApellidoMaterno,CodPerfilAcceso=@CodPerfilAcceso,FlagActivo=@Estado,Contrasena=HASHBYTES('SHA2_512',@Contrasena)
		WHERE CodUsuario=@CodUsuario
	END
	ELSE
	BEGIN
		UPDATE Usuario
		SET Usuario=@Usuario,Nombres=@Nombres,Apellidos=@ApellidoPaterno+' '+@ApellidoMaterno,CodPerfilAcceso=@CodPerfilAcceso,FlagActivo=@Estado
		WHERE CodUsuario=@CodUsuario
	END	

	SET @Mensaje='EXITO'
END
ELSE
BEGIN
	SET @Mensaje='REPETIDO'
END

COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 


--------------------------------------------------------------
CREATE PROCEDURE PA_sel_CargarLaborTrabajador
AS
SELECT CodLaborTrabajo,DescLaborTrabajo FROM LaborTrabajo WHERE FlagActivo=1
--------------------------------------------------------------

CREATE PROCEDURE [dbo].[PA_sel_BuscarEventuales]
@TextoBuscar VARCHAR(200)                    
AS                      
 IF @TextoBuscar=''                              
 BEGIN                    
  SELECT TOP 10 trab.CodTrabajador,CONVERT(VARCHAR(10),trab.FechaRegistro,103) AS FechaIngreso,trab.Nombres+' '+trab.ApellidoPaterno+' '+trab.ApellidoMaterno+' - '+trab.DocumentoIdentidad AS Nombres,lab.DescLaborTrabajo AS Labor,pla.DescPlanilla AS Planilla,    
  CASE WHEN trab.FlagActivo=1 THEN '1' WHEN trab.FlagActivo=0 THEN 1 ELSE '0' END AS FlagVigente FROM Trabajador trab    
  INNER JOIN  TipoPlanilla pla ON (trab.CodTipoPlanilla=pla.CodTipoPlanilla)    
  INNER JOIN LaborTrabajo lab ON (lab.CodLaborTrabajo=trab.CodLaborTrabajo)   
  WHERE trab.CodTipoPlanilla=2
  ORDER BY trab.FechaRegistro DESC
 END                              
 ELSE                              
 BEGIN                              
  SELECT TOP 10 trab.CodTrabajador,CONVERT(VARCHAR(10),trab.FechaRegistro,103) AS FechaIngreso,trab.Nombres+' '+trab.ApellidoPaterno+' '+trab.ApellidoMaterno+' - '+trab.DocumentoIdentidad AS Nombres,lab.DescLaborTrabajo AS Labor,pla.DescPlanilla AS Planilla,    
  CASE WHEN trab.FlagActivo=1 THEN '1' WHEN trab.FlagActivo=0 THEN 1 ELSE '0' END AS FlagVigente FROM Trabajador trab    
  INNER JOIN  TipoPlanilla pla ON (trab.CodTipoPlanilla=pla.CodTipoPlanilla)    
  INNER JOIN LaborTrabajo lab ON (lab.CodLaborTrabajo=trab.CodLaborTrabajo)   
  WHERE Nombres+' '+ApellidoPaterno+' '+ApellidoMaterno+' '+DocumentoIdentidad+' '+lab.DescLaborTrabajo+' '+pla.DescPlanilla+' ' LIKE '%'+@TextoBuscar+'%'    
  AND trab.CodTipoPlanilla=2
  ORDER BY trab.FechaRegistro DESC                     
 END 

 ALTER TABLE TRABAJADOR
 ALTER COLUMN HaberMensual INT NULL

 --------------------------------------------------------------
 ALTER PROCEDURE [dbo].[PA_ins_GuardarTrabajadorEventuales]
@CodTipoDocumentoIdentidad INT,
@DocumentoIdentidad VARCHAR(20),
@Nombres VARCHAR(100),
@ApellidoPaterno VARCHAR(100),
@ApellidoMaterno VARCHAR(100),
@Sexo CHAR(1),
@CodTipoTrabajo INT,
@CodUsuarioRegistro INT,
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 
SET Language 'Spanish';   


IF NOT EXISTS(SELECT 1 FROM Trabajador WHERE CodTipDocumentoIdent=@CodTipoDocumentoIdentidad AND DocumentoIdentidad=@DocumentoIdentidad AND Nombres=@Nombres AND ApellidoPaterno=@ApellidoPaterno) 
BEGIN
	INSERT INTO Trabajador(CodTipDocumentoIdent,DocumentoIdentidad,Nombres,ApellidoPaterno,ApellidoMaterno,Sexo,
	CodLaborTrabajo,CodTipoPlanilla,FlagActivo,FechaIngreso,FechaRegistro,CodUsuarioRegistro)
	VALUES (@CodTipoDocumentoIdentidad,@DocumentoIdentidad,@Nombres,@ApellidoPaterno,@ApellidoMaterno,@Sexo,
	@CodTipoTrabajo,2,1,CAST(GETDATE() AS DATE),GETDATE(),@CodUsuarioRegistro)

	SET @Mensaje='EXITO'
END
ELSE
BEGIN
	SET @Mensaje='REPETIDO'
END


COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 


 --------------------------------------------------------------
 CREATE TABLE Servicios(
 CodServicio INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
 DescripcionServicio VARCHAR(150) NULL,
 UnidadMedida VARCHAR(50) NULL,
 PrecioUnit NUMERIC(18,2) NULL,
 FlagActivo BIT
 )

 --------------------------------------------------------------
 CREATE TABLE PlanillaEventuales(
 CodPlanillaEventual INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
 CodTrabajador INT FOREIGN KEY REFERENCES Trabajador(CodTrabajador) NOT NULL,
 FechaInicio DATE,
 FechaFin DATE,
 CodServicio INT FOREIGN KEY REFERENCES Servicios(CodServicio),
 Cantidad NUMERIC(14,2),
 TotalPagar NUMERIC(14,2),
 FlagActivo BIT,
 FechaRegistro DATETIME NOT NULL,
 CodUsuarioRegistro INT FOREIGN KEY REFERENCES Usuario(CodUsuario) NOT NULL,
 FechaModificacion DATETIME NULL,
 CodUsuarioModificacion INT FOREIGN KEY REFERENCES Usuario(CodUsuario) NULL
 )

 
 -------------------------------------------------------------


CREATE PROCEDURE PA_ins_RegistrarServicio
@DescripcionServicio VARCHAR(500),
@UnidadMedida VARCHAR(50),
@PrecioUnitario NUMERIC(14,2),
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

IF NOT EXISTS(SELECT 1 FROM Servicios WHERE DescripcionServicio=@DescripcionServicio AND UnidadMedida=@UnidadMedida AND PrecioUnit=@PrecioUnitario)
BEGIN
	INSERT INTO Servicios(DescripcionServicio,PrecioUnit,UnidadMedida,FlagActivo)
	VALUES (@DescripcionServicio,@PrecioUnitario,@UnidadMedida,1)

	SET @Mensaje='EXITO'
END
ELSE
BEGIN
	SET @Mensaje='REPETIDO'
END

COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 
 -------------------------------------------------------------

CREATE PROCEDURE PA_sel_ObtenerServicios
AS

SELECT CodServicio,DescripcionServicio,UnidadMedida,PrecioUnit,FlagActivo FROM Servicios WHERE FlagActivo=1

 -------------------------------------------------------------

CREATE PROCEDURE PA_upd_ActualizarServicio
@CodServicio INT,
@DescripcionServicio VARCHAR(500),
@UnidadMedida VARCHAR(50),
@PrecioUnitario NUMERIC(14,2),
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

IF NOT EXISTS(SELECT 1 FROM Servicios WHERE DescripcionServicio=@DescripcionServicio AND UnidadMedida=@UnidadMedida AND PrecioUnit=@PrecioUnitario AND CodServicio!=@CodServicio)
BEGIN
	UPDATE Servicios
	SET DescripcionServicio=@DescripcionServicio,UnidadMedida=@UnidadMedida,PrecioUnit=@PrecioUnitario
	WHERE CodServicio=@CodServicio

	SET @Mensaje='EXITO'
END
ELSE
BEGIN
	SET @Mensaje='REPETIDO'
END

COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 

 -------------------------------------------------------------

CREATE PROCEDURE PA_sel_ObtenerDatosServicio
@CodServicio INT
AS

SELECT CodServicio,DescripcionServicio,UnidadMedida,PrecioUnit FROM Servicios WHERE CodServicio=@CodServicio

 -------------------------------------------------------------
CREATE PROCEDURE [dbo].[PA_gen_ObtenerListaTrabajadoresEventuales]
AS
SELECT CodTrabajador,DocumentoIdentidad+' - '+UPPER(ApellidoPaterno+' '+ApellidoMaterno)+', '+Nombres AS NombreTrabajador  
FROM Trabajador WHERE FlagActivo=1 
AND CAST(ISNULL(FechaCese,DATEADD(MONTH,1,GETDATE())) AS DATE)>CAST(GETDATE() AS DATE)
AND CodTipoPlanilla=2

 -------------------------------------------------------------
ALTER PROCEDURE PA_sel_CargarLaborTrabajo
AS

SELECT CodLaborTrabajo,DescLaborTrabajo FROM LaborTrabajo WHERE FlagActivo=1

 -------------------------------------------------------------
ALTER PROCEDURE PA_sel_CargarServicios
AS

SELECT CodServicio,DescripcionServicio FROM Servicios WHERE FlagActivo=1

 -------------------------------------------------------------

CREATE PROCEDURE [dbo].[PA_sel_CargarDatosPlanillaTrabajadorEventuales]
@CodTrabajador INT
AS
SELECT trab.CodTrabajador,lab.CodLaborTrabajo
FROM Trabajador trab 
INNER JOIN  LaborTrabajo lab ON (lab.CodLaborTrabajo=trab.CodLaborTrabajo) 
WHERE trab.CodTrabajador=@CodTrabajador

 -------------------------------------------------------------

CREATE PROCEDURE [dbo].[PA_sel_CargarDatosServiciosPlanilla]
@CodServicio INT
AS
SELECT CodServicio,UnidadMedida,PrecioUnit
FROM Servicios 
WHERE CodServicio=@CodServicio

 -------------------------------------------------------------

CREATE PROCEDURE [dbo].[PA_gen_ObtenerDatosTrabajadorEventualPlanillaPrevio]
@CodTrabajador INT
AS
SELECT ISNULL((SELECT (COUNT(*)+1) FROM PlanillaEventuales),1),trab.Nombres+' '+trab.ApellidoPaterno+' '+trab.ApellidoPaterno AS Nombres, trab.DocumentoIdentidad,lab.DescLaborTrabajo FROM Trabajador trab
LEFT JOIN LaborTrabajo lab ON (lab.CodLaborTrabajo=trab.CodLaborTrabajo)
WHERE trab.CodTrabajador=@CodTrabajador


 -------------------------------------------------------------

CREATE PROCEDURE [dbo].[PA_ins_RegistrarPlanillaEventuales]
@CodTrabajador INT,
@FechaInicio DATE,
@FechaFin DATE,
@CodServicio INT,
@Cantidad NUMERIC(14,2),
@TotalCostoTrabajador NUMERIC(14,2), 
@CodUsuarioRegistro INT,
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

IF NOT EXISTS (SELECT 1 FROM Trabajador WHERE FlagActivo=0 AND CodTrabajador=@CodTrabajador)
BEGIN
	IF NOT EXISTS (SELECT 1 FROM PlanillaEventuales WHERE CodTrabajador=@CodTrabajador AND CAST(@FechaInicio AS DATE) BETWEEN CAST(FechaInicio AS DATE) AND CAST(FechaFin AS DATE) AND FlagActivo=1)
	BEGIN	
		INSERT INTO PlanillaEventuales(CodTrabajador,FechaInicio,FechaFin,CodServicio,Cantidad,TotalPagar,FlagActivo,FechaRegistro,CodUsuarioRegistro)
		VALUES (@CodTrabajador,@FechaInicio,@FechaFin,@CodServicio,@Cantidad,@TotalCostoTrabajador,1,GETDATE(),@CodUsuarioRegistro)

		SET @Mensaje='EXITO'
	END
	ELSE
	BEGIN
		SET @Mensaje='REPETIDO'
	END
END
ELSE
BEGIN
	SET @Mensaje='CESADO'
END

COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 

 -------------------------------------------------------------
ALTER PROCEDURE [dbo].[PA_sel_ObtenerPlanillaEventualesRecientes]
AS
SELECT TOP 50 plani.CodPlanillaEventual,UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad,CONVERT(VARCHAR,plani.FechaInicio,103) AS FechaInicio,CONVERT(VARCHAR,plani.FechaFin,103) AS FechaFin,serv.DescripcionServicio,
serv.UnidadMedida,FORMAT(serv.PrecioUnit,'#,0.00','es-PE') AS PrecioUnit, FORMAT(plani.Cantidad,'#,0.00','es-PE') AS Cantidad,FORMAT(plani.TotalPagar,'#,0.00','es-PE') AS TotalPagar
FROM PlanillaEventuales plani 
INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
INNER JOIN Servicios serv ON (serv.CodServicio=plani.CodServicio)
WHERE plani.FlagActivo=1
ORDER BY plani.FechaRegistro DESC
 -------------------------------------------------------------
CREATE PROCEDURE [dbo].[PA_upd_ActualizarPlanillaEventuales]
@CodTrabajador INT,
@FechaInicio DATE,
@FechaFin DATE,
@CodServicio INT,
@Cantidad NUMERIC(14,2),
@TotalCostoTrabajador NUMERIC(14,2), 
@CodUsuarioModificacion INT,
@CodPlanillaEventual INT,
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

IF NOT EXISTS (SELECT 1 FROM Trabajador WHERE FlagActivo=0 AND CodTrabajador=@CodTrabajador)
BEGIN
	IF NOT EXISTS (SELECT 1 FROM PlanillaEventuales WHERE CodTrabajador=@CodTrabajador AND CAST(@FechaInicio AS DATE) BETWEEN CAST(FechaInicio AS DATE) AND CAST(FechaFin AS DATE) AND FlagActivo=1 AND CodPlanillaEventual!=@CodPlanillaEventual)
	BEGIN
		UPDATE PlanillaEventuales
		SET CodTrabajador=@CodTrabajador,FechaInicio=@FechaInicio,FechaFin=@FechaFin,CodServicio=@CodServicio,Cantidad=@Cantidad,
		TotalPagar=@TotalCostoTrabajador,FlagActivo=1,FechaModificacion=GETDATE(),CodUsuarioModificacion=@CodUsuarioModificacion
		WHERE CodPlanillaEventual=@CodPlanillaEventual

		SET @Mensaje='EXITO'
	END
	ELSE
	BEGIN
		SET @Mensaje='REPETIDO'
	END
END
ELSE
BEGIN
	SET @Mensaje='CESADO'
END

COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 

 -------------------------------------------------------------
CREATE PROCEDURE [dbo].[PA_sel_CargarDatosPlanillaEventual]
@CodPlanilla INT
AS

SELECT plani.FechaInicio,plani.FechaFin,plani.CodTrabajador,plani.CodServicio,
plani.Cantidad
FROM PlanillaEventuales plani INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador)
WHERE plani.CodPlanillaEventual=@CodPlanilla

 -------------------------------------------------------------
ALTER PROCEDURE [dbo].[PA_gen_ObtenerDetallePlanillaTrabajadorEventual]
@CodPlanilla INT
AS
	SELECT 
	'000000'+CAST(ROW_NUMBER() OVER (ORDER BY CodPlanillaEventual DESC) AS VARCHAR) [Nro],
	UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, lab.DescLaborTrabajo AS [Oficio],
	CONVERT(VARCHAR,FechaInicio,103) AS FechaInicial,CONVERT(VARCHAR,FechaFin,103) AS FechaFinal,
	serv.DescripcionServicio, serv.UnidadMedida, serv.PrecioUnit, plani.Cantidad, plani.TotalPagar
	FROM PlanillaEventuales plani 
	INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
	INNER JOIN LaborTrabajo lab ON (lab.CodLaborTrabajo=trab.CodLaborTrabajo)
	INNER JOIN Servicios serv ON (serv.CodServicio=plani.CodServicio)
	WHERE plani.CodPlanillaEventual=@CodPlanilla

  -------------------------------------------------------------


  ALTER PROCEDURE [dbo].[PA_ins_GuardarTrabajador]
@CodTipoDocumentoIdentidad INT,
@DocumentoIdentidad VARCHAR(20),
@Nombres VARCHAR(100),
@ApellidoPaterno VARCHAR(100),
@ApellidoMaterno VARCHAR(100),
@FechaNacimiento DATE,
@Sexo CHAR(1),
@CodEstadoCivil INT,
@FechaIngreso DATE,
@FechaCese DATE,
@HaberMensual NUMERIC(14,8),
@CodTipoAportacion INT,
@NroCuspp VARCHAR(50),
@CodTipoTrabajo INT,
@CodCargo INT,
@CodBanco INT,
@NumeroCuentaBanco VARCHAR(50),
@NumeroCuentaCTS VARCHAR(50),
@CodTipoPlanilla INT,
@CodPerfilPlanilla INT,
@CodUsuarioRegistro INT,
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 
SET Language 'Spanish';   

SET @FechaNacimiento=CAST(CONVERT(VARCHAR,@FechaNacimiento,103) AS DATE)
SET @FechaIngreso=CAST(CONVERT(VARCHAR,@FechaIngreso,103) AS DATE)


IF @FechaCese='19000101'
BEGIN
	SET @FechaCese=NULL
END
ELSE
BEGIN
	SET @FechaCese=CAST(CONVERT(VARCHAR,@FechaCese,103) AS DATE)
END

IF NOT EXISTS(SELECT 1 FROM Trabajador WHERE CodTipDocumentoIdent=@CodTipoDocumentoIdentidad AND DocumentoIdentidad=@DocumentoIdentidad AND Nombres=@Nombres AND ApellidoPaterno=@ApellidoPaterno) 
BEGIN
	INSERT INTO Trabajador(CodTipDocumentoIdent,DocumentoIdentidad,Nombres,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,Sexo,CodEstadoCivil,
	FechaIngreso,FechaCese,HaberMensual,CodTipoAportacion,NroCUSPP,CodLaborTrabajo,CodCargoTrabajo,CodFinanciera,NroCuentaFinanciera,NroCuentaCTS,CodTipoPlanilla,CodPerfilPlanilla,FlagActivo,FechaRegistro,CodUsuarioRegistro)
	VALUES (@CodTipoDocumentoIdentidad,@DocumentoIdentidad,UPPER(@Nombres),UPPER(@ApellidoPaterno),UPPER(@ApellidoMaterno),@FechaNacimiento,@Sexo,@CodEstadoCivil,
	@FechaIngreso,@FechaCese,@HaberMensual,@CodTipoAportacion,@NroCuspp,@CodTipoTrabajo,@CodCargo,@CodBanco,@NumeroCuentaBanco,@NumeroCuentaCTS,@CodTipoPlanilla,@CodPerfilPlanilla,1,GETDATE(),@CodUsuarioRegistro)

	SET @Mensaje='EXITO'
END
ELSE
BEGIN
	SET @Mensaje='REPETIDO'
END


COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 

  -------------------------------------------------------------

  ALTER PROCEDURE [dbo].[PA_upd_ActualizarTrabajador]
@CodTipoDocumentoIdentidad INT,
@DocumentoIdentidad VARCHAR(20),
@Nombres VARCHAR(100),
@ApellidoPaterno VARCHAR(100),
@ApellidoMaterno VARCHAR(100),
@FechaNacimiento DATE,
@Sexo CHAR(1),
@CodEstadoCivil INT,
@FechaIngreso DATE,
@FechaCese DATE,
@HaberMensual NUMERIC(14,8),
@CodTipoAportacion INT,
@NroCuspp VARCHAR(50),
@CodTipoTrabajo INT,
@CodCargo INT,
@CodBanco INT,
@NumeroCuentaBanco VARCHAR(50),
@NumeroCuentaCTS VARCHAR(50),
@CodTipoPlanilla INT,
@CodPerfilPlanilla INT,
@CodUsuarioModifica INT,
@CodTrabajador INT,
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 
SET Language 'Spanish';   

SET @FechaNacimiento=CAST(CONVERT(VARCHAR,@FechaNacimiento,103) AS DATE)
SET @FechaIngreso=CAST(CONVERT(VARCHAR,@FechaIngreso,103) AS DATE)


IF @FechaCese='19000101'
BEGIN
	SET @FechaCese=NULL
END
ELSE
BEGIN
	SET @FechaCese=CAST(CONVERT(VARCHAR,@FechaCese,103) AS DATE)
END

IF NOT EXISTS(SELECT 1 FROM Trabajador WHERE CodTipDocumentoIdent=@CodTipoDocumentoIdentidad AND DocumentoIdentidad=@DocumentoIdentidad AND Nombres=@Nombres AND ApellidoPaterno=@ApellidoPaterno AND CodTrabajador!=@CodTrabajador) 
BEGIN
	UPDATE Trabajador
	SET CodTipDocumentoIdent=@CodTipoDocumentoIdentidad,DocumentoIdentidad=@DocumentoIdentidad,Nombres=UPPER(@Nombres),ApellidoPaterno=UPPER(@ApellidoPaterno),ApellidoMaterno=UPPER(@ApellidoMaterno),FechaNacimiento=@FechaNacimiento,Sexo=@Sexo,CodEstadoCivil=@CodEstadoCivil,
	FechaIngreso=@FechaIngreso,FechaCese=@FechaCese,HaberMensual=@HaberMensual,CodTipoAportacion=@CodTipoAportacion,NroCUSPP=@NroCuspp,CodLaborTrabajo=@CodTipoTrabajo,CodCargoTrabajo=@CodCargo,CodFinanciera=@CodBanco,NroCuentaFinanciera=@NumeroCuentaBanco,
	NroCuentaCTS=@NumeroCuentaCTS,CodTipoPlanilla=@CodTipoPlanilla,CodPerfilPlanilla=@CodPerfilPlanilla,FechaModificacion=GETDATE(),CodUsuarioModificacion=@CodUsuarioModifica
	WHERE CodTrabajador=@CodTrabajador

	SET @Mensaje='EXITO'
END
ELSE
BEGIN
	SET @Mensaje='REPETIDO'
END


COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 


CREATE TABLE PlanillaEmpleados(
CodPlanillaEmpleados INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
CodTrabajador INT FOREIGN KEY REFERENCES Trabajador(CodTrabajador) NOT NULL,
CodPerfilPlanilla INT FOREIGN KEY REFERENCES PerfilPlanilla(CodPerfilPlanilla) NOT NULL,
FechaInicio DATE,
FechaFin DATE,
PeriodoMes CHAR(2),
PeriodoAno VARCHAR(4),
DiasLaborados NUMERIC(14,2),
DiasDomingosFeriados NUMERIC(14,2),
AsignacionFamiliar NUMERIC(14,2),
HorasSimple NUMERIC(14,2),
Movilidad NUMERIC(14,2),
OtrosIngresos NUMERIC(14,2),
VacacionesTruncas NUMERIC(14,2),
Gratificacion NUMERIC(14,2),
OtrosBeneficios NUMERIC(14,2),
SNP NUMERIC(14,2),
AporteObligatorio NUMERIC(14,2),
ComisionFlujo NUMERIC(14,2),
ComisionMixta NUMERIC(14,2),
PrimaSeguro NUMERIC(14,2),
AporteComplementario NUMERIC(14,2),
Renta5taCategoria NUMERIC(14,2),
EPS NUMERIC(14,2),
OtrosDescuentos NUMERIC(14,2),
EsSalud NUMERIC(14,2),
TotalIngresos NUMERIC(14,2), 
TotalBeneficios NUMERIC(14,2), 
TotalDescuentos NUMERIC(14,2), 
TotalAporteEmpresa NUMERIC(14,2),
TotalPagarTrabajador NUMERIC(14,2), 
TotalCostoTrabajador NUMERIC(14,2), 
FlagActivo BIT,
FechaRegistro DATETIME NOT NULL,
CodUsuarioRegistro INT FOREIGN KEY REFERENCES Usuario(CodUsuario) NOT NULL,
FechaModificacion DATETIME NULL,
CodUsuarioModificacion INT FOREIGN KEY REFERENCES Usuario(CodUsuario) NULL
)

GO

CREATE PROCEDURE PA_sel_ObtenerListadoEmpleados
AS
SELECT trab.CodTrabajador,DocumentoIdentidad+' - '+UPPER(ApellidoPaterno+' '+ApellidoMaterno)+', '+Nombres AS NombreTrabajador  
FROM Trabajador trab
LEFT JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
WHERE trab.FlagActivo=1 
AND CAST(ISNULL(FechaCese,DATEADD(MONTH,1,GETDATE())) AS DATE)>CAST(GETDATE() AS DATE)
AND trabtip.CodTipoPlanilla IN (3)
AND trabtip.FlagActivo=1

GO

CREATE PROCEDURE [dbo].[PA_ins_RegistrarPlanillaEmpleados]
@CodTrabajador INT,
@FechaInicio DATE,
@FechaFin DATE,
@PeriodoMes CHAR(2),
@PeriodoAno VARCHAR(4),
@DiasLaborados NUMERIC(14,2),
@DiasDomingosFeriados NUMERIC(14,2),
@AsignacionFamiliar NUMERIC(14,2),
@HorasExtraSimple NUMERIC(14,2),
@Pasajes NUMERIC(14,2),
@OtrosIngresos NUMERIC(14,2),
@VacacionesTruncas NUMERIC(14,2),
@Gratificacion NUMERIC(14,2),
@OtrosBeneficios NUMERIC(14,2),
@SNP NUMERIC(14,2),
@AporteObligatorio NUMERIC(14,2),
@ComisionFlujo NUMERIC(14,2),
@ComisionMixta NUMERIC(14,2),
@PrimaSeguro NUMERIC(14,2),
@AporteComplementario NUMERIC(14,2),
@Renta5taCategoria NUMERIC(14,2),
@EPS NUMERIC(14,2),
@OtrosDctos NUMERIC(14,2),
@EsSalud NUMERIC(14,2),
@TotalIngresos NUMERIC(14,2), 
@TotalBeneficios NUMERIC(14,2), 
@TotalDescuentos NUMERIC(14,2), 
@TotalAporteEmpresa NUMERIC(14,2),
@TotalPagarTrabajador NUMERIC(14,2), 
@TotalCostoTrabajador NUMERIC(14,2), 
@CodUsuarioRegistro INT,
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

DECLARE @CodPerfilPlanilla INT
SET @CodPerfilPlanilla=(SELECT CodPerfilPlanilla FROM TrabajadorTipoPlanilla WHERE CodTrabajador=@CodTrabajador AND CodTipoPlanilla=3 AND FlagActivo=1)

IF NOT EXISTS (SELECT 1 FROM Trabajador WHERE CAST(FechaCese AS DATE)<=CAST(GETDATE() AS DATE) AND CodTrabajador=@CodTrabajador)
BEGIN
	IF NOT EXISTS (SELECT 1 FROM PlanillaEmpleados WHERE CodTrabajador=@CodTrabajador AND CAST(@FechaInicio AS DATE) BETWEEN CAST(FechaInicio AS DATE) AND CAST(FechaFin AS DATE) AND FlagActivo=1)
	BEGIN	
		INSERT INTO PlanillaEmpleados(CodTrabajador,CodPerfilPlanilla,FechaInicio,FechaFin,PeriodoMes,PeriodoAno,DiasLaborados,DiasDomingosFeriados,AsignacionFamiliar,
		HorasSimple,Movilidad,OtrosIngresos,VacacionesTruncas,Gratificacion,OtrosBeneficios,SNP,AporteObligatorio,ComisionFlujo,ComisionMixta,PrimaSeguro,AporteComplementario,
		Renta5taCategoria,EPS,OtrosDescuentos,EsSalud,TotalIngresos,TotalBeneficios,TotalDescuentos,TotalAporteEmpresa,TotalPagarTrabajador,TotalCostoTrabajador,FlagActivo,
		FechaRegistro,CodUsuarioRegistro)
		VALUES (@CodTrabajador,@CodPerfilPlanilla,@FechaInicio,@FechaFin,@PeriodoMes,@PeriodoAno,@DiasLaborados,@DiasDomingosFeriados,@AsignacionFamiliar,
		@HorasExtraSimple,@Pasajes,@OtrosIngresos,@VacacionesTruncas,@Gratificacion,@OtrosDctos,@SNP,@AporteObligatorio,@ComisionFlujo,@ComisionMixta,@PrimaSeguro,@AporteComplementario,
		@Renta5taCategoria,@EPS,@OtrosDctos,@EsSalud,@TotalIngresos,@TotalBeneficios,@TotalDescuentos,@TotalAporteEmpresa,@TotalPagarTrabajador,@TotalCostoTrabajador,1,GETDATE(),@CodUsuarioRegistro)

		SET @Mensaje='EXITO'
	END
	ELSE
	BEGIN
		SET @Mensaje='REPETIDO'
	END
END
ELSE
BEGIN
	SET @Mensaje='CESADO'
END

COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 


GO

CREATE PROCEDURE [dbo].[PA_sel_CargarDatosPlanillaEmpleados]
@CodPlanilla INT
AS

SELECT plani.PeriodoAno,plani.PeriodoMes,plani.FechaInicio,plani.FechaFin,plani.CodTrabajador,plani.DiasLaborados,plani.DiasDomingosFeriados,
plani.AsignacionFamiliar,plani.Movilidad,plani.OtrosIngresos,plani.ComisionFlujo,plani.ComisionMixta,plani.Renta5taCategoria,
plani.EPS,plani.OtrosDescuentos,plani.Gratificacion,plani.VacacionesTruncas,plani.OtrosBeneficios
FROM PlanillaEmpleados plani INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador)
WHERE plani.CodPlanillaEmpleados=@CodPlanilla

GO

ALTER PROCEDURE [dbo].[PA_sel_ObtenerPlanillaEmpleadosRecientes]
AS
SET Language 'Spanish';   
SELECT TOP 50 plani.CodPlanillaEmpleados,UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad,DATENAME(MONTH, DATEADD( MONTH , CAST(plani.PeriodoMes AS INT) , -1 )) AS MesPeriodo,CONVERT(VARCHAR,plani.FechaInicio,103) AS FechaInicio,CONVERT(VARCHAR,plani.FechaFin,103) AS FechaFin,FORMAT(trab.HaberMensual ,'#,0.00','es-PE') AS HaberMensual, plani.DiasLaborados,
perf.Jornal,FORMAT((perf.Jornal*plani.DiasLaborados) ,'#,0.00','es-PE') AS Basico,FORMAT((perf.Jornal*plani.DiasDomingosFeriados),'#,0.00','es-PE') AS TotalDominical,FORMAT(plani.TotalIngresos,'#,0.00','es-PE') AS TotalIngresos,
FORMAT(plani.TotalBeneficios,'#,0.00','es-PE') AS TotalBeneficios,FORMAT(plani.TotalDescuentos,'#,0.00','es-PE') AS TotalDescuentos,FORMAT(plani.TotalAporteEmpresa,'#,0.00','es-PE') AS TotalAporteEmpresa,FORMAT(plani.TotalPagarTrabajador,'#,0.00','es-PE') AS TotalPagarTrabajador,
FORMAT(plani.TotalCostoTrabajador,'#,0.00','es-PE') AS TotalCostoTrabajador
FROM PlanillaEmpleados plani 
INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trabtip.CodPerfilPlanilla)
WHERE plani.FlagActivo=1 AND trabtip.CodTipoPlanilla=3
ORDER BY plani.FechaRegistro DESC


GO


CREATE PROCEDURE [dbo].[PA_upd_ActualizarPlanillaEmpleados]
@CodTrabajador INT,
@FechaInicio DATE,
@FechaFin DATE,
@PeriodoMes CHAR(2),
@PeriodoAno VARCHAR(4),
@DiasLaborados NUMERIC(14,2),
@DiasDomingosFeriados NUMERIC(14,2),
@AsignacionFamiliar NUMERIC(14,2),
@HorasExtraSimple NUMERIC(14,2),
@Pasajes NUMERIC(14,2),
@OtrosIngresos NUMERIC(14,2),
@VacacionesTruncas NUMERIC(14,2),
@Gratificacion NUMERIC(14,2),
@OtrosBeneficios NUMERIC(14,2),
@SNP NUMERIC(14,2),
@AporteObligatorio NUMERIC(14,2),
@ComisionFlujo NUMERIC(14,2),
@ComisionMixta NUMERIC(14,2),
@PrimaSeguro NUMERIC(14,2),
@AporteComplementario NUMERIC(14,2),
@Renta5taCategoria NUMERIC(14,2),
@EPS NUMERIC(14,2),
@OtrosDctos NUMERIC(14,2),
@EsSalud NUMERIC(14,2),
@TotalIngresos NUMERIC(14,2), 
@TotalBeneficios NUMERIC(14,2), 
@TotalDescuentos NUMERIC(14,2), 
@TotalAporteEmpresa NUMERIC(14,2),
@TotalPagarTrabajador NUMERIC(14,2), 
@TotalCostoTrabajador NUMERIC(14,2), 
@CodUsuarioModificacion INT,
@CodPlanillaEmpleados INT,
@Mensaje VARCHAR(500) OUTPUT
AS
BEGIN TRY                
BEGIN TRAN 

DECLARE @CodPerfilPlanilla INT
SET @CodPerfilPlanilla=(SELECT CodPerfilPlanilla FROM TrabajadorTipoPlanilla WHERE CodTrabajador=@CodTrabajador AND CodTipoPlanilla=3 AND FlagActivo=1)

IF NOT EXISTS (SELECT 1 FROM Trabajador WHERE CAST(FechaCese AS DATE)<=CAST(GETDATE() AS DATE) AND CodTrabajador=@CodTrabajador)
BEGIN
	IF NOT EXISTS (SELECT 1 FROM PlanillaConstruccion WHERE CodTrabajador=@CodTrabajador AND CAST(@FechaInicio AS DATE) BETWEEN CAST(FechaInicio AS DATE) AND CAST(FechaFin AS DATE) AND FlagActivo=1 AND CodPlanillaConstruccion!=@CodPlanillaEmpleados)
	BEGIN
		UPDATE PlanillaEmpleados
		SET CodTrabajador=@CodTrabajador,CodPerfilPlanilla=@CodPerfilPlanilla,FechaInicio=@FechaInicio,FechaFin=@FechaFin,PeriodoMes=@PeriodoMes,PeriodoAno=@PeriodoAno,DiasLaborados=@DiasLaborados,DiasDomingosFeriados=@DiasDomingosFeriados,
		AsignacionFamiliar=@AsignacionFamiliar,HorasSimple=@HorasExtraSimple,Movilidad=@Pasajes,OtrosIngresos=@OtrosIngresos,VacacionesTruncas=@VacacionesTruncas,
		Gratificacion=@Gratificacion,OtrosBeneficios=@OtrosBeneficios,SNP=@SNP,AporteObligatorio=@AporteObligatorio,ComisionFlujo=@ComisionFlujo,ComisionMixta=@ComisionMixta,
		PrimaSeguro=@PrimaSeguro,AporteComplementario=@AporteComplementario,Renta5taCategoria=@Renta5taCategoria,EPS=@EPS,OtrosDescuentos=@OtrosDctos,EsSalud=@EsSalud,
		TotalIngresos=@TotalIngresos,TotalBeneficios=@TotalBeneficios,TotalDescuentos=@TotalDescuentos,TotalAporteEmpresa=@TotalAporteEmpresa,TotalPagarTrabajador=@TotalPagarTrabajador,
		TotalCostoTrabajador=@TotalCostoTrabajador,FlagActivo=1,FechaModificacion=GETDATE(),CodUsuarioModificacion=@CodUsuarioModificacion
		WHERE CodPlanillaEmpleados=@CodPlanillaEmpleados

		SET @Mensaje='EXITO'
	END
	ELSE
	BEGIN
		SET @Mensaje='REPETIDO'
	END
END
ELSE
BEGIN
	SET @Mensaje='CESADO'
END

COMMIT TRAN                        
END TRY                
BEGIN CATCH                
ROLLBACK TRAN                
INSERT INTO ErrorProcedure(Mensaje,NombreProcedure,LineaError,FechaError)                
VALUES(ERROR_MESSAGE(),ERROR_PROCEDURE(),ERROR_LINE(),GETDATE())                
SET @Mensaje='ERROR: '+ERROR_MESSAGE()                
END CATCH 

GO

CREATE PROCEDURE [dbo].[PA_gen_ObtenerDetallePlanillaTrabajadorEmpleados]
@CodPlanilla INT
AS
	SELECT 
	ROW_NUMBER() OVER (ORDER BY CodPlanillaEmpleados DESC) [Nro],
	UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, carg.DescCargo AS [Status],
	perf.Jornal,plani.DiasLaborados,		
	CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,
	CAST((perf.Jornal*plani.DiasDomingosFeriados) AS NUMERIC(14,2)) AS Dominical,
	plani.Movilidad AS Pasajes,
	plani.AsignacionFamiliar AS [AsigFam],
	plani.HorasSimple AS [HESimples],
	plani.OtrosIngresos AS [OtrosIngresos],
	plani.TotalIngresos AS [TotalIngresos],
	aport.DescTipoAportacion AS Afp,
	plani.SNP AS Snp,
	plani.AporteObligatorio AS [AporteOblig],
	plani.ComisionFlujo AS ComisionFlujo,
	plani.ComisionMixta AS ComisionMixta,
	plani.PrimaSeguro AS PrimaSeguro,
	plani.AporteComplementario AS [AporteComple],
	plani.Renta5taCategoria AS [Rentade5ta],
	plani.EPS AS EPS,
	plani.OtrosDescuentos AS [OtrosDescuent],
	plani.TotalDescuentos AS [TotalDescuentos],
	plani.VacacionesTruncas AS Vacaciones,
	plani.Gratificacion AS Gratificacion,
	plani.OtrosBeneficios AS OtrosBeneficios,
	plani.TotalBeneficios AS [TotalBeneficios],
	plani.EsSalud AS EsSalud,
	plani.TotalAporteEmpresa AS [TotalEmpresa],	
	plani.TotalPagarTrabajador AS [TotalaPagar],
	plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
	CONVERT(VARCHAR,trab.FechaIngreso,103) AS FechaIngreso,
	CONVERT(VARCHAR,trab.FechaCese,103) AS FechaCese,
	trab.NroCUSPP,
	CONVERT(VARCHAR,FechaInicio,103) AS FechaInicial,CONVERT(VARCHAR,FechaFin,103) AS FechaFinal,DATENAME(MONTH,DATEADD(MONTH,CAST(PeriodoMes AS INT),-1 )) AS MesPeriodo, PeriodoAno AS AnoPeriodo	
	FROM PlanillaEmpleados plani 
	INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
	INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
	INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trabtip.CodPerfilPlanilla)
	INNER JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo)
	INNER JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
	WHERE plani.CodPlanillaEmpleados=@CodPlanilla




exec PA_gen_ObtenerListadoReportePlanilla '','','2018','11','1'

SELECT * FROM PlanillaConstruccion WHERE CodTrabajador=8 AND CAST('20181023' AS DATE) BETWEEN CAST(FechaInicio AS DATE) AND CAST(FechaFin AS DATE) AND FlagActivo=1 AND CodPlanillaConstruccion!=9

select * from PlanillaConstruccion
select * from PlanillaEventuales
SELECT * FROM EstadoCivil
select * from Trabajador
select * from Cargo
select * from PerfilPlanilla
select CodPerfilPlanilla,para.DescParametro,CampoParametro from PerfilPlanillaParametro perf inner join Parametro para on (para.CodParametro=perf.CodParametro)
SELECT * FROM Parametro
SELECT *FROM Trabajador
select * from TipoParametro
select * from UnidadMedidaParametro
select * from TipoAportacion
select * from LaborTrabajo
delete from PerfilPlanilla
where CodPerfilPlanilla in (20,21)

select * from usuario

update Trabajador
set FechaCese='20181231'
where CodTrabajador=8

select * from Trabajador 

INSERT INTO PerfilPlanillaParametro
VALUES (22,14,1,1)

update Trabajador
set CodPerfilPlanilla=22
where CodTrabajador=5

update Usuario
set Contrasena=HASHBYTES('SHA2_512','1234')
where CodUsuario=1

select para.CodParametro,para.DescParametro,parperf.CampoParametro 
from PerfilPlanillaParametro parperf 
inner  join Parametro para ON (para.CodParametro=parperf.CodParametro) 
inner join TipoParametro tip ON (tip.CodTipoParametro=para.CodTipoParametro)
where CodPerfilPlanilla=22 and tip.CodTipoParametro=3

alter table Usuario
add Contrasena VARBINARY(64)

ALTER table Usuario
drop column Contrasena

update Parametro
set FlagActivo=0
where CodParametro in (12)

INSERT INTO EstadoCivil
VALUES ('Divorciado')

select * from Parametro

update PerfilPlanilla
set Jornal=67.20
where CodPerfilPlanilla=22


select * from PerfilPlanilla

SELECT * FROM UnidadMedidaParametro

INSERT INTO UnidadMedidaParametro
VALUES('Horas')

INSERT INTO PerfilAcceso
VALUES ('Administrador',1)

INSERT INTO Parametro
VALUES('EsSalud Vida',2,1,1)

INSERT INTO TipoParametro
VALUES('Beneficios')

update UnidadMedidaParametro
set DescUnidadMedidaParametro='Horas (hrs)'
where CodUnidadMedidaParametro=4

select * from Parametro
select * from PerfilPlanillaParametro
select * from EstadoCivil

insert into EstadoCivil
values('Conviviente')


update Usuario
set Apellidos=' '
where CodUsuario=2

insert into PerfilPlanillaParametro
values (22,24,0,1)

insert into Usuario
values ('jdurand',null,'Jose Maria','Durand Misari',1,1)

select * from Usuario

INSERT INTO TipoAportacion
SELECT carg.nombre,AO,CO,PS,AC,MI,1 FROM bdsoftae1.dbo.AFPS carg


select * from tipoaportacion

delete from PlanillaConstruccion
where CodPlanillaConstruccion in (1,2)

select * from TipoPlanilla

update TipoPlanilla



set FlagActivo=1




select * from Trabajador

delete from ErrorProcedure

select * from ErrorProcedure


DBCC CHECKIDENT ('ErrorProcedure', RESEED, 0);


ALTER PROCEDURE [dbo].[PA_gen_ObtenerListadoReportePlanillaEventuales]
@FechaInicial DATE,
@FechaFinal DATE,
@AnoPeriodo CHAR(4),
@MesPeriodo CHAR(2),
@CodTipoBusqueda INT
AS

IF (@CodTipoBusqueda=1)
BEGIN	
	SELECT 
	ROW_NUMBER() OVER (ORDER BY CodPlanillaConstruccionEventuales DESC) [Nro],
	UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, UPPER(carg.DescLaborTrabajo) AS [Status],
	perf.Jornal,plani.DiasLaborados,		
	CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,			
	plani.Bonificacion AS Bonificacion,
	plani.OtrosIngresos AS OtrosIngresos,		
	plani.TotalIngresos AS TotalIngresos,
	aport.DescTipoAportacion AS Afp,	
	plani.Prestamos AS Prestamos,
	plani.OtrosDctos AS [OtrosDescuent],
	plani.TotalDescuentos AS [TotalDescuentos],		
	plani.TotalPagarTrabajador AS [TotalaPagar],
	plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
	CONVERT(VARCHAR,CAST(DATEADD(M,CAST(@MesPeriodo AS INT)-1,DATEADD(YYYY,CAST(@AnoPeriodo AS INT)-1900, 0)) AS DATE),103) AS FechaInicial,CONVERT(VARCHAR,CAST(DATEADD(D,-1,DATEADD(M,CAST(@MesPeriodo AS INT),DATEADD(YYYY, CAST(@AnoPeriodo AS INT)-1900,0))) AS DATE),103) AS FechaFinal,DATENAME(MONTH,DATEADD(MONTH,CAST(@MesPeriodo AS INT),-1 )) AS MesPeriodo, @AnoPeriodo AS AnoPeriodo
	FROM PlanillaConstruccionEventuales plani 
	INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
	INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
	INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trabtip.CodPerfilPlanilla)
	LEFT JOIN LaborTrabajo carg ON (carg.CodLaborTrabajo=trab.CodLaborTrabajo)
	LEFT JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
	WHERE plani.FlagActivo=1
	AND plani.PeriodoMes=CAST(@MesPeriodo AS INT)
	AND plani.PeriodoAno=@AnoPeriodo
	AND trabtip.CodTipoPlanilla=2
	ORDER BY plani.FechaRegistro DESC
END
ELSE IF (@CodTipoBusqueda=2)
BEGIN
	SELECT 
	ROW_NUMBER() OVER (ORDER BY CodPlanillaConstruccionEventuales DESC) [Nro],
	UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, UPPER(carg.DescLaborTrabajo) AS [Status],
	perf.Jornal,plani.DiasLaborados,		
	CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,			
	plani.Bonificacion AS Bonificacion,
	plani.OtrosIngresos AS OtrosIngresos,		
	plani.TotalIngresos AS TotalIngresos,
	aport.DescTipoAportacion AS Afp,	
	plani.Prestamos AS Prestamos,
	plani.OtrosDctos AS [OtrosDescuent],
	plani.TotalDescuentos AS [TotalDescuentos],		
	plani.TotalPagarTrabajador AS [TotalaPagar],
	plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
	CONVERT(VARCHAR,CAST(@FechaInicial AS DATE),103) AS FechaInicial,CONVERT(VARCHAR,CAST(@FechaFinal AS DATE),103) AS FechaFinal
	FROM PlanillaConstruccionEventuales plani 
	INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
	INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
	INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trabtip.CodPerfilPlanilla)
	LEFT JOIN LaborTrabajo carg ON (carg.CodLaborTrabajo=trab.CodLaborTrabajo)
	LEFT JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
	WHERE plani.FlagActivo=1
	AND plani.FechaInicio BETWEEN CAST(@FechaInicial AS DATE) AND CAST(@FechaFinal AS DATE)
	AND trabtip.CodTipoPlanilla=2
	ORDER BY plani.FechaRegistro DESC	
END

GO


CREATE PROCEDURE [dbo].[PA_gen_ObtenerListadoReportePlanillaEmpleados]
@FechaInicial DATE,
@FechaFinal DATE,
@AnoPeriodo CHAR(4),
@MesPeriodo CHAR(2),
@CodTipoBusqueda INT
AS

IF (@CodTipoBusqueda=1)
BEGIN		
	SELECT 
	ROW_NUMBER() OVER (ORDER BY CodPlanillaEmpleados DESC) [Nro],
	UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, carg.DescCargo AS [Status],
	perf.Jornal,plani.DiasLaborados,		
	CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,
	CAST((perf.Jornal*plani.DiasDomingosFeriados) AS NUMERIC(14,2)) AS Dominical,
	plani.Movilidad AS Pasajes,
	plani.AsignacionFamiliar AS [AsigFam],
	plani.OtrosIngresos AS Reintegro,		
	plani.HorasSimple AS [HESimples],		
	plani.TotalIngresos AS [TotalIngresos],
	aport.DescTipoAportacion AS Afp,
	plani.SNP AS Snp,
	plani.AporteObligatorio AS [AporteOblig],
	plani.ComisionFlujo AS ComisionFlujo,
	plani.ComisionMixta AS ComisionMixta,
	plani.PrimaSeguro AS PrimaSeguro,
	plani.AporteComplementario AS [AporteComple],	
	plani.Renta5taCategoria AS [Rentade5ta],
	plani.EPS AS EPS,
	plani.OtrosDescuentos AS [OtrosDescuent],
	plani.TotalDescuentos AS [TotalDescuentos],
	plani.VacacionesTruncas AS Vacaciones,
	plani.Gratificacion AS Gratificacion,
	plani.OtrosBeneficios AS Liquidacion,	
	plani.TotalBeneficios AS [TotalBeneficios],
	plani.EsSalud AS EsSalud,	
	plani.TotalAporteEmpresa AS [TotalEmpresa],	
	plani.TotalPagarTrabajador AS [TotalaPagar],
	plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
	CONVERT(VARCHAR,CAST(DATEADD(M,CAST(@MesPeriodo AS INT)-1,DATEADD(YYYY,CAST(@AnoPeriodo AS INT)-1900, 0)) AS DATE),103) AS FechaInicial,CONVERT(VARCHAR,CAST(DATEADD(D,-1,DATEADD(M,CAST(@MesPeriodo AS INT),DATEADD(YYYY, CAST(@AnoPeriodo AS INT)-1900,0))) AS DATE),103) AS FechaFinal,DATENAME(MONTH,DATEADD(MONTH,CAST(@MesPeriodo AS INT),-1 )) AS MesPeriodo, @AnoPeriodo AS AnoPeriodo
	FROM PlanillaEmpleados plani 
	INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
	INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
	INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trabtip.CodPerfilPlanilla)
	LEFT JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo)
	LEFT JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
	WHERE plani.FlagActivo=1
	AND plani.PeriodoMes=CAST(@MesPeriodo AS INT)
	AND plani.PeriodoAno=@AnoPeriodo
	AND trabtip.CodTipoPlanilla=3
	ORDER BY plani.FechaRegistro DESC
END
ELSE IF (@CodTipoBusqueda=2)
BEGIN
	SELECT 
	ROW_NUMBER() OVER (ORDER BY CodPlanillaEmpleados DESC) [Nro],
	UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, carg.DescCargo AS [Status],
	perf.Jornal,plani.DiasLaborados,		
	CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,
	CAST((perf.Jornal*plani.DiasDomingosFeriados) AS NUMERIC(14,2)) AS Dominical,
	plani.Movilidad AS Pasajes,
	plani.AsignacionFamiliar AS [AsigFam],
	plani.OtrosIngresos AS Reintegro,		
	plani.HorasSimple AS [HESimples],		
	plani.TotalIngresos AS [TotalIngresos],
	aport.DescTipoAportacion AS Afp,
	plani.SNP AS Snp,
	plani.AporteObligatorio AS [AporteOblig],
	plani.ComisionFlujo AS ComisionFlujo,
	plani.ComisionMixta AS ComisionMixta,
	plani.PrimaSeguro AS PrimaSeguro,
	plani.AporteComplementario AS [AporteComple],	
	plani.Renta5taCategoria AS [Rentade5ta],
	plani.EPS AS EPS,
	plani.OtrosDescuentos AS [OtrosDescuent],
	plani.TotalDescuentos AS [TotalDescuentos],
	plani.VacacionesTruncas AS Vacaciones,
	plani.Gratificacion AS Gratificacion,
	plani.OtrosBeneficios AS Liquidacion,	
	plani.TotalBeneficios AS [TotalBeneficios],
	plani.EsSalud AS EsSalud,	
	plani.TotalAporteEmpresa AS [TotalEmpresa],	
	plani.TotalPagarTrabajador AS [TotalaPagar],
	plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
	CONVERT(VARCHAR,CAST(@FechaInicial AS DATE),103) AS FechaInicial,CONVERT(VARCHAR,CAST(@FechaFinal AS DATE),103) AS FechaFinal
	FROM PlanillaEmpleados plani 
	INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
	INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
	INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trabtip.CodPerfilPlanilla)
	LEFT JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo)
	LEFT JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
	WHERE plani.FlagActivo=1
	AND plani.FechaInicio BETWEEN CAST(@FechaInicial AS DATE) AND CAST(@FechaFinal AS DATE)
	AND trabtip.CodTipoPlanilla=3
	ORDER BY plani.FechaRegistro DESC
END

GO

ALTER PROCEDURE [dbo].[PA_gen_ObtenerListadoReportePlanillaDestajo]
@FechaInicial DATE,
@FechaFinal DATE,
@AnoPeriodo CHAR(4),
@MesPeriodo CHAR(2),
@CodTipoBusqueda INT
AS

IF (@CodTipoBusqueda=1)
BEGIN		
	SELECT 
	ROW_NUMBER() OVER (ORDER BY CodPlanillaEventual DESC) [Nro],
	UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, lab.DescLaborTrabajo AS [Status],
	plani.Cantidad,	'(' + serv.UnidadMedida + '/' + CAST(serv.PrecioUnit AS VARCHAR(50)) + ')' AS Unidad,	
	plani.TotalPagar AS TotalPagar,	
	serv.DescripcionServicio AS DescripcionServicio,
	lab.DescLaborTrabajo AS DescLaborTrabajo,		
	CONVERT(VARCHAR,CAST(DATEADD(M,CAST(@MesPeriodo AS INT)-1,DATEADD(YYYY,CAST(@AnoPeriodo AS INT)-1900, 0)) AS DATE),103) AS FechaInicial,CONVERT(VARCHAR,CAST(DATEADD(D,-1,DATEADD(M,CAST(@MesPeriodo AS INT),DATEADD(YYYY, CAST(@AnoPeriodo AS INT)-1900,0))) AS DATE),103) AS FechaFinal,DATENAME(MONTH,DATEADD(MONTH,CAST(@MesPeriodo AS INT),-1 )) AS MesPeriodo, @AnoPeriodo AS AnoPeriodo
	FROM PlanillaEventuales plani 
	INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
	INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
	LEFT JOIN LaborTrabajo lab ON (lab.CodLaborTrabajo=trab.CodLaborTrabajo)
	LEFT JOIN Servicios serv ON (serv.CodServicio=plani.CodServicio)
	WHERE plani.FlagActivo=1
	AND CAST(@MesPeriodo AS INT) BETWEEN MONTH(plani.FechaInicio) AND MONTH(plani.FechaFin)
	AND CAST(@AnoPeriodo AS INT) BETWEEN YEAR(plani.FechaInicio) AND YEAR(plani.FechaFin)
	AND trabtip.CodTipoPlanilla=4
	ORDER BY plani.FechaRegistro DESC
END
ELSE IF (@CodTipoBusqueda=2)
BEGIN	
	SELECT 
	ROW_NUMBER() OVER (ORDER BY CodPlanillaEventual DESC) [Nro],
	UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, lab.DescLaborTrabajo AS [Status],
	plani.Cantidad,	'(' + serv.UnidadMedida + '/' + CAST(serv.PrecioUnit AS VARCHAR(50)) + ')' AS Unidad,	
	plani.TotalPagar AS TotalPagar,		
	serv.DescripcionServicio AS DescripcionServicio,
	lab.DescLaborTrabajo AS DescLaborTrabajo,		
	CONVERT(VARCHAR,CAST(@FechaInicial AS DATE),103) AS FechaInicial,CONVERT(VARCHAR,CAST(@FechaFinal AS DATE),103) AS FechaFinal
	FROM PlanillaEventuales plani 
	INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
	INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
	LEFT JOIN LaborTrabajo lab ON (lab.CodLaborTrabajo=trab.CodLaborTrabajo)
	LEFT JOIN Servicios serv ON (serv.CodServicio=plani.CodServicio)
	WHERE plani.FlagActivo=1
	AND plani.FechaInicio BETWEEN CAST(@FechaInicial AS DATE) AND CAST(@FechaFinal AS DATE)
	AND trabtip.CodTipoPlanilla=4
	ORDER BY plani.FechaRegistro DESC
END



SELECT * FROM Servicios



ALTER PROCEDURE [dbo].[PA_gen_ObtenerListadoReportePlanilla]
@FechaInicial DATE,
@FechaFinal DATE,
@AnoPeriodo CHAR(4),
@MesPeriodo CHAR(2),
@CodTipoPlanilla INT,
@CodTipoBusqueda INT
AS

IF (@CodTipoBusqueda=1)
BEGIN	
	IF(@CodTipoPlanilla=1)
	BEGIN
		SELECT 
		ROW_NUMBER() OVER (ORDER BY CodPlanillaConstruccion DESC) [Nro],
		UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, carg.DescCargo AS [Status],
		perf.Jornal,plani.DiasLaborados,		
		CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,
		CAST((perf.Jornal*plani.DiasDomingosFeriados) AS NUMERIC(14,2)) AS Dominical,
		plani.Pasajes AS Pasajes,
		plani.BUC AS BUC,
		plani.AsignacionFamiliar AS [AsigFam],
		plani.Reintegro AS Reintegro,
		plani.Bonificacion AS Bonificacion,
		plani.HorasSimple AS [HESimples],
		plani.HorasExtras60 AS [HE60],
		plani.HorasExtras100 AS [HE100],
		plani.TotalIngresos AS [TotalIngresos],
		aport.DescTipoAportacion AS Afp,
		plani.SNP AS Snp,
		plani.AporteObligatorio AS [AporteOblig],
		plani.ComisionFlujo AS ComisionFlujo,
		plani.ComisionMixta AS ComisionMixta,
		plani.PrimaSeguro AS PrimaSeguro,
		plani.AporteComplementario AS [AporteComple],
		plani.Conafovicer AS Conafovicer,
		plani.AporteSindical AS AporteSindical,
		plani.EsSaludVida AS EsSaludVida,
		plani.Renta5taCategoria AS [Rentade5ta],
		plani.EPS AS EPS,
		plani.OtrosDctos AS [OtrosDescuent],
		plani.TotalDescuentos AS [TotalDescuentos],
		plani.Vacacional AS Vacaciones,
		plani.Gratificacion AS Gratificacion,
		plani.Liquidacion AS Liquidacion,
		plani.BonificacionExtraSalud AS [BonoExtSalud],
		plani.BonificacionExtraPension AS [BonoExtPension],
		plani.TotalBeneficios AS [TotalBeneficios],
		plani.EsSalud AS EsSalud,
		plani.AportComplementarioAFP AS [AporteComplemenAFP],
		plani.SCTRSalud AS [CstrSalud],
		plani.SCTRPension AS [CstrPension],
		plani.TotalAporteEmpresa AS [TotalEmpresa],	
		plani.TotalPagarTrabajador AS [TotalaPagar],
		plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
		CONVERT(VARCHAR,CAST(DATEADD(M,CAST(@MesPeriodo AS INT)-1,DATEADD(YYYY,CAST(@AnoPeriodo AS INT)-1900, 0)) AS DATE),103) AS FechaInicial,CONVERT(VARCHAR,CAST(DATEADD(D,-1,DATEADD(M,CAST(@MesPeriodo AS INT),DATEADD(YYYY, CAST(@AnoPeriodo AS INT)-1900,0))) AS DATE),103) AS FechaFinal,DATENAME(MONTH,DATEADD(MONTH,CAST(@MesPeriodo AS INT),-1 )) AS MesPeriodo, @AnoPeriodo AS AnoPeriodo
		FROM PlanillaConstruccion plani 
		INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
		INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
		INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trabtip.CodPerfilPlanilla)
		LEFT JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo)
		LEFT JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
		WHERE plani.FlagActivo=1
		AND plani.PeriodoMes=CAST(@MesPeriodo AS INT)
		AND plani.PeriodoAno=@AnoPeriodo
		AND trabtip.CodTipoPlanilla=@CodTipoPlanilla
		ORDER BY plani.FechaRegistro DESC
	END
	ELSE IF (@CodTipoPlanilla=2)
	BEGIN
		SELECT 
		ROW_NUMBER() OVER (ORDER BY CodPlanillaConstruccionEventuales DESC) [Nro],
		UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, carg.DescCargo AS [Status],
		perf.Jornal,plani.DiasLaborados,		
		CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,			
		plani.Bonificacion AS Bonificacion,
		plani.OtrosIngresos AS OtrosIngresos,		
		plani.TotalIngresos AS TotalIngresos,
		aport.DescTipoAportacion AS Afp,	
		plani.Prestamos AS Prestamos,
		plani.OtrosDctos AS [OtrosDescuent],
		plani.TotalDescuentos AS [TotalDescuentos],		
		plani.TotalPagarTrabajador AS [TotalaPagar],
		plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
		CONVERT(VARCHAR,CAST(DATEADD(M,CAST(@MesPeriodo AS INT)-1,DATEADD(YYYY,CAST(@AnoPeriodo AS INT)-1900, 0)) AS DATE),103) AS FechaInicial,CONVERT(VARCHAR,CAST(DATEADD(D,-1,DATEADD(M,CAST(@MesPeriodo AS INT),DATEADD(YYYY, CAST(@AnoPeriodo AS INT)-1900,0))) AS DATE),103) AS FechaFinal,DATENAME(MONTH,DATEADD(MONTH,CAST(@MesPeriodo AS INT),-1 )) AS MesPeriodo, @AnoPeriodo AS AnoPeriodo
		FROM PlanillaConstruccionEventuales plani 
		INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
		INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
		INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trabtip.CodPerfilPlanilla)
		LEFT JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo)
		LEFT JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
		WHERE plani.FlagActivo=1
		AND plani.PeriodoMes=CAST(@MesPeriodo AS INT)
		AND plani.PeriodoAno=@AnoPeriodo
		AND trabtip.CodTipoPlanilla=@CodTipoPlanilla
		ORDER BY plani.FechaRegistro DESC
	END
	ELSE IF (@CodTipoPlanilla=3)
	BEGIN
		SELECT 
		ROW_NUMBER() OVER (ORDER BY CodPlanillaEmpleados DESC) [Nro],
		UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, carg.DescCargo AS [Status],
		perf.Jornal,plani.DiasLaborados,		
		CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,
		CAST((perf.Jornal*plani.DiasDomingosFeriados) AS NUMERIC(14,2)) AS Dominical,
		plani.Movilidad AS Pasajes,
		plani.AsignacionFamiliar AS [AsigFam],
		plani.OtrosIngresos AS Reintegro,		
		plani.HorasSimple AS [HESimples],		
		plani.TotalIngresos AS [TotalIngresos],
		aport.DescTipoAportacion AS Afp,
		plani.SNP AS Snp,
		plani.AporteObligatorio AS [AporteOblig],
		plani.ComisionFlujo AS ComisionFlujo,
		plani.ComisionMixta AS ComisionMixta,
		plani.PrimaSeguro AS PrimaSeguro,
		plani.AporteComplementario AS [AporteComple],	
		plani.Renta5taCategoria AS [Rentade5ta],
		plani.EPS AS EPS,
		plani.OtrosDescuentos AS [OtrosDescuent],
		plani.TotalDescuentos AS [TotalDescuentos],
		plani.VacacionesTruncas AS Vacaciones,
		plani.Gratificacion AS Gratificacion,
		plani.OtrosBeneficios AS Liquidacion,	
		plani.TotalBeneficios AS [TotalBeneficios],
		plani.EsSalud AS EsSalud,	
		plani.TotalAporteEmpresa AS [TotalEmpresa],	
		plani.TotalPagarTrabajador AS [TotalaPagar],
		plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
		CONVERT(VARCHAR,CAST(DATEADD(M,CAST(@MesPeriodo AS INT)-1,DATEADD(YYYY,CAST(@AnoPeriodo AS INT)-1900, 0)) AS DATE),103) AS FechaInicial,CONVERT(VARCHAR,CAST(DATEADD(D,-1,DATEADD(M,CAST(@MesPeriodo AS INT),DATEADD(YYYY, CAST(@AnoPeriodo AS INT)-1900,0))) AS DATE),103) AS FechaFinal,DATENAME(MONTH,DATEADD(MONTH,CAST(@MesPeriodo AS INT),-1 )) AS MesPeriodo, @AnoPeriodo AS AnoPeriodo
		FROM PlanillaEmpleados plani 
		INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
		INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
		INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trabtip.CodPerfilPlanilla)
		LEFT JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo)
		LEFT JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
		WHERE plani.FlagActivo=1
		AND plani.PeriodoMes=CAST(@MesPeriodo AS INT)
		AND plani.PeriodoAno=@AnoPeriodo
		AND trabtip.CodTipoPlanilla=@CodTipoPlanilla
		ORDER BY plani.FechaRegistro DESC
	END
	ELSE IF (@CodTipoPlanilla=4)
	BEGIN
		SELECT 
		ROW_NUMBER() OVER (ORDER BY CodPlanillaEventual DESC) [Nro],
		UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, lab.DescLaborTrabajo AS [Status],
		perf.Jornal,plani.Cantidad,		
		plani.TotalPagar AS TotalPagar,	
		serv.DescripcionServicio AS DescripcionServicio,
		lab.DescLaborTrabajo AS DescLaborTrabajo,		
		CONVERT(VARCHAR,CAST(DATEADD(M,CAST(@MesPeriodo AS INT)-1,DATEADD(YYYY,CAST(@AnoPeriodo AS INT)-1900, 0)) AS DATE),103) AS FechaInicial,CONVERT(VARCHAR,CAST(DATEADD(D,-1,DATEADD(M,CAST(@MesPeriodo AS INT),DATEADD(YYYY, CAST(@AnoPeriodo AS INT)-1900,0))) AS DATE),103) AS FechaFinal,DATENAME(MONTH,DATEADD(MONTH,CAST(@MesPeriodo AS INT),-1 )) AS MesPeriodo, @AnoPeriodo AS AnoPeriodo
		FROM PlanillaEventuales plani 
		INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
		INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
		INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trabtip.CodPerfilPlanilla)
		LEFT JOIN LaborTrabajo lab ON (lab.CodLaborTrabajo=trab.CodLaborTrabajo)
		LEFT JOIN Servicios serv ON (serv.CodServicio=plani.CodServicio)
		WHERE plani.FlagActivo=1
		AND CAST(@MesPeriodo AS INT) BETWEEN MONTH(plani.FechaInicio) AND MONTH(plani.FechaFin)
		AND CAST(@AnoPeriodo AS INT) BETWEEN YEAR(plani.FechaInicio) AND YEAR(plani.FechaFin)
		AND trabtip.CodTipoPlanilla=@CodTipoPlanilla
		ORDER BY plani.FechaRegistro DESC
	END
END
ELSE IF (@CodTipoBusqueda=2)
BEGIN
	IF(@CodTipoPlanilla=1)
	BEGIN
		SELECT 
		ROW_NUMBER() OVER (ORDER BY CodPlanillaConstruccion DESC) [Nro],
		UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, carg.DescCargo AS [Status],
		perf.Jornal,plani.DiasLaborados,		
		CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,
		CAST((perf.Jornal*plani.DiasDomingosFeriados) AS NUMERIC(14,2)) AS Dominical,
		plani.Pasajes AS Pasajes,
		plani.BUC AS BUC,
		plani.AsignacionFamiliar AS [AsigFam],
		plani.Reintegro AS Reintegro,
		plani.Bonificacion AS Bonificacion,
		plani.HorasSimple AS [HESimples],
		plani.HorasExtras60 AS [HE60],
		plani.HorasExtras100 AS [HE100],
		plani.TotalIngresos AS [TotalIngresos],
		aport.DescTipoAportacion AS Afp,
		plani.SNP AS Snp,
		plani.AporteObligatorio AS [AporteOblig],
		plani.ComisionFlujo AS ComisionFlujo,
		plani.ComisionMixta AS ComisionMixta,
		plani.PrimaSeguro AS PrimaSeguro,
		plani.AporteComplementario AS [AporteComple],
		plani.Conafovicer AS Conafovicer,
		plani.AporteSindical AS AporteSindical,
		plani.EsSaludVida AS EsSaludVida,
		plani.Renta5taCategoria AS [Rentade5ta],
		plani.EPS AS EPS,
		plani.OtrosDctos AS [OtrosDescuent],
		plani.TotalDescuentos AS [TotalDescuentos],
		plani.Vacacional AS Vacaciones,
		plani.Gratificacion AS Gratificacion,
		plani.Liquidacion AS Liquidacion,
		plani.BonificacionExtraSalud AS [BonoExtSalud],
		plani.BonificacionExtraPension AS [BonoExtPension],
		plani.TotalBeneficios AS [TotalBeneficios],
		plani.EsSalud AS EsSalud,
		plani.AportComplementarioAFP AS [AporteComplemenAFP],
		plani.SCTRSalud AS [CstrSalud],
		plani.SCTRPension AS [CstrPension],
		plani.TotalAporteEmpresa AS [TotalEmpresa],	
		plani.TotalPagarTrabajador AS [TotalaPagar],
		plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
		CONVERT(VARCHAR,CAST(@FechaInicial AS DATE),103) AS FechaInicial,CONVERT(VARCHAR,CAST(@FechaFinal AS DATE),103) AS FechaFinal
		FROM PlanillaConstruccion plani 
		INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
		INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
		INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trabtip.CodPerfilPlanilla)
		LEFT JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo)
		LEFT JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
		WHERE plani.FlagActivo=1
		AND plani.FechaInicio BETWEEN CAST(@FechaInicial AS DATE) AND CAST(@FechaFinal AS DATE)
		AND trabtip.CodTipoPlanilla=@CodTipoPlanilla
		ORDER BY plani.FechaRegistro DESC
	END
	IF(@CodTipoPlanilla=2)
	BEGIN
		SELECT 
		ROW_NUMBER() OVER (ORDER BY CodPlanillaConstruccionEventuales DESC) [Nro],
		UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, carg.DescCargo AS [Status],
		perf.Jornal,plani.DiasLaborados,		
		CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,			
		plani.Bonificacion AS Bonificacion,
		plani.OtrosIngresos AS OtrosIngresos,		
		plani.TotalIngresos AS TotalIngresos,
		aport.DescTipoAportacion AS Afp,	
		plani.Prestamos AS Prestamos,
		plani.OtrosDctos AS [OtrosDescuent],
		plani.TotalDescuentos AS [TotalDescuentos],		
		plani.TotalPagarTrabajador AS [TotalaPagar],
		plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
		CONVERT(VARCHAR,CAST(@FechaInicial AS DATE),103) AS FechaInicial,CONVERT(VARCHAR,CAST(@FechaFinal AS DATE),103) AS FechaFinal
		FROM PlanillaConstruccionEventuales plani 
		INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
		INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
		INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trabtip.CodPerfilPlanilla)
		LEFT JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo)
		LEFT JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
		WHERE plani.FlagActivo=1
		AND plani.FechaInicio BETWEEN CAST(@FechaInicial AS DATE) AND CAST(@FechaFinal AS DATE)
		AND trabtip.CodTipoPlanilla=@CodTipoPlanilla
		ORDER BY plani.FechaRegistro DESC
	END
	IF(@CodTipoPlanilla=3)
	BEGIN
		SELECT 
		ROW_NUMBER() OVER (ORDER BY CodPlanillaEmpleados DESC) [Nro],
		UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, carg.DescCargo AS [Status],
		perf.Jornal,plani.DiasLaborados,		
		CAST((perf.Jornal*plani.DiasLaborados) AS NUMERIC(14,2)) AS Basico,
		CAST((perf.Jornal*plani.DiasDomingosFeriados) AS NUMERIC(14,2)) AS Dominical,
		plani.Movilidad AS Pasajes,
		plani.AsignacionFamiliar AS [AsigFam],
		plani.OtrosIngresos AS Reintegro,		
		plani.HorasSimple AS [HESimples],		
		plani.TotalIngresos AS [TotalIngresos],
		aport.DescTipoAportacion AS Afp,
		plani.SNP AS Snp,
		plani.AporteObligatorio AS [AporteOblig],
		plani.ComisionFlujo AS ComisionFlujo,
		plani.ComisionMixta AS ComisionMixta,
		plani.PrimaSeguro AS PrimaSeguro,
		plani.AporteComplementario AS [AporteComple],	
		plani.Renta5taCategoria AS [Rentade5ta],
		plani.EPS AS EPS,
		plani.OtrosDescuentos AS [OtrosDescuent],
		plani.TotalDescuentos AS [TotalDescuentos],
		plani.VacacionesTruncas AS Vacaciones,
		plani.Gratificacion AS Gratificacion,
		plani.OtrosBeneficios AS Liquidacion,	
		plani.TotalBeneficios AS [TotalBeneficios],
		plani.EsSalud AS EsSalud,	
		plani.TotalAporteEmpresa AS [TotalEmpresa],	
		plani.TotalPagarTrabajador AS [TotalaPagar],
		plani.TotalCostoTrabajador AS [CostoTotaldelTrabajador],
		CONVERT(VARCHAR,CAST(@FechaInicial AS DATE),103) AS FechaInicial,CONVERT(VARCHAR,CAST(@FechaFinal AS DATE),103) AS FechaFinal
		FROM PlanillaEmpleados plani 
		INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
		INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
		INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trabtip.CodPerfilPlanilla)
		LEFT JOIN Cargo carg ON (carg.CodCargo=trab.CodCargoTrabajo)
		LEFT JOIN TipoAportacion aport ON (aport.CodTipoAportacion=trab.CodTipoAportacion)
		WHERE plani.FlagActivo=1
		AND plani.FechaInicio BETWEEN CAST(@FechaInicial AS DATE) AND CAST(@FechaFinal AS DATE)
		AND trabtip.CodTipoPlanilla=@CodTipoPlanilla
		ORDER BY plani.FechaRegistro DESC
	END
	IF(@CodTipoPlanilla=4)
	BEGIN
		SELECT 
		ROW_NUMBER() OVER (ORDER BY CodPlanillaEventual DESC) [Nro],
		UPPER(trab.ApellidoPaterno + ' ' + trab.ApellidoMaterno + ', ' + trab.Nombres) AS Nombres, trab.DocumentoIdentidad, lab.DescLaborTrabajo AS [Status],
		perf.Jornal,plani.Cantidad,		
		plani.TotalPagar AS TotalPagar,		
		serv.DescripcionServicio AS DescripcionServicio,
		lab.DescLaborTrabajo AS DescLaborTrabajo,		
		CONVERT(VARCHAR,CAST(@FechaInicial AS DATE),103) AS FechaInicial,CONVERT(VARCHAR,CAST(@FechaFinal AS DATE),103) AS FechaFinal
		FROM PlanillaEventuales plani 
		INNER JOIN Trabajador trab ON (trab.CodTrabajador=plani.CodTrabajador) 
		INNER JOIN TrabajadorTipoPlanilla trabtip ON (trabtip.CodTrabajador=trab.CodTrabajador)
		INNER JOIN PerfilPlanilla perf ON (perf.CodPerfilPlanilla=trabtip.CodPerfilPlanilla)
		LEFT JOIN LaborTrabajo lab ON (lab.CodLaborTrabajo=trab.CodLaborTrabajo)
		LEFT JOIN Servicios serv ON (serv.CodServicio=plani.CodServicio)
		WHERE plani.FlagActivo=1
		AND plani.FechaInicio BETWEEN CAST(@FechaInicial AS DATE) AND CAST(@FechaFinal AS DATE)
		AND trabtip.CodTipoPlanilla=@CodTipoPlanilla
		ORDER BY plani.FechaRegistro DESC
	END
END