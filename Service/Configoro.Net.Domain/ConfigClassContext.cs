using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Configoro.Net.Domain
{
    public class ConfigClassContext : DbContext
    {
        public ConfigClassContext() : base(ConfigurationManager.ConnectionStrings["ConfigDb"].ConnectionString)
        {

            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new NullDatabaseInitializer<ConfigClassContext>());
        }
        public DbSet<ConfigurationSetting> ConfigurationSetting { get; set; }
        public DbSet<ConfigurationSettingValue> ConfigurationSettingValue { get; set; }
        public DbSet<ConfigurationTemplate> ConfigurationTemplate { get; set; }
        public DbSet<ConfigValue> ConfigValue { get; set; }
        public DbSet<Environment> Environment { get; set; }
        public DbSet<ProcessorType> ProcessorType { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
