﻿using MySql.Data.MySqlClient;
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
            string query = "SELECT id, nombre, entrenador, ciudad FROM equipos";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string nombre = reader.GetString(reader.GetOrdinal("nombre"));
                    string entrenador = reader.GetString(reader.GetOrdinal("entrenador"));
                    string ciudad = reader.IsDBNull(reader.GetOrdinal("ciudad")) ? string.Empty : reader.GetString(reader.GetOrdinal("ciudad"));

                    Equipo equipo = new Equipo(nombre, entrenador, ciudad)
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id"))
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
            string query = "SELECT id, nombre, numero FROM jugadores WHERE equipo_id = @equipoId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@equipoId", equipoId);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Jugador jugador = new Jugador(
                        reader.GetString("nombre"),
                        reader.GetInt32("numero"),
                        equipoId  // Añadir el equipoId aquí
                    )
                    {
                        Id = reader.GetInt32("id")
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
            string query = "SELECT id, nombre, entrenador, ciudad FROM equipos";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string nombre = reader.GetString(reader.GetOrdinal("nombre"));
                    string entrenador = reader.GetString(reader.GetOrdinal("entrenador"));
                    string ciudad = reader.IsDBNull(reader.GetOrdinal("ciudad")) ? string.Empty : reader.GetString(reader.GetOrdinal("ciudad"));

                    Equipo equipo = new Equipo(nombre, entrenador, ciudad)
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id"))
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

        public void UpdateCiudadEquipo(int equipoId, string ciudad)
        {
            string query = "UPDATE equipos SET ciudad = @ciudad WHERE id = @equipoId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ciudad", ciudad);
                command.Parameters.AddWithValue("@equipoId", equipoId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<EquipoConJugadores> GetEquiposYJugadoresOrdenados()
        {
            List<EquipoConJugadores> equiposConJugadores = new List<EquipoConJugadores>();
            string query = @"
        SELECT e.id AS EquipoId, e.nombre AS EquipoNombre, e.entrenador AS EquipoEntrenador, e.ciudad AS EquipoCiudad,
               j.id AS JugadorId, j.nombre AS JugadorNombre, j.numero AS JugadorNumero
        FROM equipos e
        LEFT JOIN jugadores j ON e.id = j.equipo_id
        ORDER BY e.nombre, j.nombre";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int equipoId = reader.GetInt32(reader.GetOrdinal("EquipoId"));
                    string equipoNombre = reader.GetString(reader.GetOrdinal("EquipoNombre"));
                    string equipoEntrenador = reader.GetString(reader.GetOrdinal("EquipoEntrenador"));
                    string equipoCiudad = reader.IsDBNull(reader.GetOrdinal("EquipoCiudad")) ? string.Empty : reader.GetString(reader.GetOrdinal("EquipoCiudad"));

                    EquipoConJugadores equipoConJugadores = equiposConJugadores.Find(e => e.Equipo.Id == equipoId);
                    if (equipoConJugadores == null)
                    {
                        equipoConJugadores = new EquipoConJugadores
                        {
                            Equipo = new Equipo(equipoNombre, equipoEntrenador, equipoCiudad) { Id = equipoId },
                            Jugadores = new List<Jugador>()
                        };
                        equiposConJugadores.Add(equipoConJugadores);
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("JugadorId")))
                    {
                        Jugador jugador = new Jugador(
                            reader.GetString(reader.GetOrdinal("JugadorNombre")),
                            reader.GetInt32(reader.GetOrdinal("JugadorNumero")),
                            equipoId
                        )
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("JugadorId"))
                        };
                        equipoConJugadores.Jugadores.Add(jugador);
                    }
                }

                reader.Close();
                connection.Close();
            }

            return equiposConJugadores;
        }

        public void CambiarEquipoJugador(int jugadorId, int nuevoEquipoId)
        {
            string query = "UPDATE jugadores SET equipo_id = @nuevoEquipoId WHERE id = @jugadorId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@nuevoEquipoId", nuevoEquipoId);
                command.Parameters.AddWithValue("@jugadorId", jugadorId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Equipo> GetEquiposSinJugadores()
        {
            List<Equipo> equiposSinJugadores = new List<Equipo>();
            string query = @"
        SELECT e.id, e.nombre, e.entrenador, e.ciudad
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
                    string nombre = reader.GetString(reader.GetOrdinal("nombre"));
                    string entrenador = reader.GetString(reader.GetOrdinal("entrenador"));
                    string ciudad = reader.IsDBNull(reader.GetOrdinal("ciudad")) ? string.Empty : reader.GetString(reader.GetOrdinal("ciudad"));

                    Equipo equipo = new Equipo(nombre, entrenador, ciudad)
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id"))
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