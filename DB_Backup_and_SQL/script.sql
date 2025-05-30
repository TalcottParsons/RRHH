USE [master]
GO
/****** Object:  Database [RRHH]    Script Date: 11/19/2024 12:40:46 AM ******/
CREATE DATABASE [RRHH]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RRHH', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\RRHH.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RRHH_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\RRHH_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [RRHH] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RRHH].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RRHH] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RRHH] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RRHH] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RRHH] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RRHH] SET ARITHABORT OFF 
GO
ALTER DATABASE [RRHH] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [RRHH] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RRHH] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RRHH] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RRHH] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RRHH] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RRHH] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RRHH] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RRHH] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RRHH] SET  ENABLE_BROKER 
GO
ALTER DATABASE [RRHH] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RRHH] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RRHH] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RRHH] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RRHH] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RRHH] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RRHH] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RRHH] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RRHH] SET  MULTI_USER 
GO
ALTER DATABASE [RRHH] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RRHH] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RRHH] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RRHH] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RRHH] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RRHH] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [RRHH] SET QUERY_STORE = ON
GO
ALTER DATABASE [RRHH] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [RRHH]
GO
/****** Object:  Table [dbo].[Colaboradores]    Script Date: 11/19/2024 12:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colaboradores](
	[ColaboradorID] [int] IDENTITY(1,1) NOT NULL,
	[NombreCompleto] [nvarchar](100) NOT NULL,
	[Telefono] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Departamento] [nvarchar](50) NOT NULL,
	[Objetivo] [nvarchar](max) NULL,
	[Foto] [varbinary](max) NULL,
	[EstadoActivo] [bit] NULL,
	[FechaIngreso] [datetime] NULL,
	[Puesto] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ColaboradorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Competencias]    Script Date: 11/19/2024 12:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Competencias](
	[CompetenciaID] [int] IDENTITY(1,1) NOT NULL,
	[ColaboradorID] [int] NOT NULL,
	[Competencia] [nvarchar](100) NOT NULL,
	[Dominio] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CompetenciaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExperienciaProfesional]    Script Date: 11/19/2024 12:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExperienciaProfesional](
	[ExperienciaID] [int] IDENTITY(1,1) NOT NULL,
	[ColaboradorID] [int] NOT NULL,
	[Puesto] [nvarchar](100) NOT NULL,
	[Empresa] [nvarchar](100) NOT NULL,
	[AñoInicio] [int] NOT NULL,
	[AñoFin] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ExperienciaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormacionAcademica]    Script Date: 11/19/2024 12:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormacionAcademica](
	[FormacionID] [int] IDENTITY(1,1) NOT NULL,
	[ColaboradorID] [int] NOT NULL,
	[Institucion] [nvarchar](100) NOT NULL,
	[Titulo] [nvarchar](100) NOT NULL,
	[AñoInicio] [int] NOT NULL,
	[AñoFin] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FormacionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Habilidades]    Script Date: 11/19/2024 12:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Habilidades](
	[HabilidadID] [int] IDENTITY(1,1) NOT NULL,
	[ColaboradorID] [int] NOT NULL,
	[Habilidad] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[HabilidadID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistorialActividades]    Script Date: 11/19/2024 12:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialActividades](
	[ActividadID] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioID] [int] NOT NULL,
	[Accion] [nvarchar](255) NOT NULL,
	[FechaActividad] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ActividadID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Referencias]    Script Date: 11/19/2024 12:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Referencias](
	[ReferenciaID] [int] IDENTITY(1,1) NOT NULL,
	[ColaboradorID] [int] NOT NULL,
	[TipoReferencia] [nvarchar](20) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Telefono] [nvarchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReferenciaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 11/19/2024 12:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[NombreUsuario] [nvarchar](50) NOT NULL,
	[Contraseña] [nvarchar](255) NOT NULL,
	[Rol] [nvarchar](20) NOT NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NombreUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Colaboradores] ADD  DEFAULT ((1)) FOR [EstadoActivo]
GO
ALTER TABLE [dbo].[Colaboradores] ADD  DEFAULT (getdate()) FOR [FechaIngreso]
GO
ALTER TABLE [dbo].[Colaboradores] ADD  DEFAULT ('Sin Especificar') FOR [Puesto]
GO
ALTER TABLE [dbo].[HistorialActividades] ADD  DEFAULT (getdate()) FOR [FechaActividad]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Competencias]  WITH CHECK ADD FOREIGN KEY([ColaboradorID])
REFERENCES [dbo].[Colaboradores] ([ColaboradorID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ExperienciaProfesional]  WITH CHECK ADD FOREIGN KEY([ColaboradorID])
REFERENCES [dbo].[Colaboradores] ([ColaboradorID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FormacionAcademica]  WITH CHECK ADD FOREIGN KEY([ColaboradorID])
REFERENCES [dbo].[Colaboradores] ([ColaboradorID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Habilidades]  WITH CHECK ADD FOREIGN KEY([ColaboradorID])
REFERENCES [dbo].[Colaboradores] ([ColaboradorID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HistorialActividades]  WITH CHECK ADD FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[Referencias]  WITH CHECK ADD FOREIGN KEY([ColaboradorID])
REFERENCES [dbo].[Colaboradores] ([ColaboradorID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Referencias]  WITH CHECK ADD CHECK  (([TipoReferencia]='Laboral' OR [TipoReferencia]='Personal'))
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD CHECK  (([Rol]='Operador' OR [Rol]='Admin'))
GO
/****** Object:  StoredProcedure [dbo].[BuscarColaborador]    Script Date: 11/19/2024 12:40:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarColaborador]
    @Nombre NVARCHAR(100) = NULL, -- Nombre parcial o completo del colaborador
    @ID INT = NULL -- ID del colaborador
AS
BEGIN
    SELECT c.ColaboradorID, c.NombreCompleto, c.Departamento, c.Puesto, c.EstadoActivo, c.FechaIngreso
    FROM Colaboradores c
    WHERE (@Nombre IS NULL OR c.NombreCompleto LIKE '%' + @Nombre + '%')
      AND (@ID IS NULL OR c.ColaboradorID = @ID);
END;
GO
/****** Object:  StoredProcedure [dbo].[BuscarGeneral]    Script Date: 11/19/2024 12:40:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[BuscarGeneral]
    @Campo NVARCHAR(50),
    @Valor NVARCHAR(100)
AS
BEGIN
    -- Búsqueda por Colaborador
    IF @Campo = 'Colaborador'
    BEGIN
        SELECT c.ColaboradorID, c.NombreCompleto, c.Departamento, c.Puesto, c.EstadoActivo, c.FechaIngreso
        FROM Colaboradores c
        WHERE c.NombreCompleto LIKE '%' + @Valor + '%';
    END
    -- Búsqueda por Habilidad
    ELSE IF @Campo = 'Habilidad'
    BEGIN
        SELECT c.ColaboradorID, c.NombreCompleto, h.Habilidad
        FROM Colaboradores c
        INNER JOIN Habilidades h ON c.ColaboradorID = h.ColaboradorID
        WHERE h.Habilidad LIKE '%' + @Valor + '%';
    END
    -- Búsqueda por Competencia
    ELSE IF @Campo = 'Competencia'
    BEGIN
        SELECT c.ColaboradorID, c.NombreCompleto, comp.Competencia, comp.Dominio
        FROM Colaboradores c
        INNER JOIN Competencias comp ON c.ColaboradorID = comp.ColaboradorID
        WHERE comp.Competencia LIKE '%' + @Valor + '%';
    END
    -- Búsqueda por Departamento
    ELSE IF @Campo = 'Departamento'
    BEGIN
        SELECT c.ColaboradorID, c.NombreCompleto, c.Departamento, c.Puesto
        FROM Colaboradores c
        WHERE c.Departamento LIKE '%' + @Valor + '%';
    END
    ELSE
    BEGIN
        PRINT 'Campo no válido para búsqueda.';
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[BuscarPorCompetencia]    Script Date: 11/19/2024 12:40:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarPorCompetencia]
    @Competencia NVARCHAR(100) -- Competencia específica a buscar
AS
BEGIN
    SELECT c.ColaboradorID, c.NombreCompleto, comp.Competencia, comp.Dominio
    FROM Colaboradores c
    INNER JOIN Competencias comp ON c.ColaboradorID = comp.ColaboradorID
    WHERE comp.Competencia LIKE '%' + @Competencia + '%';
END;
GO
/****** Object:  StoredProcedure [dbo].[BuscarPorHabilidad]    Script Date: 11/19/2024 12:40:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarPorHabilidad]
    @Habilidad NVARCHAR(100) -- Habilidad específica a buscar
AS
BEGIN
    SELECT c.ColaboradorID, c.NombreCompleto, h.Habilidad
    FROM Colaboradores c
    INNER JOIN Habilidades h ON c.ColaboradorID = h.ColaboradorID
    WHERE h.Habilidad LIKE '%' + @Habilidad + '%';
END;
GO
USE [master]
GO
ALTER DATABASE [RRHH] SET  READ_WRITE 
GO
