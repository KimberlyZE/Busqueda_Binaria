/*9. Crea un programa que permita registrar información de estudiantes (nombre, edad,
promedio) en un archivo binario. Implementa funciones para agregar, buscar y listar
estudiantes según se indica:
- Cantidad de estudiantes menores de edad
- Cantidad de estudiantes mayores de edad
- Promedio mayor

 Realizado por: Arelys Castillo y Kimberly Zapata*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio9_S10
{
    public class Estudiante
    {
        public string Nombre;
        public int Edad;
        public double Promedio;

        // Constructor para inicializar un objeto Estudiante
        public Estudiante(string nombre, int edad, double promedio)
        {
            Nombre = nombre;
            Edad = edad;
            Promedio = promedio;
        }
    }

    internal class Program
    {
        // Archivo donde se almacenan los datos de los estudiantes
        private static string rutaArchivo = "estudiantes.dat";

        // Método para agregar un estudiante
        private static void AgregarEstudiante()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Registro de Estudiante");

                Console.Write("Ingrese el nombre: ");
                string nombre = Console.ReadLine();

                Console.Write("Ingrese la edad: ");
                int edad = int.Parse(Console.ReadLine());

                Console.Write("Ingrese el promedio: ");
                double promedio = double.Parse(Console.ReadLine());

                // Agregar estudiante al archivo
                using (FileStream mArchivo = new FileStream(rutaArchivo, FileMode.Append))
                using (BinaryWriter Escritor = new BinaryWriter(mArchivo, Encoding.UTF8))
                {
                    Escritor.Write(nombre);
                    Escritor.Write(edad);
                    Escritor.Write(promedio);
                }

                Console.WriteLine("\nEstudiante registrado correctamente.");

                // Pregunta si desea agregar otro registro.
                Console.Write("¿Desea agregar otro estudiante? (s/n): ");
            } while (Console.ReadLine().ToLower() == "s"); // Repite si la respuesta es 's'

            Console.Clear();
        }

        // Método para listar todos los estudiantes
        private static void ListarEstudiantes()
        {
            Console.WriteLine("\nEstudiantes Registrados:");

            // Leer los datos del archivo
            using (FileStream mArchivo = new FileStream(rutaArchivo, FileMode.Open))
            using (BinaryReader Lector = new BinaryReader(mArchivo, Encoding.UTF8))
            {
                while (mArchivo.Position < mArchivo.Length)
                {
                    // Leer datos del estudiante
                    string nombre = Lector.ReadString();
                    int edad = Lector.ReadInt32();
                    double promedio = Lector.ReadDouble();

                    Console.WriteLine("Nombre: {0}", nombre);
                    Console.WriteLine("Edad: {0}", edad);
                    Console.WriteLine("Promedio: {0}", promedio);
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        // Método para buscar estudiantes por edad
        private static void BuscarEstudiantesPorEdad()
        {
            Console.WriteLine("\n« Estudiantes menores de edad »");
            List<Estudiante> menores = new List<Estudiante>(); // Lista para estudiantes menores
            List<Estudiante> mayores = new List<Estudiante>(); // Lista para estudiantes mayores

            // Leer los datos del archivo
            using (FileStream mArchivo = new FileStream(rutaArchivo, FileMode.Open))
            using (BinaryReader Lector = new BinaryReader(mArchivo, Encoding.UTF8))
            {
                while (mArchivo.Position < mArchivo.Length)
                {
                    string nombre = Lector.ReadString();
                    int edad = Lector.ReadInt32();
                    double promedio = Lector.ReadDouble();

                    // Crear un objeto Estudiante
                    Estudiante estudiante = new Estudiante(nombre, edad, promedio);

                    // Clasificar estudiante por edad
                    if (edad < 18)
                        menores.Add(estudiante); // Agregar a menores
                    else
                        mayores.Add(estudiante); // Agregar a mayores
                }
            }

            // Imprimir estudiantes menores
            foreach (var estudiante in menores)
            {
                Console.WriteLine("Nombre: {0}", estudiante.Nombre);
                Console.WriteLine("Edad: {0}", estudiante.Edad);
                Console.WriteLine("Promedio: {0}", estudiante.Promedio);
                Console.WriteLine();
            }

            int totalMenores = 0;
            foreach (var estudiante in menores)
            {
                totalMenores++;
            }
            Console.WriteLine("Total: {0}", totalMenores); // Total de menores

            Console.WriteLine("\n« Estudiantes mayores de edad »");

            // Imprimir estudiantes mayores
            foreach (var estudiante in mayores)
            {
                Console.WriteLine("Nombre: {0}", estudiante.Nombre);
                Console.WriteLine("Edad: {0}", estudiante.Edad);
                Console.WriteLine("Promedio: {0}", estudiante.Promedio);
                Console.WriteLine();
            }

            int totalMayores = 0;
            foreach (var estudiante in mayores)
            {
                totalMayores++;
            }
            Console.WriteLine("Total: {0}", totalMayores); // Total de mayores

            Console.ReadKey();
            Console.Clear();
        }

        // Método para listar estudiantes por promedio
        private static void ListarEstudiantesPorPromedio()
        {
            Console.WriteLine("\nEstudiantes ordenados por promedio (mayor a menor):");
            List<Estudiante> estudiantes = new List<Estudiante>(); // Lista de estudiantes

            // Leer los datos del archivo
            using (FileStream mArchivo = new FileStream(rutaArchivo, FileMode.Open))
            using (BinaryReader Lector = new BinaryReader(mArchivo, Encoding.UTF8))
            {
                while (mArchivo.Position < mArchivo.Length)
                {
                    string nombre = Lector.ReadString();
                    int edad = Lector.ReadInt32();
                    double promedio = Lector.ReadDouble();

                    // Agregar estudiante a la lista
                    estudiantes.Add(new Estudiante(nombre, edad, promedio));
                }
            }

            // Ordenamiento de burbuja
            for (int i = 0; i < estudiantes.Count - 1; i++)
            {
                for (int j = 0; j < estudiantes.Count - i - 1; j++)
                {
                    // Comparar promedios para ordenar
                    if (estudiantes[j].Promedio < estudiantes[j + 1].Promedio)
                    {
                        // Intercambiar estudiantes
                        Estudiante temp = estudiantes[j];
                        estudiantes[j] = estudiantes[j + 1];
                        estudiantes[j + 1] = temp;
                    }
                }
            }

            // Imprimir la lista ordenada
            foreach (var estudiante in estudiantes)
            {
                Console.WriteLine("Nombre: {0}", estudiante.Nombre);
                Console.WriteLine("Edad: {0}", estudiante.Edad);
                Console.WriteLine("Promedio: {0}", estudiante.Promedio);
                Console.WriteLine();
            }

            // Llamar al método para mostrar el estudiante con mayor promedio
            MayorPromedio(estudiantes);

            Console.ReadKey();
            Console.Clear();
        }

        // Método para mostrar el estudiante con el mejor promedio
        private static void MayorPromedio(List<Estudiante> estudiantes)
        {
            if (estudiantes.Count > 0)
            {
                Estudiante mejorEstudiante = estudiantes[0]; // El primero en la lista es el mejor
                Console.WriteLine("\nEstudiante con el mejor promedio:");
                Console.WriteLine("Nombre: {0}", mejorEstudiante.Nombre);
                Console.WriteLine("Edad: {0}", mejorEstudiante.Edad);
                Console.WriteLine("Promedio: {0}", mejorEstudiante.Promedio);
            }
            else
            {
                Console.WriteLine("No hay estudiantes registrados.");
            }
        }


        static void Main(string[] args)
        {
            string opcion;

            do
            {
                Console.WriteLine("Gestión de Estudiantes");
                Console.WriteLine("1. Agregar Estudiante");
                Console.WriteLine("2. Listar Estudiantes");
                Console.WriteLine("3. Buscar Estudiantes por Edad");
                Console.WriteLine("4. Listar Estudiantes por Promedio");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Clear();
                        AgregarEstudiante();
                        break;
                    case "2":
                        Console.Clear();
                        ListarEstudiantes();
                        break;
                    case "3":
                        Console.Clear();
                        BuscarEstudiantesPorEdad();
                        break;
                    case "4":
                        Console.Clear();
                        ListarEstudiantesPorPromedio();
                        break;
                    case "5":
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Inténtalo de nuevo.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            } while (opcion != "5");
        }
    }
}