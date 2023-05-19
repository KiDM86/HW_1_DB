using Npgsql;
using static NHibernate.Engine.Query.CallableParser;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using NHibernate.Engine;
using System.Data;
using System.Linq.Expressions;
using System.Net.Mail;

namespace HW_2_DB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateUsersTable();
            CreateAdsTable();
            CreateOrdersTable();
            InsertUsers();
            InsertAds();
            InsertOrders();
            SelectAction();
        }


        const string connectionString = "Host = localhost;Port = 5432; Username = postgres;Password = password;Database = Avito";

        static void CreateUsersTable()
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var sql = @"
CREATE SEQUENCE users_id_seq;

CREATE TABLE users
(
   id           BIGINT                     NOT NULL       DEFAULT NEXTVAL('users_id_seq'),
   name         CHARACTER VARYING(255)     NOT NULL,
   rating       INTEGER                    NOT NULL,
   reviews      INTEGER,
   ads          INTEGER                    NOT NULL,
   email        CHARACTER VARYING(255)     NOT NULL,

CONSTRAINT users_pkey PRIMARY KEY (id),
CONSTRAINT users_email_unique UNIQUE (email)

);

CREATE INDEX users_name_idx ON users(name);
CREATE UNIQUE INDEX users_email_unq_idx ON users(lower(email));
";

            using var cmd = new NpgsqlCommand( sql, connection);

            var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

            Console.WriteLine($"Created USERS table. Affected Rows Count: {affectedRowsCount}");

        }

        static void CreateAdsTable()
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var sql = @"
CREATE SEQUENCE ads_id_seq;

CREATE TABLE ads
(
   id           BIGINT                      NOT NULL         DEFAULT NEXTVAL('ads_id_seq'),
   user_id      BIGINT                      NOT NULL,
   header       CHARACTER VARYING(255)      NOT NULL,
   price        BIGINT                      NOT NULL,
   adress       CHARACTER VARYING(255)      NOT NULL,
   views        INTEGER,
   likes        INTEGER,

CONSTRAINT ads_pkey PRIMARY KEY (id),
CONSTRAINT ads_fk_user_id FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);
";

            using var cmd = new NpgsqlCommand(sql,connection);

            var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

            Console.WriteLine($"Created ADS table. Affected Rows Count: {affectedRowsCount}");

        }

        static void CreateOrdersTable()
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var sql = @"
CREATE SEQUENCE orders_id_seq;

CREATE TABLE orders
(
   id           BIGINT                      NOT NULL           DEFAULT NEXTVAL('orders_id_seq'),
   user_id      BIGINT                      NOT NULL,
   date         CHARACTER VARYING(255)      NOT NULL,
   delivery     BOOLEAN,
   status       CHARACTER VARYING(255)      NOT NULL,


CONSTRAINT orders_pkey PRIMARY KEY (id),
CONSTRAINT orders_fk_ads_id FOREIGN KEY (user_id) REFERENCES ads(id) ON DELETE CASCADE
);
";

            using var cmd = new NpgsqlCommand(sql, connection);

            var affectedRowsCount = cmd.ExecuteNonQuery().ToString();

            Console.WriteLine($"Created ORDERS table. Affected Rows Count: {affectedRowsCount}");

        }

        static void InsertUsers()
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var sql = @"
INSERT INTO users (name, rating, reviews, ads, email)
VALUES (@name, @rating, @reviews, @ads, @email)
";

            using var cmd1 = new NpgsqlCommand(sql, connection);
            var parameters = cmd1.Parameters;
            parameters.Add(new NpgsqlParameter("name", "Nikolay"));
            parameters.Add(new NpgsqlParameter("rating", 3));
            parameters.Add(new NpgsqlParameter("reviews", 1));
            parameters.Add(new NpgsqlParameter("ads", 1));
            parameters.Add(new NpgsqlParameter("email", "nik87@gmail.com"));

            var affectedRowsCount = cmd1.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into USERS table. Affected Rows Count: {affectedRowsCount}");

            sql = @"
