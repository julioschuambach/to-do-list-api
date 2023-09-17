CREATE DATABASE [ToDoListDatabase]
GO

USE [ToDoListDatabase]
GO

CREATE TABLE [ToDos]
(
    [Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    [Description] NVARCHAR(100) NOT NULL,
    [Done] BIT NOT NULL DEFAULT(0),
    [ExpectedCompletionDate] DATETIME NULL,
    [CompletionDate] DATETIME NULL,
    [CreatedDate] DATETIME NOT NULL,
    [LastUpdatedDate] DATETIME NOT NULL
)