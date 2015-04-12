
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/12/2015 21:40:22
-- Generated from EDMX file: C:\Users\Jaime\Source\Repos\AiDent\aident-core\Metodos\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [perceptodent_data];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_MuestraCaracteristicaMedida]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CaracteristicaMedidaSet] DROP CONSTRAINT [FK_MuestraCaracteristicaMedida];
GO
IF OBJECT_ID(N'[dbo].[FK_MuestraPaciente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PacienteSet] DROP CONSTRAINT [FK_MuestraPaciente];
GO
IF OBJECT_ID(N'[dbo].[FK_MpatTestFood]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MpatSet] DROP CONSTRAINT [FK_MpatTestFood];
GO
IF OBJECT_ID(N'[dbo].[FK_HistoriaClinicaPaciente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PacienteSet] DROP CONSTRAINT [FK_HistoriaClinicaPaciente];
GO
IF OBJECT_ID(N'[dbo].[FK_MpatExperimento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MpatSet] DROP CONSTRAINT [FK_MpatExperimento];
GO
IF OBJECT_ID(N'[dbo].[FK_ExperimentoPaciente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PacienteSet] DROP CONSTRAINT [FK_ExperimentoPaciente];
GO
IF OBJECT_ID(N'[dbo].[FK_TestFoodTipoTestFood]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestFoodSet] DROP CONSTRAINT [FK_TestFoodTipoTestFood];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CaracteristicaMedidaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CaracteristicaMedidaSet];
GO
IF OBJECT_ID(N'[dbo].[ExperimentoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExperimentoSet];
GO
IF OBJECT_ID(N'[dbo].[HistoriaClinicaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HistoriaClinicaSet];
GO
IF OBJECT_ID(N'[dbo].[MpatSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MpatSet];
GO
IF OBJECT_ID(N'[dbo].[MuestraSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MuestraSet];
GO
IF OBJECT_ID(N'[dbo].[PacienteSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PacienteSet];
GO
IF OBJECT_ID(N'[dbo].[ProcedimientoClinicoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProcedimientoClinicoSet];
GO
IF OBJECT_ID(N'[dbo].[PruebasSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PruebasSet];
GO
IF OBJECT_ID(N'[dbo].[TestFoodSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestFoodSet];
GO
IF OBJECT_ID(N'[dbo].[TipoTestFoodSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TipoTestFoodSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CaracteristicaMedidaSet'
CREATE TABLE [dbo].[CaracteristicaMedidaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Valor] nvarchar(max)  NOT NULL,
    [Muestra_Id] int  NOT NULL
);
GO

-- Creating table 'ExperimentoSet'
CREATE TABLE [dbo].[ExperimentoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Codigo] nvarchar(max)  NOT NULL,
    [NumeroPacientes] int  NOT NULL,
    [idMpat] int  NOT NULL
);
GO

-- Creating table 'HistoriaClinicaSet'
CREATE TABLE [dbo].[HistoriaClinicaSet] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'MpatSet'
CREATE TABLE [dbo].[MpatSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [idTestFood] int  NOT NULL,
    [ciclosMasticatorios] int  NOT NULL,
    [ciclosEvaluacion] int  NOT NULL,
    [procedimiento] nvarchar(max)  NOT NULL,
    [Estado] int  NOT NULL,
    [nombre] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MuestraSet'
CREATE TABLE [dbo].[MuestraSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Ciclos] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PacienteSet'
CREATE TABLE [dbo].[PacienteSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Muestra_Id] int  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [DNI] nvarchar(max)  NOT NULL,
    [Ubicacion] nvarchar(max)  NOT NULL,
    [Sexo] nvarchar(max)  NOT NULL,
    [IdExperimento] int  NOT NULL,
    [idHistoriaClinica] int  NOT NULL
);
GO

-- Creating table 'ProcedimientoClinicoSet'
CREATE TABLE [dbo].[ProcedimientoClinicoSet] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'PruebasSet'
CREATE TABLE [dbo].[PruebasSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Valor1] nvarchar(max)  NOT NULL,
    [Valor2] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TestFoodSet'
CREATE TABLE [dbo].[TestFoodSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(max)  NOT NULL,
    [caracteristicaMonitorzadas] nvarchar(max)  NOT NULL,
    [IdTipo] int  NOT NULL
);
GO

-- Creating table 'TipoTestFoodSet'
CREATE TABLE [dbo].[TipoTestFoodSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [descripcion] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CaracteristicaMedidaSet'
ALTER TABLE [dbo].[CaracteristicaMedidaSet]
ADD CONSTRAINT [PK_CaracteristicaMedidaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExperimentoSet'
ALTER TABLE [dbo].[ExperimentoSet]
ADD CONSTRAINT [PK_ExperimentoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HistoriaClinicaSet'
ALTER TABLE [dbo].[HistoriaClinicaSet]
ADD CONSTRAINT [PK_HistoriaClinicaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MpatSet'
ALTER TABLE [dbo].[MpatSet]
ADD CONSTRAINT [PK_MpatSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MuestraSet'
ALTER TABLE [dbo].[MuestraSet]
ADD CONSTRAINT [PK_MuestraSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PacienteSet'
ALTER TABLE [dbo].[PacienteSet]
ADD CONSTRAINT [PK_PacienteSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProcedimientoClinicoSet'
ALTER TABLE [dbo].[ProcedimientoClinicoSet]
ADD CONSTRAINT [PK_ProcedimientoClinicoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PruebasSet'
ALTER TABLE [dbo].[PruebasSet]
ADD CONSTRAINT [PK_PruebasSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TestFoodSet'
ALTER TABLE [dbo].[TestFoodSet]
ADD CONSTRAINT [PK_TestFoodSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TipoTestFoodSet'
ALTER TABLE [dbo].[TipoTestFoodSet]
ADD CONSTRAINT [PK_TipoTestFoodSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Muestra_Id] in table 'CaracteristicaMedidaSet'
ALTER TABLE [dbo].[CaracteristicaMedidaSet]
ADD CONSTRAINT [FK_MuestraCaracteristicaMedida]
    FOREIGN KEY ([Muestra_Id])
    REFERENCES [dbo].[MuestraSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MuestraCaracteristicaMedida'
CREATE INDEX [IX_FK_MuestraCaracteristicaMedida]
ON [dbo].[CaracteristicaMedidaSet]
    ([Muestra_Id]);
GO

-- Creating foreign key on [Muestra_Id] in table 'PacienteSet'
ALTER TABLE [dbo].[PacienteSet]
ADD CONSTRAINT [FK_MuestraPaciente]
    FOREIGN KEY ([Muestra_Id])
    REFERENCES [dbo].[MuestraSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MuestraPaciente'
CREATE INDEX [IX_FK_MuestraPaciente]
ON [dbo].[PacienteSet]
    ([Muestra_Id]);
GO

-- Creating foreign key on [idTestFood] in table 'MpatSet'
ALTER TABLE [dbo].[MpatSet]
ADD CONSTRAINT [FK_MpatTestFood]
    FOREIGN KEY ([idTestFood])
    REFERENCES [dbo].[TestFoodSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MpatTestFood'
CREATE INDEX [IX_FK_MpatTestFood]
ON [dbo].[MpatSet]
    ([idTestFood]);
GO

-- Creating foreign key on [idHistoriaClinica] in table 'PacienteSet'
ALTER TABLE [dbo].[PacienteSet]
ADD CONSTRAINT [FK_HistoriaClinicaPaciente]
    FOREIGN KEY ([idHistoriaClinica])
    REFERENCES [dbo].[HistoriaClinicaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HistoriaClinicaPaciente'
CREATE INDEX [IX_FK_HistoriaClinicaPaciente]
ON [dbo].[PacienteSet]
    ([idHistoriaClinica]);
GO

-- Creating foreign key on [IdTipo] in table 'TestFoodSet'
ALTER TABLE [dbo].[TestFoodSet]
ADD CONSTRAINT [FK_TestFoodTipoTestFood]
    FOREIGN KEY ([IdTipo])
    REFERENCES [dbo].[TipoTestFoodSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestFoodTipoTestFood'
CREATE INDEX [IX_FK_TestFoodTipoTestFood]
ON [dbo].[TestFoodSet]
    ([IdTipo]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------