INSERT INTO users (name, rating, reviews, ads, email)
VALUES (@name, @rating, @reviews, @ads, @email)
";

            using var cmd2 = new NpgsqlCommand(sql, connection);
            parameters = cmd2.Parameters;
            parameters.Add(new NpgsqlParameter("name", "Ivan"));
            parameters.Add(new NpgsqlParameter("rating", 5));
            parameters.Add(new NpgsqlParameter("reviews", 10));
            parameters.Add(new NpgsqlParameter("ads", 7));
            parameters.Add(new NpgsqlParameter("email", "ivans@gmail.com"));

            affectedRowsCount = cmd2.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into USERS table. Affected Rows Count: {affectedRowsCount}");

            sql = @"
INSERT INTO users (name, rating, reviews, ads, email)
VALUES (@name, @rating, @reviews, @ads, @email)
";

            using var cmd3 = new NpgsqlCommand(sql, connection);
            parameters = cmd3.Parameters;
            parameters.Add(new NpgsqlParameter("name", "Sergey Korolev"));
            parameters.Add(new NpgsqlParameter("rating", 5));
            parameters.Add(new NpgsqlParameter("reviews", 4));
            parameters.Add(new NpgsqlParameter("ads", 5));
            parameters.Add(new NpgsqlParameter("email", "sergio55@mail.ru"));

            affectedRowsCount = cmd3.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into USERS table. Affected Rows Count: {affectedRowsCount}");

            sql = @"
INSERT INTO users (name, rating, reviews, ads, email)
VALUES (@name, @rating, @reviews, @ads, @email)
";
            using var cmd4 = new NpgsqlCommand(sql, connection);
            parameters = cmd4.Parameters;
            parameters.Add(new NpgsqlParameter("name", "Victor Goncharenko"));
            parameters.Add(new NpgsqlParameter("rating", 4));
            parameters.Add(new NpgsqlParameter("reviews", 3));
            parameters.Add(new NpgsqlParameter("ads", 1));
            parameters.Add(new NpgsqlParameter("email", "vigo77@gmail.com"));

            affectedRowsCount = cmd4.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into USERS table. Affected Rows Count: {affectedRowsCount}");

            sql = @"
INSERT INTO users (name, rating, reviews, ads, email)
VALUES (@name, @rating, @reviews, @ads, @email)
";
            using var cmd5 = new NpgsqlCommand(sql, connection);
            parameters = cmd5.Parameters;
            parameters.Add(new NpgsqlParameter("name", "GeorgyK"));
            parameters.Add(new NpgsqlParameter("rating", 3));
            parameters.Add(new NpgsqlParameter("reviews", 9));
            parameters.Add(new NpgsqlParameter("ads", 5));
            parameters.Add(new NpgsqlParameter("email", "gogak@list.ru"));

            affectedRowsCount = cmd5.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into USERS table. Affected Rows Count: {affectedRowsCount}");
        }

        static void InsertAds()
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var sql = @"
INSERT INTO ads (user_id, header, price, adress, views, likes)
VALUES (@user_id, @header, @price, @adress, @views, @likes)
";
            using var cmd1 = new NpgsqlCommand( sql, connection);
            var parameters = cmd1.Parameters;
            parameters.Add(new NpgsqlParameter("user_id", 1));
            parameters.Add(new NpgsqlParameter("header", "Sony PFR V-1 Headspeakers"));
            parameters.Add(new NpgsqlParameter("price", 8100));
            parameters.Add(new NpgsqlParameter("adress", "г.Москва, ул. Преображенский Вал, 9"));
            parameters.Add(new NpgsqlParameter("views", 21));
            parameters.Add(new NpgsqlParameter("likes", 15));

            sql = @"
