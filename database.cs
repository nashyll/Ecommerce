using System;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;




namespace SqlServerSample
{

    public sealed class SqlConnectionStringBuilder : System.Data.Common.DbConnectionStringBuilder
    {
        public SqlConnectionStringBuilder(string connectStr)
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Connecting to SQL Server and Creating a registration table... ");

                //Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(GetConnectionString());
                builder.Values
                builder.DataSource = "localhost";   // update me
                builder.UserID = "Nash";              // update me
                builder.Password = "Shan";      // update me
                builder.InitialCatalog = "master";

                // Connect to SQL
                Console.Write("Connecting to SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");

                    // Create a sample database

                    String sql = "DROP DATABASE IF EXISTS [registrationDB]; CREATE DATABASE [registrationDB]";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Done.");
                    }

                    // Create a Table and insert some sample data

                    Console.ReadKey(true);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("USE registrationDB; ");
                    sb.Append("CREATE TABLE Employees ( ");
                    sb.Append(" EmpId INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    sb.Append(" EmpName NVARCHAR(50) NOT NULL, ");
                    sb.Append(" EmpAddress NVARCHAR(50) NOT NULL, ");
                    sb.Append(" EmpCell NVARCHAR(10) NOT NULL, ");
                    sb.Append(" empEmail NVARCHAR(50) NOT NULL,");
                    sb.Append(" EmpPass NVARCHAR(10) NOT NULL, ");
                    sb.Append("); ");
                    sb.Append(" ");
                    sb.Append(" CustId INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    sb.Append(" CustName NVARCHAR(50) NOT NULL, ");
                    sb.Append(" CustAddress NVARCHAR(50) NOT NULL, ");
                    sb.Append(" CustCell NVACREATE TABLE Customers (RCHAR(10) NOT NULL, ");
                    sb.Append(" CustEmail NVARCHAR(50) NOT NULL,");
                    sb.Append(" CustPass NVARCHAR(10) NOT NULL, ");
                    sb.Append("); ");
                    sb.Append("CREATE TABLE Products ( ");
                    sb.Append(" ProdId INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    sb.Append(" ProdName NVARCHAR(50) NOT NULL, ");
                    sb.Append(" ProdPrice FLOAT NOT NULL, ");
                    sb.Append(" ProdCat NVARCHAR(10) NOT NULL, ");
                    sb.Append("); ");
                    sb.Append("CREATE TABLE Sales ( ");
                    sb.Append(" SalesId INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    sb.Append(" SalesDate DATE NOT NULL, ");
                    sb.Append(" EmpId INT IDENTITY(1,1) NOT NULL,FOREIGN KEY ");
                    sb.Append(" CustId INT IDENTITY(1,1) NOT NULL,FOREIGN KEY ");
                    sb.Append(" ProdId INT IDENTITY(1,1) NOT NULL,FOREIGN KEY ");
                    sb.Append("); ");
                    sb.Append("INSERT INTO Employees (Id, Name, Address, Cellphone, Email, Password) VALUES ");
                    sb.Append("(9735486762346, N'Emmanuel', N'2 goodwill drive', '0825435675', 'eman@mail.com','Andy'), ");
                    sb.Append("(9123456778912, N'Shanice', N'4 salty drive', '0712291275', 'shan@mail.com','Nashyll'), ");
                    sb.Append("(9654345678906, N'Nash', N'9 varsity drive', '0524367896', 'nash@mail.com','Nashyll123'), ");
                    sb.Append("INSERT INTO Customers (Id, Name, Address, Cellphone, Email, Password) VALUES ");
                    sb.Append("(9734573987656, N'Lee', N'37 flower road', '0825577783', 'lee@mail.com','Password1'), ");
                    sb.Append("(9364723849753, N'Kiann', N'10 glenashley drive', '0425739846', 'ked@mail.com','rocco45'), ");
                    sb.Append("(9764532567532, N'Revel', N'45 clark road', '0789678964', 'revel@mail.com','tazz'), ");
                    sb.Append("INSERT INTO Products (Id, Name, Price, Catergory) VALUES ");
                    sb.Append("(1, N'BMW M3 Front Brake pads', 1500, N'Brakes'), ");
                    sb.Append("(2, N'BMW M3 Rear Brake pads', 1500, N'Brakes'), ");
                    sb.Append("(3, N'Audi TT Front Brake pads', 1150, N'Brakes'), ");
                    sb.Append("(4, N'Audi TT Rear Brake pads', 1150, N'Brakes'), ");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Done.");
                    }

                    // INSERT demo
                    Console.Write("Inserting a new row into table, press any key to continue...");
                    Console.ReadKey(true);
                    sb.Clear();
                    sb.Append("INSERT Products (Id, Name, Price, Catergory) ");
                    sb.Append("VALUES (@Id, @Name, @Price, @Catergory);");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", "Jake");
                        command.Parameters.AddWithValue("@location", "United States");
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) inserted");
                    }

                    // UPDATE demo
                    String userToUpdate = "Nikita";
                    Console.Write("Updating 'Location' for user '" + userToUpdate + "', press any key to continue...");
                    Console.ReadKey(true);
                    sb.Clear();
                    sb.Append("UPDATE Employees SET Location = N'United States' WHERE Name = @name");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userToUpdate);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) updated");
                    }

                    // DELETE demo
                    String userToDelete = "Jared";
                    Console.Write("Deleting user '" + userToDelete + "', press any key to continue...");
                    Console.ReadKey(true);
                    sb.Clear();
                    sb.Append("DELETE FROM Employees WHERE Name = @name;");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userToDelete);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) deleted");
                    }

                    // READ demo
                    Console.WriteLine("Reading data from table, press any key to continue...");
                    Console.ReadKey(true);
                    sql = "SELECT Id, Name, Location FROM Employees;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
        }

        private static string GetConnectionString()
        {
            // To avoid storing the connection string in your code,
            // you can retrieve it from a configuration file.
            return "Server=(local);Integrated Security=SSPI;" +
                "Initial Catalog=AdventureWorks";
        }
    }
}
