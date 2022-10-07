using BetterCinema.Api.Models;

namespace BetterCinema.Api.Data
{
    public static class DataSeeder
    {
        public static void AddInitialData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<CinemaDbContext>();

            bool wasCreated = dbContext.Database.EnsureCreated();

            if (!wasCreated)
            {
                return;
            }

            dbContext.AddTheaters();
            dbContext.AddMovies();
            dbContext.AddSessions();

            dbContext.SaveChanges();
        }

        public static CinemaDbContext AddTheaters(this CinemaDbContext dbContext)
        {

            Theater[] theaters = new[]
            {
                 new Theater {
                    Name = $"Borum Cinema",
                    Address = "Savanorių pr. 4, Kaunas",
                    Description = "Neseniai duris Kaune atvėręs kino teatras.",
                    IsConfimed = true,
                 },
                 new Theater {
                    Name = $"AMC",
                    Address = "Karaliaus Mindaugo g. 5, Kaunas",
                    Description = "Pats seniausiaias kino teatras Kaune.",
                    IsConfimed = true,
                },
                new Theater {
                    Name = $"Vilniaus kino teatras",
                    Address = "Pylimo g. 8, Vilnius",
                    Description = "Kino teatras Viniuje",
                    IsConfimed = true,
                }
            };

            dbContext.Theaters.AddRange(theaters);
            dbContext.SaveChanges();

            return dbContext;
        }

        public static CinemaDbContext AddMovies(this CinemaDbContext dbContext)
        {

            Movie[] movies = new[]
            {
                 // First theater
                 new Movie {
                    Title = $"Amsterdam",
                    ReleaseDate = new DateTime(2022,10,7),
                    Description = "Trys draugai – gydytojas, medicinos sesuo ir advokatas – tampa žmogžudystės liudininkais, o galiausiai ir įtariamaisiais. Tuomet jie atsiduria slapčiausio Amerikos istorijoje sąmokslo epicentre.",
                    Genre = "Drama",
                    Director = "David O. Russell",
                    TheaterId = 1
                 },
                 new Movie {
                    Title = $"Nesijaudink, brangioji",
                    ReleaseDate = new DateTime(2022,9,2),
                    Description = "Aktorės ir režisierės Olivijos Wilde seksualaus psichologinio trilerio „Nesijaudink, brangioji“ centre – Alisa ir Džekas Čeimbersai (aktoriai Florence Pugh ir Harry Styles). Vyrui pakeitus darbą, jauna šeima persikelia į naujojo darbdavio įkurtą uždarą kompanijos miestelį Kalifornijoje. Čia vadžias tvirtai savo rankose laiko charizmatiškas kompanijos vadovas Frenkas (akt. Chris Pine).",
                    Genre = "Trileris",
                    Director = "Olivia Wilde",
                    TheaterId = 1
                 },
                 // Second theater
                 new Movie {
                    Title = $"Šypsena",
                    ReleaseDate = new DateTime(2022,10,7),
                    Description = "Tapusi keisto, traumuojančio incidento, susijusio su paciente, liudininke, daktarė Rouzė Cotter (akt. Sosie Bacon) pradeda patirti bauginančius įvykius, kurių ji niekaip negali paaiškinti.",
                    Genre = "Siaubo",
                    Director = "Edgar Whrite",
                    TheaterId = 2
                 },
                  new Movie {
                    Title = $"Ten, kur gieda vėžiai",
                    ReleaseDate = new DateTime(2021,10,7),
                    Description = "Kaja Klark (aktorė Daisy Edgar-Jones) savo gimtojo miestelio Šiaurės Karolinoje gyventojams visuomet kėlė prieštaringus jausmus, iš kurių stipriausi – nepasitikėjimas, įtarumas ir panieka. Mergaitė gimė šalia miestelio tyvuliuojančiose pelkėse, kur buvo įsikūrusi jos šeima. Pirmiausiai kelerių metų dukrą paliko motina, tuomet vienas po kito išvyko vyresnieji broliai ir seserys, o galiausiai pradingo ir tėvas. Nuo tada Kaja pelkėse augo viena.\r\n",
                    Genre = "Drama",
                    Director = "Daisy Edgar-Jones",
                    TheaterId = 2
                 },
                   new Movie {
                    Title = $"Pakalikai 2",
                    ReleaseDate = new DateTime(2022,6,15),
                    Description = "Animacinės franšizės „Bjaurusis Aš“ skyriaus „Pakalikai“ tęsinys atskleis draugiškojo piktadario Felonijaus Gru kilmės istoriją.\r\n",
                    Genre = "Komedija",
                    Director = "David O. Russell",
                    TheaterId = 2
                 },
            };
  
            dbContext.Movies.AddRange(movies);
            dbContext.SaveChanges();
            return dbContext;
        }

