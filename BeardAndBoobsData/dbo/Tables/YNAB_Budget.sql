CREATE TABLE [dbo].[YNAB_Budget] (
    [Month]                   DATETIME       NULL,
    [Category Group/Category] NVARCHAR (255) NULL,
    [Category Group]          NVARCHAR (255) NULL,
    [Category]                NVARCHAR (255) NULL,
    [Budgeted]                MONEY          NULL,
    [Activity]                MONEY          NULL,
    [Available]               MONEY          NULL
);

