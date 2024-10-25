/*6.Diseña un programa que permita registrar usuarios en un archivo binario. Cada usuario
debe tener un ID, Nombre y Email. Implementa funcionalidades para agregar, listar y buscar
usuarios.*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Busqueda_Binaria_Ejercicio6
{
    // Se crea la clase que representa un usuario con ID, nombre y email
    class Usuario
    {
        //Almacena los datos
        public int ID;        
        public string Nombre; 
        public string Email;  

        // Constructor que inicializa los datos del usuario
        public Usuario(int id, string nombre, string email)
        {
            //Aqui se asignan a cada uno, ID pasa a ser id...
            ID = id;       
            Nombre = nombre; 
            Email = email;  
        }
    }

    class Program
    {
        static string rutaArchivo = "usuarios.dat";  // Nombre del archivo donde se guardan los usuarios y se pone static para que todos los métodos puedan usar la variable sin crear un objeto
        static List<Usuario> usuarios = new List<Usuario>();  // Lista para guardar los usuarios en memoria y también static para que siempre sea la misma lista en todos los métodos

        static void Main(string[] args)
        {
            // Se cargan los usuarios del archivo al inicio del programa
            CargarUsuariosDesdeArchivo();

            while (true)  // Bucle del menú principal
            {
                Console.WriteLine("\n--- Menu ---");
                Console.WriteLine("1. Agregar usuario");
                Console.WriteLine("2. Listar usuarios");
                Console.WriteLine("3. Buscar usuario");
                Console.WriteLine("4. Salir");
                Console.Write("Selecciona una opción: ");
                int opcion = int.Parse(Console.ReadLine());  // Se lee la opción del usuario

                // Se comprueba qué opción se selecciona
                if (opcion == 1)
                {
                    AgregarUsuario(); // Se llama a la función para agregar un nuevo usuario
                }
                else if (opcion == 2)
                {
                    ListarUsuarios(); // Se llama a la función para listar todos los usuarios
                }
                else if (opcion == 3)
                {
                    BuscarUsuario(); // Se llama a la función para buscar un usuario
                }
                else if (opcion == 4)
                {
                    break;  // Si se elige salir, se sale del bucle
                }
                else
                {
                    Console.WriteLine("Opción inválida, intenta de nuevo."); // Mensaje si la opción no es válida
                }
            }
        }

        // Función que carga los usuarios desde el archivo al inicio del programa
        static void CargarUsuariosDesdeArchivo()
        {
            if (File.Exists(rutaArchivo)) // Se verifica si el archivo existe
            {
                using (FileStream archivo = new FileStream(rutaArchivo, FileMode.Open)) // Se abre el archivo para leer
                using (BinaryReader lector = new BinaryReader(archivo)) // Se crea un lector para el archivo
                {
                    while (archivo.Position < archivo.Length)  // Mientras no se haya leído todo el archivo
                    {
                        // Se leen el ID, nombre y email del usuario
                        int id = lector.ReadInt32();  
                        string nombre = lector.ReadString();  
                        string email = lector.ReadString();  

                        // Se crea un nuevo usuario y se agrega a la lista
                        usuarios.Add(new Usuario(id, nombre, email));
                    }
                }
            }
        }

        // Función para agregar un nuevo usuario
        static void AgregarUsuario()
        {
            Console.Write("Ingrese el ID del usuario: "); // Se pide al usuario que ingrese un ID
            int id = int.Parse(Console.ReadLine());  // Se lee 

            Console.Write("Ingrese el nombre del usuario: "); // Se pide el nombre del usuario
            string nombre = Console.ReadLine();  // Se lee 

            Console.Write("Ingrese el email del usuario: "); // Se pide el email del usuario
            string email = Console.ReadLine();  // Se lee 

            // Se crea un nuevo usuario y se agrega a la lista
            usuarios.Add(new Usuario(id, nombre, email));

            // Se guarda el nuevo usuario en el archivo
            using (FileStream archivo = new FileStream(rutaArchivo, FileMode.Append))  // Se abre el archivo para agregar sin borrar lo que hay
            using (BinaryWriter escritor = new BinaryWriter(archivo)) // Se crea un escritor para el archivo
            {
                // Se escriben el ID, nombre y el email del nuevo usuario en el archivo
                escritor.Write(id);         
                escritor.Write(nombre);      
                escritor.Write(email);       
            }

            Console.WriteLine("Usuario agregado con éxito."); // Mensaje de confirmación
        }

        // Función para listar todos los usuarios
        static void ListarUsuarios()
        {
            Console.WriteLine("\n--- Lista de Usuarios ---"); 

            if (usuarios.Count > 0) // Se comprueba si hay usuarios en la lista
            {
                // Se usa un bucle para mostrar todos los usuarios en la lista
                for (int i = 0; i < usuarios.Count; i++)
                {
                    // Se muestran los datos del usuario
                    Console.WriteLine($"ID: {usuarios[i].ID}, Nombre: {usuarios[i].Nombre}, Email: {usuarios[i].Email}");
                }
            }
            else
            {
                // Mensaje si no hay usuarios registrados
                Console.WriteLine("No hay usuarios registrados aún.");
            }
        }

        // Función para buscar un usuario por su ID
        static void BuscarUsuario()
        {
            Console.Write("Ingrese el ID del usuario que desea buscar: "); // Se pide el ID
            int idBuscado = int.Parse(Console.ReadLine()); // Se lee el ID que se busca

            Usuario usuarioEncontrado = null;  // Se crea una variable para guardar el usuario encontrado

            // Se busca el usuario en la lista
            for (int i = 0; i < usuarios.Count; i++)
            {
                // Se comprueba si el ID del usuario actual es el que se busca
                if (usuarios[i].ID == idBuscado)
                {
                    usuarioEncontrado = usuarios[i]; // Si se encuentra, se guarda
                    break;  // Se sale del bucle porque ya se encontró
                }
            }

            // Se verifica si se encontró al usuario
            if (usuarioEncontrado != null)  // Si el usuario no es null, significa que se encontró
            {
                // Se muestran los datos del usuario encontrado
                Console.WriteLine($"Usuario encontrado: ID: {usuarioEncontrado.ID}, Nombre: {usuarioEncontrado.Nombre}, Email: {usuarioEncontrado.Email}");
            }
            else
            {
                // Mensaje si el usuario no fue encontrado
                Console.WriteLine("Usuario no encontrado.");
            }

            Console.ReadKey(); 
        }
    }
}


