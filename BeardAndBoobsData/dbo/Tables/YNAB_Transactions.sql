CREATE TABLE [dbo].[YNAB_Transactions] (
    [Account]                 NVARCHAR (255) NULL,
    [Flag]                    NVARCHAR (255) NULL,
    [Date]                    DATETIME       NULL,
    [Payee]                   NVARCHAR (255) NULL,
    [Category Group/Category] NVARCHAR (255) NULL,
    [Category Group]          NVARCHAR (255) NULL,
    [Category]                NVARCHAR (255) NULL,
    [Memo]                    NVARCHAR (255) NULL,
    [Outflow]                 MONEY          NULL,
    [Inflow]                  MONEY          NULL,
    [Cleared]                 NVARCHAR (255) NULL
);

