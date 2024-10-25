/*Ejercicio No. 1 1. Crea un nuevo proyecto en C#.

2. Define un método “BusquedaBinaria” que reciba un arreglo
ordenado y un valor a buscar.
3. Utiliza un bucle “while” para implementar el algoritmo de
búsqueda binaria.

 Realizado por: Arelys Castillo y Kimberly Zapata
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusquedaBinaria
{
    internal class Busqueda
    {
        private int[] vector;
        public void cargar()
        {
            Console.WriteLine("Busqueda Binaria");
            Console.WriteLine("Ingrese 10 elementos (pueden ser duplicados)"); //Ejercicio2; Ejercicio4
            vector = new int[10];
            for (int f = 0; f < vector.Length; f++)
            {
                Console.Write("Ingrese elemento " + (f + 1) + ": ");
                vector[f] = int.Parse(Console.ReadLine());
            }
        }

        public void OrdenarBurbuja()
        {
            int n = vector.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (vector[j] > vector[j + 1])
                    {
                        int temp = vector[j];
                        vector[j] = vector[j + 1];
                        vector[j + 1] = temp;
                    }
                }
            }
        }

        public void busqueda(int num)
        {
            int l = 0, h = 9;
            int m = 0;
            bool found = false;

            while (l <= h && found == false)
            {
                m = (l + h) / 2;
                if (vector[m] == num)
                    found = true;
                if (vector[m] > num)
                    h = m - 1;
                else
                    l = m + 1;
            }
            if (found == false)
            {
                Console.Write("\nEl elemento {0} no está en el arreglo", num);
            }
            else
            {
                Console.Write("\nEl elemento {0} está en la posición: {1}", num, m + 1);
            }
        }

        public void Imprimir()
        {
            for (int f = 0; f < vector.Length; f++)
            {
                Console.Write(vector[f] + " ");
            }
        }

        //Ejercicio 3
        public void ContarOcurrencias(int num)
        {
            int count = 0; // Variable para contar las ocurrencias
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i] == num)
                {
                    count++; // Incrementa el contador si se encuentra el número
                }
            }

            if (count > 0)
            {
                Console.Write("\nEl elemento {0} aparece {1} veces en el arreglo.", num, count);
            }
            else
            {
                Console.Write("\nEl elemento {0} no está en el arreglo.", num);
            }
        }

        static void Main(string[] args)
        {
            Busqueda pv = new Busqueda();
            pv.cargar();
            pv.OrdenarBurbuja();
            pv.Imprimir();
            Console.Write("\n\nIngresa el elemento a buscar: ");
            int num = int.Parse(Console.ReadLine());
            pv.busqueda(num);
            pv.ContarOcurrencias(num); //Ejercicio3
            Console.ReadKey();

        }
    }
}
