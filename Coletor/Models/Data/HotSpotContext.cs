using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotSpotAPI.Models.Data;
using HotSpotAPI.Models.Models;

namespace HotSpotAPI.Models.Data
{
    public class HotSpotContext : DbContext
    {
        public HotSpotContext(DbContextOptions<HotSpotContext> options)
        : base(options)
    {
    }

        public DbSet<Cadastro> Cadastros { get; set; } = null!;


        //FIX para a Injeção de Dependencia da Base de Dados no Program.cs
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("HotSpot");
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}