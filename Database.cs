using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public static class Database
    {
       private static string dbName = $"{ Environment.CurrentDirectory }\\App_Data\\Database1.mdf";

        //Microsoft SQL
        public static DataTable SendQuery(string query)
        {
            //сервер
            //SqlConnection sqlConnection = new SqlConnection("Server = localhost;Database = u1236046_1234;User = u1236046_user;Password = Almaty2000;");

            //файл
            SqlConnection sqlConnection = new SqlConnection($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={dbName};Integrated Security=True");

            sqlConnection.Open();

            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.CommandTimeout = 60;
            SqlDataAdapter chitat1 = new SqlDataAdapter(command);

            DataTable resultat = new DataTable();

            chitat1.Fill(resultat);

            sqlConnection.Close();

            return resultat;
        }

        //mysql
        //public static DataTable SendQuery(string query)
        //{
        //    MySqlConnection sqlConnection = new MySqlConnection("Server = localhost;Database = u1236046_1234;User = root;Password = root;");

        //    sqlConnection.Open();

        //    MySqlCommand command = new MySqlCommand(query, sqlConnection);
        //    command.CommandTimeout = 60;
        //    MySqlDataAdapter chitat1 = new MySqlDataAdapter(command);

        //    DataTable resultat = new DataTable();

        //    chitat1.Fill(resultat);

        //    sqlConnection.Close();

        //    return resultat;
        //}

        public static void ClearTable(string name) => SendQuery($"Delete from {name};");
    }
}
