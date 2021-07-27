using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    public VaultKeep Create(VaultKeep data)
    {
      var sql = @"
      INSERT INTO vault_keeps (creatorId, vaultId, keepId) 
      VALUES (@CreatorId, @VaultId, @KeepId); 
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public int Delete(int id)
    {
      var sql = @"
      DELETE FROM vault_keeps
      WHERE id = @id
      LIMIT 1;
      ";
      return _db.Execute(sql, new { id });
    }

    public VaultKeep GetById(int id)
    {
      var sql = @"SELECT * FROM vault_keeps WHERE id = @id;";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    public List<VaultKeepView> GetKeepsByVaultId(int vaultId)
    {
      var sql = @"SELECT 
      k.*,
      vk.id AS vaultKeepId,
      vk.vaultId,
      vk.keepId,
      a.* 
    FROM vault_keeps vk
    JOIN keeps k ON k.id = vk.keepId
    JOIN accounts a ON a.id = k.creatorId
    WHERE vk.vaultId = @vaultId";
      return _db.Query<VaultKeepView, Account, VaultKeepView>(sql, (vkv, a) =>
      {
        vkv.Creator = a;
        return vkv;
      }, new { vaultId }).ToList();
    }
    // public List<VaultKeep> GetvaultsByKeepId(int keepId)
    // {
    //   var sql = @"SELECT * 
    // FROM vault_keep vk
    // JOIN vaults v ON v.id = vk.vaultId
    // JOIN accounts a ON a.id = v.creatorId
    // WHERE vk.keepId = @keepId";
    //   return _db.Query<VaultKeep, Vault, Account, VaultKeep>(sql, (vk, v, a) =>
    //   {
    //     vk.Vaults = v;
    //     v.Creator = a;
    //     return vk;
    //   }, new { keepId }).ToList();
    // }

  }

}