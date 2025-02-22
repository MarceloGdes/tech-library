﻿using Microsoft.EntityFrameworkCore;
using TechLibrary.Api.Domain.Entities;

namespace TechLibrary.Api.Infraestructure.DataAccess;

public class TechLibraryDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\Marcelo\\Documents\\Programação\\RocketSeat\\NLW_CONNECT\\TechLibraryDb.db");
    }
}
