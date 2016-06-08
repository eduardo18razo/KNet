using System;
using System.Configuration;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Security.Cryptography;
using System.Text;
using KiiniNet.Entities.Cat.Arbol.Nodos;
using KiiniNet.Entities.Cat.Arbol.Organizacion;
using KiiniNet.Entities.Cat.Arbol.Ubicaciones;
using KiiniNet.Entities.Cat.Arbol.Ubicaciones.Domicilio;
using KiiniNet.Entities.Cat.Mascaras;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Operacion;
using KiiniNet.Entities.Operacion.Usuarios;
using KiiniNet.Entities.Parametros;

namespace KinniNet.Data.Help
{
    public class DataBaseModelContext : ObjectContext
    {
        private const string ConnectionString = "name=connection";
        private const string ContainerName = "connection";

        public DataBaseModelContext()
            : base(ConnectionString, ContainerName)
        {
            try
            {
                //Sistema
                _parametrosTelefonos = CreateObjectSet<ParametrosTelefonos>();
                _tipoUsuario = CreateObjectSet<TipoUsuario>();
                _rol = CreateObjectSet<Rol>();
                _rolTipoUsuario = CreateObjectSet<RolTipoUsuario>();
                _rolTipoGrupo = CreateObjectSet<RolTipoGrupo>();
                _tipoGrupo = CreateObjectSet<TipoGrupo>();
                _subRol = CreateObjectSet<SubRol>();
                _grupoUsuario = CreateObjectSet<GrupoUsuario>();
                _subGrupoUsuario = CreateObjectSet<SubGrupoUsuario>();
                _colonia = CreateObjectSet<Colonia>();
                _municipio = CreateObjectSet<Municipio>();
                _estado = CreateObjectSet<Estado>();
                _tipoTelefono = CreateObjectSet<TipoTelefono>();
                _catalogos = CreateObjectSet<Catalogos>();
                _tipoArbolAcceso = CreateObjectSet<TipoArbolAcceso>();
                _tipoInfConsulta = CreateObjectSet<TipoInfConsulta>();
                _tipoDocumento = CreateObjectSet<TipoDocumento>();
                _tipoEncuesta = CreateObjectSet<TipoEncuesta>();
                _respuestaTipoEncuesta = CreateObjectSet<RespuestaTipoEncuesta>();
                _menu = CreateObjectSet<Menu>();
                _rolMenu = CreateObjectSet<RolMenu>();

                //Ubicacion
                _pais = CreateObjectSet<Pais>();
                _campus = CreateObjectSet<Campus>();
                _torre = CreateObjectSet<Torre>();
                _piso = CreateObjectSet<Piso>();
                _zona = CreateObjectSet<Zona>();
                _subZona = CreateObjectSet<SubZona>();
                _siteRack = CreateObjectSet<SiteRack>();

                //Organizacion
                _holding = CreateObjectSet<Holding>();
                _compania = CreateObjectSet<Compania>();
                _direccion = CreateObjectSet<Direccion>();
                _subDireccion = CreateObjectSet<SubDireccion>();
                _gerencia = CreateObjectSet<Gerencia>();
                _subGerencia = CreateObjectSet<SubGerencia>();
                _jefatura = CreateObjectSet<Jefatura>();

                //Nodos
                _nivel1 = CreateObjectSet<Nivel1>();
                _nivel2 = CreateObjectSet<Nivel2>();
                _nivel3 = CreateObjectSet<Nivel3>();
                _nivel4 = CreateObjectSet<Nivel4>();
                _nivel5 = CreateObjectSet<Nivel5>();
                _nivel6 = CreateObjectSet<Nivel6>();
                _nivel7 = CreateObjectSet<Nivel7>();

                //Mascara
                _mascara = CreateObjectSet<Mascara>();
                _campoMascara = CreateObjectSet<CampoMascara>();


                //Operacion
                _usuario = CreateObjectSet<Usuario>();
                _ubicacion = CreateObjectSet<Ubicacion>();
                _organizacion = CreateObjectSet<Organizacion>();
                _domicilio = CreateObjectSet<Domicilio>();
                _correoUsuario = CreateObjectSet<CorreoUsuario>();
                _telefonoUsuario = CreateObjectSet<TelefonoUsuario>();
                _usuarioRol = CreateObjectSet<UsuarioRol>();
                _usuarioGrupo = CreateObjectSet<UsuarioGrupo>();
                _tipoCampoMascara = CreateObjectSet<TipoCampoMascara>();
                _arbolAcceso = CreateObjectSet<ArbolAcceso>();
                _informacionConsulta = CreateObjectSet<InformacionConsulta>();
                _informacionConsultaDatos = CreateObjectSet<InformacionConsultaDatos>();
                _sla = CreateObjectSet<Sla>();
                _slaDetalle = CreateObjectSet<SlaDetalle>();
                _encuesta = CreateObjectSet<Encuesta>();
                _encuestaPregunta = CreateObjectSet<EncuestaPregunta>();
                _inventarioArbolAcceso = CreateObjectSet<InventarioArbolAcceso>();
                _inventarioInfConsulta = CreateObjectSet<InventarioInfConsulta>();
                _grupoUsuarioInventarioArbol = CreateObjectSet<GrupoUsuarioInventarioArbol>();
                _hitConsulta = CreateObjectSet<HitConsulta>();
                _hitGrupoUsuario = CreateObjectSet<HitGrupoUsuario>();
                _respuestaEncuesta = CreateObjectSet<RespuestaEncuesta>();
                _slaEstimadoTicket = CreateObjectSet<SlaEstimadoTicket>();
                _slaEstimadoTicketDetalle = CreateObjectSet<SlaEstimadoTicketDetalle>();
                _ticket = CreateObjectSet<Ticket>();
                _area = CreateObjectSet<Area>();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public DataBaseModelContext(string connectionString)
            : base(ConnectionString, ContainerName)
        {
        }

        public DataBaseModelContext(EntityConnection connection)
            : base(ConnectionString, ContainerName)
        {
        }

        public static string DecryptedConnectionString
        {
            get
            {
                byte[] ToD, Denc;

                if (string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["GastosConnection"].ToString()))
                    throw new Exception("No se encuentra cadena de conexion");

                ToD = Convert.FromBase64String(ConfigurationManager.ConnectionStrings["GastosConnection"].ToString());

                Denc = ProtectedData.Unprotect(ToD, null, DataProtectionScope.LocalMachine);
                string cadena = Encoding.ASCII.GetString(Denc).Replace("&quot;", "\"");
                //System.Xml.Linq.XDocument x1 = System.Xml.Linq.XDocument.Parse(x);
                return cadena;
            }
        }



        public ObjectSet<ParametrosTelefonos> ParametrosTelefonos
        {
            get
            {
                return _parametrosTelefonos;
            }
        }

        private ObjectSet<ParametrosTelefonos> _parametrosTelefonos;

        #region Operativo
        public ObjectSet<Usuario> Usuario
        {
            get
            {
                return _usuario;
            }
        }

        public ObjectSet<Ubicacion> Ubicacion
        {
            get
            {
                return _ubicacion;
            }
        }

        public ObjectSet<Organizacion> Organizacion
        {
            get
            {
                return _organizacion;
            }
        }

        public ObjectSet<Domicilio> Domicilio
        {
            get
            {
                return _domicilio;
            }
        }

        public ObjectSet<CorreoUsuario> CorreoUsuario
        {
            get
            {
                return _correoUsuario;
            }
        }

        public ObjectSet<TelefonoUsuario> TelefonoUsuario
        {
            get
            {
                return _telefonoUsuario;
            }
        }

        public ObjectSet<UsuarioRol> UsuarioRol
        {
            get
            {
                return _usuarioRol;
            }
        }

        public ObjectSet<UsuarioGrupo> UsuarioGrupo
        {
            get
            {
                return _usuarioGrupo;
            }
        }

        public ObjectSet<TipoCampoMascara> TipoCampoMascara
        {
            get
            {
                return _tipoCampoMascara;
            }
        }

        public ObjectSet<Catalogos> Catalogos
        {
            get
            {
                return _catalogos;
            }
        }

        public ObjectSet<ArbolAcceso> ArbolAcceso
        {
            get
            {
                return _arbolAcceso;
            }
        }

        public ObjectSet<InformacionConsulta> InformacionConsulta
        {
            get
            {
                return _informacionConsulta;
            }
        }

        public ObjectSet<InformacionConsultaDatos> InformacionConsultaDatos
        {
            get
            {
                return _informacionConsultaDatos;
            }
        }

        public ObjectSet<Sla> Sla
        {
            get
            {
                return _sla;
            }
        }

        public ObjectSet<SlaDetalle> SlaDetalle
        {
            get
            {
                return _slaDetalle;
            }
        }


        public ObjectSet<Encuesta> Encuesta
        {
            get
            {
                return _encuesta;
            }
        }

        public ObjectSet<EncuestaPregunta> EncuestaPregunta
        {
            get
            {
                return _encuestaPregunta;
            }
        }
        public ObjectSet<InventarioArbolAcceso> InventarioArbolAcceso
        {
            get
            {
                return _inventarioArbolAcceso;
            }
        }

        public ObjectSet<InventarioInfConsulta> InventarioInfConsulta
        {
            get
            {
                return _inventarioInfConsulta;
            }
        }

        public ObjectSet<GrupoUsuarioInventarioArbol> GrupoUsuarioInventarioArbol
        {
            get
            {
                return _grupoUsuarioInventarioArbol;
            }
        }

        public ObjectSet<HitConsulta> HitConsulta
        {
            get
            {
                return _hitConsulta;
            }
        }

        public ObjectSet<HitGrupoUsuario> HitGrupoUsuario
        {
            get
            {
                return _hitGrupoUsuario;
            }
        }

        public ObjectSet<RespuestaEncuesta> RespuestaEncuesta
        {
            get
            {
                return _respuestaEncuesta;
            }
        }

        public ObjectSet<SlaEstimadoTicket> SlaEstimadoTicket
        {
            get
            {
                return _slaEstimadoTicket;
            }
        }

        public ObjectSet<SlaEstimadoTicketDetalle> SlaEstimadoTicketDetalle
        {
            get
            {
                return _slaEstimadoTicketDetalle;
            }
        }

        public ObjectSet<Ticket> Ticket
        {
            get
            {
                return _ticket;
            }
        }

        public ObjectSet<Area> Area
        {
            get
            {
                return _area;
            }
        }



        private ObjectSet<Usuario> _usuario;
        private ObjectSet<Ubicacion> _ubicacion;
        private ObjectSet<Organizacion> _organizacion;
        private ObjectSet<Domicilio> _domicilio;
        private ObjectSet<CorreoUsuario> _correoUsuario;
        private ObjectSet<TelefonoUsuario> _telefonoUsuario;
        private ObjectSet<UsuarioRol> _usuarioRol;
        private ObjectSet<UsuarioGrupo> _usuarioGrupo;
        private ObjectSet<TipoCampoMascara> _tipoCampoMascara;
        private ObjectSet<Catalogos> _catalogos;
        private ObjectSet<ArbolAcceso> _arbolAcceso;
        private ObjectSet<InformacionConsulta> _informacionConsulta;
        private ObjectSet<InformacionConsultaDatos> _informacionConsultaDatos;
        private ObjectSet<Sla> _sla;
        private ObjectSet<SlaDetalle> _slaDetalle;

        private ObjectSet<Encuesta> _encuesta;
        private ObjectSet<EncuestaPregunta> _encuestaPregunta;
        private ObjectSet<InventarioArbolAcceso> _inventarioArbolAcceso;
        private ObjectSet<InventarioInfConsulta> _inventarioInfConsulta;
        private ObjectSet<GrupoUsuarioInventarioArbol> _grupoUsuarioInventarioArbol;
        private ObjectSet<HitConsulta> _hitConsulta;
        private ObjectSet<HitGrupoUsuario> _hitGrupoUsuario;
        private ObjectSet<RespuestaEncuesta> _respuestaEncuesta;
        private ObjectSet<SlaEstimadoTicket> _slaEstimadoTicket;
        private ObjectSet<SlaEstimadoTicketDetalle> _slaEstimadoTicketDetalle;
        private ObjectSet<Ticket> _ticket;
        private ObjectSet<Area> _area;

        #region Mascara
        public ObjectSet<Mascara> Mascara
        {
            get
            {
                return _mascara;
            }
        }

        public ObjectSet<CampoMascara> CampoMascara
        {
            get
            {
                return _campoMascara;
            }
        }

        private ObjectSet<Mascara> _mascara;
        private ObjectSet<CampoMascara> _campoMascara;
        #endregion Mascara


        #endregion Operativo

        #region catalogos

        #region Systema

        public ObjectSet<TipoUsuario> TipoUsuario
        {
            get
            {
                return _tipoUsuario;
            }
        }

        public ObjectSet<Pais> Pais
        {
            get
            {
                return _pais;
            }
        }

        public ObjectSet<Colonia> Colonia
        {
            get
            {
                return _colonia;
            }
        }

        public ObjectSet<Municipio> Municipio
        {
            get
            {
                return _municipio;
            }
        }

        public ObjectSet<Estado> Estado
        {
            get
            {
                return _estado;
            }
        }

        public ObjectSet<TipoTelefono> TipoTelefono
        {
            get
            {
                return _tipoTelefono;
            }
        }

        public ObjectSet<Rol> Rol
        {
            get
            {
                return _rol;
            }
        }

        public ObjectSet<RolTipoUsuario> RolTipoUsuario
        {
            get
            {
                return _rolTipoUsuario;
            }
        }

        public ObjectSet<RolTipoGrupo> RolTipoGrupo
        {
            get
            {
                return _rolTipoGrupo;
            }
        }

        public ObjectSet<TipoGrupo> TipoGrupo
        {
            get
            {
                return _tipoGrupo;
            }
        }

        public ObjectSet<SubRol> SubRol
        {
            get
            {
                return _subRol;
            }
        }

        public ObjectSet<TipoArbolAcceso> TipoArbolAcceso
        {
            get
            {
                return _tipoArbolAcceso;
            }
        }

        public ObjectSet<TipoInfConsulta> TipoInfConsulta
        {
            get
            {
                return _tipoInfConsulta;
            }
        }

        public ObjectSet<TipoDocumento> TipoDocumento
        {
            get
            {
                return _tipoDocumento;
            }
        }
        public ObjectSet<TipoEncuesta> TipoEncuesta
        {
            get
            {
                return _tipoEncuesta;
            }
        }

        public ObjectSet<RespuestaTipoEncuesta> RespuestaTipoEncuesta
        {
            get
            {
                return _respuestaTipoEncuesta;
            }
        }

        public ObjectSet<Menu> Menu
        {
            get
            {
                return _menu;
            }
        }

        public ObjectSet<RolMenu> RolMenu
        {
            get
            {
                return _rolMenu;
            }
        }


        private ObjectSet<TipoUsuario> _tipoUsuario;
        private ObjectSet<Pais> _pais;
        private ObjectSet<Colonia> _colonia;
        private ObjectSet<Municipio> _municipio;
        private ObjectSet<Estado> _estado;
        private ObjectSet<TipoTelefono> _tipoTelefono;
        private ObjectSet<Rol> _rol;
        private ObjectSet<RolTipoUsuario> _rolTipoUsuario;
        private ObjectSet<RolTipoGrupo> _rolTipoGrupo;
        private ObjectSet<TipoGrupo> _tipoGrupo;
        private ObjectSet<SubRol> _subRol;
        private ObjectSet<TipoArbolAcceso> _tipoArbolAcceso;
        private ObjectSet<TipoInfConsulta> _tipoInfConsulta;
        private ObjectSet<TipoDocumento> _tipoDocumento;
        private ObjectSet<TipoEncuesta> _tipoEncuesta;
        private ObjectSet<RespuestaTipoEncuesta> _respuestaTipoEncuesta;
        private ObjectSet<Menu> _menu;
        private ObjectSet<RolMenu> _rolMenu;

        #endregion Systema

        #region Usuario

        public ObjectSet<GrupoUsuario> GrupoUsuario
        {
            get
            {
                return _grupoUsuario;
            }
        }

        public ObjectSet<SubGrupoUsuario> SubGrupoUsuario
        {
            get
            {
                return _subGrupoUsuario;
            }
        }

        private ObjectSet<GrupoUsuario> _grupoUsuario;

        private ObjectSet<SubGrupoUsuario> _subGrupoUsuario;


        #region Ubicacion
        public ObjectSet<Campus> Campus
        {
            get
            {
                return _campus;
            }
        }

        public ObjectSet<Torre> Torre
        {
            get
            {
                return _torre;
            }
        }

        public ObjectSet<Piso> Piso
        {
            get
            {
                return _piso;
            }
        }

        public ObjectSet<Zona> Zona
        {
            get
            {
                return _zona;
            }
        }

        public ObjectSet<SubZona> SubZona
        {
            get
            {
                return _subZona;
            }
        }

        public ObjectSet<SiteRack> SiteRack
        {
            get
            {
                return _siteRack;
            }
        }

        private ObjectSet<Campus> _campus;
        private ObjectSet<Torre> _torre;
        private ObjectSet<Piso> _piso;
        private ObjectSet<Zona> _zona;
        private ObjectSet<SubZona> _subZona;
        private ObjectSet<SiteRack> _siteRack;
        #endregion Ubicacion

        #region Organizacion
        public ObjectSet<Holding> Holding
        {
            get
            {
                return _holding;
            }
        }

        public ObjectSet<Compania> Compañia
        {
            get
            {
                return _compania;
            }
        }

        public ObjectSet<Direccion> Direccion
        {
            get
            {
                return _direccion;
            }
        }

        public ObjectSet<SubDireccion> SubDireccion
        {
            get
            {
                return _subDireccion;
            }
        }

        public ObjectSet<Gerencia> Gerencia
        {
            get
            {
                return _gerencia;
            }
        }

        public ObjectSet<SubGerencia> SubGerencia
        {
            get
            {
                return _subGerencia;
            }
        }

        public ObjectSet<Jefatura> Jefatura
        {
            get
            {
                return _jefatura;
            }
        }

        private ObjectSet<Holding> _holding;
        private ObjectSet<Compania> _compania;
        private ObjectSet<Direccion> _direccion;
        private ObjectSet<SubDireccion> _subDireccion;
        private ObjectSet<Gerencia> _gerencia;
        private ObjectSet<SubGerencia> _subGerencia;
        private ObjectSet<Jefatura> _jefatura;

        #endregion Organizacion


        #region ArbolesAcceso

        public ObjectSet<Nivel1> Nivel1
        {
            get
            {
                return _nivel1;
            }
        }

        public ObjectSet<Nivel2> Nivel2
        {
            get
            {
                return _nivel2;
            }
        }
        public ObjectSet<Nivel3> Nivel3
        {
            get
            {
                return _nivel3;
            }
        }
        public ObjectSet<Nivel4> Nivel4
        {
            get
            {
                return _nivel4;
            }
        }
        public ObjectSet<Nivel5> Nivel5
        {
            get
            {
                return _nivel5;
            }
        }
        public ObjectSet<Nivel6> Nivel6
        {
            get
            {
                return _nivel6;
            }
        }
        public ObjectSet<Nivel7> Nivel7
        {
            get
            {
                return _nivel7;
            }
        }
        private ObjectSet<Nivel1> _nivel1;
        private ObjectSet<Nivel2> _nivel2;
        private ObjectSet<Nivel3> _nivel3;
        private ObjectSet<Nivel4> _nivel4;
        private ObjectSet<Nivel5> _nivel5;
        private ObjectSet<Nivel6> _nivel6;
        private ObjectSet<Nivel7> _nivel7;
        #endregion ArbolesAcceso

        #endregion Usuario

        #endregion catalogos

    }
}
