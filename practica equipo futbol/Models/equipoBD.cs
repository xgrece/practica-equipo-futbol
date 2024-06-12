using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace practica_equipo_futbol.Models
{
    public class EquipoBD
    {
        private string connectionString = "Server=127.0.0.1;Database=equipos;User ID=root;Password=123;";

        public List<Equipo> GetEquipos()
        {
            List<Equipo> equipos = new List<Equipo>();
            string query = "SELECT * FROM equipos";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Equipo equipo = new Equipo(reader.GetString(1), reader.GetString(2))
                    {
                        Id = reader.GetInt32(0)
                    };
                    equipos.Add(equipo);
                }

                reader.Close();
                connection.Close();
            }

            return equipos;
        }

        public List<Jugador> GetJugadoresPorEquipoId(int equipoId)
        {
            List<Jugador> jugadores = new List<Jugador>();
            string query = "SELECT * FROM jugadores WHERE equipo_id = @equipoId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@equipoId", equipoId);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Jugador jugador = new Jugador(reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3))
                    {
                        Id = reader.GetInt32(0)
                    };
                    jugadores.Add(jugador);
                }

                reader.Close();
                connection.Close();
            }

            return jugadores;
        }

        public List<Equipo> GetEquiposConEntrenadores()
        {
            List<Equipo> equipos = new List<Equipo>();
            string query = "SELECT * FROM equipos";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Equipo equipo = new Equipo(reader.GetString(1), reader.GetString(2))
                    {
                        Id = reader.GetInt32(0)
                    };
                    equipos.Add(equipo);
                }

                reader.Close();
                connection.Close();
            }

            return equipos;
        }

        public int GetNumeroJugadoresPorEquipo(int equipoId)
        {
            int numeroJugadores = 0;
            string query = "SELECT COUNT(*) FROM jugadores WHERE equipo_id = @equipoId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@equipoId", equipoId);
                connection.Open();
                numeroJugadores = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }

            return numeroJugadores;
        }

        public List<Equipo> GetEquiposSinJugadores()
        {
            List<Equipo> equiposSinJugadores = new List<Equipo>();
            string query = @"
                SELECT e.id, e.nombre, e.entrenador
                FROM equipos e
                LEFT JOIN jugadores j ON e.id = j.equipo_id
                WHERE j.id IS NULL";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Equipo equipo = new Equipo(reader.GetString(1), reader.GetString(2))
                    {
                        Id = reader.GetInt32(0)
                    };
                    equiposSinJugadores.Add(equipo);
                }

                reader.Close();
                connection.Close();
            }

            return equiposSinJugadores;
        }
    }
}