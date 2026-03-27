using DreamHome_Mobile_SQLite.Contexts;
using DreamHome_Mobile_SQLite.Data;
using DreamHome_Mobile_SQLite.Data.Repositories;
using DreamHome_Mobile_SQLite.Helpers;
using DreamHome_Mobile_SQLite.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DreamHome_Mobile_SQLite
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // EF Core + SQLite registration
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "dreamhome.db");
            builder.Services.AddDbContextFactory<DreamHomeDbContext>(o =>
                  o.UseSqlite($"Filename={dbPath}"));

            // Add services to the container.
            builder.Services.AddSingleton<IDreamHomeService, DreamHomeService>();
            builder.Services.AddSingleton<IDreamHomeRepository, DreamHomeRepository>();
            builder.Services.AddSingleton<IBranchContext, BranchContext>();
            builder.Services.AddSingleton<AppShell>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // Create/Seed database
            using (var scope = app.Services.CreateScope())
            {
                var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DreamHomeDbContext>>();
                using var db = factory.CreateDbContext();
                DbInitializer.Initialize(db);
            }

            ServiceHelper.Initialize(app.Services);

            return app;
        }
    }
}
