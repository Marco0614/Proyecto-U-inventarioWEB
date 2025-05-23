CREATE DATABASE [DB_FARMACIA]

GO

USE [DB_FARMACIA]

GO

--SCHEMAS

CREATE SCHEMA [SCH_ADMINISTRATIVO]

GO

CREATE SCHEMA [SCH_FARMACIA]

GO

--TABLA REGISTRO_USUARIO

CREATE TABLE [SCH_ADMINISTRATIVO].[T_REGISTRO_USUARIO]
(
	[ID_USUARIO] SMALLINT NOT NULL IDENTITY(1,1),
	[IDENTIFICACION_USUARIO] NVARCHAR(20) NOT NULL,
	[NOMBRE_COMPLETO] NVARCHAR(30) NOT NULL,
	[CORREO] NVARCHAR(30) NOT NULL, 
	[TIPO_USUARIO] NVARCHAR(30) NOT NULL, 
	[ESTADO] NVARCHAR(40) NOT NULL, 
	[CONTRASEŅA] NVARCHAR(50) NOT NULL

	CONSTRAINT [PK_T_REGISTRO_USUARIO] PRIMARY KEY CLUSTERED
	(
		[ID_USUARIO]
	)
)

GO

--TABLA REGISTRO_CLIENTE

CREATE TABLE [SCH_ADMINISTRATIVO].[T_REGISTRO_CLIENTE]
(
	[ID_CLIENTE] SMALLINT NOT NULL IDENTITY(1,1),
	[IDENTIFICACION_CLIENTE] NVARCHAR(20) NOT NULL,
	[NOMBRE_COMPLETO] NVARCHAR(30) NOT NULL,
	[CORREO] NVARCHAR(40) NOT NULL

	CONSTRAINT [PK_T_REGISTRO_CLIENTE] PRIMARY KEY CLUSTERED
	(
		[ID_CLIENTE]
	)

)

GO

--TABLA TIPOS_PRODUCTO

CREATE TABLE [SCH_FARMACIA].[T_TIPOS_PRODUCTO]
(
	[CODIGO_TIPO_PRODUCTO] INT NOT NULL IDENTITY(1,1),
	[DESCRIPCION_TIPO_PRODUCTO] NVARCHAR(300) NOT NULL

	CONSTRAINT [PK_T_TIPOS_PRODUCTO] PRIMARY KEY CLUSTERED
	(
		[CODIGO_TIPO_PRODUCTO]
	)
)

GO

--TABLA PRODUCTO

CREATE TABLE [SCH_FARMACIA].[T_PRODUCTO]
(
	[NOMBRE_CATEGORIA] NVARCHAR(50) NOT NULL,
	[CODIGO_TIPO_PRODUCTO] INT NOT NULL,
	[NOMBRE_PRODUCTO] NVARCHAR(70) NOT NULL

	CONSTRAINT [PK_T_PRODUCTO] PRIMARY KEY CLUSTERED
	(
		[NOMBRE_CATEGORIA]
	)
)

GO

--FOREIGN KEY TABLA PRODUCTO

ALTER TABLE [SCH_FARMACIA].[T_PRODUCTO] WITH NOCHECK
ADD CONSTRAINT [FK_T_PRODUCTO_T_TIPOS_PRODUCTO_CODIGO_TIPO_PRODUCTO] FOREIGN KEY(CODIGO_TIPO_PRODUCTO)
	REFERENCES [SCH_FARMACIA].[T_TIPOS_PRODUCTO] (CODIGO_TIPO_PRODUCTO)

GO

ALTER TABLE [SCH_FARMACIA].[T_PRODUCTO] CHECK CONSTRAINT [FK_T_PRODUCTO_T_TIPOS_PRODUCTO_CODIGO_TIPO_PRODUCTO]

GO

--TABLA PRODUCTOS_VENDIDO

CREATE TABLE [SCH_FARMACIA].[T_PRODUCTOS_VENDIDO]
(
	[CODIGO_PRODUCTO] INT NOT NULL IDENTITY(1,1),
	[CODIGO_TIPO_PRODUCTO] INT NOT NULL,
	[DESCRIPCION_PRODUCTO] NVARCHAR(300) NOT NULL,
	[PRECIO] MONEY NOT NULL,
	[ESTADO] NCHAR(30) NOT NULL,
	[CANTIDAD] INT NOT NULL

	CONSTRAINT [PK_T_PRODUCTOS_VENDIDO] PRIMARY KEY CLUSTERED
	(
		[CODIGO_PRODUCTO]
	)
)

GO

--FOREIGN KEY TABLA PRODUCTOS_VENDIDO

ALTER TABLE [SCH_FARMACIA].[T_PRODUCTOS_VENDIDO] WITH NOCHECK
ADD CONSTRAINT [FK_T_PRODUCTOS_VENDIDO_T_TIPOS_PRODUCTO_CODIGO_TIPO_PRODUCTO] FOREIGN KEY(CODIGO_TIPO_PRODUCTO)
	REFERENCES [SCH_FARMACIA].[T_TIPOS_PRODUCTO] (CODIGO_TIPO_PRODUCTO)

