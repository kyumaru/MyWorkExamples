using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication1.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebApplication1
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
            : base("name=DefaultConnection")
        {
        }

        /*
        This code creates a DbSet property for each entity set.In Entity Framework terminology, 
        an entity set typically corresponds to a database table, and an entity corresponds to a row in the table.
        */

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectItem> ProjectItems { get; set; }//use only to implement hierarchy in the db


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<ProjectItem>()
                .HasMany(c => c.assignees).WithMany(i => i.projectItems)
                .Map(t => t.MapLeftKey("ProjectItemID")
                    .MapRightKey("UserID")
                    .ToTable("ProjectItemUser"));
            
            /* this was a tryout to have the db cascade the deletion on project to linked projecTask
            modelBuilder.Entity<ProjectTask>()
                .HasOptional(a => a.Project)//this must be a navigation property
                .WithOptionalDependent()
                .WillCascadeOnDelete(true);
                */
        }
    }
}