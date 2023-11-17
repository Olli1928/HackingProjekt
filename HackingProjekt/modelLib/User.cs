using System.ComponentModel.DataAnnotations;

namespace HackingProjekt.modelLib
{
    public class User
    {
        public int Id { get; set; }
        public string userName{ get; set; }
  
        public string  Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
