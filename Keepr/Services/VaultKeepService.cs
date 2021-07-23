using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepService
  {
    private readonly VaultKeepRepository _vkrepo;

    public VaultKeepService(VaultKeepRepository vkrepo)
    {
      _vkrepo = vkrepo;
    }

    public VaultKeep Create(VaultKeep data)
    {
      return _vkrepo.Create(data);
    }

    // public int Delete(int id, string id){

    // }
  }
}