using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;
using API.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Tests
{
    [TestClass()]
    public class SeriesControllerTests
    {
        //public SeriesDbContext _context;
        SeriesController SeriesController;

        public SeriesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;");
            SeriesController = new SeriesController(new SeriesDbContext(builder.Options));

            //var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;"); // Chaine de connexion à mettre dans les ( )
            //SeriesDbContext context = new SeriesDbContext(builder.Options);
        }
        /*
        [TestMethod()]
        public void SeriesControllerTest()
        {
            Assert.Fail();
        }
        */
        [TestMethod()]
        public void GetSeriesTest()
        {

            List<Serie> serieList = new List<Serie>();
            serieList.AddRange(new List<Serie> {
                new Serie(
                    1,
                    "Scrubs",
                    "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !",
                    9,
                    184,
                    2001,
                    "ABC (US)"),
                new Serie(
                    2,
                    "James May's 20th Century",
                    "The world in 1999 would have been unrecognisable to anyone from 1900. James May takes a look at some of the greatest developments of the 20th century, and reveals how they shaped the times we live in now.",
                    1,
                    6,
                    2007,
                    "BBC Two"),
                new Serie(
                    3,
                    "True Blood",
                    "Ayant trouvé un substitut pour se nourrir sans tuer (du sang synthétique), les vampires vivent désormais parmi les humains. Sookie, une serveuse capable de lire dans les esprits, tombe sous le charme de Bill, un mystérieux vampire. Une rencontre qui bouleverse la vie de la jeune femme...",
                    7,
                    81,
                    2008,
                    "HBO"),
            });


            var getSeries = SeriesController.GetSeries().Result;

            List<Serie> listeSeriesRecuperees = new List<Serie>();
            listeSeriesRecuperees = getSeries.Value.Where(s => s.Serieid <= 3).ToList();

            CollectionAssert.AreEqual(serieList, listeSeriesRecuperees);
        }

        [TestMethod()]
        public void GetSerieTest()
        {
            Serie serieList = new Serie(
                    1,
                    "Scrubs",
                    "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !",
                    9,
                    184,
                    2001,
                    "ABC (US)");

            var getSeries = SeriesController.GetSerie(1).Result.Value;
            Assert.AreEqual(serieList, getSeries);

            //CollectionAssert.AreEqual(serieList, listeSeriesRecuperees);
        }
        /*
        [TestMethod()]
        public void PutSerieTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PostSerieTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteSerieTest()
        {
            Assert.Fail();
        }*/
    }
}