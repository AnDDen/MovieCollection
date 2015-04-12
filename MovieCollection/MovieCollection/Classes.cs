using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollection
{
    public struct Genre
    {
        public int GenreID;
        public string Name;

        public Genre(int id, string name)
        {
            GenreID = id;
            Name = name;
        }
    }
    
    public struct Studio
    {
        public int StudioID;
        public string Name;

        public Studio(int id, string name)
        {
            StudioID = id;
            Name = name;
        }
    }

    public struct Human
    {
        public int HumanID;
        public string Name;
        public string Surname;

        public Human(int id, string name, string surname)
        {
            HumanID = id;
            Name = name;
            Surname = surname;
        }
    }

    public struct RoleType
    {
        public int TypeID;
        public string Name;

        public RoleType(int id, string name)
        {
            TypeID = id;
            Name = name;
        }
    }

    public class Movie
    {
        public int MovieID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public int Age { get; set; }

        public string Link { get; set; }

        public Studio MovieStudio { get; set; }

        public List<Genre> Genres { get; set; }

        public Movie()
        {
        }
    }

    public class Image
    {
    }
}
