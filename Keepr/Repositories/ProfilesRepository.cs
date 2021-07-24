using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    public List<Account> GetAll()
    {
      var sql = "SELECT * FROM accounts";
      return _db.Query<Account>(sql).ToList();
    }
    public Account GetById(int id)
    {
      var sql = @"SELECT * FROM accounts WHERE id = @id";
      return _db.QueryFirstOrDefault<Account>(sql, new { id });
    }
    public Account Create(Account data)
    {
      // add or change values as needed REVIEW
      var sql = @"
      INSERT INTO accounts (name, count, completed) 
      VALUES (@Name, @Count, @Completed); 
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }
    public int Update(Account data)
    {
      // add or change values as needed REVIEW
      string sql = @"
      UPDATE accounts
      SET
      name = @Name,
      count = @Count,
      completed = @Completed
      WHERE id = @Id;
      ";
      return _db.Execute(sql, data);
    }
    public int Delete(int id)
    {
      string sql = @"
      DELETE FROM accounts
      WHERE id = @id
      ";
      return _db.Execute(sql, new { id });
    }
  }

}