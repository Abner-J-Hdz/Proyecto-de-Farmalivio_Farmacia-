USE [Farmacia_Farmalivio]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 12/04/2021 05:53:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre_user] [nvarchar](25) NOT NULL,
	[apellido_user] [nvarchar](30) NOT NULL,
	[email] [nvarchar](30) NOT NULL,
	[contrasena] [nvarchar](max) NULL,
	[estado] [bit] NOT NULL,
	[imagen] [nvarchar](50) NULL,
	[id_rol] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cliente]    Script Date: 12/04/2021 05:53:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cliente](
	[id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[nombre_cliente] [nvarchar](25) NOT NULL,
	[apellido_cliente] [nvarchar](30) NOT NULL,
	[direccion] [nvarchar](100) NULL,
	[estado] [bit] NOT NULL,
	[cedula] [nvarchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[estado]    Script Date: 12/04/2021 05:53:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estado](
	[id_estado] [int] NOT NULL,
	[nombre_estado] [nvarchar](15) NULL,
	[descripcion] [nvarchar](50) NULL,
 CONSTRAINT [PK_estado] PRIMARY KEY CLUSTERED 
(
	[id_estado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[venta]    Script Date: 12/04/2021 05:53:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venta](
	[id_venta] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[id_cliente] [int] NOT NULL,
	[fecha] [date] NOT NULL,
	[iva] [decimal](18, 2) NULL,
	[descuento] [decimal](18, 2) NULL,
	[total] [decimal](18, 0) NOT NULL,
	[num_fact]  AS ('F-'+right('00000000'+CONVERT([varchar],[id_venta]),(8))),
	[id_estado] [int] NOT NULL,
 CONSTRAINT [PK__venta__459533BF19EAD34B] PRIMARY KEY CLUSTERED 
(
	[id_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vista_venta]    Script Date: 12/04/2021 05:53:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vista_venta]
AS
SELECT        dbo.venta.id_venta, dbo.venta.fecha, dbo.venta.total, dbo.venta.num_fact, dbo.usuario.nombre_user + ' ' + dbo.usuario.apellido_user AS usuario, dbo.cliente.nombre_cliente + ' ' + dbo.cliente.apellido_cliente AS cliente, 
                         dbo.estado.nombre_estado, dbo.venta.id_usuario
FROM            dbo.venta INNER JOIN
                         dbo.usuario ON dbo.venta.id_usuario = dbo.usuario.id_usuario INNER JOIN
                         dbo.cliente ON dbo.venta.id_cliente = dbo.cliente.id_cliente INNER JOIN
                         dbo.estado ON dbo.venta.id_estado = dbo.estado.id_estado
GO
/****** Object:  Table [dbo].[producto]    Script Date: 12/04/2021 05:53:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[id_producto] [int] IDENTITY(1,1) NOT NULL,
	[nombre_producto] [nvarchar](30) NOT NULL,
	[codigo_producto] [nvarchar](25) NOT NULL,
	[descripcion_producto] [nvarchar](100) NULL,
	[precio_venta] [decimal](18, 2) NOT NULL,
	[stock] [int] NOT NULL,
	[contiene] [decimal](18, 2) NOT NULL,
	[undxcajas] [int] NOT NULL,
	[id_umedida] [int] NOT NULL,
	[id_generico] [int] NULL,
	[id_presentacion] [int] NOT NULL,
	[id_laboratorio] [int] NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK__producto__FF341C0DA6F9F190] PRIMARY KEY CLUSTERED 
(
	[id_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__producto__40F9A2069B073963] UNIQUE NONCLUSTERED 
(
	[codigo_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalle_venta]    Script Date: 12/04/2021 05:53:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalle_venta](
	[id_detalle_venta] [int] IDENTITY(1,1) NOT NULL,
	[id_venta] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio] [decimal](18, 2) NOT NULL,
	[subtotal] [decimal](18, 2) NULL,
	[iva] [decimal](18, 2) NULL,
	[descuento] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_detalle_venta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vista_detalle_venta]    Script Date: 12/04/2021 05:53:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vista_detalle_venta]
AS
SELECT        dbo.detalle_venta.id_detalle_venta, dbo.detalle_venta.id_venta, dbo.detalle_venta.id_producto, dbo.detalle_venta.cantidad, dbo.detalle_venta.precio, dbo.detalle_venta.subtotal, dbo.detalle_venta.iva, 
                         dbo.detalle_venta.descuento, dbo.producto.nombre_producto, dbo.producto.codigo_producto
FROM            dbo.detalle_venta INNER JOIN
                         dbo.producto ON dbo.detalle_venta.id_producto = dbo.producto.id_producto
GO
/****** Object:  Table [dbo].[detalle_compra]    Script Date: 12/04/2021 05:53:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalle_compra](
	[id_detalle_compra] [int] IDENTITY(1,1) NOT NULL,
	[id_compra] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
	[cantidad_cajas] [int] NOT NULL,
	[precio_compra] [decimal](18, 2) NOT NULL,
	[undxcajas] [int] NOT NULL,
	[total_und] [decimal](18, 0) NOT NULL,
	[enexposicion] [bit] NULL,
	[iva] [decimal](18, 2) NULL,
	[descuento] [decimal](18, 2) NULL,
	[id_lote] [int] NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_detalle_compra] PRIMARY KEY CLUSTERED 
(
	[id_detalle_compra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[presentacion]    Script Date: 12/04/2021 05:53:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[presentacion](
	[id_presentacion] [int] IDENTITY(1,1) NOT NULL,
	[nombre_presentacion] [nvarchar](20) NOT NULL,
	[descripcion_presentacion] [nvarchar](100) NULL,
	[estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_presentacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre_presentacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[laboratorio]    Script Date: 12/04/2021 05:53:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[laboratorio](
	[id_laboratorio] [int] IDENTITY(1,1) NOT NULL,
	[nombre_laboratorio] [nvarchar](20) NOT NULL,
	[descripcion_laboratorio] [nvarchar](100) NULL,
	[estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_laboratorio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre_laboratorio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vista_detalle_compra]    Script Date: 12/04/2021 05:53:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vista_detalle_compra]
as SELECT        dbo.detalle_compra.id_detalle_compra, dbo.detalle_compra.id_compra, dbo.detalle_compra.id_producto, dbo.detalle_compra.cantidad_cajas, dbo.detalle_compra.precio_compra, dbo.detalle_compra.undxcajas, 
              dbo.detalle_compra.total_und, dbo.detalle_compra.enexposicion, dbo.detalle_compra.iva, dbo.detalle_compra.descuento, dbo.detalle_compra.id_lote, dbo.detalle_compra.estado, dbo.producto.nombre_producto, 
              dbo.producto.codigo_producto, dbo.producto.descripcion_producto, dbo.producto.contiene,
			  stuff(
							(select ', ' + laboratorio.nombre_laboratorio
							from laboratorio
							inner join producto on producto.id_laboratorio = laboratorio.id_laboratorio
							where producto.id_producto = detalle_compra.id_producto
							for xml path('')), 1,2,'')as laboratorio,
			   stuff(
							(select ', ' + presentacion.nombre_presentacion
							from presentacion
							inner join producto on producto.id_presentacion = presentacion.id_presentacion
							where producto.id_producto = detalle_compra.id_producto
							for xml path('')), 1,2,'')as presentacion

FROM            dbo.detalle_compra INNER JOIN
                         dbo.producto ON dbo.detalle_compra.id_producto = dbo.producto.id_producto
GO
/****** Object:  Table [dbo].[compra]    Script Date: 12/04/2021 05:53:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[compra](
	[id_compra] [int] IDENTITY(1,1) NOT NULL,
	[id_proveedor] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[fecha] [date] NOT NULL,
	[num_fact] [nvarchar](10) NOT NULL,
	[iva] [decimal](18, 2) NULL,
	[subtotal] [decimal](18, 2) NULL,
	[descuento] [decimal](18, 2) NULL,
	[Total] [decimal](18, 2) NULL,
	[id_estado] [int] NOT NULL,
 CONSTRAINT [PK_compra] PRIMARY KEY CLUSTERED 
(
	[id_compra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[proveedor]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[proveedor](
	[id_proveedor] [int] IDENTITY(1,1) NOT NULL,
	[direccion] [nvarchar](30) NULL,
	[estado] [bit] NOT NULL,
	[nombre_proveedor] [nvarchar](25) NOT NULL,
	[ruc] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK__proveedo__8D3DFE28E474E614] PRIMARY KEY CLUSTERED 
(
	[id_proveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__proveedo__6D71DBE8901DF372] UNIQUE NONCLUSTERED 
(
	[nombre_proveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__proveedo__C2B74E61CD697FCB] UNIQUE NONCLUSTERED 
(
	[ruc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vista_Compra_listado]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vista_Compra_listado]
AS
SELECT        dbo.compra.id_compra, dbo.compra.fecha, dbo.compra.num_fact, dbo.compra.iva, dbo.compra.subtotal, dbo.compra.descuento, dbo.compra.Total, dbo.compra.id_estado, dbo.usuario.nombre_user, dbo.usuario.apellido_user, 
                         dbo.proveedor.nombre_proveedor
FROM            dbo.compra INNER JOIN
                         dbo.usuario ON dbo.compra.id_usuario = dbo.usuario.id_usuario INNER JOIN
                         dbo.proveedor ON dbo.compra.id_proveedor = dbo.proveedor.id_proveedor
GO
/****** Object:  Table [dbo].[categoria]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categoria](
	[id_categoria] [int] IDENTITY(1,1) NOT NULL,
	[nombre_categoria] [nvarchar](20) NOT NULL,
	[descripcion_categoria] [nvarchar](100) NULL,
	[estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre_categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[producto_categoria]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto_categoria](
	[id_productocategoria] [int] IDENTITY(1,1) NOT NULL,
	[id_categoria] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
 CONSTRAINT [PK_producto_categoria] PRIMARY KEY CLUSTERED 
(
	[id_productocategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vista_producto_categoria]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vista_producto_categoria]
AS
SELECT        dbo.producto_categoria.id_productocategoria, dbo.producto_categoria.id_categoria, dbo.producto_categoria.id_producto, dbo.categoria.nombre_categoria, dbo.categoria.estado
FROM            dbo.producto_categoria INNER JOIN
                         dbo.categoria ON dbo.producto_categoria.id_categoria = dbo.categoria.id_categoria
WHERE        (dbo.categoria.estado = 1)
GO
/****** Object:  Table [dbo].[patologia]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[patologia](
	[id_patologia] [int] IDENTITY(1,1) NOT NULL,
	[nombre_patologia] [nvarchar](20) NOT NULL,
	[descripcion_patologia] [nvarchar](100) NULL,
	[estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_patologia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre_patologia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[producto_patologia]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto_patologia](
	[id_productopatologia] [int] IDENTITY(1,1) NOT NULL,
	[id_producto] [int] NOT NULL,
	[id_patologia] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_productopatologia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vista_producto_patologia]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vista_producto_patologia]
AS
SELECT        dbo.producto_patologia.id_productopatologia, dbo.producto_patologia.id_producto, dbo.producto_patologia.id_patologia, dbo.patologia.nombre_patologia, dbo.patologia.estado
FROM            dbo.producto_patologia INNER JOIN
                         dbo.patologia ON dbo.producto_patologia.id_patologia = dbo.patologia.id_patologia
WHERE        (dbo.patologia.estado = 1)
GO
/****** Object:  Table [dbo].[lote]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lote](
	[id_lote] [int] IDENTITY(1,1) NOT NULL,
	[nlote] [nvarchar](50) NOT NULL,
	[fecha_vencimiento] [date] NOT NULL,
	[estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_lote] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nlote] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalle_producto]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalle_producto](
	[id_detalle_producto] [int] IDENTITY(1,1) NOT NULL,
	[id_producto] [int] NOT NULL,
	[id_detalle_compra] [int] NOT NULL,
	[id_lote] [int] NOT NULL,
	[numero_caja] [decimal](16, 2) NOT NULL,
	[undxcajas] [int] NOT NULL,
	[enexposicion] [bit] NULL,
	[estado] [bit] NULL,
	[id_compra] [int] NOT NULL,
	[iva] [decimal](15, 2) NOT NULL,
	[precio_compra] [decimal](15, 2) NOT NULL,
	[stock] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK__detalle___C882082564EBE82D] PRIMARY KEY CLUSTERED 
(
	[id_detalle_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vista_detalle_producto]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vista_detalle_producto]
AS
SELECT        dbo.producto.nombre_producto, dbo.detalle_producto.numero_caja, dbo.detalle_producto.undxcajas, dbo.detalle_producto.stock, dbo.detalle_producto.enexposicion, dbo.detalle_producto.estado, 
                         dbo.detalle_producto.precio_compra, dbo.detalle_producto.iva, dbo.producto.codigo_producto, dbo.lote.nlote, dbo.lote.fecha_vencimiento, dbo.proveedor.nombre_proveedor, dbo.compra.fecha, dbo.compra.num_fact, 
                         dbo.detalle_compra.descuento
FROM            dbo.detalle_producto INNER JOIN
                         dbo.producto ON dbo.detalle_producto.id_producto = dbo.producto.id_producto INNER JOIN
                         dbo.compra ON dbo.detalle_producto.id_compra = dbo.compra.id_compra INNER JOIN
                         dbo.lote ON dbo.detalle_producto.id_lote = dbo.lote.id_lote INNER JOIN
                         dbo.proveedor ON dbo.compra.id_proveedor = dbo.proveedor.id_proveedor INNER JOIN
                         dbo.detalle_compra ON dbo.producto.id_producto = dbo.detalle_compra.id_producto AND dbo.compra.id_compra = dbo.detalle_compra.id_compra AND dbo.lote.id_lote = dbo.detalle_compra.id_lote
GO
/****** Object:  View [dbo].[vista_buscar_producto_dev]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vista_buscar_producto_dev]
AS
SELECT        dbo.producto.nombre_producto, dbo.producto.codigo_producto, dbo.detalle_producto.id_detalle_producto, dbo.detalle_producto.id_producto, dbo.detalle_producto.id_detalle_compra, dbo.detalle_producto.id_lote, 
                         dbo.detalle_producto.numero_caja, dbo.detalle_producto.undxcajas, dbo.detalle_producto.estado, dbo.detalle_producto.enexposicion, dbo.detalle_producto.iva, dbo.detalle_producto.precio_compra, dbo.detalle_producto.stock, 
                         dbo.compra.id_proveedor, dbo.compra.fecha, dbo.compra.num_fact, dbo.producto.contiene, dbo.producto.id_umedida, dbo.producto.id_presentacion, dbo.producto.id_laboratorio
FROM            dbo.detalle_producto INNER JOIN
                         dbo.compra ON dbo.detalle_producto.id_compra = dbo.compra.id_compra INNER JOIN
                         dbo.producto ON dbo.detalle_producto.id_producto = dbo.producto.id_producto
GO
/****** Object:  Table [dbo].[generico]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[generico](
	[id_generico] [int] IDENTITY(1,1) NOT NULL,
	[nombre_generico] [nvarchar](20) NOT NULL,
	[descripcion_generico] [nvarchar](100) NULL,
	[estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_generico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre_generico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[umedida]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[umedida](
	[id_umedida] [int] IDENTITY(1,1) NOT NULL,
	[nombre_umedida] [nvarchar](20) NOT NULL,
	[descripcion_umedida] [nvarchar](100) NULL,
	[estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_umedida] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre_umedida] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vista_producto]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vista_producto]

as SELECT        dbo.producto.id_producto, dbo.producto.nombre_producto, dbo.producto.codigo_producto, dbo.producto.descripcion_producto, 
			  dbo.producto.precio_venta, dbo.producto.stock, dbo.producto.contiene, dbo.producto.undxcajas, 
              dbo.producto.id_umedida, dbo.producto.id_generico, dbo.producto.id_presentacion, dbo.producto.id_laboratorio, 
			  dbo.producto.estado, dbo.presentacion.nombre_presentacion, dbo.laboratorio.nombre_laboratorio, 
              dbo.umedida.nombre_umedida, dbo.generico.nombre_generico,
			  	stuff(
				(select ', ' + patologia.nombre_patologia
				from patologia
				inner join producto_patologia on producto_patologia.id_patologia = patologia.id_patologia
				where producto_patologia.id_producto = producto.id_producto
				for xml path('')), 1,2,'')as patologias,
				stuff(
				(select ', ' + categoria.nombre_categoria
				from categoria
				inner join producto_categoria on producto_categoria.id_categoria = categoria.id_categoria
				where producto_categoria.id_producto = producto.id_producto
				for xml path('')), 1,2,'')as categorias

FROM            dbo.producto INNER JOIN
                         dbo.presentacion ON dbo.producto.id_presentacion = dbo.presentacion.id_presentacion INNER JOIN
                         dbo.laboratorio ON dbo.producto.id_laboratorio = dbo.laboratorio.id_laboratorio INNER JOIN
                         dbo.umedida ON dbo.producto.id_umedida = dbo.umedida.id_umedida INNER JOIN
                         dbo.generico ON dbo.producto.id_generico = dbo.generico.id_generico
GO
/****** Object:  Table [dbo].[detalle_devolucion]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalle_devolucion](
	[id_detalledevolucion] [int] IDENTITY(1,1) NOT NULL,
	[id_devolucion] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
	[cantidad_cajas] [decimal](15, 2) NULL,
	[precio_cajas] [decimal](15, 2) NULL,
	[iva] [decimal](15, 2) NULL,
	[subtotal] [decimal](15, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_detalledevolucion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[devolucion]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[devolucion](
	[id_devolucion] [int] IDENTITY(1,1) NOT NULL,
	[id_proveedor] [int] NOT NULL,
	[fecha] [date] NOT NULL,
	[descripcion] [nvarchar](1) NULL,
	[total] [decimal](18, 2) NULL,
	[id_usuario] [int] NOT NULL,
	[id_tipo_devolucion] [int] NOT NULL,
	[num_devolucion]  AS ('F-'+right('0000000'+CONVERT([varchar],[id_devolucion]),(9))),
 CONSTRAINT [PK__devoluci__0BBAEF8D7CC39064] PRIMARY KEY CLUSTERED 
(
	[id_devolucion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rol]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rol](
	[id_rol] [int] IDENTITY(1,1) NOT NULL,
	[rol] [nvarchar](25) NOT NULL,
	[descripcion] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipo_devolucion]    Script Date: 12/04/2021 05:53:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipo_devolucion](
	[id_tipo_devolucion] [int] IDENTITY(1,1) NOT NULL,
	[tipo_devolucion] [nvarchar](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_tipo_devolucion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[categoria] ADD  DEFAULT ('1') FOR [estado]
GO
ALTER TABLE [dbo].[cliente] ADD  DEFAULT ('1') FOR [estado]
GO
ALTER TABLE [dbo].[detalle_producto] ADD  CONSTRAINT [DF__detalle_p__enexp__3F466844]  DEFAULT ('1') FOR [enexposicion]
GO
ALTER TABLE [dbo].[detalle_producto] ADD  CONSTRAINT [DF__detalle_pro__iva__403A8C7D]  DEFAULT ('0') FOR [iva]
GO
ALTER TABLE [dbo].[detalle_producto] ADD  CONSTRAINT [DF__detalle_p__preci__412EB0B6]  DEFAULT ('0') FOR [precio_compra]
GO
ALTER TABLE [dbo].[detalle_venta] ADD  DEFAULT ('0') FOR [subtotal]
GO
ALTER TABLE [dbo].[detalle_venta] ADD  DEFAULT ('0') FOR [iva]
GO
ALTER TABLE [dbo].[detalle_venta] ADD  DEFAULT ('0') FOR [descuento]
GO
ALTER TABLE [dbo].[devolucion] ADD  CONSTRAINT [DF__devolucio__total__4CA06362]  DEFAULT ('0') FOR [total]
GO
ALTER TABLE [dbo].[generico] ADD  DEFAULT ('1') FOR [estado]
GO
ALTER TABLE [dbo].[laboratorio] ADD  DEFAULT ('1') FOR [estado]
GO
ALTER TABLE [dbo].[lote] ADD  DEFAULT ('1') FOR [estado]
GO
ALTER TABLE [dbo].[patologia] ADD  DEFAULT ('1') FOR [estado]
GO
ALTER TABLE [dbo].[presentacion] ADD  DEFAULT ('1') FOR [estado]
GO
ALTER TABLE [dbo].[producto] ADD  CONSTRAINT [DF__producto__estado__7F2BE32F]  DEFAULT ('1') FOR [estado]
GO
ALTER TABLE [dbo].[proveedor] ADD  CONSTRAINT [DF__proveedor__estad__30F848ED]  DEFAULT ('1') FOR [estado]
GO
ALTER TABLE [dbo].[umedida] ADD  DEFAULT ('1') FOR [estado]
GO
ALTER TABLE [dbo].[usuario] ADD  DEFAULT ('1') FOR [estado]
GO
ALTER TABLE [dbo].[venta] ADD  CONSTRAINT [DF__venta__iva__5165187F]  DEFAULT ('0') FOR [iva]
GO
ALTER TABLE [dbo].[venta] ADD  CONSTRAINT [DF__venta__descuento__52593CB8]  DEFAULT ('0') FOR [descuento]
GO
ALTER TABLE [dbo].[compra]  WITH CHECK ADD  CONSTRAINT [FK_compra_estado] FOREIGN KEY([id_estado])
REFERENCES [dbo].[estado] ([id_estado])
GO
ALTER TABLE [dbo].[compra] CHECK CONSTRAINT [FK_compra_estado]
GO
ALTER TABLE [dbo].[compra]  WITH CHECK ADD  CONSTRAINT [FK_compra_proveedor] FOREIGN KEY([id_proveedor])
REFERENCES [dbo].[proveedor] ([id_proveedor])
GO
ALTER TABLE [dbo].[compra] CHECK CONSTRAINT [FK_compra_proveedor]
GO
ALTER TABLE [dbo].[compra]  WITH CHECK ADD  CONSTRAINT [FK_compra_usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[compra] CHECK CONSTRAINT [FK_compra_usuario]
GO
ALTER TABLE [dbo].[detalle_compra]  WITH CHECK ADD  CONSTRAINT [FK_detalle_compra_compra] FOREIGN KEY([id_compra])
REFERENCES [dbo].[compra] ([id_compra])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[detalle_compra] CHECK CONSTRAINT [FK_detalle_compra_compra]
GO
ALTER TABLE [dbo].[detalle_compra]  WITH CHECK ADD  CONSTRAINT [FK_detalle_compra_lote] FOREIGN KEY([id_lote])
REFERENCES [dbo].[lote] ([id_lote])
GO
ALTER TABLE [dbo].[detalle_compra] CHECK CONSTRAINT [FK_detalle_compra_lote]
GO
ALTER TABLE [dbo].[detalle_compra]  WITH CHECK ADD  CONSTRAINT [FK_detalle_compra_producto] FOREIGN KEY([id_producto])
REFERENCES [dbo].[producto] ([id_producto])
GO
ALTER TABLE [dbo].[detalle_compra] CHECK CONSTRAINT [FK_detalle_compra_producto]
GO
ALTER TABLE [dbo].[detalle_devolucion]  WITH CHECK ADD  CONSTRAINT [FK_detalle_devolucion_devolucion] FOREIGN KEY([id_devolucion])
REFERENCES [dbo].[devolucion] ([id_devolucion])
GO
ALTER TABLE [dbo].[detalle_devolucion] CHECK CONSTRAINT [FK_detalle_devolucion_devolucion]
GO
ALTER TABLE [dbo].[detalle_devolucion]  WITH CHECK ADD  CONSTRAINT [FK_detalle_devolucion_producto] FOREIGN KEY([id_producto])
REFERENCES [dbo].[producto] ([id_producto])
GO
ALTER TABLE [dbo].[detalle_devolucion] CHECK CONSTRAINT [FK_detalle_devolucion_producto]
GO
ALTER TABLE [dbo].[detalle_producto]  WITH CHECK ADD  CONSTRAINT [FK_detalle_producto_compra] FOREIGN KEY([id_compra])
REFERENCES [dbo].[compra] ([id_compra])
GO
ALTER TABLE [dbo].[detalle_producto] CHECK CONSTRAINT [FK_detalle_producto_compra]
GO
ALTER TABLE [dbo].[detalle_producto]  WITH CHECK ADD  CONSTRAINT [FK_detalle_producto_detalle_compra] FOREIGN KEY([id_detalle_compra])
REFERENCES [dbo].[detalle_compra] ([id_detalle_compra])
GO
ALTER TABLE [dbo].[detalle_producto] CHECK CONSTRAINT [FK_detalle_producto_detalle_compra]
GO
ALTER TABLE [dbo].[detalle_producto]  WITH CHECK ADD  CONSTRAINT [FK_detalle_producto_lote] FOREIGN KEY([id_lote])
REFERENCES [dbo].[lote] ([id_lote])
GO
ALTER TABLE [dbo].[detalle_producto] CHECK CONSTRAINT [FK_detalle_producto_lote]
GO
ALTER TABLE [dbo].[detalle_venta]  WITH CHECK ADD  CONSTRAINT [FK_detalle_venta_producto] FOREIGN KEY([id_producto])
REFERENCES [dbo].[producto] ([id_producto])
GO
ALTER TABLE [dbo].[detalle_venta] CHECK CONSTRAINT [FK_detalle_venta_producto]
GO
ALTER TABLE [dbo].[detalle_venta]  WITH CHECK ADD  CONSTRAINT [FK_detalle_venta_venta] FOREIGN KEY([id_venta])
REFERENCES [dbo].[venta] ([id_venta])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[detalle_venta] CHECK CONSTRAINT [FK_detalle_venta_venta]
GO
ALTER TABLE [dbo].[devolucion]  WITH CHECK ADD  CONSTRAINT [FK_devolucion_proveedor] FOREIGN KEY([id_proveedor])
REFERENCES [dbo].[proveedor] ([id_proveedor])
GO
ALTER TABLE [dbo].[devolucion] CHECK CONSTRAINT [FK_devolucion_proveedor]
GO
ALTER TABLE [dbo].[devolucion]  WITH CHECK ADD  CONSTRAINT [FK_devolucion_tipo_devolucion] FOREIGN KEY([id_tipo_devolucion])
REFERENCES [dbo].[tipo_devolucion] ([id_tipo_devolucion])
GO
ALTER TABLE [dbo].[devolucion] CHECK CONSTRAINT [FK_devolucion_tipo_devolucion]
GO
ALTER TABLE [dbo].[devolucion]  WITH CHECK ADD  CONSTRAINT [FK_devolucion_usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[devolucion] CHECK CONSTRAINT [FK_devolucion_usuario]
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD  CONSTRAINT [FK_producto_generico] FOREIGN KEY([id_generico])
REFERENCES [dbo].[generico] ([id_generico])
GO
ALTER TABLE [dbo].[producto] CHECK CONSTRAINT [FK_producto_generico]
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD  CONSTRAINT [FK_producto_laboratorio] FOREIGN KEY([id_laboratorio])
REFERENCES [dbo].[laboratorio] ([id_laboratorio])
GO
ALTER TABLE [dbo].[producto] CHECK CONSTRAINT [FK_producto_laboratorio]
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD  CONSTRAINT [FK_producto_presentacion] FOREIGN KEY([id_presentacion])
REFERENCES [dbo].[presentacion] ([id_presentacion])
GO
ALTER TABLE [dbo].[producto] CHECK CONSTRAINT [FK_producto_presentacion]
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD  CONSTRAINT [FK_producto_umedida] FOREIGN KEY([id_umedida])
REFERENCES [dbo].[umedida] ([id_umedida])
GO
ALTER TABLE [dbo].[producto] CHECK CONSTRAINT [FK_producto_umedida]
GO
ALTER TABLE [dbo].[producto_categoria]  WITH CHECK ADD  CONSTRAINT [FK_producto_categoria_categoria] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[categoria] ([id_categoria])
GO
ALTER TABLE [dbo].[producto_categoria] CHECK CONSTRAINT [FK_producto_categoria_categoria]
GO
ALTER TABLE [dbo].[producto_categoria]  WITH CHECK ADD  CONSTRAINT [FK_producto_categoria_producto] FOREIGN KEY([id_producto])
REFERENCES [dbo].[producto] ([id_producto])
GO
ALTER TABLE [dbo].[producto_categoria] CHECK CONSTRAINT [FK_producto_categoria_producto]
GO
ALTER TABLE [dbo].[producto_patologia]  WITH CHECK ADD  CONSTRAINT [FK_producto_patologia_patologia] FOREIGN KEY([id_patologia])
REFERENCES [dbo].[patologia] ([id_patologia])
GO
ALTER TABLE [dbo].[producto_patologia] CHECK CONSTRAINT [FK_producto_patologia_patologia]
GO
ALTER TABLE [dbo].[producto_patologia]  WITH CHECK ADD  CONSTRAINT [FK_producto_patologia_producto] FOREIGN KEY([id_producto])
REFERENCES [dbo].[producto] ([id_producto])
GO
ALTER TABLE [dbo].[producto_patologia] CHECK CONSTRAINT [FK_producto_patologia_producto]
GO
ALTER TABLE [dbo].[usuario]  WITH CHECK ADD  CONSTRAINT [FK_usuario_rol] FOREIGN KEY([id_rol])
REFERENCES [dbo].[rol] ([id_rol])
GO
ALTER TABLE [dbo].[usuario] CHECK CONSTRAINT [FK_usuario_rol]
GO
ALTER TABLE [dbo].[venta]  WITH CHECK ADD  CONSTRAINT [FK_venta_cliente] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[cliente] ([id_cliente])
GO
ALTER TABLE [dbo].[venta] CHECK CONSTRAINT [FK_venta_cliente]
GO
ALTER TABLE [dbo].[venta]  WITH CHECK ADD  CONSTRAINT [FK_venta_estado] FOREIGN KEY([id_estado])
REFERENCES [dbo].[estado] ([id_estado])
GO
ALTER TABLE [dbo].[venta] CHECK CONSTRAINT [FK_venta_estado]
GO
ALTER TABLE [dbo].[venta]  WITH CHECK ADD  CONSTRAINT [FK_venta_usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[venta] CHECK CONSTRAINT [FK_venta_usuario]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[21] 2[11] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "detalle_venta"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 211
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "producto"
            Begin Extent = 
               Top = 6
               Left = 249
               Bottom = 168
               Right = 453
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vista_detalle_venta'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vista_detalle_venta'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "venta"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "usuario"
            Begin Extent = 
               Top = 4
               Left = 525
               Bottom = 134
               Right = 695
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cliente"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "estado"
            Begin Extent = 
               Top = 80
               Left = 690
               Bottom = 193
               Right = 861
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vista_venta'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vista_venta'
GO