GO

ALTER TABLE [SCH_FARMACIA].[T_PRODUCTOS_VENDIDO] CHECK CONSTRAINT [FK_T_PRODUCTOS_VENDIDO_T_TIPOS_PRODUCTO_CODIGO_TIPO_PRODUCTO]

GO

--TABLA FACTURA

CREATE TABLE [SCH_FARMACIA].[T_FACTURA]
(
	[ID_FACTURA] SMALLINT NOT NULL IDENTITY(1,1),
	[CODIGO_FACTURA] INT NOT NULL,
	[FECHA_COMPRA] DATETIME NOT NULL,
	[ID_CLIENTE] SMALLINT NOT NULL,
	[METODO_PAGO] NVARCHAR(30) NOT NULL,
	[SUBTOTAL] MONEY NOT NULL,
	[IVA] MONEY NOT NULL,
	[TOTAL] MONEY NOT NULL,
	[CODIGO_PRODUCTO] INT NOT NULL,
	[CANTIDAD_PRODUCTOS] INT NOT NULL

	CONSTRAINT [PK_T_FACTURA] PRIMARY KEY CLUSTERED
	(
		[ID_FACTURA]
	)
)

GO

--FOREING KEYS TABLA FACTURA

ALTER TABLE [SCH_FARMACIA].[T_FACTURA] WITH NOCHECK
ADD CONSTRAINT [FK_T_FACTURA_T_REGISTRO_CLIENTE_ID_CLIENTE] FOREIGN KEY(ID_CLIENTE)
	REFERENCES [SCH_ADMINISTRATIVO].[T_REGISTRO_CLIENTE] (ID_CLIENTE)

GO

ALTER TABLE [SCH_FARMACIA].[T_FACTURA] CHECK CONSTRAINT [FK_T_FACTURA_T_REGISTRO_CLIENTE_ID_CLIENTE]

GO

ALTER TABLE [SCH_FARMACIA].[T_FACTURA] WITH NOCHECK
ADD CONSTRAINT [FK_T_FACTURA_T_PRODUCTOS_VENDIDO_CODIGO_PRODUCTO] FOREIGN KEY(CODIGO_PRODUCTO)
	REFERENCES [SCH_FARMACIA].[T_PRODUCTOS_VENDIDO] (CODIGO_PRODUCTO)

GO

ALTER TABLE [SCH_FARMACIA].[T_FACTURA] CHECK CONSTRAINT [FK_T_FACTURA_T_PRODUCTOS_VENDIDO_CODIGO_PRODUCTO]

GO

--TABLA FACTURAS_USUARIO

CREATE TABLE [SCH_FARMACIA].[T_FACTURAS_USUARIO]
(
	[ID_FACTURAS_USUARIO] SMALLINT NOT NULL IDENTITY(1,1),
	[ID_FACTURA] SMALLINT NOT NULL,
	[ID_CLIENTE] SMALLINT NOT NULL

	CONSTRAINT [PK_T_FACTURAS_USUARIO] PRIMARY KEY CLUSTERED
	(
		[ID_FACTURAS_USUARIO]
	)
)

GO

--FOREING KEYS TABLA FACTURAS_USUARIO

ALTER TABLE [SCH_FARMACIA].[T_FACTURAS_USUARIO] WITH NOCHECK
ADD CONSTRAINT [FK_T_FACTURAS_USUARIO_T_FACTURA_ID_FACTURA] FOREIGN KEY(ID_FACTURA)
	REFERENCES [SCH_FARMACIA].[T_FACTURA] (ID_FACTURA)

GO

ALTER TABLE [SCH_FARMACIA].[T_FACTURAS_USUARIO] CHECK CONSTRAINT [FK_T_FACTURA_T_FACTURA_ID_FACTURA]

GO

ALTER TABLE [SCH_FARMACIA].[T_FACTURAS_USUARIO] WITH NOCHECK
ADD CONSTRAINT [FK_T_FACTURAS_USUARIO_T_REGISTRO_CLIENTE_ID_CLIENTE] FOREIGN KEY(ID_CLIENTE)
	REFERENCES [SCH_ADMINISTRATIVO].[T_REGISTRO_CLIENTE] (ID_CLIENTE)

GO

ALTER TABLE [SCH_FARMACIA].[T_FACTURAS_USUARIO] CHECK CONSTRAINT [FK_T_FACTURA_T_REGISTRO_CLIENTE_ID_CLIENTE]

GO

select * from [SCH_ADMINISTRATIVO].[T_REGISTRO_USUARIO];

Insert into [SCH_ADMINISTRATIVO].[T_REGISTRO_USUARIO] (IDENTIFICACION_USUARIO,NOMBRE_COMPLETO,CORREO,TIPO_USUARIO,ESTADO,CONTRASEŅA) 
values ('123456', 'Marco Cordoba Quesada', 'administrador@amcr.com', 'Administrador','Activo' ,'admin');