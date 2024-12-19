﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace RRHH
{
    public class Colaborador
    {
        // Propiedades de la clase
        public int ColaboradorID { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Departamento { get; set; }
        public string Objetivo { get; set; }
        public byte[] Foto { get; set; }
        public bool EstadoActivo { get; set; } = true;

        // Método para agregar un colaborador
        public void AgregarColaborador()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "INSERT INTO Colaboradores (NombreCompleto, Telefono, Email, Departamento, Objetivo, Foto, EstadoActivo) " +
                               "VALUES (@NombreCompleto, @Telefono, @Email, @Departamento, @Objetivo, @Foto, @EstadoActivo)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@NombreCompleto", NombreCompleto);
                comando.Parameters.AddWithValue("@Telefono", Telefono);
                comando.Parameters.AddWithValue("@Email", Email);
                comando.Parameters.AddWithValue("@Departamento", Departamento);
                comando.Parameters.AddWithValue("@Objetivo", Objetivo ?? (object)DBNull.Value);
                comando.Parameters.Add("@Foto", SqlDbType.VarBinary).Value = Foto ?? (object)DBNull.Value;
                comando.Parameters.AddWithValue("@EstadoActivo", EstadoActivo);
                comando.ExecuteNonQuery();
            }
        }



        // Método para actualizar un colaborador
        public void ActualizarColaborador()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "UPDATE Colaboradores SET NombreCompleto = @NombreCompleto, Telefono = @Telefono, " +
                               "Email = @Email, Departamento = @Departamento, Objetivo = @Objetivo, Foto = @Foto " +
                               "WHERE ColaboradorID = @ColaboradorID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ColaboradorID", ColaboradorID);
                comando.Parameters.AddWithValue("@NombreCompleto", NombreCompleto);
                comando.Parameters.AddWithValue("@Telefono", Telefono);
                comando.Parameters.AddWithValue("@Email", Email);
                comando.Parameters.AddWithValue("@Departamento", Departamento);
                comando.Parameters.AddWithValue("@Objetivo", Objetivo ?? (object)DBNull.Value);
                comando.Parameters.Add("@Foto", SqlDbType.VarBinary).Value = Foto ?? (object)DBNull.Value;
                comando.ExecuteNonQuery();
            }
        }



        // Método para eliminar un colaborador (marcar como inactivo)
        public void EliminarColaborador()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "UPDATE Colaboradores SET EstadoActivo = 0 WHERE ColaboradorID = @ColaboradorID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ColaboradorID", ColaboradorID);
                comando.ExecuteNonQuery();
            }
        }

        // Método para buscar colaboradores
        public static List<Colaborador> BuscarColaboradores(string criterio)
        {
            List<Colaborador> listaColaboradores = new List<Colaborador>();

            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "SELECT ColaboradorID, NombreCompleto, Telefono, Email, Departamento, Objetivo, EstadoActivo, Foto " +
                               "FROM Colaboradores WHERE NombreCompleto LIKE @Criterio OR Departamento LIKE @Criterio";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Criterio", "%" + criterio + "%");
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    listaColaboradores.Add(new Colaborador
                    {
                        ColaboradorID = Convert.ToInt32(reader["ColaboradorID"]),
                        NombreCompleto = reader["NombreCompleto"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Email = reader["Email"].ToString(),
                        Departamento = reader["Departamento"].ToString(),
                        Objetivo = reader["Objetivo"] == DBNull.Value ? null : reader["Objetivo"].ToString(),
                        EstadoActivo = Convert.ToBoolean(reader["EstadoActivo"]),
                        Foto = reader["Foto"] == DBNull.Value ? null : (byte[])reader["Foto"]
                    });
                }
            }

            return listaColaboradores;
        }
    }
}
