IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Suppliers] (
    [Id] uniqueidentifier NOT NULL,
    [Name] varchar(200) NOT NULL,
    [Document] varchar(14) NOT NULL,
    [SupplierType] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Suppliers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Andresses] (
    [Id] uniqueidentifier NOT NULL,
    [SupplierId] uniqueidentifier NOT NULL,
    [Street] varchar(200) NULL,
    [Number] varchar(50) NULL,
    [Complement] varchar(200) NULL,
    [PostCode] varchar(8) NULL,
    [Neighborhood] varchar(100) NULL,
    [City] varchar(100) NULL,
    [State] varchar(50) NULL,
    CONSTRAINT [PK_Andresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Andresses_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [SupplierId] uniqueidentifier NOT NULL,
    [Name] varchar(200) NOT NULL,
    [Description] varchar(1000) NOT NULL,
    [Image] varchar(100) NOT NULL,
    [Value] decimal(18,2) NOT NULL,
    [Date] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE UNIQUE INDEX [IX_Andresses_SupplierId] ON [Andresses] ([SupplierId]);

GO

CREATE INDEX [IX_Products_SupplierId] ON [Products] ([SupplierId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200616033256_tables', N'3.1.5');

GO