INSERT INTO ads (user_id, header, price, adress, views, likes)
VALUES (@user_id, @header, @price, @adress, @views, @likes)
";
            var affectedRowsCount = cmd1.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into ADS table. Affected Rows Count: {affectedRowsCount}");

            using var cmd2 = new NpgsqlCommand(sql, connection);
            parameters = cmd2.Parameters;
            parameters.Add(new NpgsqlParameter("user_id", 2));
            parameters.Add(new NpgsqlParameter("header", "Колеса Dunloop 235/45 R17"));
            parameters.Add(new NpgsqlParameter("price", 12000));
            parameters.Add(new NpgsqlParameter("adress", "г.Санкт-Петербург, ул. Рубинштейна, 1"));
            parameters.Add(new NpgsqlParameter("views", 35));
            parameters.Add(new NpgsqlParameter("likes", 21));

            affectedRowsCount = cmd2.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into ADS table. Affected Rows Count: {affectedRowsCount}");

            sql = @"
INSERT INTO ads (user_id, header, price, adress, views, likes)
VALUES (@user_id, @header, @price, @adress, @views, @likes)
";
            using var cmd3 = new NpgsqlCommand(sql, connection);
            parameters = cmd3.Parameters;
            parameters.Add(new NpgsqlParameter("user_id", 2));
            parameters.Add(new NpgsqlParameter("header", "Toyota Tundra 2008 г.в."));
            parameters.Add(new NpgsqlParameter("price", 2300000));
            parameters.Add(new NpgsqlParameter("adress", "г.Санкт-Петербург, ул. Рубинштейна, 1"));
            parameters.Add(new NpgsqlParameter("views", 46));
            parameters.Add(new NpgsqlParameter("likes", 12));

            affectedRowsCount = cmd3.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into ADS table. Affected Rows Count: {affectedRowsCount}");

            sql = @"
INSERT INTO ads (user_id, header, price, adress, views, likes)
VALUES (@user_id, @header, @price, @adress, @views, @likes)
";
            using var cmd4 = new NpgsqlCommand(sql, connection);
            parameters = cmd4.Parameters;
            parameters.Add(new NpgsqlParameter("user_id", 2));
            parameters.Add(new NpgsqlParameter("header", "Сноуборд Burton Custom"));
            parameters.Add(new NpgsqlParameter("price", 35000));
            parameters.Add(new NpgsqlParameter("adress", "г.Санкт-Петербург, ул. Рубинштейна, 1"));
            parameters.Add(new NpgsqlParameter("views", 33));
            parameters.Add(new NpgsqlParameter("likes", 25));

            affectedRowsCount = cmd4.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into ADS table. Affected Rows Count: {affectedRowsCount}");

            sql = @"
INSERT INTO ads (user_id, header, price, adress, views, likes)
VALUES (@user_id, @header, @price, @adress, @views, @likes)
";
            using var cmd5 = new NpgsqlCommand(sql, connection);
            parameters = cmd5.Parameters;
            parameters.Add(new NpgsqlParameter("user_id", 3));
            parameters.Add(new NpgsqlParameter("header", "Casio G-Shock"));
            parameters.Add(new NpgsqlParameter("price", 15000));
            parameters.Add(new NpgsqlParameter("adress", "г.Пермь, ул. Новаторов, 14"));
            parameters.Add(new NpgsqlParameter("views", 5));
            parameters.Add(new NpgsqlParameter("likes", 10));

            affectedRowsCount = cmd5.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into ADS table. Affected Rows Count: {affectedRowsCount}");

            sql = @"
INSERT INTO ads (user_id, header, price, adress, views, likes)
VALUES (@user_id, @header, @price, @adress, @views, @likes)
";
            using var cmd6 = new NpgsqlCommand(sql, connection);
            parameters = cmd6.Parameters;
            parameters.Add(new NpgsqlParameter("user_id", 4));
            parameters.Add(new NpgsqlParameter("header", "Фонарь светододный Black Diamond"));
            parameters.Add(new NpgsqlParameter("price", 5000));
            parameters.Add(new NpgsqlParameter("adress", "г.Мытищи, Олимпийский проспект, 87"));
            parameters.Add(new NpgsqlParameter("views", 13));
            parameters.Add(new NpgsqlParameter("likes", 4));

            affectedRowsCount = cmd6.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into ADS table. Affected Rows Count: {affectedRowsCount}");

            sql = @"
