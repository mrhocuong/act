#region copyright

//-------------------------- CNWallet ---------------------------
//
// Project: CNWallet.EFCore
// File name: DbContextFactory.cs
// Author: Cuong Ho
// Created time (yyyy/mm/dd): 2018/3/30
//
//----------------------- Copyright 2018 © ----------------------------------------

#endregion


using System;
using System.Reflection;
using act.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Mubo.EF.Contexts
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            return CreateCoreContext();
        }

        public static AppDbContext CreateCoreContext()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();

            if (builder.IsConfigured == false)
                builder.UseSqlServer(GetConnectionString(),
                    optionsBuilder => 
                        optionsBuilder.MigrationsAssembly(GetMigrationAssemblyName()));

            return new AppDbContext(builder.Options);
        }

        public static string GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("connections.json", false, true).Build();
            var connectionString = configuration.GetConnectionString(nameof(AppDbContext));
            Console.WriteLine("Use connection string: " + connectionString);

            return connectionString;
        }

        public static string GetMigrationAssemblyName()
        {
            return typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name;
        }
    }
}