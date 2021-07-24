using System;
using System.Collections.Generic;
using Keepr.Models;

namespace Keeepr.Controllers
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _prepo;

    public List<Account> GetAll()
    {
      return _prepo.GetAll();
    }

    public Account GetById(int id)
    {
      var profile = _prepo.GetById(id);
      if (profile == null)
      {
        throw new Exception("Profile doesnt exist.");
      }
      return profile;
    }

    public Account Create(Account data)
    {
      return _prepo.Create(data);
    }

    public object Update(int id, Account data)
    {
      var original = GetById(id);
      if (original.Id != data.Id)
      {
        throw new Exception("Doesnt look like you know who you are. well your not this guy, try again.");
      }
      data.Email = original.Email;
      data.Name = data.Name ?? original.Name;
      data.Picture = data.Picture ?? original.Picture;
      return _prepo.Update(data);
    }

    public object Delete(int id, string userId)
    {
      var original = GetById(id);
      if (original.Id != userId)
      {
        throw new Exception("This would be considered murder. if i let you delete someone else.");
      }
      return _prepo.Delete(id);
    }
  }
}