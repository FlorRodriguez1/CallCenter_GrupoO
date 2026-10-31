using System;
using System.Collections.Generic;
using Datos;
using Dominio;

namespace Negocio
{
    public class PrioridadNegocio
    {
        public List<Prioridad> Listar()
        {
            List<Prioridad> lista = new List<Prioridad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_ListarPrioridades");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Prioridad aux = new Prioridad();
                    aux.IdPrioridad = (int)datos.Lector["IdPrioridad"];
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