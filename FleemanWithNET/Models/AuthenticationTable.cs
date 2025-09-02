using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleeman_with_dot_net.Models
{
    [Table("authentication_table")]
    public class AuthenticationTable
    {
        [Key]
        public int AuthId {get; set;}
      
        public string Password { get; set; }
        public string email { get; set; }
        public int Role_Id { get; set; }
        
    }
}