INSERT INTO ads (user_id, header, price, adress, views, likes)
VALUES (@user_id, @header, @price, @adress, @views, @likes)
";
            using var cmd7 = new NpgsqlCommand(sql, connection);
            parameters = cmd7.Parameters;
            parameters.Add(new NpgsqlParameter("user_id", 5));
            parameters.Add(new NpgsqlParameter("header", "Kawasaki Vulcan V800"));
            parameters.Add(new NpgsqlParameter("price", 650000));
            parameters.Add(new NpgsqlParameter("adress", "г.Москва, Сиреневый бульвар, 12"));
            parameters.Add(new NpgsqlParameter("views", 120));
            parameters.Add(new NpgsqlParameter("likes", 44));

            affectedRowsCount = cmd7.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into ADS table. Affected Rows Count: {affectedRowsCount}");

        }

        static void InsertOrders()
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var sql = @"
INSERT INTO orders (user_id, date, delivery, status)
VALUES (@user_id, @date, @delivery, @status)
";
            using var cmd1 = new NpgsqlCommand(sql, connection);
            var parameters = cmd1.Parameters;
            parameters.Add(new NpgsqlParameter("user_id", 1));
            parameters.Add(new NpgsqlParameter("date", "16-03-2023"));
            parameters.Add(new NpgsqlParameter("delivery", true));
            parameters.Add(new NpgsqlParameter("status", "Active"));

            var affectedRowsCount = cmd1.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into ORDERS table. Affected Rows Count: {affectedRowsCount}");

            sql = @"
INSERT INTO orders (user_id, date, delivery, status)
VALUES (@user_id, @date, @delivery, @status)
";
            using var cmd2 = new NpgsqlCommand(sql, connection);
            parameters = cmd2.Parameters;
            parameters.Add(new NpgsqlParameter("user_id", 2));
            parameters.Add(new NpgsqlParameter("date", "11-04-2023"));
            parameters.Add(new NpgsqlParameter("delivery", true));
            parameters.Add(new NpgsqlParameter("status", "Done"));

            affectedRowsCount = cmd2.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into ORDERS table. Affected Rows Count: {affectedRowsCount}");


            sql = @"
INSERT INTO orders (user_id, date, delivery, status)
VALUES (@user_id, @date, @delivery, @status)
";
            using var cmd3 = new NpgsqlCommand(sql, connection);
            parameters = cmd3.Parameters;
            parameters.Add(new NpgsqlParameter("user_id", 3));
            parameters.Add(new NpgsqlParameter("date", "10-01-2022"));
            parameters.Add(new NpgsqlParameter("delivery", false));
            parameters.Add(new NpgsqlParameter("status", "Active"));

            affectedRowsCount = cmd3.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into ORDERS table. Affected Rows Count: {affectedRowsCount}");

            sql = @"
INSERT INTO orders (user_id, date, delivery, status)
VALUES (@user_id, @date, @delivery, @status)
";
            using var cmd4 = new NpgsqlCommand(sql, connection);
            parameters = cmd4.Parameters;
            parameters.Add(new NpgsqlParameter("user_id", 4));
            parameters.Add(new NpgsqlParameter("date", "05-02-2023"));
            parameters.Add(new NpgsqlParameter("delivery", false));
            parameters.Add(new NpgsqlParameter("status", "Done"));

            affectedRowsCount = cmd4.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into ORDERS table. Affected Rows Count: {affectedRowsCount}");

            sql = @"
