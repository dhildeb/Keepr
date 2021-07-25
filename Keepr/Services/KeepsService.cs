using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _krepo;

    public KeepsService(KeepsRepository krepo)
    {
      _krepo = krepo;
    }

    public List<Keep> GetAll()
    {
      return _krepo.GetAll();
    }
    public Keep GetById(int id)
    {
      var keep = _krepo.GetById(id);
      if (keep == null)
      {
        throw new Exception("that keep does not exist");
      }
      keep.Views++;
      _krepo.ViewCount(id, keep.Views);
      return keep;
    }
    public Keep Create(Keep data, string userId)
    {
      data.CreatorId = userId;
      var newKeep = _krepo.Create(data);
      return _krepo.GetById(newKeep.Id);
    }

    public void KeepCount(int keepId, int n)
    {
      var keep = GetById(keepId);
      _krepo.KeepCount(keepId, keep.Keeps += n);
    }

    public int Update(Keep data, int id, string userId)
    {
      var original = GetById(id);
      if (original == null)
      {
        throw new Exception("that keep does not exist.");
      }
      if (original.CreatorId != userId)
      {
        throw new Exception("Only the creator can edit this keep.");
      }
      data.Id = id;
      data.Keeps = original.Keeps;
      data.Views = original.Views;
      data.Shares = original.Shares;
      data.Description = data.Description ?? original.Description;
      data.Img = data.Img ?? original.Img;
      data.Name = data.Name ?? original.Name;
      data.Tag = data.Tag ?? original.Tag;

      return _krepo.Update(data);

    }
    public string Delete(int id, string userId)
    {
      var keep = GetById(id);
      if (keep.CreatorId != userId)
      {
        throw new Exception("Only the creator can delete this keep.");
      }
      var delete = _krepo.Delete(id);
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