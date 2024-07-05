using practica_equipo_futbol.Models;
using System;
using System.Collections.Generic;

namespace practica_equipo_futbol
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*

            // Cargar equipo
            Equipo equipo1 = new Equipo("Juan", "Mariano");

            // Cargar jugadores
            Jugador jugador1 = new Jugador("Valentino", 9);
            Jugador jugador2 = new Jugador("Thiago", 5);

            // Agregar los jugadores
            equipo1.AgregarJugador(jugador1);
            equipo1.AgregarJugador(jugador2);

            // Mostrar información
            Console.WriteLine($"Equipo: {equipo1.Nombre}");
            Console.WriteLine($"Entrenador: {equipo1.Entrenador}");
            Console.WriteLine("Jugadores:");
            foreach (var jugador in equipo1.Jugadores)
            {
                Console.WriteLine($"Jugador: {jugador.Nombre}, Número: {jugador.Numero}");
            }

            // Buscar los jugadores
            bool buscarJugador1 = equipo1.BuscarJugador("Valentino");
            bool buscarJugador2 = equipo1.BuscarJugador("Juan");

            // Mostrar información sobre la búsqueda
            Console.WriteLine($"Jugador Valentino: {buscarJugador1}");
            Console.WriteLine($"Jugador Juan: {buscarJugador2}");

            // Eliminar jugador
            Jugador jugadorABorrar = equipo1.Jugadores.Find(j => j.Nombre == "Valentino");
            if (jugadorABorrar != null)
            {
                // Si se encontró, eliminarlo del equipo
                equipo1.BorrarJugador(jugadorABorrar);
            }

            // Crear equipo visitante y local
            EquipoLocal equipoLocal = new EquipoLocal("Los Pibes", "Luis");
            EquipoVisitante equipoVisitante = new EquipoVisitante("Villeros", "Jose");

            // Crear una instancia de la clase Partido con los equipos local y visitante
            Partido partido = new Partido(equipoLocal, equipoVisitante);

            // Simular el partido y obtener el resultado
            string resultadoPartido = partido.SimularPartido();

            // Mostrar el resultado del partido
            Console.WriteLine($"Resultado del partido: {resultadoPartido}");
            */

            EquipoBD equipoBD = new EquipoBD();
            int opcion;

            do
            {
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1 - Ver jugadores de un equipo");
                Console.WriteLine("2 - Actualizar ciudad de un equipo");
                Console.WriteLine("3 - Ver todos los equipos y sus entrenadores");
                Console.WriteLine("4 - Ver equipos y jugadores ordenados");
                Console.WriteLine("5 - Ver número de jugadores por equipo");
                Console.WriteLine("6 - Cambiar equipo de un jugador");
                Console.WriteLine("7 - Ver equipos sin jugadores");
                Console.WriteLine("0 - Salir");
                Console.Write("Opción: ");
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Por favor, ingrese una opción válida.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        // Obtener la lista de jugadores de un equipo específico
                        Console.WriteLine("Ingrese el ID del equipo para ver sus jugadores:");
                        int equipoId = int.Parse(Console.ReadLine());
                        var jugadores = equipoBD.GetJugadoresPorEquipoId(equipoId);

                        Console.WriteLine($"\nJugadores del equipo con ID {equipoId}:");
                        foreach (var jugador in jugadores)
                        {
                            Console.WriteLine($"ID: {jugador.Id}, Nombre: {jugador.Nombre}, Número: {jugador.Numero}");
                        }
                        break;

                    case 2:
                        // Actualizar ciudad de un equipo
                        Console.WriteLine("Ingrese el ID del equipo:");
                        int equipoIdCiudad = int.Parse(Console.ReadLine());
                        Console.WriteLine("Ingrese la nueva ciudad del equipo:");
                        string nuevaCiudad = Console.ReadLine();
                        equipoBD.UpdateCiudadEquipo(equipoIdCiudad, nuevaCiudad);
                        Console.WriteLine($"Ciudad del equipo con ID {equipoIdCiudad} actualizada a {nuevaCiudad}.");
                        break;

                    case 3:
                        // Obtener la lista de todos los equipos y sus entrenadores
                        var equiposConEntrenadores = equipoBD.GetEquiposConEntrenadores();
                        Console.WriteLine("Lista de todos los equipos y sus entrenadores: \n");
                        foreach (var equipo in equiposConEntrenadores)
                        {
                            Console.WriteLine($"ID: {equipo.Id}, Nombre: {equipo.Nombre}, Entrenador: {equipo.Entrenador}, Ciudad: {equipo.Ciudad}");
                        }
                        break;

                    case 4:
                        // Lista ordenada de jugadores y equipos
                        var equiposConJugadores = equipoBD.GetEquiposYJugadoresOrdenados();

                        Console.WriteLine("Lista de equipos y sus jugadores ordenados por nombre del equipo y del jugador:");
                        foreach (var equipoConJugadores in equiposConJugadores)
                        {
                            var equipo = equipoConJugadores.Equipo;
                            Console.WriteLine($"\nEquipo: {equipo.Nombre}, Entrenador: {equipo.Entrenador}, Ciudad: {equipo.Ciudad}");

                            foreach (var jugador in equipoConJugadores.Jugadores)
                            {
                                Console.WriteLine($"\tJugador: {jugador.Nombre}, Número: {jugador.Numero}");
                            }
                        }
                        break;

                    case 5:
                        // Mostrar número de jugadores por equipo
                        Console.WriteLine("\nNúmero de jugadores por equipo: \n");
                        var equipos = equipoBD.GetEquiposConEntrenadores();
                        foreach (var equipo in equipos)
                        {
                            int numeroJugadores = equipoBD.GetNumeroJugadoresPorEquipo(equipo.Id);
                            Console.WriteLine($"Equipo: {equipo.Nombre}, Número de Jugadores: {numeroJugadores}");
                        }
                        break;

                    case 6:
                        // Cambiar equipo del jugador
                        Console.WriteLine("Ingrese el ID del jugador a cambiar de equipo:");
                        int jugadorId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Ingrese el ID del nuevo equipo:");
                        int nuevoEquipoId = int.Parse(Console.ReadLine());
                        equipoBD.CambiarEquipoJugador(jugadorId, nuevoEquipoId);
                        Console.WriteLine($"Se cambió el equipo del jugador con ID {jugadorId} al equipo con ID {nuevoEquipoId}.");
                        break;

                    case 7:
                        // Obtener la lista de equipos que no tienen jugadores
                        var equiposSinJugadores = equipoBD.GetEquiposSinJugadores();
                        Console.WriteLine("\nEquipos que no tienen jugadores: \n");
                        foreach (var equipo in equiposSinJugadores)
                        {
                            Console.WriteLine($"ID: {equipo.Id}, Nombre: {equipo.Nombre}, Entrenador: {equipo.Entrenador}");
                        }
                        break;

                    case 0:
                        Console.WriteLine("Saliendo del programa");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }

                Console.WriteLine("\n----------------------------------------------\n");
            } while (opcion != 0);
        }
    }
}
        
    
