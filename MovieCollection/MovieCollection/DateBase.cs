using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

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

        public static string ToSQLStr(string str)
        {
            if (str != null)
                return "'" + str + "'";
            return "NULL";
        }

        public static int InsertMovie(Movie movie)
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

                string sql = @"SELECT COUNT(*) FROM Movie;";

                SQLiteCommand command = new SQLiteCommand(sql, connection);

                int id = Convert.ToInt32(command.ExecuteScalar()) + 1;

                sql = @"INSERT INTO Movie (Movie_ID, Name, Description, Year, Age, Link, Studio_ID) VALUES " +
                    string.Format("({0}, {1}, {2}, {3}, {4}, {5}, {6});", id, ToSQLStr(movie.Name), ToSQLStr(movie.Description), movie.Year, movie.Age,
                                   ToSQLStr(movie.Link), (studioID == -1) ? "NULL" : studioID.ToString() );

                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                // Insert into MOVIE_GENRE

                foreach (Genre g in movie.Genres)
                {
                    command = new SQLiteCommand(sql, connection);
                    sql = @"INSERT INTO Movie_Genre (Movie_ID, Genre_ID) VALUES " + string.Format("({0}, {1});", id, g.GenreID);
                    command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();
                }

                // Insert into ROLE + HUMAN

                foreach (Role r in movie.Roles)
                {
                    command = new SQLiteCommand(sql, connection);

                    int humanID = GetHumanID(r.Human);
                    if (humanID == -1)
                        humanID = InsertHuman(r.Human);

                    if (r.Type != RoleType.Actor)
                        sql = @"INSERT INTO Role (Movie_ID, Type_ID, Human_ID) VALUES " + string.Format("({0}, {1}, {2});", id, (int)r.Type, humanID);
                    else
                        sql = @"INSERT INTO Role (Movie_ID, Type_ID, Human_ID, Character) VALUES " 
                            + string.Format("({0}, {1}, {2}, {3});", id, (int)r.Type, humanID, ToSQLStr(r.Character));

                    command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();
                }

                // Insert into IMAGE

                foreach (Image img in movie.Images)
                {
                    InsertImage(img, id);
                }

                connection.Close();
                return id;
            }
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

                string sql = string.Format("SELECT Studio_ID FROM Studio WHERE (Name = {0});", ToSQLStr(name));

                SQLiteCommand command = new SQLiteCommand(sql, connection);
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

                sql = @"INSERT INTO Studio (Studio_ID, Name) VALUES " + string.Format("({0}, {1});", id, ToSQLStr(name));

                command = new SQLiteCommand(sql, connection);
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

                string sql = string.Format("SELECT Human_ID FROM Human WHERE (Name = {0}) AND (Surname = {1});", ToSQLStr(h.Name), ToSQLStr(h.Surname));

                SQLiteCommand command = new SQLiteCommand(sql, connection);
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

                sql = @"INSERT INTO Human (Human_ID, Name, Surname) VALUES " + string.Format("({0}, {1}, {2});", id, ToSQLStr(h.Name), ToSQLStr(h.Surname));

                command = new SQLiteCommand(sql, connection);
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

                string sql = @"SELECT COUNT(*) FROM Image;";

                SQLiteCommand command = new SQLiteCommand(sql, connection);

                int id = Convert.ToInt32(command.ExecuteScalar()) + 1;

                sql = @"INSERT INTO Image (Image_ID, URL, Description, Movie_ID) VALUES " 
                    + string.Format("({0}, {1}, {2}, {3});", id, ToSQLStr(img.URL), ToSQLStr(img.Description), movieID);

                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                connection.Close();

                return id;
            }
        }
    }
}
