using System;
using System.Collections.Generic;

class Grafo
{
    private const int MAX_NODOS = 10;
    private const int INFINITO = 9999;

    private string[] nombres;
    private int[,] matriz;
    private bool esDirigido;
    private int numNodos;

    public Grafo(bool esDirigido)
    {
        this.esDirigido = esDirigido;
        this.numNodos = 0;
        this.nombres = new string[MAX_NODOS];
        this.matriz = new int[MAX_NODOS, MAX_NODOS];

        for (int i = 0; i < MAX_NODOS; i++)
        {
            for (int j = 0; j < MAX_NODOS; j++)
            {
                matriz[i, j] = 0;
            }
        }
    }

    public int AgregarNodo(string nombre)
    {
        if (numNodos >= MAX_NODOS)
        {
            Console.WriteLine("Error: Límite de nodos alcanzado.");
            return -1;
        }
        nombres[numNodos] = nombre;
        return numNodos++;
    }

    public int BuscarNodo(string nombre)
    {
        for (int i = 0; i < numNodos; i++)
        {
            if (nombres[i] == nombre)
            {
                return i;
            }
        }
        return -1;
    }

    public void AgregarArista(string origen, string destino, int peso)
    {
        int i = BuscarNodo(origen);
        int j = BuscarNodo(destino);
        if (i == -1 || j == -1)
        {
            Console.WriteLine("Error: Nodo no encontrado.");
            return;
        }
        matriz[i, j] = peso;
        if (!esDirigido)
        {
            matriz[j, i] = peso;
        }
    }

    public void ImprimirGrafo()
    {
        Console.Write("\n   ");
        for (int i = 0; i < numNodos; i++)
        {
            Console.Write("{0,-10} ", nombres[i]);
        }
        Console.WriteLine();

        for (int i = 0; i < numNodos; i++)
        {
            Console.Write("{0,-10} ", nombres[i]);
            for (int j = 0; j < numNodos; j++)
            {
                Console.Write("{0,-10} ", matriz[i, j]);
            }
            Console.WriteLine();
        }
    }

    public void CalcularCentralidadGrado()
    {
        Console.WriteLine("\nCentralidad de grado:");
        for (int i = 0; i < numNodos; i++)
        {
            int grado = 0;
            for (int j = 0; j < numNodos; j++)
            {
                if (matriz[i, j] > 0) grado++;
            }
            float centralidad = (float)grado / (numNodos - 1);
            Console.WriteLine("{0}: {1} conexiones, centralidad = {2:F4}", nombres[i], grado, centralidad);
        }
    }

    private void Dijkstra(int origen, int[] distancias)
    {
        bool[] visitado = new bool[MAX_NODOS];
        for (int i = 0; i < numNodos; i++) distancias[i] = INFINITO;
        distancias[origen] = 0;

        for (int count = 0; count < numNodos - 1; count++)
        {
            int min = INFINITO, u = -1;
            for (int i = 0; i < numNodos; i++)
            {
                if (!visitado[i] && distancias[i] <= min)
                {
                    min = distancias[i];
                    u = i;
                }
            }

            if (u == -1) break;
            visitado[u] = true;

            for (int v = 0; v < numNodos; v++)
            {
                if (!visitado[v] && matriz[u, v] > 0 && 
                    distancias[u] + matriz[u, v] < distancias[v])
                {
                    distancias[v] = distancias[u] + matriz[u, v];
                }
            }
        }
    }

    public void CalcularCentralidadCercania()
    {
        Console.WriteLine("\nCentralidad de cercanía:");
        for (int i = 0; i < numNodos; i++)
        {
            int[] distancias = new int[MAX_NODOS];
            Dijkstra(i, distancias);
            int suma = 0;
            for (int j = 0; j < numNodos; j++)
            {
                if (i != j && distancias[j] != INFINITO)
                {
                    suma += distancias[j];
                }
            }
            float centralidad = (suma > 0) ? (float)(numNodos - 1) / suma : 0;
            Console.WriteLine("{0}: centralidad = {1:F4}", nombres[i], centralidad);
        }
    }