        public static CinemaDbContext AddSessions(this CinemaDbContext dbContext)
        {
            Session[] sessions = new[]
            {
                 new Session {
                       Start = DateTime.Now.AddHours(5),
                       End = DateTime.Now.AddHours(7),
                       Hall = "1",
                       MovieId = 1,           
                  },
                  new Session {
                       Start = DateTime.Now.AddHours(7),
                       End = DateTime.Now.AddHours(9),
                       Hall = "2",
                       MovieId = 1,
                  },
                  new Session {
                       Start = DateTime.Now.AddHours(9),
                       End = DateTime.Now.AddHours(11),
                       Hall = "2",
                       MovieId = 1,
                  },

                   new Session {
                       Start = DateTime.Now.AddHours(5),
                       End = DateTime.Now.AddHours(7),
                       Hall = "1",
                       MovieId = 2,
                  },
                  new Session {
                       Start = DateTime.Now.AddHours(7),
                       End = DateTime.Now.AddHours(9),
                       Hall = "2",
                       MovieId = 2,
                  },
                  new Session {
                       Start = DateTime.Now.AddHours(9),
                       End = DateTime.Now.AddHours(11),
                       Hall = "2",
                       MovieId = 2,
                  },

                   new Session {
                       Start = DateTime.Now.AddHours(5),
                       End = DateTime.Now.AddHours(7),
                       Hall = "A",
                       MovieId = 3,
                  },
                  new Session {
                       Start = DateTime.Now.AddHours(7),
                       End = DateTime.Now.AddHours(9),
                       Hall = "B",
                       MovieId = 3,
                  },
                  new Session {
                       Start = DateTime.Now.AddHours(9),
                       End = DateTime.Now.AddHours(11),
                       Hall = "C",
                       MovieId = 3,
                  },

                   new Session {
                       Start = DateTime.Now.AddHours(5),
                       End = DateTime.Now.AddHours(7),
                       Hall = "A",
                       MovieId = 4,
                  },
                  new Session {
                       Start = DateTime.Now.AddHours(7),
                       End = DateTime.Now.AddHours(9),
                       Hall = "E",
                       MovieId = 4,
                  },
                  new Session {
                       Start = DateTime.Now.AddHours(9),
                       End = DateTime.Now.AddHours(11),
                       Hall = "F",
                       MovieId = 4,
                  },

                  new Session {
                       Start = DateTime.Now.AddHours(5),
                       End = DateTime.Now.AddHours(7),
                       Hall = "A",
                       MovieId = 5,
                  },
                  new Session {
                       Start = DateTime.Now.AddHours(7),
                       End = DateTime.Now.AddHours(9),
                       Hall = "E",
                       MovieId = 5,
                  },
                  new Session {
                       Start = DateTime.Now.AddHours(9),
                       End = DateTime.Now.AddHours(11),
                       Hall = "F",
                       MovieId = 5,
                  },
            };

            dbContext.Sessions.AddRange(sessions);
            dbContext.SaveChanges();
            return dbContext;
        }

    }
}
