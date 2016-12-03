using HomeLibrary.BLL;
using HomeLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DetectiveRepository detRepo = new DetectiveRepository();
            //var getAll = detRepo.GetAll();
            //var getById1 = detRepo.GetById(1);
            //var getById4 = detRepo.GetById(4);
            //var getById5 = detRepo.GetById(5);

            //Detective detective = new Detective
            //{
            //    Author = "test",
            //    Title = "test",
            //    Pages = 100,
            //    HeroesNumber = 10,
            //    Edition = "test",
            //    Estimate = new Estimate
            //    {
            //        Availability = true,
            //        Origin = "test",
            //        Recommendation = "test",
            //        Worth = "test"
            //    },
            //    Year = 300000,
            //    Section = new Entities.Section
            //    {
            //        Id = 1
            //    }
            //};

            ////var add = repo.Add(detective);
            //detective = new Detective
            //{
            //    Id = 8,
            //    Author = "my awesome author",
            //    Title = "my awesome title",
            //    Pages = 1000001,
            //    HeroesNumber = 1000001,
            //    Edition = "my awesome edition",
            //    Estimate = new Estimate
            //    {
            //        Id = 9,
            //        Availability = true,
            //        Origin = "my awesome origin",
            //        Recommendation = "my awesome recommendation",
            //        Worth = "my awesome worth"
            //    },
            //    Year = 1000001,
            //    Section = new Entities.Section
            //    {
            //        Id = 1
            //    }
            //};


            ////var update = repo.Edit(detective);

            ////repo.Delete(9, 10);
            //var searchAuthor = detRepo.Search("hor111", Helpers.Enums.BookFilter.Author);
            //var searchTitle = detRepo.Search("11", Helpers.Enums.BookFilter.Title);
            //var searchEdition = detRepo.Search("34", Helpers.Enums.BookFilter.Edition);
            //var searchEdition2 = detRepo.Search("ion2", Helpers.Enums.BookFilter.Edition);


            SchoolbookRepository schoolRepo = new SchoolbookRepository();
            var getAll = schoolRepo.GetAll();
            var getById1 = schoolRepo.GetById(10);
            var getById4 = schoolRepo.GetById(11);

            //Schoolbook schoolbook = new Schoolbook
            //{
            //    Author = "test",
            //    Title = "test",
            //    Pages = 100,
            //    Edition = "test",
            //    Estimate = new Estimate
            //    {
            //        Availability = true,
            //        Origin = "test",
            //        Recommendation = "test",
            //        Worth = "test"
            //    },
            //    Year = 300000,
            //    Section = new Entities.Section
            //    {
            //        Id = 3
            //    },
            //    Form = 9999,
            //    Subject = "test"
            //};

            //var add = schoolRepo.Add(schoolbook);

            //schoolbook = new Schoolbook
            //{
            //    Id = 13,
            //    Author = "my awesome author",
            //    Title = "my awesome title",
            //    Pages = 1000001,
            //    Edition = "my awesome edition",
            //    Estimate = new Estimate
            //    {
            //        Id = 16,
            //        Availability = true,
            //        Origin = "my awesome origin",
            //        Recommendation = "my awesome recommendation",
            //        Worth = "my awesome worth"
            //    },
            //    Year = 1000001,
            //    Section = new Entities.Section
            //    {
            //        Id = 3
            //    },
            //    Form = 333333,
            //    Subject = "my awesome Subject"
            //};


            //var update = schoolRepo.Edit(schoolbook);

            schoolRepo.Delete(13, 16);
            var searchAuthor = schoolRepo.Search("1", Helpers.Enums.BookFilter.Author);
            var searchTitle = schoolRepo.Search("it", Helpers.Enums.BookFilter.Title);
            var searchEdition = schoolRepo.Search("ed", Helpers.Enums.BookFilter.Edition);
        }
    }
}