    public void RutaMasBarata(string origen, string destino)
    {
        int i = BuscarNodo(origen);
        int j = BuscarNodo(destino);
        if (i == -1 || j == -1)
        {
            Console.WriteLine("Error: Nodo no encontrado.");
            return;
        }
        int[] distancias = new int[MAX_NODOS];
        Dijkstra(i, distancias);
        if (distancias[j] == INFINITO)
        {
            Console.WriteLine("No hay ruta de {0} a {1}", origen, destino);
        }
        else
        {
            Console.WriteLine("Ruta más barata de {0} a {1}: Costo = {2}", origen, destino, distancias[j]);
        }
    }
}

class Program
{
    static void Main()
    {
        // Ejemplo 1: Red Social
        Console.WriteLine("=== RED SOCIAL ===");
        Grafo red = new Grafo(false);
        red.AgregarNodo("Ana");
        red.AgregarNodo("Carlos");
        red.AgregarNodo("Maria");
        red.AgregarNodo("Juan");
        red.AgregarNodo("Elena");
        red.AgregarNodo("Pedro");
        red.AgregarNodo("Laura");

        red.AgregarArista("Ana", "Carlos", 1);
        red.AgregarArista("Ana", "Maria", 1);
        red.AgregarArista("Carlos", "Maria", 1);
        red.AgregarArista("Carlos", "Juan", 1);
        red.AgregarArista("Maria", "Elena", 1);
        red.AgregarArista("Elena", "Pedro", 1);
        red.AgregarArista("Pedro", "Laura", 1);
        red.AgregarArista("Laura", "Ana", 1);
        red.AgregarArista("Juan", "Pedro", 1);

        red.ImprimirGrafo();
        red.CalcularCentralidadGrado();
        red.CalcularCentralidadCercania();

        // Ejemplo 2: Tránsito Urbano
        Console.WriteLine("\n=== TRÁNSITO URBANO ===");
        Grafo ciudad = new Grafo(true);
        ciudad.AgregarNodo("Plaza");
        ciudad.AgregarNodo("Mercado");
        ciudad.AgregarNodo("Hospital");
        ciudad.AgregarNodo("Univ");
        ciudad.AgregarNodo("Parque");
        ciudad.AgregarNodo("CComercial");
        ciudad.AgregarNodo("Estacion");

        ciudad.AgregarArista("Plaza", "Mercado", 5);
        ciudad.AgregarArista("Plaza", "Hospital", 8);
        ciudad.AgregarArista("Mercado", "Univ", 10);
        ciudad.AgregarArista("Hospital", "Univ", 6);
        ciudad.AgregarArista("Univ", "Parque", 4);
        ciudad.AgregarArista("Parque", "CComercial", 7);
        ciudad.AgregarArista("CComercial", "Estacion", 3);
        ciudad.AgregarArista("Estacion", "Plaza", 12);
        ciudad.AgregarArista("Mercado", "Parque", 9);
        ciudad.AgregarArista("Hospital", "CComercial", 11);

        ciudad.ImprimirGrafo();

        // Ejemplo 3: Red de Vuelos
        Console.WriteLine("\n=== RED DE VUELOS ===");
        Grafo vuelos = new Grafo(true);
        vuelos.AgregarNodo("Madrid");
        vuelos.AgregarNodo("Barcelona");
        vuelos.AgregarNodo("Lisboa");
        vuelos.AgregarNodo("Paris");
        vuelos.AgregarNodo("Roma");
        vuelos.AgregarNodo("Londres");

        vuelos.AgregarArista("Madrid", "Barcelona", 90);
        vuelos.AgregarArista("Madrid", "Lisboa", 110);
        vuelos.AgregarArista("Madrid", "Paris", 150);
        vuelos.AgregarArista("Barcelona", "Roma", 120);
        vuelos.AgregarArista("Barcelona", "Paris", 100);
        vuelos.AgregarArista("Lisboa", "Londres", 170);
        vuelos.AgregarArista("Paris", "Londres", 130);
        vuelos.AgregarArista("Roma", "Madrid", 160);

        vuelos.ImprimirGrafo();

        vuelos.RutaMasBarata("Madrid", "Londres");
        vuelos.RutaMasBarata("Barcelona", "Londres");
        vuelos.RutaMasBarata("Lisboa", "Roma");

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}
