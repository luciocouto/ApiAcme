using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAcme.Models;

namespace ApiAcme.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AcmeContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Authors.
            if (context.Authors.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Authors[]
            {
            new Authors{Id=6661 ,FirstName="EvilMinion1",LastName="Banana1",Email="evilMinion1@evilminion.com", Birthdate=DateTime.Parse("2005-09-01")},
            new Authors{Id=6662 ,FirstName="EvilMinion2",LastName="Banana2",Email="evilMinion2@evilminion.com", Birthdate=DateTime.Parse("2002-09-01")},
            new Authors{Id=6663 ,FirstName="EvilMinion3",LastName="Banana3",Email="evilMinion3@evilminion.com", Birthdate=DateTime.Parse("2003-09-01")},
            new Authors{Id=6664 ,FirstName="EvilMinion4",LastName="Banana4",Email="evilMinion4@evilminion.com", Birthdate=DateTime.Parse("2002-09-01")},
            new Authors{Id=6665 ,FirstName="EvilMinion5",LastName="Banana5",Email="evilMinion5@evilminion.com", Birthdate=DateTime.Parse("2002-09-01")},
            new Authors{Id=6666 ,FirstName="EvilMinion6",LastName="Banana6",Email="evilMinion6@evilminion.com", Birthdate=DateTime.Parse("2001-09-01")},
            new Authors{Id=6667 ,FirstName="EvilMinion7",LastName="Banana7",Email="evilMinion7@evilminion.com", Birthdate=DateTime.Parse("2003-09-01")},
            new Authors{Id=6668 ,FirstName="EvilMinion8",LastName="Banana8",Email="evilMinion8@evilminion.com", Birthdate=DateTime.Parse("2005-09-01")}
            };

            foreach (Authors s in students)
            {
                context.Authors.Add(s);
            }

            context.SaveChanges();

            var courses = new Posts[]
            {
            new Posts{Id=6661 ,AuthorId=6661 , Title="EvilMinion6661talk",Descriptionpost="I hate Minions", Contentspost="1-Banana no pewde na! i Hate Minions!!!!!!!!!!", Datepost=DateTime.Parse("2005-09-01")},
            new Posts{Id=6662 ,AuthorId=6662 , Title="EvilMinion6662talk",Descriptionpost="I hate Minions", Contentspost="2-Banana no pewde na! i Hate Minions!!!!!!!!!!", Datepost=DateTime.Parse("2005-09-01")},
            new Posts{Id=6663 ,AuthorId=6663 , Title="EvilMinion6663talk",Descriptionpost="I hate Minions", Contentspost="3-Banana no pewde na! i Hate Minions!!!!!!!!!!", Datepost=DateTime.Parse("2005-09-01")},
            new Posts{Id=6664 ,AuthorId=6664 , Title="EvilMinion6664talk",Descriptionpost="I hate Minions", Contentspost="4-Banana no pewde na! i Hate Minions!!!!!!!!!!", Datepost=DateTime.Parse("2005-09-01")},
            new Posts{Id=6665 ,AuthorId=6665 , Title="EvilMinion6665talk",Descriptionpost="I hate Minions", Contentspost="5-Banana no pewde na! i Hate Minions!!!!!!!!!!", Datepost=DateTime.Parse("2005-09-01")},
            new Posts{Id=6666 ,AuthorId=6666 , Title="EvilMinion6666talk",Descriptionpost="I hate Minions", Contentspost="6-Banana no pewde na! i Hate Minions!!!!!!!!!!", Datepost=DateTime.Parse("2005-09-01")},
            new Posts{Id=6667 ,AuthorId=6667 , Title="EvilMinion6667talk",Descriptionpost="I hate Minions", Contentspost="7-Banana no pewde na! i Hate Minions!!!!!!!!!!", Datepost=DateTime.Parse("2005-09-01")},
            new Posts{Id=6668 ,AuthorId=6668 , Title="EvilMinion6668talk",Descriptionpost="I hate Minions", Contentspost="8-Banana no pewde na! i Hate Minions!!!!!!!!!!", Datepost=DateTime.Parse("2005-09-01")}
            };

            foreach (Posts c in courses)
            {
                context.Posts.Add(c);
            }

            context.SaveChanges();

        var enrollments = new Rates[]
            {
            new Rates{Id=6661 ,AuthorId=6661 ,PostId=6661, Noterate="1-Banana no pewde na! i Hate Minions!!!!!!!!!!", Daterate=DateTime.Parse("2005-09-08")},
            new Rates{Id=6662 ,AuthorId=6662 ,PostId=6662, Noterate="2-Banana no pewde na! i Hate Minions!!!!!!!!!!", Daterate=DateTime.Parse("2005-09-08")},
            new Rates{Id=6663 ,AuthorId=6663 ,PostId=6663, Noterate="3-Banana no pewde na! i Hate Minions!!!!!!!!!!", Daterate=DateTime.Parse("2005-09-08")},
            new Rates{Id=6664 ,AuthorId=6664 ,PostId=6664, Noterate="4-Banana no pewde na! i Hate Minions!!!!!!!!!!", Daterate=DateTime.Parse("2005-09-08")},
            new Rates{Id=6665 ,AuthorId=6665 ,PostId=6665, Noterate="5-Banana no pewde na! i Hate Minions!!!!!!!!!!", Daterate=DateTime.Parse("2005-09-08")},
            new Rates{Id=6666 ,AuthorId=6666 ,PostId=6666, Noterate="6-Banana no pewde na! i Hate Minions!!!!!!!!!!", Daterate=DateTime.Parse("2005-09-08")},
            new Rates{Id=6667 ,AuthorId=6667 ,PostId=6667, Noterate="7-Banana no pewde na! i Hate Minions!!!!!!!!!!", Daterate=DateTime.Parse("2005-09-08")},
            new Rates{Id=6668 ,AuthorId=6668 ,PostId=6668, Noterate="8-Banana no pewde na! i Hate Minions!!!!!!!!!!", Daterate=DateTime.Parse("2005-09-08")},
         
            };

            foreach (Rates e in enrollments)
            {
                context.Rates.Add(e);
            }

            context.SaveChanges();
        }
    }
}
