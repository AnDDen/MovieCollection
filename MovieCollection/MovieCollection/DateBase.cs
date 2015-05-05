using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Globalization;

namespace MovieCollection
{
    static class DataBase
    {
        private const string PATH = "movies.sqlite";

        public static void Create()
        {
            SQLiteConnection.CreateFile(PATH);
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=movies.sqlite;Version=3;"))
            {
                connection.Open();

                // Create table Genre
                string sql = @"CREATE TABLE Genre (
                            Genre_ID NUMBER PRIMARY KEY,
                            Name VARCHAR2(200) NOT NULL UNIQUE
                            )";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                // Create table Studio
                sql = @"CREATE TABLE Studio (
                    Studio_ID NUMBER PRIMARY KEY,
                    Name VARCHAR2(1000) NOT NULL UNIQUE
                    )";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                // Create table Movie
                sql = @"CREATE TABLE Movie (
                    Movie_ID NUMBER PRIMARY KEY,
                    Name VARCHAR2(200) NOT NULL,
                    Description VARCHAR2(2000), 
                    Year NUMBER CHECK (Year > 1900),
                    Age NUMBER CHECK (Age >= 0),
                    Link VARCHAR2(200),
                    Studio_ID NUMBER NULL,
                    CONSTRAINT studio_fk FOREIGN KEY (Studio_ID) REFERENCES Studio(Studio_ID)
                    )";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                // Create table Movie_Genre
                sql = @"CREATE TABLE Movie_Genre (
                    Movie_ID NUMBER NOT NULL,
                    Genre_ID NUMBER NOT NULL,
                    CONSTRAINT mg_movie_fk FOREIGN KEY (Movie_ID) REFERENCES Movie(Movie_ID),
                    CONSTRAINT mg_genre_fk FOREIGN KEY (Genre_ID) REFERENCES Genre(Genre_ID)
                    )";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                // Create table Image
                sql = @"CREATE TABLE Image (
                    Image_ID NUMBER PRIMARY KEY,
                    URL VARCHAR2(200) NOT NULL,
                    Description VARCHAR2(1000), 
                    Movie_ID NUMBER NOT NULL,
                    CONSTRAINT movie_fk FOREIGN KEY (Movie_ID) REFERENCES Movie(Movie_ID)
                    )";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                // Create table Human
                sql = @"CREATE TABLE Human (
                    Human_ID NUMBER PRIMARY KEY,
                    Name VARCHAR2(200) NOT NULL,
                    Surname VARCHAR2(200) NOT NULL
                    )";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                // Create table Role_Type
                sql = @"CREATE TABLE Role_Type (
                    Type_ID NUMBER PRIMARY KEY,
                    Name VARCHAR2(200) NOT NULL
                    )";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                // Create table Role
                sql = @"CREATE TABLE Role (
                    Movie_ID NUMBER NOT NULL,
                    Type_ID NUMBER NOT NULL,
                    Human_ID NUMBER NOT NULL,
                    Character VARCHAR2(200),
                    CONSTRAINT movie_fk1 FOREIGN KEY (Movie_ID) REFERENCES Movie(Movie_ID),
                    CONSTRAINT type_fk2 FOREIGN KEY (Type_ID) REFERENCES Role_Type(Type_ID),
                    CONSTRAINT human_fk3 FOREIGN KEY (Human_ID) REFERENCES Human(Human_ID)
                    )";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                // Insert role types
                sql = @"INSERT INTO Role_Type VALUES (1, 'Актёр');
                    INSERT INTO Role_Type VALUES (2, 'Сценарист');
                    INSERT INTO Role_Type VALUES (3, 'Режиссёр');
                    ";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                // Insert genres
                sql = @"INSERT INTO Genre VALUES (1, 'Аниме');
                    INSERT INTO Genre VALUES (2, 'Биография');
                    INSERT INTO Genre VALUES (3, 'Боевик');
                    INSERT INTO Genre VALUES (4, 'Вестерн');
                    INSERT INTO Genre VALUES (5, 'Военный');
                    INSERT INTO Genre VALUES (6, 'Детектив');
                    INSERT INTO Genre VALUES (7, 'Детский');
                    INSERT INTO Genre VALUES (8, 'Для взрослых');
                    INSERT INTO Genre VALUES (9, 'Документальный');
                    INSERT INTO Genre VALUES (10, 'Драма');
                    INSERT INTO Genre VALUES (11, 'История');
                    INSERT INTO Genre VALUES (12, 'Комедия');
                    INSERT INTO Genre VALUES (13, 'Концерт');
                    INSERT INTO Genre VALUES (14, 'Короткометражка');
                    INSERT INTO Genre VALUES (15, 'Криминал');
                    INSERT INTO Genre VALUES (16, 'Мелодрама');
                    INSERT INTO Genre VALUES (17, 'Мультфильм');
                    INSERT INTO Genre VALUES (18, 'Мюзикл');
                    INSERT INTO Genre VALUES (19, 'Нуар');
                    INSERT INTO Genre VALUES (20, 'Приключения');
                    INSERT INTO Genre VALUES (21, 'Семейный');
                    INSERT INTO Genre VALUES (22, 'Спорт');
                    INSERT INTO Genre VALUES (23, 'Триллер');
                    INSERT INTO Genre VALUES (24, 'Ужасы');
                    INSERT INTO Genre VALUES (25, 'Фантастика');
                    INSERT INTO Genre VALUES (26, 'Фэнтези');
                    ";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static int InsertMovie(Movie movie, int movieID)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=movies.sqlite;Version=3;"))
            {
                connection.Open();

                int studioID;

                if (movie.MovieStudio != null)
                {
                    studioID = GetStudioID(movie.MovieStudio);
                    if (studioID == -1)
                        studioID = InsertStudio(movie.MovieStudio);
                }
                else
                    studioID = -1;

                // Insert into MOVIE

                string sql = @"INSERT INTO Movie (Movie_ID, Name, Description, Year, Age, Link, Studio_ID) VALUES (@ID, @NAME, @DESCR, @YEAR, @AGE, @LINK, @STUDIO_ID)";

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", movieID);
                command.Parameters.AddWithValue("@NAME", movie.Name);
                command.Parameters.AddWithValue("@DESCR", movie.Description);
                command.Parameters.AddWithValue("@YEAR", movie.Year);
                command.Parameters.AddWithValue("@AGE", movie.Age);
                command.Parameters.AddWithValue("@LINK", movie.Link);
                command.Parameters.AddWithValue("@STUDIO_ID", (studioID == -1) ? null : studioID.ToString());

                command.ExecuteNonQuery();

                // Insert into MOVIE_GENRE

                foreach (Genre g in movie.Genres)
                {
                    command = new SQLiteCommand(sql, connection);
                    sql = @"INSERT INTO Movie_Genre (Movie_ID, Genre_ID) VALUES (@MOVIE_ID, @GENRE_ID);";
                    command = new SQLiteCommand(sql, connection);
                    command.Parameters.AddWithValue("@MOVIE_ID", movieID);
                    command.Parameters.AddWithValue("@GENRE_ID", g.GenreID);

                    command.ExecuteNonQuery();
                }

                // Insert into ROLE & HUMAN

                foreach (Role r in movie.Roles)
                {
                    command = new SQLiteCommand(sql, connection);

                    int humanID = GetHumanID(r.Human);
                    if (humanID == -1)
                        humanID = InsertHuman(r.Human);

                    if (r.Type != RoleType.Actor)
                    {
                        sql = @"INSERT INTO Role (Movie_ID, Type_ID, Human_ID) VALUES (@MOVIE_ID, @TYPE_ID, @HUMAN_ID)";
                        command = new SQLiteCommand(sql, connection);
                        command.Parameters.AddWithValue("@MOVIE_ID", movieID);
                        command.Parameters.AddWithValue("@TYPE_ID", (int)r.Type);
                        command.Parameters.AddWithValue("@HUMAN_ID", humanID);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        sql = @"INSERT INTO Role (Movie_ID, Type_ID, Human_ID, Character) VALUES (@MOVIE_ID, @TYPE_ID, @HUMAN_ID, @CHARACTER)";
                        command = new SQLiteCommand(sql, connection);
                        command.Parameters.AddWithValue("@MOVIE_ID", movieID);
                        command.Parameters.AddWithValue("@TYPE_ID", (int)r.Type);
                        command.Parameters.AddWithValue("@HUMAN_ID", humanID);
                        command.Parameters.AddWithValue("@CHARACTER", r.Character);
                        command.ExecuteNonQuery();
                    }
                }

                // Insert into IMAGE

                foreach (Image img in movie.Images)
                {
                    InsertImage(img, movieID);
                }

                connection.Close();
                return movieID;
            }
        }

        public static int InsertMovie(Movie movie)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=movies.sqlite;Version=3;");

            connection.Open();
            string sql = @"SELECT COUNT(*) FROM Movie;";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            int id = Convert.ToInt32(command.ExecuteScalar()) + 1;
            connection.Close();

            return InsertMovie(movie, id);
        }

        public static List<Genre> GetGenres()
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=movies.sqlite;Version=3;"))
            {
                connection.Open();

                List<Genre> genres = new List<Genre>();

                string sql = @"SELECT Genre_ID, Name
                           FROM Genre;";

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                SQLiteDataReader r = command.ExecuteReader();
                while (r.Read())
                {
                    genres.Add(new Genre(Convert.ToInt32(r["Genre_ID"]), r["Name"].ToString()));
                }
                r.Close();

                connection.Close();

                return genres;
            }
        }

        public static int GetStudioID(string name)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=movies.sqlite;Version=3;"))
            {
                connection.Open();

                int id = -1;

                string sql = "SELECT Studio_ID FROM Studio WHERE (Name = @NAME);";

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@NAME", name);
                SQLiteDataReader r = command.ExecuteReader();
                while (r.Read())
                {
                    id = Convert.ToInt32(r["Studio_ID"]);
                }
                r.Close();

                connection.Close();

                return id;
            }
        }

        public static int InsertStudio(string name)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=movies.sqlite;Version=3;"))
            {
                connection.Open();

                string sql = @"SELECT COUNT(*) FROM Studio;";

                SQLiteCommand command = new SQLiteCommand(sql, connection);

                int id = Convert.ToInt32(command.ExecuteScalar()) + 1;

                sql = @"INSERT INTO Studio (Studio_ID, Name) VALUES (@STUDIO_ID, @NAME)";

                command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@STUDIO_ID", id);
                command.Parameters.AddWithValue("@NAME", name);
                command.ExecuteNonQuery();

                connection.Close();

                return id;
            }
        }

        public static int GetHumanID(Human h)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=movies.sqlite;Version=3;"))
            {
                connection.Open();

                int id = -1;

                string sql = "SELECT Human_ID FROM Human WHERE (Name = @NAME) AND (Surname = @SURNAME);";

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@NAME", h.Name);
                command.Parameters.AddWithValue("@SURNAME", h.Surname);
                SQLiteDataReader r = command.ExecuteReader();
                while (r.Read())
                {
                    id = Convert.ToInt32(r["Human_ID"]);
                }
                r.Close();

                connection.Close();

                return id;
            }
        }

        public static int InsertHuman(Human h)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=movies.sqlite;Version=3;"))
            {
                connection.Open();

                string sql = @"SELECT COUNT(*) FROM Human;";

                SQLiteCommand command = new SQLiteCommand(sql, connection);

                int id = Convert.ToInt32(command.ExecuteScalar()) + 1;

                sql = @"INSERT INTO Human (Human_ID, Name, Surname) VALUES (@ID, @NAME, @SUrNAME)";

                command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@NAME", h.Name);
                command.Parameters.AddWithValue("@SURNAME", h.Surname);
                command.ExecuteNonQuery();

                connection.Close();

                return id;
            }
        }

        public static int InsertImage(Image img, int movieID)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=movies.sqlite;Version=3;"))
            {
                connection.Open();

                string sql = @"SELECT MAX(Image_ID) FROM Image;";

                SQLiteCommand command = new SQLiteCommand(sql, connection);

                int id = Convert.ToInt32(command.ExecuteScalar()) + 1;

                sql = @"INSERT INTO Image (Image_ID, URL, Description, Movie_ID) VALUES (@IMAGE_ID, @URL, @DESCRIPTION, @MOVIE_ID)"; 

                command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@IMAGE_ID", id);
                command.Parameters.AddWithValue("@URL", img.URL);
                command.Parameters.AddWithValue("@DESCRIPTION", img.Description);
                command.Parameters.AddWithValue("@MOVIE_ID", movieID);
                command.ExecuteNonQuery();

                connection.Close();

                return id;
            }
        }

        public static Dictionary<int, string> SearchMovies(string searchStr)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=movies.sqlite;Version=3;"))
            {
                connection.Open();

                Dictionary<int, string> res = new Dictionary<int, string>();

                string sql = @"SELECT Movie_ID, Name FROM Movie WHERE Name LIKE @SEARCHNAME COLLATE UTF8CI;";

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@SEARCHNAME", "%" + searchStr + "%");
                SQLiteDataReader r = command.ExecuteReader();
                while (r.Read())
                {
                    res.Add(Convert.ToInt32(r["Movie_ID"]), r["Name"].ToString());
                }
                r.Close();

                connection.Close();

                return res;
            }
        }

        public static Movie LoadMovie(int movieID)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=movies.sqlite;Version=3;"))
            {
                connection.Open();

                string sql = "SELECT Name, Description, Year, Age, Link, Studio_ID FROM Movie WHERE Movie_ID = @ID;";

                Movie movie;
                string name = "", description = "", link = "";
                int year = 0, age = 0, studioID = -1;

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", movieID);
                SQLiteDataReader r = command.ExecuteReader();
                while (r.Read()){
                    name = r["Name"].ToString();
                    description = r["Description"].ToString();
                    link = r["Link"].ToString();
                    year = Convert.ToInt32(r["Year"]);
                    age = Convert.ToInt32(r["Age"]);
                    studioID = Convert.ToInt32(r["Studio_ID"]);
                }
                r.Close();

                string studioName = "";

                sql = @"SELECT Name FROM Studio WHERE Studio_ID = @STUDIO_ID"; 

                command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@STUDIO_ID", studioID);
                r = command.ExecuteReader();
                while (r.Read())
                {
                    studioName = r["Name"].ToString();
                }
                r.Close();

                movie = new Movie(name, description, year, age, link, studioName, new List<Genre>(), new List<Image>(), new List<Role>());

                sql = @"SELECT Genre_ID, Name 
                        FROM Genre g
                        WHERE 
                            (SELECT COUNT(*)
                            FROM MOVIE_GENRE mg
                            WHERE (g.Genre_ID = mg.Genre_ID) AND (mg.Movie_ID = @MOVIE_ID)) > 0";

                command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@MOVIE_ID", movieID);
                r = command.ExecuteReader();
                while (r.Read())
                {
                    movie.Genres.Add(new Genre(Convert.ToInt32(r["Genre_ID"]), r["Name"].ToString()));
                }
                r.Close();

                sql = @"SELECT URL, Description 
                        FROM IMAGE 
                        WHERE MOVIE_ID = @MOVIE_ID";

                command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@MOVIE_ID", movieID);
                r = command.ExecuteReader();
                while (r.Read())
                {
                    movie.Images.Add(new Image(r["URL"].ToString(), r["Description"].ToString()));
                }
                r.Close();

                sql = @"SELECT Type_ID, Human_ID, Character 
                        FROM ROLE
                        WHERE Movie_ID = @MOVIE_ID";

                command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@MOVIE_ID", movieID);
                r = command.ExecuteReader();
                while (r.Read())
                {
                    int humanID = Convert.ToInt32(r["Human_ID"]);
                    Human human = null;

                    string queryHuman = "SELECT Name, Surname FROM Human WHERE Human_ID = @HUMAN_ID";
                    SQLiteCommand commandHuman = new SQLiteCommand(queryHuman, connection);
                    commandHuman.Parameters.AddWithValue("@HUMAN_ID", humanID);
                    SQLiteDataReader rHuman = commandHuman.ExecuteReader();
                    while (rHuman.Read())
                    {
                        human = new Human(rHuman["Name"].ToString(), rHuman["Surname"].ToString());
                    }
                    rHuman.Close();


                    RoleType type = RoleType.Actor;
                    switch (Convert.ToInt32(r["Type_ID"]))
                    {
                        case 1 : type = RoleType.Actor; break;
                        case 2 : type = RoleType.Writer; break;
                        case 3 : type = RoleType.Director; break;
                    }

                    Role role = new Role(human, type, movie);
                    if (type == RoleType.Actor)
                        role.Character = r["Character"].ToString();

                    movie.Roles.Add(role);
                }
                r.Close();

                connection.Close();

                return movie;
            }
        }

        public static void UpdateMovie(int movieID, Movie movie)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=movies.sqlite;Version=3;");
            
            connection.Open();

            string sql = @"DELETE FROM Image WHERE Movie_ID = @MOVIE_ID";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@MOVIE_ID", movieID);
            command.ExecuteNonQuery();

            sql = @"DELETE FROM Movie_Genre WHERE Movie_ID = @MOVIE_ID";
            command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@MOVIE_ID", movieID);
            command.ExecuteNonQuery();

            sql = @"DELETE FROM Role WHERE Movie_ID = @MOVIE_ID";
            command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@MOVIE_ID", movieID);
            command.ExecuteNonQuery();

            sql = @"DELETE FROM Movie WHERE Movie_ID = @MOVIE_ID";
            command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@MOVIE_ID", movieID);
            command.ExecuteNonQuery();

            connection.Close();

            InsertMovie(movie, movieID);
        }

        public static Dictionary<int, string> SearchMovies(string searchStr, int year1, int year2, int genreID, string studio)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=movies.sqlite;Version=3;"))
            {
                connection.Open();

                Dictionary<int, string> res = new Dictionary<int, string>();

                string sql = @"SELECT Movie_ID, Name FROM Movie m WHERE ((Year >= @YEAR1) AND (YEAR <= @YEAR2)";

                if (searchStr != "") sql += " AND (Name LIKE @SEARCHNAME)";
                if (studio != "") sql += " AND (@STUDIO IN (SELECT Name FROM Studio s WHERE s.Studio_ID = m.Studio_ID))";
                if (genreID > 0) sql += " AND (@GENRE_ID IN (SELECT Genre_ID FROM Movie_Genre mg WHERE mg.Movie_ID = m.Movie_ID))";

                sql += ") ORDER BY Name;";

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@YEAR1", year1);
                command.Parameters.AddWithValue("@YEAR2", year2);
                if (searchStr != "") command.Parameters.AddWithValue("@SEARCHNAME", "%" + searchStr + "%");
                if (studio != "") command.Parameters.AddWithValue("@STUDIO", studio);
                if (genreID > 0) command.Parameters.AddWithValue("@GENRE_ID", genreID);

                SQLiteDataReader r = command.ExecuteReader();
                while (r.Read())
                {
                    res.Add(Convert.ToInt32(r["Movie_ID"]), r["Name"].ToString());
                }
                r.Close();

                connection.Close();

                return res;
            }
        }
    }
}
