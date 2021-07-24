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

    public VaultKeepsService(VaultKeepsRepository vkrepo, VaultsService vs)
    {
      _vkrepo = vkrepo;
      _vs = vs;
    }

    public VaultKeep Create(VaultKeep data, string userId)
    {
      var vault = _vs.GetById(data.VaultId);
      if (vault?.CreatorId != userId)
      {
        throw new Exception("Only the creator can do that.");
      }
      data.CreatorId = userId;
      return _vkrepo.Create(data);
    }

    public int Delete(int id, string userId)
    {
      var vaultKeep = _vkrepo.GetById(id);
      if (vaultKeep?.CreatorId != userId)
      {
        throw new Exception("Only the creator can do that.");
      }
      return _vkrepo.Delete(id);
    }

    public List<VaultKeep> GetKeepsByVaultId(int id)
    {
      return _vkrepo.GetKeepsByVaultId(id);
    }
  }
}