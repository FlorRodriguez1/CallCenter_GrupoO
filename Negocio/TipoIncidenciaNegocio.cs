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
                datos.SetearProcedimiento("SP_ListarTiposIncidencia");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    TipoIncidencia aux = new TipoIncidencia();
                    aux.IdTipo = (int)datos.Lector["IdTipo"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}