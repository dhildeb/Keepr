using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkrepo;
    private readonly VaultsService _vs;
    private readonly KeepsService _ks;

    public VaultKeepsService(VaultKeepsRepository vkrepo, VaultsService vs, KeepsService ks)
    {
      _vkrepo = vkrepo;
      _vs = vs;
      _ks = ks;
    }

    public VaultKeep Create(VaultKeep data, string userId)
    {
      var vault = _vs.GetById(data.VaultId, userId);
      if (vault?.CreatorId != userId)
      {
        throw new Exception("Only the creator can do that.");
      }
      var n = 1;
      _ks.KeepCount(data.KeepId, n);
      data.CreatorId = userId;
      return _vkrepo.Create(data);
    }

    public VaultKeep Delete(int id, string userId)
    {
      var vaultKeep = _vkrepo.GetById(id);
      if (vaultKeep?.CreatorId != userId)
      {
        throw new Exception("Only the creator can do that.");
      }
      if (_vkrepo.Delete(id) != 1)
      {
        throw new Exception("didnt work");
      }
      var n = -1;
      _ks.KeepCount(vaultKeep.KeepId, n);
      return vaultKeep;
    }

    public List<VaultKeep> GetKeepsByVaultId(int id, string userId)
    {
      var vault = _vs.GetById(id, userId);

      return _vkrepo.GetKeepsByVaultId(id);
    }
  }
}