INSERT INTO orders (user_id, date, delivery, status)
VALUES (@user_id, @date, @delivery, @status)
";
            using var cmd5 = new NpgsqlCommand(sql, connection);
            parameters = cmd5.Parameters;
            parameters.Add(new NpgsqlParameter("user_id", 4));
            parameters.Add(new NpgsqlParameter("date", "01-05-2021"));
            parameters.Add(new NpgsqlParameter("delivery", true));
            parameters.Add(new NpgsqlParameter("status", "Active"));

            affectedRowsCount = cmd5.ExecuteNonQuery().ToString();
            Console.WriteLine($"Insert into ORDERS table. Affected Rows Count: {affectedRowsCount}");
        }

        static void ViewAll()
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var sql = @"
SELECT id, name, rating, reviews, ads, email FROM users
";
            using var cmd1 = new NpgsqlCommand(sql, connection);
            var parameters = cmd1.Parameters;

            var reader = cmd1.ExecuteReader();
            Console.WriteLine("USERS:");
            while (reader.Read())
            {
                var id = reader.GetInt64(0);
                var name = reader.GetString(1);
                var rating = reader.GetInt64(2);
                var reviews = reader.GetInt64(3);
                var ads = reader.GetInt64(4);
                var email = reader.GetString(5);

                Console.WriteLine($"[id = {id}, name = {name}, rating = {rating}, reviews = {reviews}, ads = {ads}, email = {email}]");
            }

            reader.Close();
            Console.WriteLine();

            sql = @"
SELECT id, user_id, header, price, adress, views,likes FROM ads
";

            using var cmd2 = new NpgsqlCommand(sql, connection);
            parameters = cmd2.Parameters;

            reader = cmd2.ExecuteReader();
            Console.WriteLine("ADS:");
            while (reader.Read())
            {
                var id = reader.GetInt64(0);
                var user_id = reader.GetInt64(1);
                var header = reader.GetString(2);
                var price = reader.GetInt64(3);
                var adress = reader.GetString(4);
                var views = reader.GetInt64(5);
                var likes = reader.GetInt64(6);

                Console.WriteLine($"[id = {id}, user_id = {user_id}, header = {header}, price = {price}, adress = {adress}, views = {views}, likes = {likes}]");
            }

            reader.Close();
            Console.WriteLine();

            sql = @"
