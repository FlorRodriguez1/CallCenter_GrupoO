using System;
using System.Collections.Generic;
using Datos;
using Dominio;

namespace Negocio
{
    public class TipoIncidenciaNegocio
    {
        public List<TipoIncidencia> Listar()
        {
            List<TipoIncidencia> lista = new List<TipoIncidencia>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IdTipo, Descripcion, Activo FROM TiposIncidencia ORDER BY Descripcion");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                    lista.Add(LeerTipo(datos));

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public TipoIncidencia Obtener(int idTipo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_ObtenerTipoIncidencia");
                datos.SetearParametro("@IdTipo", idTipo);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                    return LeerTipo(datos);

                return null;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Agregar(TipoIncidencia nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_AgregarTipoIncidencia");
                datos.SetearParametro("@Descripcion", nuevo.Descripcion);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Modificar(TipoIncidencia tipo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_ModificarTipoIncidencia");
                datos.SetearParametro("@IdTipo", tipo.IdTipo);
                datos.SetearParametro("@Descripcion", tipo.Descripcion);
                datos.SetearParametro("@Activo", tipo.Activo);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        private TipoIncidencia LeerTipo(AccesoDatos datos)
        {
            TipoIncidencia aux = new TipoIncidencia();
            aux.IdTipo = (int)datos.Lector["IdTipo"];
            aux.Descripcion = (string)datos.Lector["Descripcion"];
            aux.Activo = (bool)datos.Lector["Activo"];
            return aux;
        }
    }
}