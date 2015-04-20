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

        public IList<Movie> Movies { get; set; }

        public Studio(int id, string name, List<Movie> movies)
        {
            StudioID = id;
            Name = name;
            Movies = movies;
        }

        public Studio(int id, string name) : this(id, name, new List<Movie>()) { }
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

    public enum RoleType
    {
        Actor = 1,
        Writer = 2,
        Director = 3
    }

    public class Movie
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Age { get; set; }
        public string Link { get; set; }
        public string MovieStudio { get; set; }

        public IList<Genre> Genres { get; set; }
        public IList<Image> Images { get; set; }
        public IList<Role> Roles { get; set; }

        public Movie(string name, string description, int year, int age, string link, string movieStudio,
            IList<Genre> genres, IList<Image> images, IList<Role> roles)
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
        public Movie(string name) : this(name, null, 2015, 0, null, null,
            new List<Genre>(), new List<Image>(), new List<Role>()) { } 
    }

    public class Role
    {
        public Human Human { get; set; }
        public RoleType Type { get; set; }
        public Movie Movie { get; set; }
        public string Character { get; set; }

        public Role(Human human, RoleType type, Movie movie, string character)
        {
            Human = human;
            Type = type;
            Movie = movie;
            if (type == RoleType.Actor)
                Character = character;
            else
                Character = null;
        }

        public Role(Human human, RoleType type, Movie movie) : this(human, type, movie, null) { }
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
