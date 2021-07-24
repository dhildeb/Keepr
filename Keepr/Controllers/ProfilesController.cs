using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;

    public ProfilesController(ProfilesService ps)
    {
      _ps = ps;
    }

    [HttpGet]
    public ActionResult<List<Account>> GetAll()
    {
      try
      {
        List<Account> Accounts = _ps.GetAll();
        return Ok(Accounts);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Account> GetById(string id)
    {
      try
      {
        Account Accounts = _ps.GetById(id);
        return Ok(Accounts);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfileId(string id)
    {
      try
      {
        return _ps.GetKeepsByProfileId(id);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Account>> Update([FromBody] Account data, string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        data.Id = userInfo.Id;
        return Ok(_ps.Update(id, data));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_ps.Delete(id, userInfo.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}
