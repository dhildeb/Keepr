using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    public List<Vault> GetAll()
    {
      var sql = @"SELECT * 
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      ";
      return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }).ToList();
    }
    public Vault GetById(int id)
    {
      var sql = @"SELECT 
      v.*, 
      a.*
      FROM vaults v 
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.id = @id";
      return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { id }).FirstOrDefault();
    }
    public int Create(Vault data)
    {
      var sql = @"
      INSERT INTO vaults (creatorId, name, description, isPrivate) 
      VALUES (@CreatorId, @Name, @Description, @IsPrivate); 
      SELECT LAST_INSERT_ID();
      ";
      return _db.ExecuteScalar<int>(sql, data);
    }
    public int Update(Vault data)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @Name,
      description = @Description
      WHERE id = @Id;
      ";
      return _db.Execute(sql, data);
    }
    public int Delete(int id)
    {
      string sql = @"
      DELETE FROM vaults
      WHERE id = @id
      ";
      return _db.Execute(sql, new { id });
    }

    public ActionResult<List<Vault>> GetVaultsByProfileId(string id)
    {
      var sql = @"
      SELECT * FROM vaults
      WHERE creatorId = @id AND isPrivate = 0;
      ";
      return _db.Query<Vault>(sql, new { id }).ToList();
    }

    public ActionResult<List<Vault>> GetVaultsByUserId(string id)
    {
      var sql = @"
      SELECT * FROM vaults
      WHERE creatorId = @id;
      ";
      return _db.Query<Vault>(sql, new { id }).ToList();
    }
  }

}