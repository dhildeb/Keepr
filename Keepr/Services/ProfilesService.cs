using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _prepo;
    private readonly KeepsRepository _krepo;

    public ProfilesService(ProfilesRepository prepo, KeepsRepository krepo)
    {
      _prepo = prepo;
      _krepo = krepo;
    }

    public List<Account> GetAll()
    {
      return _prepo.GetAll();
    }

    public Account GetById(string id)
    {
      var profile = _prepo.GetById(id);
      if (profile == null)
      {
        throw new Exception("Profile doesnt exist.");
      }
      return profile;
    }

    public object Update(string id, Account data)
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

    public object Delete(string id, string userId)
    {
      var original = GetById(id);
      if (original.Id != userId)
      {
        throw new Exception("This would be considered murder. if i let you delete someone else.");
      }
      return _prepo.Delete(id);
    }

    public ActionResult<List<Keep>> GetKeepsByProfileId(string id)
    {
      return _krepo.GetKeepsByProfileId(id);
    }
  }
}