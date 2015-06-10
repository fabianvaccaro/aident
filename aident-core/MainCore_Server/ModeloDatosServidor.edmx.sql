
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/09/2015 19:06:33
-- Generated from EDMX file: C:\Users\Jaime\Source\Repos\AiDent\aident-core\MainCore_Server\ModeloDatosServidor.edmx
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

IF OBJECT_ID(N'[dbo].[FK_MpatCiclosEvaluacion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CiclosEvaluacionSet] DROP CONSTRAINT [FK_MpatCiclosEvaluacion];
GO
IF OBJECT_ID(N'[dbo].[FK_ProcedimientoClinicoMpat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProcedimientoClinicoSet] DROP CONSTRAINT [FK_ProcedimientoClinicoMpat];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CiclosEvaluacionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CiclosEvaluacionSet];
GO
IF OBJECT_ID(N'[dbo].[MpatSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MpatSet];
GO
IF OBJECT_ID(N'[dbo].[ProcedimientoClinicoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProcedimientoClinicoSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CiclosEvaluacionSet'
CREATE TABLE [dbo].[CiclosEvaluacionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [idMpat] int  NOT NULL,
    [numeroCiclos] int  NOT NULL,
    [orden] int  NOT NULL
);
GO

-- Creating table 'MpatSet'
CREATE TABLE [dbo].[MpatSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nombre] nvarchar(max)  NOT NULL,
    [ciclosMasticatorios] int  NOT NULL,
    [Estado] int  NOT NULL,
    [DescripcionTestFood] nvarchar(max)  NOT NULL,
    [UsuarioSetId] int  NOT NULL
);
GO

-- Creating table 'ProcedimientoClinicoSet'
CREATE TABLE [dbo].[ProcedimientoClinicoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [orden] int  NOT NULL,
    [descripcion] nvarchar(max)  NOT NULL,
    [idMpat] int  NOT NULL
);
GO

-- Creating table 'UsuarioSet'
CREATE TABLE [dbo].[UsuarioSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Usuario] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CaracteristicasExtraidasSet'
CREATE TABLE [dbo].[CaracteristicasExtraidasSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [idMuestra] int  NOT NULL,
    [codigoCaracteristica] int  NOT NULL,
    [Valor] float  NOT NULL
);
GO

-- Creating table 'MuestraSet'
CREATE TABLE [dbo].[MuestraSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CiclosMasticatorios] int  NOT NULL,
    [idPaciente] int  NOT NULL,
    [idExperimento] int  NOT NULL
);
GO

-- Creating table 'ExperimentoSet'
CREATE TABLE [dbo].[ExperimentoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [idUsuario] int  NOT NULL,
    [idMpat] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CiclosEvaluacionSet'
ALTER TABLE [dbo].[CiclosEvaluacionSet]
ADD CONSTRAINT [PK_CiclosEvaluacionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MpatSet'
ALTER TABLE [dbo].[MpatSet]
ADD CONSTRAINT [PK_MpatSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProcedimientoClinicoSet'
ALTER TABLE [dbo].[ProcedimientoClinicoSet]
ADD CONSTRAINT [PK_ProcedimientoClinicoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsuarioSet'
ALTER TABLE [dbo].[UsuarioSet]
ADD CONSTRAINT [PK_UsuarioSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CaracteristicasExtraidasSet'
ALTER TABLE [dbo].[CaracteristicasExtraidasSet]
ADD CONSTRAINT [PK_CaracteristicasExtraidasSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MuestraSet'
ALTER TABLE [dbo].[MuestraSet]
ADD CONSTRAINT [PK_MuestraSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExperimentoSet'
ALTER TABLE [dbo].[ExperimentoSet]
ADD CONSTRAINT [PK_ExperimentoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [idMpat] in table 'CiclosEvaluacionSet'
ALTER TABLE [dbo].[CiclosEvaluacionSet]
ADD CONSTRAINT [FK_MpatCiclosEvaluacion]
    FOREIGN KEY ([idMpat])
    REFERENCES [dbo].[MpatSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MpatCiclosEvaluacion'
CREATE INDEX [IX_FK_MpatCiclosEvaluacion]
ON [dbo].[CiclosEvaluacionSet]
    ([idMpat]);
GO

-- Creating foreign key on [idMpat] in table 'ProcedimientoClinicoSet'
ALTER TABLE [dbo].[ProcedimientoClinicoSet]
ADD CONSTRAINT [FK_ProcedimientoClinicoMpat]
    FOREIGN KEY ([idMpat])
    REFERENCES [dbo].[MpatSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcedimientoClinicoMpat'
CREATE INDEX [IX_FK_ProcedimientoClinicoMpat]
ON [dbo].[ProcedimientoClinicoSet]
    ([idMpat]);
GO

-- Creating foreign key on [idMuestra] in table 'CaracteristicasExtraidasSet'
ALTER TABLE [dbo].[CaracteristicasExtraidasSet]
ADD CONSTRAINT [FK_MuestraSetCaracteristicasExtraidasSet]
    FOREIGN KEY ([idMuestra])
    REFERENCES [dbo].[MuestraSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MuestraSetCaracteristicasExtraidasSet'
CREATE INDEX [IX_FK_MuestraSetCaracteristicasExtraidasSet]
ON [dbo].[CaracteristicasExtraidasSet]
    ([idMuestra]);
GO

-- Creating foreign key on [idUsuario] in table 'ExperimentoSet'
ALTER TABLE [dbo].[ExperimentoSet]
ADD CONSTRAINT [FK_ExperimentoSetUsuarioSet]
    FOREIGN KEY ([idUsuario])
    REFERENCES [dbo].[UsuarioSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ExperimentoSetUsuarioSet'
CREATE INDEX [IX_FK_ExperimentoSetUsuarioSet]
ON [dbo].[ExperimentoSet]
    ([idUsuario]);
GO

-- Creating foreign key on [idExperimento] in table 'MuestraSet'
ALTER TABLE [dbo].[MuestraSet]
ADD CONSTRAINT [FK_ExperimentoSetMuestraSet]
    FOREIGN KEY ([idExperimento])
    REFERENCES [dbo].[ExperimentoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ExperimentoSetMuestraSet'
CREATE INDEX [IX_FK_ExperimentoSetMuestraSet]
ON [dbo].[MuestraSet]
    ([idExperimento]);
GO

-- Creating foreign key on [idMpat] in table 'ExperimentoSet'
ALTER TABLE [dbo].[ExperimentoSet]
ADD CONSTRAINT [FK_ExperimentoSetMpatSet]
    FOREIGN KEY ([idMpat])
    REFERENCES [dbo].[MpatSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ExperimentoSetMpatSet'
CREATE INDEX [IX_FK_ExperimentoSetMpatSet]
ON [dbo].[ExperimentoSet]
    ([idMpat]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------