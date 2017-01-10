using System.Configuration;

namespace KinniNet.Business.Utils
{
    public static class BusinessVariables
    {
        public static class Directorios
        {
            public static string RepositorioTemporalInformacionConsulta = ConfigurationManager.AppSettings["Repositorio"] + ConfigurationManager.AppSettings["RepositorioInfomracionConsultas"] + ConfigurationManager.AppSettings["CarpetaTemporal"];
            public static string RepositorioInformacionConsulta = ConfigurationManager.AppSettings["Repositorio"] + ConfigurationManager.AppSettings["RepositorioInfomracionConsultas"];
            public static string RepositorioInformacionConsultaHtml = ConfigurationManager.AppSettings["PathInformacionConsultaHtml"];
            public static string RepositorioTemporalMascara = ConfigurationManager.AppSettings["Repositorio"] + ConfigurationManager.AppSettings["RepositorioMascara"] + ConfigurationManager.AppSettings["CarpetaTemporal"];
            public static string RepositorioMascara = ConfigurationManager.AppSettings["Repositorio"] + ConfigurationManager.AppSettings["RepositorioMascara"];
            public static string RepositorioTemporal = ConfigurationManager.AppSettings["Repositorio"] + ConfigurationManager.AppSettings["CarpetaTemporal"];
            public static string RepositorioRepositorio = ConfigurationManager.AppSettings["Repositorio"];
        }

        public static class ComboBoxCatalogo
        {
            public static int Index = 0;
            public static int Value = 0;
            public static string Descripcion = "==SELECCIONE==";
            public static bool Habilitado = false;
        }

        public static class ParametrosMascaraCaptura
        {
            public static string PrefijoTabla = "Mascara";
            public static string PrefijoComandoInsertar = "uspInsert";
            public static string PrefijoComandoActualizar = "uspUpdate";
            public static string NombreCampoRandom = "CLAVEREGISTRO";
            public static string TipoCampoRandom = "NVARCHAR(20)";
            public static string CampoRandom = NombreCampoRandom + " " + TipoCampoRandom;
            public static string CaracteresCampoRandom = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            public static int LongitudRandom = 4;

        }

        public static class ParametrosCatalogo
        {
            public static string PrefijoTabla = "USER_";
            public static string PrefijoComandoInsertar = "InsertCatalogoSistema";
            public static string PrefijoComandoActualizar = "UpdateCatalogoSistema";
        }

        public static class EnumeradoresKiiniNet
        {
            public enum EnumEstatusTicket
            {
                Abierto = 1,
                Cancelado = 2,
                ReTipificado = 3,
                ReAbierto = 4,
                Resuelto = 5,
                Cerrado = 6,
                EnEspera = 7
            }

            public enum EnumEstatusAsignacion
            {
                PorAsignar = 1,
                Asignado = 2,
                ReAsignado = 3,
                Escalado = 4,
                Autoasignado = 5
            }

            public enum EnumCanal
            {
                Web = 1,
                Telefono = 2,
                Correo = 3,
                Chat = 4
            }
        }

        public static int[] IdsPublicos =
        {
            (int) EnumTiposUsuario.ClienteInvitado,
            (int) EnumTiposUsuario.EmpleadoInvitado,
            (int) EnumTiposUsuario.ProveedorInvitado
        };

        public enum EnumTiposUsuario
        {
            Empleado = 1,
            EmpleadoInvitado = 2,
            Cliente = 3,
            ClienteInvitado = 4,
            Proveedor = 5,
            ProveedorInvitado = 6,
        }
        public enum EnumTipoObjeto
        {
            Tabla = 1,
            Store = 2
        }
        public enum EnumTiposDocumento
        {
            Word = 1,
            PowerPoint = 2,
            Excel = 3, 
            Pdf = 4,
            Imagen = 5
        }
        public enum EnumTiposInformacionConsulta
        {
            EditorDeContenido = 1,
            Documento = 2,
            PaginaHtml = 3,
            Servicio = 4
        }

        public enum EnumTiposGrupos
        {
            Administrador = 1,
            Acceso = 2,
            EspecialDeConsulta = 3,
            ResponsableDeAtención = 4,
            ResponsableDeInformaciónPublicada = 5,
            ResponsableDeOperación = 6,
            ResponsableDeDesarrollo = 7,
            Responsablemantenimientoubicacionempleado = 8,
            Responsablemantenimientoorganizacionempleado = 9,
            Responsablemantenimientousuarioempleado = 10,
            Responsablemantenimientousuariocliente = 11,
            Responsablemantenimientousuarioproveedor = 12,
            DueñoDelServicio = 13,
            ContactCenter = 14
        }

        public enum EnumRoles
        {
            Administrador = 1,
            Acceso = 2,
            EspecialDeConsulta = 3,
            ResponsableDeAtención = 4,
            ResponsableDeInformaciónPublicada = 5,
            ResponsableDeOperación = 6,
            ResponsableDeDesarrollo = 7,
            Responsablemantenimientoubicacionempleado = 8,
            Responsablemantenimientoorganizacionempleado = 9,
            Responsablemantenimientousuarioempleado = 10,
            Responsablemantenimientousuariocliente = 11,
            Responsablemantenimientousuarioproveedor = 12,
            DueñoDelServicio = 13,
            ContactCenter = 14
        }

        public enum EnumSubRoles
        {
            Autorizador = 1,
            Solicitante = 2,
            Supervisor = 3,
            PrimererNivel = 4,
            SegundoNivel = 5,
            TercerNivel = 6,
            CuartoNivel = 7,
        }

        public enum EnumTipoArbol
        {
            Consultas = 1,
            Servicio = 2,
            Incidentes = 3,
            IncidentesMonitoreo = 4
        }

        public enum EnumMenu
        {
            MiInformación = 1,
            Operacion = 2,
            Consultas = 3,
            Servicio = 4,
            Incidentes = 5,
            SolicitarMtto = 6,
            Reportes = 7,
            Mantenimiento = 8
        }

        public enum EnumTipoEncuesta
        {
            Logica = 1,
            Calificacion = 2,
            OpcionMultiple = 3
        }

        public enum EnumTipoCorreo
        {
            AltaUsuario = 1,
            RecuperarCuenta = 2
        }

        public enum EnumTipoLink
        {
            Confirmacion = 1,
            Reset = 2
        }

        public enum EnumTipoTelefono
        {
            Casa = 1,
            Celular = 2,
            Oficina = 3
        }
    }
}
