namespace Keepr.Models
{
  public class VaultKeep
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public int VaultId { get; set; }
    public int KeepId { get; set; }
    public Keep Keeps { get; set; }
    public Vault Vaults { get; set; }
  }
  public class VaultKeepView
  {

  }
}