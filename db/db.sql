/****** Object:  Database [db_nexos]    Script Date: 17/01/2021 11:44:58 p. m. ******/
CREATE DATABASE [db_nexos]
GO
USE [db_nexos]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17/01/2021 11:44:59 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctores]    Script Date: 17/01/2021 11:44:59 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctores](
	[Id] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Apellido] [nvarchar](max) NOT NULL,
	[CodigoProfesional] [nvarchar](max) NOT NULL,
	[Especialidad] [nvarchar](max) NOT NULL,
	[Hospital] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Doctores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctoresPacientes]    Script Date: 17/01/2021 11:44:59 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctoresPacientes](
	[DoctoresId] [uniqueidentifier] NOT NULL,
	[PacientesId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_DoctoresPacientes] PRIMARY KEY CLUSTERED 
(
	[DoctoresId] ASC,
	[PacientesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pacientes]    Script Date: 17/01/2021 11:44:59 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pacientes](
	[Id] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Apellido] [nvarchar](max) NOT NULL,
	[SeguroSocial] [nvarchar](max) NOT NULL,
	[CodigoPostal] [nvarchar](max) NULL,
	[Telefono] [nvarchar](max) NULL,
 CONSTRAINT [PK_Pacientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_DoctoresPacientes_PacientesId]    Script Date: 17/01/2021 11:44:59 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_DoctoresPacientes_PacientesId] ON [dbo].[DoctoresPacientes]
(
	[PacientesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DoctoresPacientes]  WITH CHECK ADD  CONSTRAINT [FK_DoctoresPacientes_Doctores_DoctoresId] FOREIGN KEY([DoctoresId])
REFERENCES [dbo].[Doctores] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DoctoresPacientes] CHECK CONSTRAINT [FK_DoctoresPacientes_Doctores_DoctoresId]
GO
ALTER TABLE [dbo].[DoctoresPacientes]  WITH CHECK ADD  CONSTRAINT [FK_DoctoresPacientes_Pacientes_PacientesId] FOREIGN KEY([PacientesId])
REFERENCES [dbo].[Pacientes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DoctoresPacientes] CHECK CONSTRAINT [FK_DoctoresPacientes_Pacientes_PacientesId]
GO
