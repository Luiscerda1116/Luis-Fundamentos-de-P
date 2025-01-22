// Clase para representar cada nodo de la lista
public class Nodo
{
    public int Dato { get; set; }
    public Nodo Siguiente { get; set; }

    public Nodo(int dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

// Clase para manejar la lista enlazada
public class ListaEnlazada
{
    private Nodo cabeza;

    public ListaEnlazada()
    {
        cabeza = null;
    }

    // Método para agregar elementos a la lista
    public void AgregarElemento(int dato)
    {
        Nodo nuevoNodo = new Nodo(dato);
        
        if (cabeza == null)
        {
            cabeza = nuevoNodo;
            return;
        }

        Nodo actual = cabeza;
        while (actual.Siguiente != null)
        {
            actual = actual.Siguiente;
        }
        actual.Siguiente = nuevoNodo;
    }

    // Método para contar elementos de la lista
    public int ContarElementos()
    {
        int contador = 0;
        Nodo actual = cabeza;

        // Recorremos la lista hasta llegar al final
        while (actual != null)
        {
            contador++; // Incrementamos el contador por cada nodo
            actual = actual.Siguiente; // Avanzamos al siguiente nodo
        }

        return contador;
    }

    // Método para mostrar la lista (útil para verificar)
    public void MostrarLista()
    {
        Nodo actual = cabeza;
        while (actual != null)
        {
            Console.Write(actual.Dato + " -> ");
            actual = actual.Siguiente;
        }
        Console.WriteLine("null");
    }
}
