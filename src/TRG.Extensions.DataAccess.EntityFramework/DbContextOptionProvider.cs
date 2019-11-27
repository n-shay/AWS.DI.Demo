//using System;
//using Microsoft.EntityFrameworkCore;

//namespace TRG.Extensions.DataAccess.EntityFramework
//{
//    public class DbContextOptionProvider : IDbContextOptionProvider
//    {
//        private readonly object mutex = new object();
//        private readonly ISettingsProvider settingsProvider;
//        private DbContextOptionsBuilder dbContextOptionsBuilder;
//        private readonly ILoggerFactory loggerFactory;

//        public DbContextOptionProvider(ISettingsProvider settingsProvider, ILoggerFactory loggerFactory)
//        {
//            this.settingsProvider = settingsProvider;
//            this.loggerFactory = loggerFactory;
//        }

//        public DbContextOptions Get(string migrationScope = null)
//        {
//            if (this.dbContextOptionsBuilder == null)
//            {
//                lock (this.mutex)
//                {
//                    if (this.dbContextOptionsBuilder == null)
//                    {
//                        this.dbContextOptionsBuilder = new DbContextOptionsBuilder();
//                        var defaultConnectionStringName =
//                            this.settingsProvider.Get<string>(Settings.Data.DefaultConnectionString);
//                        var enableLogging =
//                            this.settingsProvider.Get<bool>(Settings.Data.EnableLogging);
//                        var connectionString = this.settingsProvider.GetConnectionString(defaultConnectionStringName);

//                        if (enableLogging)
//                            this.dbContextOptionsBuilder.UseLoggerFactory(this.loggerFactory);

//                        switch (this.settingsProvider.GetEnvironment())
//                        {
//                            case EnvironmentType.Development:
//                                var useInMemoryData = this.settingsProvider.Get<bool>(Settings.Data.UseInMemoryData);
//                                if (useInMemoryData)
//                                    this.dbContextOptionsBuilder.UseInMemoryDatabase(defaultConnectionStringName);
//                                else
//                                    this.dbContextOptionsBuilder.UseSqlServer(
//                                        connectionString,
//                                        ConfigureSqlServerOptions(migrationScope));
//                                break;
//                            case EnvironmentType.Staging:
//                                this.dbContextOptionsBuilder.UseSqlServer(
//                                    connectionString,
//                                    ConfigureSqlServerOptions(migrationScope));
//                                break;
//                            case EnvironmentType.Production:
//                                this.dbContextOptionsBuilder.UseSqlServer(
//                                    connectionString,
//                                    ConfigureSqlServerOptions(migrationScope));
//                                break;
//                            default:
//                                throw new ArgumentOutOfRangeException();
//                        }
//                    }
//                }
//            }

//            return this.dbContextOptionsBuilder.Options;
//        }

//        private static Action<SqlServerDbContextOptionsBuilder> ConfigureSqlServerOptions(string migrationScope)
//        {
//            return options =>
//                {
//                    if (migrationScope != null)
//                        options.MigrationsHistoryTable(migrationScope, "migration")
//                               .MigrationsAssembly("Innovaticx.StrikePros.Infrastructure.EntityFramework.Migrations");
//                };
//        }
//    }
//}