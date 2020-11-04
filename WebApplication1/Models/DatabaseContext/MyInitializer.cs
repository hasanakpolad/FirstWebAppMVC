using Bogus;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models.DatabaseContext
{
    public class MyInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            var faker = new Faker("tr");
            for (int i = 0; i < 50; i++)
            {
                if (i % 2 == 0)
                {
                    context.Users.Add(new Users
                    {

                        Name = faker.Name.FirstName(),
                        Surname = faker.Name.LastName(),
                        Mail = faker.Internet.Email(),
                        //Rank = { "Kullanıcı", "Yetkili", "Patron" }

                    });
                }
                else
                {
                    context.Users.Add(new Users
                    {

                        Name = faker.Name.FirstName(),
                        Surname = faker.Name.LastName(),
                        Mail = faker.Internet.Email(),
                        //Rank = { "Kullanıcı", "Yetkili", "Patron" }
                        //Rank = "SuperUser"

                    });
                }
            }
            for (int i = 0; i < 15; i++)
            {
                context.Orders.Add(new Orders
                {
                    Title = "deneme",
                    Explain = "deneme",
                    Status = "baslamadı",
                    CreateTime = DateTime.Today
                }) ;
            }
            for (int i = 0; i < 15; i++)
            {
                context.Notes.Add(new Notes
                {
                    Title = "deneme not",
                    Note = "deneme not",
                    General = true
                    
                    
                });
            }
            for (int i = 0; i < 15; i++)
            {
                context.Daily.Add(new Daily
                {
                    Note = "deneme",
                    Status = "başlamadı",
                    StartTime = DateTime.Today,
                    FinishTime = DateTime.Now.Date


                });
            }
            for (int i = 0; i < 15; i++)
            {
                context.Group.Add(new Group
                {
                    GroupName = "deneme grup " + i.ToString(),
                    Explain = "grup sayısı " + i.ToString()
                });
            }
            for (int i = 0; i < 30; i++)
            {
                context.Category.Add(new Category
                {
                    CategoryName = "deneme cat" + i.ToString(),
                    
                });
            }
            context.SaveChanges();
        }
    }
}