SELECT id, user_id, date, delivery, status FROM orders
";

            using var cmd3 = new NpgsqlCommand(sql, connection);
            parameters = cmd3.Parameters;

            reader = cmd3.ExecuteReader();
            Console.WriteLine("ORDERS:");

            while (reader.Read())
            {
                var id = reader.GetInt64(0);
                var user_id = reader.GetInt64(1);
                var date = reader.GetString(2);
                var delivery = reader.GetBoolean(3);
                var status = reader.GetString(4);

                Console.WriteLine($"[id = {id}, user_id = {user_id}, date = {date}, delivery = {delivery}, status = {status}]");
            }

            reader.Close();

        }
        
        //выбор действий пользователем
        static void SelectAction()
        {
            bool press0 = false;
            
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
           
            while (press0 == false)
            {
                Console.WriteLine("Что Вы хотите сделать?");
                Console.WriteLine("0 - Закончить работу");
                Console.WriteLine("1 - Добавить в таблицу USERS");
                Console.WriteLine("2 - Добавить в таблицу ADS");
                Console.WriteLine("3 - Добавить в таблицу ORDERS");
                Console.WriteLine("4 - Вывести содержимое всех таблиц AVITO");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D0:
                        press0 = true;
                        break;

                    case ConsoleKey.D1:
                        Console.WriteLine();
                        Console.WriteLine("Введите имя:"); 
                        string name = Console.ReadLine();
                        int rating = GetInt("Введите рейтинг(целое число):");
                        int reviews = GetInt("Введите кол-во отзывов(целое число):");
                        int ads = GetInt("Введите кол-во объявлений(целое число):");
                        Console.WriteLine("Введите email:");
                        string email = Console.ReadLine();

                        var sql = @"
INSERT INTO users (name, rating, reviews, ads, email)
VALUES (@name, @rating, @reviews, @ads, @email)
";

                        var cmd1 = new NpgsqlCommand(sql, connection);
                        var parameters = cmd1.Parameters;
                        parameters.Add(new NpgsqlParameter("name", name));
                        parameters.Add(new NpgsqlParameter("rating", rating));
                        parameters.Add(new NpgsqlParameter("reviews", reviews));
                        parameters.Add(new NpgsqlParameter("ads", ads));
                        parameters.Add(new NpgsqlParameter("email", email));

                        var affectedRowsCount = cmd1.ExecuteNonQuery().ToString();
                        Console.WriteLine($"Insert into USERS table. Affected Rows Count: {affectedRowsCount}");
                        Console.WriteLine($"Добавлено в USERS:  [name = {name}, rating = {rating}, rewiews = {reviews}, ads = {ads}, email = {email}]");
                        break;

                    case ConsoleKey.D2:
                        Console.WriteLine();
                        int user_id = GetInt("Введите id пользователя:");
                        Console.WriteLine("Введите заголовок объявления:");
                        string header = Console.ReadLine();
                        int price = GetInt("Введите стоимость(целое число):");
                        Console.WriteLine("Введите адрес:");
                        string adress = Console.ReadLine();
                        int views = GetInt("Кол-во просмотров(целое число):");
                        int likes = GetInt("Кол-во лайков(целое число):");

                        sql = @"
INSERT INTO ads (user_id, header, price, adress, views, likes)
VALUES (@user_id, @header, @price, @adress, @views, @likes)
";

                        var cmd2 = new NpgsqlCommand(sql, connection);
                        parameters = cmd2.Parameters;
                        parameters.Add(new NpgsqlParameter("user_id", user_id));
                        parameters.Add(new NpgsqlParameter("header", header));
                        parameters.Add(new NpgsqlParameter("price", price));
                        parameters.Add(new NpgsqlParameter("adress", adress));
                        parameters.Add(new NpgsqlParameter("views", views));
                        parameters.Add(new NpgsqlParameter("likes", likes));

                        affectedRowsCount = cmd2.ExecuteNonQuery().ToString();
                        Console.WriteLine($"Insert into ADS table. Affected Rows Count: {affectedRowsCount}");
                        Console.WriteLine($"Добавлено в ADS:  [user_id = {user_id}, header = {header}, price = {price}, adress = {adress}, views = {views}, likes = {likes} ]");
                        break;

                    case ConsoleKey.D3:
                        Console.WriteLine();
                        user_id = GetInt("Введите id пользователя:");
                        Console.WriteLine("Введите дату:");
                        string date = Console.ReadLine();
                        bool delivery = GetBool("Наличие доставки(True/False):");
                        Console.WriteLine("Введите статус(Active/Done):");
                        string status = Console.ReadLine();


                        sql = @"
INSERT INTO orders (user_id, date, delivery, status)
VALUES (@user_id, @date, @delivery, @status)
";

                        var cmd3 = new NpgsqlCommand(sql, connection);
                        parameters = cmd3.Parameters;
                        parameters.Add(new NpgsqlParameter("user_id", user_id));
                        parameters.Add(new NpgsqlParameter("date", date));
                        parameters.Add(new NpgsqlParameter("delivery", delivery));
                        parameters.Add(new NpgsqlParameter("status", status));

                        affectedRowsCount = cmd3.ExecuteNonQuery().ToString();
                        Console.WriteLine($"Insert into ORDERS table. Affected Rows Count: {affectedRowsCount}");
                        Console.WriteLine($"Добавлено в ORDERS:  [user_id = {user_id}, date = {date}, delivery = {delivery}, status = {status} ]");
                        break;

                    case ConsoleKey.D4:
                        Console.WriteLine();
                        ViewAll();
                        break;

                }

            }

        }
        //получение целочисленных значений
        static int GetInt(string quest)
        {
            int retVal;

            while (true)
            {
                Console.WriteLine(quest);

                if (int.TryParse(Console.ReadLine(), out retVal))
                {
                    return retVal;
                }
                else
                {
                    Console.WriteLine("Значение должно быть целым!");
                }
            }
        }

        //получение булевых значений
        static bool GetBool(string quest)
        {
            bool retVal;

            while (true)
            {
                Console.WriteLine(quest);

                if (bool.TryParse(Console.ReadLine(), out retVal))
                {
                    return retVal;
                }
                else
                {
                    Console.WriteLine("Значение должно быть True или False!");
                }
            }
        }

    }
}


