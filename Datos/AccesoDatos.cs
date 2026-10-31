using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        // Se prueban en orden; se usa la primera que logre conectarse
        private static readonly string[] nombresConexion = { "CallCenterDB", "CallCenterDBLocal" };

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection();
            comando = new SqlCommand();
        }

        private void AbrirConexion()
        {
            Exception ultimoError = null;

            foreach (string nombre in nombresConexion)
            {
                ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings[nombre];
                if (cs == null)
                    continue;

                try
                {
                    conexion.ConnectionString = cs.ConnectionString;
                    conexion.Open();
                    return;
                }
                catch (SqlException ex)
                {
                    ultimoError = ex;
                }
            }

            throw new Exception("No se pudo conectar a la base de datos con ninguna de las cadenas configuradas.", ultimoError);
        }

        public void SetearProcedimiento(string nombre)
        {
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombre;
        }

        public void SetearConsulta(string consulta)
        {
            comando.CommandType = CommandType.Text;
            comando.CommandText = consulta;
        }

        public void SetearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor ?? DBNull.Value);
        }

        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            AbrirConexion();
            lector = comando.ExecuteReader();
        }

        public void EjecutarAccion()
        {
            comando.Connection = conexion;
            AbrirConexion();
            comando.ExecuteNonQuery();
        }

        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }
    }
}