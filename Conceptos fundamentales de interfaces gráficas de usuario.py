import tkinter as tk
from tkinter import ttk
from tkinter import messagebox

class AplicacionTareas:
    def __init__(self, master):
        self.master = master
        master.title("Gestor de Tareas")
        master.geometry("400x300")

        # Etiqueta y campo de entrada
        self.label = tk.Label(master, text="Nueva Tarea:")
        self.label.pack(pady=5)

        self.entrada_tarea = tk.Entry(master, width=40)
        self.entrada_tarea.pack(pady=5)

        # Botones
        self.boton_agregar = tk.Button(master, text="Agregar", command=self.agregar_tarea)
        self.boton_agregar.pack(pady=5)

        self.boton_limpiar = tk.Button(master, text="Limpiar", command=self.limpiar_seleccion)
        self.boton_limpiar.pack(pady=5)

        # Tabla
        self.tabla = ttk.Treeview(master, columns=("Tarea",), show="headings")
        self.tabla.heading("Tarea", text="Tarea")
        self.tabla.pack(pady=10, fill=tk.BOTH, expand=True)

    def agregar_tarea(self):
        tarea = self.entrada_tarea.get()
        if tarea:
            self.tabla.insert("", tk.END, values=(tarea,))
            self.entrada_tarea.delete(0, tk.END)
        else:
            messagebox.showwarning("Advertencia", "Por favor, ingrese una tarea.")

    def limpiar_seleccion(self):
        seleccion = self.tabla.selection()
        if seleccion:
            for item in seleccion:
                self.tabla.delete(item)
        else:
            respuesta = messagebox.askyesno("Confirmar", "¿Desea limpiar toda la lista?")
            if respuesta:
                self.tabla.delete(*self.tabla.get_children())

if __name__ == "__main__":
    root = tk.Tk()
    app = AplicacionTareas(root)
    root.mainloop()