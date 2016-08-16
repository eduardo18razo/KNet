using System;
using System.Collections.Generic;
using KiiniNet.Entities.Cat.Arbol.Nodos;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public class ServiceArbolAcceso : IServiceArbolAcceso
    {
        public List<Nivel1> ObtenerNivel1(int idTipoArbol, int idTipoUsuario, bool insertarSeleccion)
        {
            try
            {
                using (BusinessArbolAcceso negocio = new BusinessArbolAcceso())
                {
                    return negocio.ObtenerNivel1(idTipoArbol, idTipoUsuario, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Nivel2> ObtenerNivel2(int idTipoArbol, int idTipoUsuario, int idNivel1, bool insertarSeleccion)
        {
            try
            {
                using (BusinessArbolAcceso negocio = new BusinessArbolAcceso())
                {
                    return negocio.ObtenerNivel2(idTipoArbol, idTipoUsuario, idNivel1, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Nivel3> ObtenerNivel3(int idTipoArbol, int idTipoUsuario, int idNivel2, bool insertarSeleccion)
        {
            try
            {
                using (BusinessArbolAcceso negocio = new BusinessArbolAcceso())
                {
                    return negocio.ObtenerNivel3(idTipoArbol, idTipoUsuario, idNivel2, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Nivel4> ObtenerNivel4(int idTipoArbol, int idTipoUsuario, int idNivel3, bool insertarSeleccion)
        {
            try
            {
                using (BusinessArbolAcceso negocio = new BusinessArbolAcceso())
                {
                    return negocio.ObtenerNivel4(idTipoArbol, idTipoUsuario, idNivel3, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Nivel5> ObtenerNivel5(int idTipoArbol, int idTipoUsuario, int idNivel4, bool insertarSeleccion)
        {
            try
            {
                using (BusinessArbolAcceso negocio = new BusinessArbolAcceso())
                {
                    return negocio.ObtenerNivel5(idTipoArbol, idTipoUsuario, idNivel4, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Nivel6> ObtenerNivel6(int idTipoArbol, int idTipoUsuario, int idNivel5, bool insertarSeleccion)
        {
            try
            {
                using (BusinessArbolAcceso negocio = new BusinessArbolAcceso())
                {
                    return negocio.ObtenerNivel6(idTipoArbol, idTipoUsuario, idNivel5, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Nivel7> ObtenerNivel7(int idTipoArbol, int idTipoUsuario, int idNivel6, bool insertarSeleccion)
        {
            try
            {
                using (BusinessArbolAcceso negocio = new BusinessArbolAcceso())
                {
                    return negocio.ObtenerNivel7(idTipoArbol, idTipoUsuario, idNivel6, insertarSeleccion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EsNodoTerminal(int idTipoUsuario, int idTipoArbol, int nivel1, int? nivel2, int? nivel3, int? nivel4, int? nivel5, int? nivel6, int? nivel7)
        {
            try
            {
                using (BusinessArbolAcceso negocio = new BusinessArbolAcceso())
                {
                    return negocio.EsNodoTerminal(idTipoUsuario, idTipoArbol, nivel1, nivel2, nivel3, nivel4, nivel5, nivel6, nivel7);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GuardarArbol(ArbolAcceso arbol)
        {
            try
            {
                using (BusinessArbolAcceso negocio = new BusinessArbolAcceso())
                {
                    negocio.GuardarArbol(arbol);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ArbolAcceso> ObtenerArblodesAccesoByGruposUsuario(int idUsuario, int idTipoArbol, int idArea)
        {
            try
            {
                using (BusinessArbolAcceso negocio = new BusinessArbolAcceso())
                {
                    return negocio.ObtenerArbolesAccesoByUsuarioTipoArbol(idUsuario, idTipoArbol, idArea);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ArbolAcceso ObtenerArbolAcceso(int idArbol)
        {
            try
            {
                using (BusinessArbolAcceso negocio = new BusinessArbolAcceso())
                {
                    return negocio.ObtenerArbolAcceso(idArbol);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ArbolAcceso> ObtenerArbolesAccesoAll(int? idArea, int? idTipoUsuario, int? idTipoArbol, int? nivel1, int? nivel2, int? nivel3, int? nivel4, int? nivel5, int? nivel6, int? nivel7)
        {
            try
            {
                using (BusinessArbolAcceso negocio = new BusinessArbolAcceso())
                {
                    return negocio.ObtenerArbolesAccesoAll(idArea, idTipoUsuario, idTipoArbol, nivel1, nivel2, nivel3, nivel4, nivel5, nivel6, nivel7);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void HabilitarArbol(int idArbol, bool habilitado)
        {
            try
            {
                using (BusinessArbolAcceso negocio = new BusinessArbolAcceso())
                {
                    negocio.HabilitarArbol(idArbol, habilitado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ActualizardArbol(int idArbolAcceso, ArbolAcceso arbolAcceso, string descripcion)
        {
            try
            {
                using (BusinessArbolAcceso negocio = new BusinessArbolAcceso())
                {
                    negocio.ActualizardArbol(idArbolAcceso, arbolAcceso, descripcion);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
