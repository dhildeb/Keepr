using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;
using Microsoft.AspNetCore.Mvc;

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

    public void ViewCount(int id, int views)
    {
      var sql = @"
      UPDATE keeps
      SET
      views = @views
      WHERE id = @id;";
      _db.Execute(sql, new { id, views });
    }

    public Keep GetById(int id)
    {
      var sql = @"SELECT * 
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.id = @id";
      return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { id }).FirstOrDefault();
    }

    internal void KeepCount(int id, int keeps)
    {
      var sql = @"
      UPDATE keeps
      SET
      keeps = @keeps
      WHERE id = @id;";
      _db.Execute(sql, new { id, keeps });
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
      name = @Name,
      description = @Description,
      tag = @Tag,
      img = @Img,
      views = @Views,
      shares = @Shares,
      keeps = @Keeps

      WHERE id = @Id LIMIT 1;
      ";
      return _db.Execute(sql, data);
    }

    internal ActionResult<List<Keep>> GetKeepsByProfileId(string id)
    {
      var sql = @"
      SELECT * FROM keeps
      WHERE creatorId = @id;
      ";
      return _db.Query<Keep>(sql, new { id }).ToList();
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