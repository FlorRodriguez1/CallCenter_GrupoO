using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class IncidenciaNegocio
    {
        public int Agregar(Incidencia nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearProcedimiento("SP_AgregarIncidencia");
                datos.SetearParametro("@IdCliente", nueva.Cliente.IdCliente);
                datos.SetearParametro("@IdTipo", nueva.Tipo.IdTipo);
                datos.SetearParametro("@IdPrioridad", nueva.Prioridad.IdPrioridad);
                datos.SetearParametro("@IdEstado", (int)nueva.Estado);
                datos.SetearParametro("@IdUsuarioCreador", nueva.UsuarioCreador.IdUsuario);
                datos.SetearParametro("@IdUsuarioAsignado", nueva.UsuarioAsignado.IdUsuario);
                datos.SetearParametro("@Problematica", nueva.Problematica);

                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["IdIncidencia"];
                }

                return 0;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
