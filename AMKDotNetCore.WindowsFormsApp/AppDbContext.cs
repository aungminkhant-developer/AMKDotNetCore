﻿using AMKDotNetCore.WindowsFormsApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMKDotNetCore.WindowsFormsApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }
        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
