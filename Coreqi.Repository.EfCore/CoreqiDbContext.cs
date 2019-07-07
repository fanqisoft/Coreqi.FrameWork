/*
 *┌────────────────────────────────────────────────┐
 *│　描   述：CoreqiDbContext                                                    
 *│　作   者：fanqi                                              
 *│　版   本：1.0                                              
 *│　创建时间：2019/7/6 周六 20:12:13                        
 *└────────────────────────────────────────────────┘
 */

using Coreqi.Configuration;
using Coreqi.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coreqi.Repository.EfCore
{
    public class CoreqiDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connString = ConfigHelper.GetConfig<string>("sqlServerConnString");
                optionsBuilder.UseSqlServer(connString);
            }
        }

        public DbSet<User> Users { get; set; }
    }
}