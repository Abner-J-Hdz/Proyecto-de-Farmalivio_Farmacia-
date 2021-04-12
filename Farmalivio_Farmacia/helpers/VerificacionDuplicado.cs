using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Farmalivio_Farmacia.helpers;
using Farmalivio_Farmacia.Models;
namespace Farmalivio_Farmacia.helpers
{
    public class VerificacionDuplicado
    {
        public bool VerificarCategoria(string par)
        {
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
                var varCat = new categoria();
                 varCat = contex.categoria.Where(x => x.nombre_categoria == par).SingleOrDefault();
                if (varCat != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool VerificarLaboratorio(string par)
        {
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
                var varCat = new laboratorio();
                varCat = contex.laboratorio.Where(x => x.nombre_laboratorio == par).SingleOrDefault() ;
                if (varCat != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool VerificarPatologia(string par)
        {
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
                var varCat = new patologia();
                varCat = contex.patologia.Where(x => x.nombre_patologia == par).SingleOrDefault();
                if (varCat != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool VerificarGenerico(string par)
        {
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
                var varCat = new generico();
                varCat = contex.generico.Where(x => x.nombre_generico == par).SingleOrDefault() ;
                if (varCat != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool VerificarPresentacion(string par)
        {
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
                var varCat = new presentacion();
                varCat = contex.presentacion.Where(x => x.nombre_presentacion == par).SingleOrDefault();
                if (varCat != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool VerificarMedida(string par)
        {
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
                var varCat = new umedida();
                varCat = contex.umedida.Where(x => x.nombre_umedida == par).SingleOrDefault();
                if (varCat != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool VerificarProveedor(string par)
        {
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
                var varCat = new proveedor();
                varCat = contex.proveedor.Where(x => x.nombre_proveedor == par).SingleOrDefault();
                if (varCat != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool VerificarProveedorRuc(string par)
        {
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
                var varCat = new proveedor();
                varCat = contex.proveedor.Where(x => x.ruc == par).SingleOrDefault();
                if (varCat != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool VerificarCliente(string par, int id)
        {
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
                var varCat = new cliente();
                varCat = contex.cliente.Where(x => x.cedula == par && x.id_cliente!=id).SingleOrDefault();
                if (varCat != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool VerificarProductoCodigo(string par)
        {
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
                var varCat = new producto();
                varCat = contex.producto.Where(x => x.codigo_producto == par).SingleOrDefault();
                if (varCat != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool VerificarProductoCodigoEdit(string par, int id)
        {
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
                var varCat = new producto();
                varCat = contex.producto.Where(x => x.codigo_producto == par && x.id_producto!=id).SingleOrDefault();
                if (varCat != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool VerificarLote(string par)
        {
            using (Farmacia_FarmalivioEntities contex = new Farmacia_FarmalivioEntities())
            {
                var varCat = new lote();
                varCat = contex.lote.Where(x => x.nlote == par).SingleOrDefault();
                if (varCat != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}