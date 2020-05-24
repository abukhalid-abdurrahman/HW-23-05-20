using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Practics
{
    class ModelRepository<T>
    {
        private string connectionString = "Data Source=localhost;Initial catalog=Faridun;Integrated Security=True";
        private string tableName;
        public ModelRepository(string tableName)
        {
            this.tableName = tableName;
        }
        public List<T> Read()
        {
            List<T> model = new List<T>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = $"select * from {tableName}";
                model = db.Query<T>(sqlQuery).ToList();
            }
            return model;
        }
        public T SelectById(int? Id)
        {
            T model;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = $"select * from {tableName} where Id = @Id";
                model = db.Query<T>(sqlQuery, new { Id }).FirstOrDefault();
            }
            return model;
        }
        public T SelectByParam(string param)
        {
            T model;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = $"select * from {tableName} where {param}";
                model = db.Query<T>(sqlQuery).FirstOrDefault();
            }
            return model;
        }
        public int Create(T model, string columns, string values)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"insert into {tableName} ({columns}) values({values}); select cast(SCOPE_IDENTITY() as int)";
                int newPersonId = db.Query<int>(sqlQuery, model).FirstOrDefault();
                return newPersonId;
            }
        }
    }
}
