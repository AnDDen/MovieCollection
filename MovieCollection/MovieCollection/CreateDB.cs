using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace MovieCollection
{
    public static class DataBase
    {
        private const string PATH = "movies.sqlite";
        
        public static void Create()
        {
            SQLiteConnection.CreateFile(PATH);
            SQLiteConnection connection;
            connection = new SQLiteConnection("Data Source=movies.sqlite;Version=3;");
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
}
