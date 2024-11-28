using Datos;
using Entidades;
using System;
using System.Collections.Generic;

namespace Negocios
{
    public class N_Categoria
    {
        D_Categorias DC = new D_Categorias();
        D_Categorias BC = new D_Categorias(); // variable de capa de dato para BuscarPorCriterio (BC)

        public string InsertarCategoria(E_Categoria categoria)
        {
            string Mensaje = string.Empty;
            // Validaciones
            if (string.IsNullOrWhiteSpace(categoria.NombreCategoria))
                Mensaje = "Error: El nombre de la categoría es obligatorio.";
            if (categoria.DescripcionCategoria.Length > 250)
                Mensaje = "Error: La descripción no puede exceder los 250 caracteres.";
            if (BC.BuscarCategoriaPorCriterio(categoria.NombreCategoria).Count > 0) // Llamada a la capa de datos para buscar el nombre de la categoría.                                                                        //con la variable BC para no generar conflicot con la connectioString de DC
                Mensaje = "Error: El nombre de la categoría " + categoria.NombreCategoria + " ya existe en la base de datos.";
            if (Mensaje == string.Empty) //Validaciones correctas
            {
                int R;
                Mensaje = "Error: La categoria " + categoria.NombreCategoria + " no se ha podido insertar";

                R = DC.IBM_Categorias("INSERTAR", categoria);
                if (R > 0)
                    Mensaje = "Exito: La categoria " + categoria.NombreCategoria + " se ha insertado correctamente";
                return Mensaje;
            }
            return Mensaje;
        }

        public string BorrarCategoria(int idCategoria)
        {
            E_Categoria categoria = new E_Categoria();
            categoria.IdCategoria = idCategoria;
            int R;
            string mensaje = "Error: la categoria no ha podido ser borrada.";
            //if (BC.BuscarCategoriaPorID(idCategoria))
            //{
            //    mensaje = "Error: la categoria no ha podido ser borrada.";
            //}


            R = DC.IBM_Categorias("BORRAR", categoria);
            if (R > 0)
                mensaje = "Exito: La categoria " + categoria.NombreCategoria + " se ha borrado correctamente";
            return mensaje;
        }

        public string ModificarCategoria(E_Categoria categoria)
        {
            string Mensaje = string.Empty;
            // Validaciones
            if (categoria.IdCategoria <= 0)
                Mensaje += "El ID de la categoría debe ser mayor que cero.";
            if (string.IsNullOrWhiteSpace(categoria.NombreCategoria))
                Mensaje += "El nombre de la categoría es obligatorio.";
            if (categoria.DescripcionCategoria.Length > 250)
                Mensaje += "La descripción no puede exceder los 250 caracteres.";
            if (DC.BuscarCategoriaPorCriterio(categoria.NombreCategoria) != null) // Buscar el nombre de la categoría.
                Mensaje += "El nombre de la categoría " + categoria.NombreCategoria + " ya existe en la base de datos.";
            if (Mensaje == string.Empty) //Validaciones correctas
            {
                try
                {
                    if (DC.ModificarCategoria(categoria)) // Llamada a la capa de datos para insertar los datos.
                        Mensaje += "La categoría fue insertada correctamente.";
                    else
                        Mensaje += "La categoría no pudo ser insertada.";
                }
                catch (Exception ex)
                {
                    return "Ocurrió un error inesperado." + ex.Message;
                }
            }
            return Mensaje;
        }

        public List<E_Categoria> ListarCategorias()
        {
            try
            {
                return DC.ListarCategorias();
            }
            catch (Exception ex)
            {
                return new List<E_Categoria>(); // Retornar una lista vacía en caso de error
            }
        }

        public E_Categoria BuscarCategoriaPorID(int idCategoria)
        {
            if (idCategoria <= 0)
            {
                return null;
            }
            else
            {
                try
                {
                    return DC.BuscarCategoriaPorID(idCategoria);
                }
                catch (Exception ex)
                {
                    return null; // Retornar null en caso de error
                }
            }
        }

        public List<E_Categoria> BuscarCategoriaPorCriterio(string criterio)
        {
            // Validación del criterio
            if (string.IsNullOrWhiteSpace(criterio))
            {
                return new List<E_Categoria>();
            }
            try
            {
                return DC.BuscarCategoriaPorCriterio(criterio);
            }
            catch (Exception)
            {
                return new List<E_Categoria>(); // Retornar una lista vacía en caso de error
            }
        }
    }
}