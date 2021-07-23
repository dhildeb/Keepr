using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    public List<Keep> GetAll()
    {
      var sql = @"SELECT * 
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      ";
      return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }).ToList();
    }
    public Keep GetById(int id)
    {
      var sql = @"SELECT * 
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE id = @id";
      return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { id }).FirstOrDefault();
    }
    public Keep Create(Keep data)
    {
      var sql = @"
      INSERT INTO keeps(creatorId, name, description, tag, img, views, shares, keeps) 
      VALUES(@CreatorId, @Name, @Description, @Tag, @Img, @Views, @Shares, @Keeps); 
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }
    public int Update(Keep data)
    {
      string sql = @"
      UPDATE keeps
      SET
      creatorId = @CreatorId,
      name = @Name,
      description = @Description,
      tag = @Tag,
      img = @Img,
      views = @Views,
      shares = @Shares,
      keeps = @Keeps

      WHERE id = @Id;
      ";
      return _db.Execute(sql, data);
    }
    public int Delete(int id)
    {
      string sql = @"
      DELETE FROM keeps
      WHERE id = @id
      ";
      return _db.Execute(sql, new { id });
    }
  }

}