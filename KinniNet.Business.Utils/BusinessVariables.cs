using System.ComponentModel;

namespace KinniNet.Business.Utils
{
    public static class BusinessVariables
    {

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
                Cerrado = 6
            }

            public enum EnumEstatusAsignacion
            {
                PorAsignar = 1,
                Asignado = 2,
                ReAsignado = 3,
                Escalado = 4,
                Autoasignado = 5
            }
        }
        public enum EnumTiposUsuario
        {
            Empleado = 1,
            EmpleadoInvitado = 2,
            Cliente = 3,
            ClienteInvitado = 4,
            Proveedor = 5,
            ProveedorInvitado = 6,
        }

        public enum EnumTiposDocumento
        {
            Word = 1,
            PowerPoint = 2,
            Excel = 3
        }
        public enum EnumTiposInformacionConsulta
        {
            Texto = 1,
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
            ResponsableDeMantenimiento = 5,
            ResponsableDeOperación = 6,
            ResponsableDeDesarrollo = 7,
        }

        public enum EnumRoles
        {
            Administrador = 1,
            Acceso = 2,
            EspecialDeConsulta = 3,
            ResponsableDeAtención = 4,
            ResponsableDeMantenimiento = 5,
            ResponsableDeOperación = 6,
            ResponsableDeDesarrollo = 7,
        }

        public enum EnumSubRoles
        {
            Dueño = 1,
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
    }
}
