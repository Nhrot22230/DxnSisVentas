using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tester.DxnSisventas;

namespace tester
{
    internal class Program
    {
        // APIS
        public static DocumentosAPIClient apiDocumentos = new DocumentosAPIClient();
        public static PersonasAPIClient apiPersonas = new PersonasAPIClient();
        public static ProductosAPIClient apiProductos = new ProductosAPIClient();

        // LISTAS
        public static BindingList<empleado> empleados = (new BindingList<empleado>(apiPersonas.listarEmpleados("")));
        public static BindingList<producto> productos = (new BindingList<producto>(apiProductos.listarProductos("")));
        public static BindingList<cliente> clientes = (new BindingList<cliente>(apiPersonas.listarClientes("")));
        public static ordenVenta crearUnaOrden()
        {
            ordenVenta ordenVenta = new ordenVenta();
            ordenVenta.encargadoVenta = empleados.Where(e => e.rol == rol.EncargadoAlmacen).FirstOrDefault();
            ordenVenta.repartidor = empleados.Where(e => e.rol == rol.Repartidor).FirstOrDefault();
            ordenVenta.cliente = clientes.FirstOrDefault();
            // ATRIBUTOS GENERALES
            ordenVenta.estadoSpecified = true;
            ordenVenta.estado = estadoOrden.Pendiente;
            
            ordenVenta.fechaCreacionSpecified = true;
            ordenVenta.fechaCreacion = DateTime.Now;
            ordenVenta.tipoVentaSpecified = true;
            ordenVenta.tipoVenta = tipoVenta.Delivery;
            ordenVenta.metodoPagoSpecified = true;
            ordenVenta.metodoPago = metodoPago.Efectivo;
            ordenVenta.porcentajeDescuento = 0;
            // LINEAS DE ORDEN
            BindingList<lineaOrden> lineasOrden = new BindingList<lineaOrden>();
            for (int i = 0; i < 7; i++)
            {
                lineaOrden lineaOrden = new lineaOrden();
                lineaOrden.producto = productos[i];
                lineaOrden.cantidad = i + 1;
                lineaOrden.subtotal = lineaOrden.cantidad * lineaOrden.producto.precioUnitario;
                lineasOrden.Add(lineaOrden);
            }
            ordenVenta.lineasOrden = lineasOrden.ToArray();
            ordenVenta.total = lineasOrden.Sum(l => l.subtotal);
            int resultado = apiDocumentos.insertarOrdenVenta(ordenVenta);
            if (resultado > 0)
                Console.WriteLine("Orden de venta creada");
            else
                Console.WriteLine("Error al crear orden de venta");
            return ordenVenta;
        }
        public static void mostrarOrdenesDeVenta()
        {
            BindingList<ordenVenta> ordenesVenta = new BindingList<ordenVenta>(apiDocumentos.listarOrdenVenta(""));
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("LISTADO DE ORDENES DE VENTA:");
            foreach (var orden in ordenesVenta)
            {
                Console.WriteLine("ID de Orden: " + orden.idOrdenVentaCadena);
                Console.WriteLine("Cliente: " + orden.cliente.nombre + " " + orden.cliente.apellidoPaterno);
                Console.WriteLine("Encargado de Venta: " + orden.encargadoVenta.nombre + " " + orden.encargadoVenta.apellidoPaterno);
                Console.WriteLine("Repartidor: " + orden.repartidor.nombre + " " + orden.repartidor.apellidoPaterno);
                Console.WriteLine("Fecha de Creación: " + orden.fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"));
                Console.WriteLine("Fecha de Entrega: " + orden.fechaEntrega.ToString("yyyy-MM-dd HH:mm:ss"));
                Console.WriteLine("Tipo de Venta: " + orden.tipoVenta);
                Console.WriteLine("Método de Pago: " + orden.metodoPago);
                Console.WriteLine("Estado: " + orden.estado);
                Console.WriteLine("Porcentaje de Descuento: " + orden.porcentajeDescuento + "%");
                Console.WriteLine("Total: " + orden.total);
                Console.WriteLine("Lineas de Orden:");

                foreach (var linea in orden.lineasOrden)
                {
                    Console.WriteLine("    Producto: " + linea.producto.nombre);
                    Console.WriteLine("    Cantidad: " + linea.cantidad);
                    Console.WriteLine("    Subtotal: " + linea.subtotal);
                }
                Console.WriteLine("---------------------------------------------------");
            }
        }
        public static void testerOrdenVenta()
        {

            crearUnaOrden();

            // LISTAR ORDENES DE VENTA
            mostrarOrdenesDeVenta();


            // ACTUALIZAR UNA ORDEN DE VENTA
            BindingList<ordenVenta> ordenesVenta = new BindingList<ordenVenta>(apiDocumentos.listarOrdenVenta(""));
            ordenVenta orden_venta = ordenesVenta.Last();
            orden_venta.fechaEntregaSpecified = true;
            orden_venta.fechaEntrega = DateTime.Now.AddDays(1);
            orden_venta.estadoSpecified = true;
            orden_venta.estado = estadoOrden.Entregado;
            orden_venta.cliente =clientes.Last();
            orden_venta.encargadoVenta = empleados.Last();
            int resultado = apiDocumentos.actualizarOrdenVenta(orden_venta);            
            if(resultado > 0)
                Console.WriteLine("Orden de venta actualizada");
            else
                Console.WriteLine("Error al actualizar orden de venta");
            mostrarOrdenesDeVenta();
        }


        static void Main(string[] args)
        {
            testerOrdenVenta();   
        }
    }
}
