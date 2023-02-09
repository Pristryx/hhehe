using System.ComponentModel.DataAnnotations;

namespace ASAssignment212344H.Models
{
    public class Login
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required]
        //public string Token { get; set; }

        public bool RememberMe { get; set; }
    }
}
