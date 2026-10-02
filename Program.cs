using System;
using System.Collections.Generic;

namespace AlgoritmosOrdenamiento_Daniel_Santa_Cruz
{
    internal class Program
    {
        // Guardamos los datos de la lista para no perderlos
        static List<int> datosRegistrados = new List<int>();

        static void Main(string[] args)
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("========================================================");
                Console.WriteLine("         SISTEMA DE ALGORITMOS DE ORDENAMIENTO          ");
                Console.WriteLine("========================================================");
                Console.WriteLine();
                Console.WriteLine("[1] Registrar Números");
                Console.WriteLine("[2] Mostrar Lista Actual");
                Console.WriteLine("[3] Bubble Sort");
                Console.WriteLine("[4] Insertion Sort");
                Console.WriteLine("[5] Merge Sort");
                Console.WriteLine("[6] Salir");
                Console.WriteLine();
                Console.WriteLine("--------------------------------------------------------");
                Console.Write("Seleccione una opción: ");

                string seleccion = Console.ReadLine();

                switch (seleccion)
                {
                    case "1":
                        AgregarDatos();
                        break;
                    case "2":
                        VerLista();
                        break;
                    case "3":
                        OrdenarBurbuja();
                        break;
                    case "4":
                        OrdenarInsercion();
                        break;
                    case "5":
                        OrdenarMerge();
                        break;
                    case "6":
                        continuar = false;
                        Console.WriteLine("\nCerrando el programa. ¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("\nOpción inválida. Presione ENTER para reintentar.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // OPCION 1: Registro de enteros
        static void AgregarDatos()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRAR NÚMEROS ===");
            Console.Write("¿Cuántos números quiere guardar?: ");

            if (int.TryParse(Console.ReadLine(), out int total) && total > 0)
            {
                int contador = 0;
                while (contador < total)
                {
                    Console.Write($"Número {contador + 1}: ");
                    if (int.TryParse(Console.ReadLine(), out int valor))
                    {
                        datosRegistrados.Add(valor);
                        contador++;
                    }
                    else
                    {
                        Console.WriteLine(" Entrada no válida. Ingrese un entero.");
                    }
                }
                Console.WriteLine("\n¡Todos los números se guardaron correctamente!");
            }
            else
            {
                Console.WriteLine("\nCantidad no válida.");
            }

            Pausar();
        }

        // OPCION 2: Mostrar la lista guardada
        static void VerLista()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA ACTUAL DE DATOS ===");

            if (datosRegistrados.Count == 0)
            {
                Console.WriteLine("No hay datos registrados aún.");
            }
            else
            {
                MostrarArreglo("Lista guardada", datosRegistrados);
            }

            Pausar();
        }

        // OPCION 3: Ordenamiento por Burbuja
        static void OrdenarBurbuja()
        {
            Console.Clear();
            Console.WriteLine("=== ORDENAMIENTO: BUBBLE SORT ===");

            if (!TieneDatos()) return;

           
            List<int> arreglo = new List<int>(datosRegistrados);
            int n = arreglo.Count;

            for (int paso = 0; paso < n - 1; paso++)
            {
                for (int k = 0; k < n - paso - 1; k++)
                {
                    if (arreglo[k] > arreglo[k + 1])
                    {
                        // Intercambiamos valores
                        int aux = arreglo[k];
                        arreglo[k] = arreglo[k + 1];
                        arreglo[k + 1] = aux;
                    }
                }
            }

            MostrarResultados("Bubble Sort", arreglo);
            Pausar();
        }

        // OPCION 4: Ordenamiento por Insercion
        static void OrdenarInsercion()
        {
            Console.Clear();
            Console.WriteLine("=== ORDENAMIENTO: INSERTION SORT ===");

            if (!TieneDatos()) return;

            List<int> arreglo = new List<int>(datosRegistrados);

            for (int pos = 1; pos < arreglo.Count; pos++)
            {
                int clave = arreglo[pos];
                int idx = pos - 1;

                while (idx >= 0 && arreglo[idx] > clave)
                {
                    arreglo[idx + 1] = arreglo[idx];
                    idx--;
                }
                arreglo[idx + 1] = clave;
            }

            MostrarResultados("Insertion Sort", arreglo);
            Pausar();
        }

        // OPCION 5: Ordenamiento Merge Sort
        static void OrdenarMerge()
        {
            Console.Clear();
            Console.WriteLine("=== ORDENAMIENTO: MERGE SORT ===");

            if (!TieneDatos()) return;

            List<int> arreglo = new List<int>(datosRegistrados);
            arreglo = AplicarMergeSort(arreglo);

            MostrarResultados("Merge Sort", arreglo);
            Pausar();
        }

        // Función recursiva para dividir las listas en Merge Sort
        static List<int> AplicarMergeSort(List<int> datos)
        {
            if (datos.Count <= 1) return datos;

            int puntoMedio = datos.Count / 2;
            List<int> parteIzquierda = new List<int>();
            List<int> parteDerecha = new List<int>();

            for (int a = 0; a < puntoMedio; a++)
            {
                parteIzquierda.Add(datos[a]);
            }

            for (int b = puntoMedio; b < datos.Count; b++)
            {
                parteDerecha.Add(datos[b]);
            }

            parteIzquierda = AplicarMergeSort(parteIzquierda);
            parteDerecha = AplicarMergeSort(parteDerecha);

            return MezclarSublistas(parteIzquierda, parteDerecha);
        }

        // Función para combinar sublistas ordenadas en Merge Sort
        static List<int> MezclarSublistas(List<int> izq, List<int> der)
        {
            List<int> mezcla = new List<int>();
            int ptrIzq = 0, ptrDer = 0;

            while (ptrIzq < izq.Count && ptrDer < der.Count)
            {
                if (izq[ptrIzq] <= der[ptrDer])
                {
                    mezcla.Add(izq[ptrIzq]);
                    ptrIzq++;
                }
                else
                {
                    mezcla.Add(der[ptrDer]);
                    ptrDer++;
                }
            }

            while (ptrIzq < izq.Count)
            {
                mezcla.Add(izq[ptrIzq]);
                ptrIzq++;
            }

            while (ptrDer < der.Count)
            {
                mezcla.Add(der[ptrDer]);
                ptrDer++;
            }

            return mezcla;
        }

        // Imprime lista original y lista ordenada
        static void MostrarResultados(string nombreAlgoritmo, List<int> listaOrdenada)
        {
            MostrarArreglo("Lista Original", datosRegistrados);
            MostrarArreglo($"Lista Ordenada ({nombreAlgoritmo})", listaOrdenada);
        }

        // Muestra en consola los números separados por coma
        static void MostrarArreglo(string etiqueta, List<int> elementos)
        {
            Console.Write($"{etiqueta}: ");
            for (int x = 0; x < elementos.Count; x++)
            {
                Console.Write(elementos[x]);
                if (x < elementos.Count - 1) Console.Write(", ");
            }
            Console.WriteLine();
        }

        // Verifica si hay números para procesar
        static bool TieneDatos()
        {
            if (datosRegistrados.Count == 0)
            {
                Console.WriteLine("La lista está vacía. Ingrese números primero.");
                Pausar();
                return false;
            }
            return true;
        }

      
        static void Pausar()
        {
            Console.WriteLine("\nPresione ENTER para volver al menú...");
            Console.ReadLine();
        }
    }
}
