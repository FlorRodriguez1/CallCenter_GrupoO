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
                datos.SetearConsulta("SELECT IdPrioridad, Descripcion, Activo FROM Prioridades ORDER BY IdPrioridad");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                    lista.Add(LeerPrioridad(datos));

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Prioridad Obtener(int idPrioridad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_ObtenerPrioridad");
                datos.SetearParametro("@IdPrioridad", idPrioridad);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                    return LeerPrioridad(datos);

                return null;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Agregar(Prioridad nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_AgregarPrioridad");
                datos.SetearParametro("@Descripcion", nueva.Descripcion);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Modificar(Prioridad prioridad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_ModificarPrioridad");
                datos.SetearParametro("@IdPrioridad", prioridad.IdPrioridad);
                datos.SetearParametro("@Descripcion", prioridad.Descripcion);
                datos.SetearParametro("@Activo", prioridad.Activo);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        private Prioridad LeerPrioridad(AccesoDatos datos)
        {
            Prioridad aux = new Prioridad();
            aux.IdPrioridad = (int)datos.Lector["IdPrioridad"];
            aux.Descripcion = (string)datos.Lector["Descripcion"];
            aux.Activo = (bool)datos.Lector["Activo"];
            return aux;
        }
    }
}