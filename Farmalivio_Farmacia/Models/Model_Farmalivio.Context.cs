﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Farmalivio_Farmacia.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Farmacia_FarmalivioEntities : DbContext
    {
        public Farmacia_FarmalivioEntities()
            : base("name=Farmacia_FarmalivioEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<bajar_producto> bajar_producto { get; set; }
        public virtual DbSet<categoria> categoria { get; set; }
        public virtual DbSet<cliente> cliente { get; set; }
        public virtual DbSet<compra> compra { get; set; }
        public virtual DbSet<detalle_compra> detalle_compra { get; set; }
        public virtual DbSet<detalle_devolucion> detalle_devolucion { get; set; }
        public virtual DbSet<detalle_producto> detalle_producto { get; set; }
        public virtual DbSet<detalle_venta> detalle_venta { get; set; }
        public virtual DbSet<devolucion> devolucion { get; set; }
        public virtual DbSet<estado> estado { get; set; }
        public virtual DbSet<generico> generico { get; set; }
        public virtual DbSet<laboratorio> laboratorio { get; set; }
        public virtual DbSet<lote> lote { get; set; }
        public virtual DbSet<motivo> motivo { get; set; }
        public virtual DbSet<patologia> patologia { get; set; }
        public virtual DbSet<presentacion> presentacion { get; set; }
        public virtual DbSet<producto> producto { get; set; }
        public virtual DbSet<producto_categoria> producto_categoria { get; set; }
        public virtual DbSet<producto_patologia> producto_patologia { get; set; }
        public virtual DbSet<proveedor> proveedor { get; set; }
        public virtual DbSet<rol> rol { get; set; }
        public virtual DbSet<tipo_devolucion> tipo_devolucion { get; set; }
        public virtual DbSet<umedida> umedida { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }
        public virtual DbSet<venta> venta { get; set; }
        public virtual DbSet<table_test> table_test { get; set; }
        public virtual DbSet<view_rpt_prodmasVendido> view_rpt_prodmasVendido { get; set; }
        public virtual DbSet<view_rpt_ultimasVentas> view_rpt_ultimasVentas { get; set; }
        public virtual DbSet<view_rpt_ventas> view_rpt_ventas { get; set; }
        public virtual DbSet<vista_buscar_producto_dev> vista_buscar_producto_dev { get; set; }
        public virtual DbSet<vista_Compra_listado> vista_Compra_listado { get; set; }
        public virtual DbSet<vista_detalle_compra> vista_detalle_compra { get; set; }
        public virtual DbSet<vista_detalle_producto> vista_detalle_producto { get; set; }
        public virtual DbSet<vista_detalle_venta> vista_detalle_venta { get; set; }
        public virtual DbSet<vista_prod_devolver> vista_prod_devolver { get; set; }
        public virtual DbSet<vista_producto> vista_producto { get; set; }
        public virtual DbSet<vista_producto_categoria> vista_producto_categoria { get; set; }
        public virtual DbSet<vista_producto_patologia> vista_producto_patologia { get; set; }
        public virtual DbSet<vista_productos_a_vencerse> vista_productos_a_vencerse { get; set; }
        public virtual DbSet<vista_productos_Agotados> vista_productos_Agotados { get; set; }
        public virtual DbSet<vista_Productos_devolver> vista_Productos_devolver { get; set; }
        public virtual DbSet<vista_proximo_a_Agotarse> vista_proximo_a_Agotarse { get; set; }
        public virtual DbSet<vista_venta> vista_venta { get; set; }
    }
}