
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/13/2018 13:53:47
-- Generated from EDMX file: F:\EPU Save\Git\dnlt_project\DaoNguyenLighting\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DNLT];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Image_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Image] DROP CONSTRAINT [FK_Image_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_Collection]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Collection];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Collection]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Collection];
GO
IF OBJECT_ID(N'[dbo].[Image]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Image];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Collections'
CREATE TABLE [dbo].[Collections] (
    [CollectionID] int  NOT NULL,
    [CollectionName] nvarchar(50)  NULL
);
GO

-- Creating table 'Images'
CREATE TABLE [dbo].[Images] (
    [ImageID] int  NOT NULL,
    [ImageTitle] nvarchar(50)  NULL,
    [ImagePath] nvarchar(100)  NULL,
    [ProductID] int  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [ProductID] int  NOT NULL,
    [ProductName] nvarchar(50)  NULL,
    [ProductPrice] int  NULL,
    [ProductContent] nvarchar(50)  NULL,
    [ProductAmount] int  NULL,
    [CollectionID] int  NULL,
    [Views] int  NULL,
    [Date] datetime  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL,
    [Name] nvarchar(50)  NULL,
    [Phone] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CollectionID] in table 'Collections'
ALTER TABLE [dbo].[Collections]
ADD CONSTRAINT [PK_Collections]
    PRIMARY KEY CLUSTERED ([CollectionID] ASC);
GO

-- Creating primary key on [ImageID] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [PK_Images]
    PRIMARY KEY CLUSTERED ([ImageID] ASC);
GO

-- Creating primary key on [ProductID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([ProductID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [UserID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CollectionID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Product_Collection]
    FOREIGN KEY ([CollectionID])
    REFERENCES [dbo].[Collections]
        ([CollectionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_Collection'
CREATE INDEX [IX_FK_Product_Collection]
ON [dbo].[Products]
    ([CollectionID]);
GO

-- Creating foreign key on [ProductID] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [FK_Image_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Image_Product'
CREATE INDEX [IX_FK_Image_Product]
ON [dbo].[Images]
    ([ProductID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------