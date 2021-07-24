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
    public Account GetById(string id)
    {
      var sql = @"SELECT * FROM accounts WHERE id = @id";
      return _db.QueryFirstOrDefault<Account>(sql, new { id });
    }
    public int Update(Account data)
    {
      string sql = @"
      UPDATE accounts
      SET
      name = @Name,
      email = @Email,
      picture = @Picture
      WHERE id = @Id;
      ";
      return _db.Execute(sql, data);
    }
    public int Delete(string id)
    {
      string sql = @"
      DELETE FROM accounts
      WHERE id = @id
      ";
      return _db.Execute(sql, new { id });
    }
  }

}