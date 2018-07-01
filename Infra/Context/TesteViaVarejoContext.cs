﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Context
{
    public class TesteViaVarejoContext : DbContext
    {
        public TesteViaVarejoContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Friend> Friends { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
