using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollection
{
    public class Genre
    {
        public int GenreID { get; set; }
        public string Name { get; set; }

        public Genre(int id, string name)
        {
            GenreID = id;
            Name = name;
        }
    }
    
    public class Studio
    {
        public int StudioID { get; set; }
        public string Name { get; set; }

        public Studio(int id, string name)
        {
            StudioID = id;
            Name = name;
        }
    }

    public class Human
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public Human(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }

    /*
    public class RoleType
    {
        public int TypeID { get; set; }
        public string Name { get; set; }

        public RoleType(int id, string name)
        {
            TypeID = id;
            Name = name;
        }
    } */

    enum RoleType
    {
        Actor,
        Writer,
        Director
    }

    public class Movie
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Age { get; set; }
        public string Link { get; set; }
        public Studio MovieStudio { get; set; }

        public List<int> Genres { get; set; }
        public List<Image> Images { get; set; }
        public Dictionary<Human, RoleType> Roles { get; set; }

        public Movie(string name, string description, int year, int age, string link, Studio movieStudio, 
            List<int> genres, List<Image> images, Dictionary<Human, RoleType> roles)
        {
            Name = name;
            Description = description;
            Year = year;
            Age = age;
            Link = link;
            MovieStudio = movieStudio;
            Genres = genres;
            Images = images;
            Roles = roles;
        }
        public Movie(string name) : this(name, "", 0, 0, "", null, 
            new List<int>(), new List<Image>(), new Dictionary<Human, RoleType>()) { } 
    }

    public class Image
    {
        public string URL { get; set; }
        public string Description { get; set; }

        public Image(string url, string description)
        {
            URL = url;
            Description = description;
        }
        public Image(string url) : this(url, "") { }
    }
}
