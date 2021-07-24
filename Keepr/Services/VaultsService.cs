using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vrepo;

    public VaultsService(VaultsRepository vrepo)
    {
      _vrepo = vrepo;
    }

    public List<Vault> GetAll()
    {
      return _vrepo.GetAll();
    }
    public Vault GetById(int id)
    {
      var vault = _vrepo.GetById(id);
      if (vault == null)
      {
        throw new Exception("Vault does not exist.");
      }
      return vault;
    }
    public Vault Create(Vault data, string userId)
    {
      data.CreatorId = userId;
      var id = _vrepo.Create(data);
      var newVault = _vrepo.GetById(id);
      return newVault;
    }
    public int Update(Vault data, int id, string userId)
    {
      var original = GetById(id);
      if (original.CreatorId != userId)
      {
        throw new Exception("Only the creator can edit this vault.");
      }
      data.Name = data.Name ?? original.Name;
      data.Description = data.Description ?? original.Description;
      data.Id = original.Id;
      var update = _vrepo.Update(data);
      if (update < 1)
      {
        throw new Exception("Update did not work.");
      }
      if (update > 1)
      {
        throw new Exception("Oops, Something Bad happened...");
      }
      return update;
    }

    public string Delete(int id, string userId)
    {
      var vault = GetById(id);
      if (vault.CreatorId != userId)
      {
        throw new Exception("Only the creator can delete this.");
      }
      var delete = _vrepo.Delete(id);
      if (delete < 1)
      {
        throw new Exception("Delete did not work.");
      }
      if (delete > 1)
      {
        throw new Exception("Oops... something bad happened....");
      }
      return "Successfully Deleted";
    }
  }
}