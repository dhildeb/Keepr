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
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _kc;

    public KeepsController(KeepsService kc)
    {
      _kc = kc;
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAll()
    {
      try
      {
        List<Keep> Keeps = _kc.GetAll();
        return Ok(Keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Keep> GetById(int id)
    {
      try
      {
        Keep Keeps = _kc.GetById(id);
        return Ok(Keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep data)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Keep Keep = _kc.Create(data, userInfo.Id);
        return Ok(Keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Keep>> Update([FromBody] Keep data, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_kc.Update(data, id, userInfo.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_kc.Delete(id, userInfo.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // if doing many to many change TODO m2mO
    // [HttpGet("{id}/m2mO")]
    // public ActionResult<List<Keep>> Getm2mOByKeepId(int id)
    // {
    //   try
    //   {
    //     List<m2mOKeep> data = _kc.Getm2mOByKeepId(id);
    //     return Ok(data);
    //   }
    //   catch (System.Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }


  }
}
