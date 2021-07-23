namespace Keepr.Models
{
  public class Keep
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Tag { get; set; }
    public string Img { get; set; } = "https://source.unsplash.com/random";
    public int Views { get; set; } = 0;
    public int Shares { get; set; } = 0;
    public int Keeps { get; set; } = 0;
    public Account Creator { get; set; }
  }
}