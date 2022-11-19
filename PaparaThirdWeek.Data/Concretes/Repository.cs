using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Data.Context;
using PaparaThirdWeek.Domain;
using PaparaThirdWeek.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Data.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly PaparaAppDbContext _context;
        public Repository(PaparaAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAll()
        {
            var sql = "select * from Companies";
            using (var connection = new SqlConnection(_context.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<T>(sql);
                return result.ToList();
            }
        }

        public async Task<T> GetById(int id)
        {
            var sql = "select * from Companies where Id=@Id";
            string temp = _context.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(temp))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<T>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> Add(T entity)
        {
            var sql = @"INSERT INTO [dbo].[Companies]
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
     VALUES(@Name, @Adress,@City,@TaxNumber,@Email,@IsDeleted,@CreatedDate,@CreatedBy,@LastUpdateAt,@LastUpdateBy)";
            using (var connection = new SqlConnection(_context.GetConnectionString("DefaultConnection")))
            //connectionstring'ten connectionu çağırıdm 
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = "delete from Companies where Id=@Id";
            using (var connection = new SqlConnection(_context.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> Update(T entity, int id)
        {
            var sql = "update Companies set Name=@Name, Adress=@Adress,City=@City,TaxNumber=@TaxNumber,Email=@Email,IsDeleted=@IsDeleted,CreatedDate=@CreatedDate,CreatedBy=@CreatedBy,LastUpdateAt=@LastUpdateAt,LastUpdateBy=@LastUpdateBy";
            using (var connection = new SqlConnection(_context.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;

            }
        }
    }
}
