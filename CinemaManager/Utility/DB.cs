using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager.Utility
{
    public class DB
    {
        private SqlConnection sqlCnn = new SqlConnection();
        public bool IsDbOk;

        public DB()
        {
            try
            {
                if (sqlCnn.State == System.Data.ConnectionState.Closed)
                {
                    sqlCnn.ConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
                    sqlCnn.Open();
                    IsDbOk = true;
                }
            }
            catch (Exception ex)
            {
                IsDbOk = false;
                Console.WriteLine($"Errore nella Connessione al DB AdventureWorks2019: {ex.Message}");
                return;
            }
        }

        public void CreateCinemaTable()
        {
            try
            {
                string createTableQuery = "CREATE TABLE Cinema (ID INT IDENTITY(1,1) PRIMARY KEY, MovieName NVARCHAR(50) NOT NULL, Description NVARCHAR(150), Type NVARCHAR(35), MainActor NVARCHAR(40), PublicEvaluation INT, ReleaseDate DATETIME)";

                using (SqlCommand cmd = new SqlCommand(createTableQuery, sqlCnn))
                {
                    cmd.ExecuteNonQuery();
                }

                string insertQuery = "INSERT INTO Cinema (MovieName, Description, Type, MainActor, PublicEvaluation, ReleaseDate) VALUES (@MovieName1, @Description1, @Type1, @MainActor1, @PublicEvaluation1, @ReleaseDate1), (@MovieName2, @Description2, @Type2, @MainActor2, @PublicEvaluation2, @ReleaseDate2), (@MovieName3, @Description3, @Type3, @MainActor3, @PublicEvaluation3, @ReleaseDate3)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, sqlCnn))
                {

                    cmd.Parameters.AddWithValue("@MovieName1", "Matrix");
                    cmd.Parameters.AddWithValue("@Description1", "Film fantasy d'azione");
                    cmd.Parameters.AddWithValue("@Type1", "Fantasy");
                    cmd.Parameters.AddWithValue("@MainActor1", "Keanu Reeves");
                    cmd.Parameters.AddWithValue("@PublicEvaluation1", 9);
                    cmd.Parameters.AddWithValue("@ReleaseDate1", new DateTime(1999, 3, 31));

                    cmd.Parameters.AddWithValue("@MovieName2", "Quasi Amici");
                    cmd.Parameters.AddWithValue("@Description2", "Un ricco in carrozzina diventa amico del suo badante e vanno in giro");
                    cmd.Parameters.AddWithValue("@Type2", "Commedia");
                    cmd.Parameters.AddWithValue("@MainActor2", "Omar Sy");
                    cmd.Parameters.AddWithValue("@PublicEvaluation2", 7);
                    cmd.Parameters.AddWithValue("@ReleaseDate2", new DateTime(2012, 2, 24));

                    cmd.Parameters.AddWithValue("@MovieName3", "Delivery Man");
                    cmd.Parameters.AddWithValue("@Description3", "Fattorino di una macelleria con problemi finanziari che scopre di avere 300 figli che vogliono conoscerlo");
                    cmd.Parameters.AddWithValue("@Type3", "Commedia");
                    cmd.Parameters.AddWithValue("@MainActor3", "Vince Vaughn");
                    cmd.Parameters.AddWithValue("@PublicEvaluation3", 10);
                    cmd.Parameters.AddWithValue("@ReleaseDate3", new DateTime(2013, 2, 15));

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GetAdapterCinema()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Cinema", sqlCnn);
                adapter.Fill(ds, "Cinema");

                DataTable table = ds.Tables["Cinema"];

                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine("ID: " + row["ID"]);
                    Console.WriteLine("MovieName: " + row["MovieName"]);
                    Console.WriteLine("Description: " + row["Description"]);
                    Console.WriteLine("Type: " + row["Type"]);
                    Console.WriteLine("MainActor: " + row["MainActor"]);
                    Console.WriteLine("PublicEvaluation: " + row["PublicEvaluation"]);
                    Console.WriteLine("ReleaseDate: " + row["ReleaseDate"]);
                    Console.WriteLine();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GetAdapterCinema2()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT COLUMN_NAME, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM [AdventureWorks2019].INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Cinema'", sqlCnn);
                adapter.Fill(ds, "Cinema");

                DataTable table = ds.Tables["Cinema"];

                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        string columnName = column.ColumnName;
                        string columnValue = row[column]?.ToString();

                        if (!string.IsNullOrEmpty(columnValue))
                        {
                            Console.WriteLine($"{columnName}: {columnValue}");
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool CheckIfTableExists(string tableName)
        {
            string sqlQueryText = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName";

            using (SqlCommand cmd = new SqlCommand(sqlQueryText, sqlCnn))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                int tableCount = (int)cmd.ExecuteScalar();

                return tableCount > 0;
            }
        }


    }
}
