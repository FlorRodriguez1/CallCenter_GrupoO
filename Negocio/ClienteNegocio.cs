using System;
using System.Collections.Generic;
using Datos;
using Dominio;

namespace Negocio
{
    public class ClienteNegocio
    {
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IdCliente, DNI, Nombre, Apellido, Email, Telefono, Activo FROM Clientes ORDER BY Apellido, Nombre");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(LeerCliente(datos));
                }

                return lista;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public Cliente Obtener(int idCliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IdCliente, DNI, Nombre, Apellido, Email, Telefono, Activo FROM Clientes WHERE IdCliente = @IdCliente");
                datos.SetearParametro("@IdCliente", idCliente);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                    return LeerCliente(datos);

                return null;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Agregar(Cliente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO Clientes (DNI, Nombre, Apellido, Email, Telefono) VALUES (@DNI, @Nombre, @Apellido, @Email, @Telefono)");
                datos.SetearParametro("@DNI", nuevo.DNI);
                datos.SetearParametro("@Nombre", nuevo.Nombre);
                datos.SetearParametro("@Apellido", nuevo.Apellido);
                datos.SetearParametro("@Email", nuevo.Email);
                datos.SetearParametro("@Telefono", nuevo.Telefono);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void Modificar(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Clientes SET DNI = @DNI, Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono, Activo = @Activo WHERE IdCliente = @IdCliente");
                datos.SetearParametro("@IdCliente", cliente.IdCliente);
                datos.SetearParametro("@DNI", cliente.DNI);
                datos.SetearParametro("@Nombre", cliente.Nombre);
                datos.SetearParametro("@Apellido", cliente.Apellido);
                datos.SetearParametro("@Email", cliente.Email);
                datos.SetearParametro("@Telefono", cliente.Telefono);
                datos.SetearParametro("@Activo", cliente.Activo);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        // Evita repetir el mapeo en Listar y Obtener
        private Cliente LeerCliente(AccesoDatos datos)
        {
            Cliente aux = new Cliente();
            aux.IdCliente = (int)datos.Lector["IdCliente"];
            aux.DNI = (string)datos.Lector["DNI"];
            aux.Nombre = (string)datos.Lector["Nombre"];
            aux.Apellido = (string)datos.Lector["Apellido"];
            aux.Email = (string)datos.Lector["Email"];
            aux.Telefono = datos.Lector["Telefono"] is DBNull ? null : (string)datos.Lector["Telefono"];
            aux.Activo = (bool)datos.Lector["Activo"];
            return aux;
        }
        /*//prueba para guardar clientes
        public void GuardarCliente(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearProcedimiento("SP_GuardarCliente");
                datos.SetearParametro("@DNI", cliente.DNI);
                datos.SetearParametro("@Nombre", cliente.Nombre);
                datos.SetearParametro("@Apellido", cliente.Apellido);
                datos.SetearParametro("@Email", cliente.Email);
                datos.SetearParametro("@Telefono", cliente.Telefono);
                datos.EjecutarAccion();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }*/

    }
}