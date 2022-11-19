using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Four.Week.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDbConnection con = new SqlConnection("Server=BOSLT253;Database=PaparaTestDb;Trusted_Connection=True;");
            con.Open();
            con.Execute(@"INSERT INTO [dbo].[Companies]
           ([Name]
           ,[Adress]
           ,[City]
           ,[TaxNumber]
           ,[Email]
           ,[IsDeleted]
           ,[CreatedDate]
           ,[CreatedBy]
           ,[LastUpdateAt]
           ,[LastUpdateBy])
            VALUES(@Name, 
             @Adress,
             @City, 
             @TaxNumber, 
             @Email, 
             @IsDeleted,
             @CreatedDate, 
             @CreatedBy, 
             @LastUpdateAt, @LastUpdateBy)", new Company
            {
                Name = "Google",
                Adress = "Los Angeles",
                City = "USA",
                CreatedBy = "DapperUser",
                CreatedDate = DateTime.Now,
                Email = "support@gmail.com",
                IsDeleted = false,
                TaxNumber = "+24454534534"
            });

            var companyList = con.Query<Company>("select * from Companies")
                .ToList();
            foreach (var item in companyList)
            {
                Console.WriteLine(item.Name);
            }

            con.Close();
        }
    }
}
