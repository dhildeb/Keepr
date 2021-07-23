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
      INSERT INTO vault_keeps (vaultId, keepId) 
      VALUES (@VaultId, @KeepId); 
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }
    public int Delete(VaultKeep data)
    {
      var sql = @"
      DELETE FROM Vault_Keeps
      WHERE vaultId = @VaultId AND keepId = @KeepId 
      ";
      return _db.Execute(sql, data);
    }
    public List<VaultKeep> GetKeepsByVaultId(int vaultId)
    {
      //join profile TODO
      var sql = @"SELECT * 
    FROM vault_keep vk
    Join keeps k ON k.id = vk.keepId
    WHERE vaultId = @vaultId";
      return _db.Query<VaultKeep, Keep, VaultKeep>(sql, (vk, k) =>
      {
        vk.Keeps = k;
        return vk;
      }, new { vaultId }).ToList();
    }
    public List<VaultKeep> GetvaultsByKeepId(int keepId)
    {
      //join profile TODO
      var sql = @"SELECT * 
    FROM vault_keep vk
    Join vaults v ON v.id = vk.vaultId
    WHERE keepId = @keepId";
      return _db.Query<VaultKeep, Vault, VaultKeep>(sql, (vk, v) =>
      {
        vk.Vaults = v;
        return vk;
      }, new { keepId }).ToList();
    }

  }

}