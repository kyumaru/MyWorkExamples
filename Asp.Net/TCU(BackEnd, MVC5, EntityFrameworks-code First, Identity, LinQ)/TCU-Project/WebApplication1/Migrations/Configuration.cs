namespace WebApplication1.Migrations
{
    using WebApplication1.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CustomLibraries;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.MainDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        
        protected override void Seed(WebApplication1.MainDbContext context)
        {


            var users = new List<User>
                        {
                            new User{   Email = "a@a.com", Password =CustomEncrypt.Encrypt("a") , isAdmin= true,
                                        Name ="Scott", Major="Ing. Eléctrica", Ucard="a55478",
                                        projectItems = new List<ProjectItem>()
                            }


                        };
            users.ForEach(s => context.Users.AddOrUpdate(p => p.Email, s));
            context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            /* projects and projectTasks share the same table in database since the inheritance dictates they are projectItems,
            so they should share the same pool of incremental ids. Note thata seed method forcefully did input data into the db
            resulting in the posibility of iserting a project and a projectTask with the same ID -make sure this does not happen-
            it voids the db integrity
            */
            /*
           var projects = new List<Project>
                       {
                           new Project {   ID=1, Name = "Guayabo",  Description = "Este proyecto consiste en....",
                                           hours = 0, approved=false,
                                           assignees = new List<User>()
                           },
                           new Project {   ID=2, Name = "Panes",  Description = "Para el proyecto panes es necesario que....",
                                           hours = 0, approved=false,
                                           assignees = new List<User>()
                           }

                       };
           projects.ForEach(s => context.Projects.AddOrUpdate(p => p.ID, s));
           context.SaveChanges();

           var projectTasks = new List<ProjectTask>
                       {
                           new ProjectTask {   ID=3, Name = "Bajar guayabas",  Description = "Deben recuperse un total de 5 kilos de guayabas....",
                                               hours = 0, approved=false,
                                               ProjectID=1,
                                               assignees = new List<User>()
                           },

                           new ProjectTask {   ID =4, Name= "Crear maquina tostar",  Description = "Deben hacer un tostador que....",
                                               hours = 0, approved=false,
                                               ProjectID=2,
                                               assignees = new List<User>()
                           }

                       };

           projectTasks.ForEach(s => context.ProjectTasks.AddOrUpdate(p => p.ID, s));
           context.SaveChanges();

           //for users
           var users = new List<User>
                       {
                           new User {  Email = "a@a", Password = "a", isAdmin= true,
                                       Name ="Jean", Major="Geologia", Ucard="b12666",
                                       projectItems = new List<ProjectItem>()
                           },

                           new User {  Email = "b@b", Password = "b", isAdmin= true,
                                       Name ="Luc", Major="Medicina", Ucard="a15422",
                                       projectItems = new List<ProjectItem>()
                           },

                           new User{   Email = "c@c", Password = "c", isAdmin= true,
                                       Name ="Charles", Major="Ing. Industrial", Ucard="b18721",
                                       projectItems = new List<ProjectItem>()
                           },

                           new User{   Email = "d@d", Password = "d", isAdmin= true,
                                       Name ="Scott", Major="Ing. Electrica", Ucard="a55478",
                                       projectItems = new List<ProjectItem>()
                           }

                       };
           users.ForEach(s => context.Users.AddOrUpdate(p => p.Email, s));
           context.SaveChanges();

           AddOrUpdateProjectItemUser(context, "Guayabo", "Jean");
           AddOrUpdateProjectItemUser(context, "Guayabo", "Scott");
           AddOrUpdateProjectItemUser(context, "Panes", "Charles");
           AddOrUpdateProjectItemUser(context, "Panes", "Scott");//a user is linked to many project
           AddOrUpdateProjectItemUser(context, "Bajar guayabas", "Jean");
           AddOrUpdateProjectItemUser(context, "Bajar guayabas", "Scott");

           AddOrUpdateLinkTaskToProject(context, "Crear maquina tostar", "Panes");
           AddOrUpdateLinkTaskToProject(context, "Bajar guayabas", "Guayabo");

    *///closing seed method
        }
        
        //Note how this is done through the context classes

        //-asignar responsables-
        void AddOrUpdateProjectItemUser(MainDbContext context, string projectItemName, string userName)
        {
            var crs = context.ProjectItems.SingleOrDefault(c => c.Name == projectItemName);//returns the entity(row) of the entity set
            var inst = crs.assignees.SingleOrDefault(i => i.Name == userName);
            if (inst == null)
                crs.assignees.Add(context.Users.Single(i => i.Name == userName));
        }

        //TODO asign projectTasks to project

        void AddOrUpdateLinkTaskToProject(MainDbContext context, string projectTaskName, string projectName)
        {
            var projectEntityRow = context.Projects.SingleOrDefault(c => c.Name == projectName);//returns the entity(row) of the entity set
            var projectTask = projectEntityRow.ProjectTasks.SingleOrDefault(i => i.Name == projectTaskName);

            if (projectTask == null)
                projectEntityRow.ProjectTasks.Add(context.ProjectTasks.Single(i => i.Name == projectTaskName));
        }       

    }
    
}
