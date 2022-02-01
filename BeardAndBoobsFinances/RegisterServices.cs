namespace BeardAndBoobsFinances
{
    public static class RegisterServices
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            builder.Services.AddTransient<IBudgetData, BudgetData>();
            builder.Services.AddTransient<IBudgetDataColumns, BudgetDataColumns>();
            builder.Services.AddTransient<IBudgetDataSummary, BudgetDataSummary>();
            builder.Services.AddTransient<IBudgetSummaryColumns, BudgetSummaryColumns>();
            builder.Services.AddHttpClient();
        }
    }
}
