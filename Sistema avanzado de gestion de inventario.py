import json
from typing import Dict, List


class Producto:
    def __init__(self, id: int, nombre: str, cantidad: int, precio: float):
        self.id = id
        self.nombre = nombre
        self.cantidad = cantidad
        self.precio = precio

    def to_dict(self) -> Dict:
        return {
            "id": self.id,
            "nombre": self.nombre,
            "cantidad": self.cantidad,
            "precio": self.precio
        }

    @classmethod
    def from_dict(cls, data: Dict):
        return cls(data["id"], data["nombre"], data["cantidad"], data["precio"])


class Inventario:
    def __init__(self):
        self.productos: Dict[int, Producto] = {}

    def añadir_producto(self, producto: Producto) -> None:
        self.productos[producto.id] = producto

    def eliminar_producto(self, id: int) -> None:
        if id in self.productos:
            del self.productos[id]

    def actualizar_producto(self, id: int, cantidad: int = None, precio: float = None) -> None:
        if id in self.productos:
            if cantidad is not None:
                self.productos[id].cantidad = cantidad
            if precio is not None:
                self.productos[id].precio = precio

    def buscar_producto(self, nombre: str) -> List[Producto]:
        return [p for p in self.productos.values() if nombre.lower() in p.nombre.lower()]

    def mostrar_productos(self) -> List[Producto]:
        return list(self.productos.values())

    def guardar_inventario(self, nombre_archivo: str) -> None:
        with open(nombre_archivo, 'w') as f:
            json.dump([p.to_dict() for p in self.productos.values()], f)

    def cargar_inventario(self, nombre_archivo: str) -> None:
        try:
            with open(nombre_archivo, 'r') as f:
                datos = json.load(f)
                self.productos = {p["id"]: Producto.from_dict(p) for p in datos}
        except FileNotFoundError:
            print(f"El archivo {nombre_archivo} no existe. Se iniciará con un inventario vacío.")


def menu_principal():
    print("\n--- Sistema de Gestión de Inventario ---")
    print("1. Añadir producto")
    print("2. Eliminar producto")
    print("3. Actualizar producto")
    print("4. Buscar producto")
    print("5. Mostrar todos los productos")
    print("6. Guardar inventario")
    print("7. Cargar inventario")
    print("8. Salir")
    return input("Seleccione una opción: ")


def main():
    inventario = Inventario()

    while True:
        opcion = menu_principal()

        if opcion == "1":
            id = int(input("ID del producto: "))
            nombre = input("Nombre del producto: ")
            cantidad = int(input("Cantidad: "))
            precio = float(input("Precio: "))
            producto = Producto(id, nombre, cantidad, precio)
            inventario.añadir_producto(producto)
            print("Producto añadido con éxito.")

        elif opcion == "2":
            id = int(input("ID del producto a eliminar: "))
            inventario.eliminar_producto(id)
            print("Producto eliminado con éxito.")

        elif opcion == "3":
            id = int(input("ID del producto a actualizar: "))
            cantidad = int(input("Nueva cantidad (deje en blanco para no cambiar): ") or None)
            precio = float(input("Nuevo precio (deje en blanco para no cambiar): ") or None)
            inventario.actualizar_producto(id, cantidad, precio)
            print("Producto actualizado con éxito.")

        elif opcion == "4":
            nombre = input("Nombre del producto a buscar: ")
            productos = inventario.buscar_producto(nombre)
            if productos:
                for p in productos:
                    print(f"ID: {p.id}, Nombre: {p.nombre}, Cantidad: {p.cantidad}, Precio: {p.precio}")
            else:
                print("No se encontraron productos con ese nombre.")

        elif opcion == "5":
            productos = inventario.mostrar_productos()
            for p in productos:
                print(f"ID: {p.id}, Nombre: {p.nombre}, Cantidad: {p.cantidad}, Precio: {p.precio}")

        elif opcion == "6":
            nombre_archivo = input("Nombre del archivo para guardar: ")
            inventario.guardar_inventario(nombre_archivo)
            print("Inventario guardado con éxito.")

        elif opcion == "7":
            nombre_archivo = input("Nombre del archivo para cargar: ")
            inventario.cargar_inventario(nombre_archivo)
            print("Inventario cargado con éxito.")

        elif opcion == "8":
            print("Gracias por usar el Sistema de Gestión de Inventario. ¡Hasta luego!")
            break

        else:
            print("Opción no válida. Por favor, intente de nuevo.")


if __name__ == "__main__":
    main()