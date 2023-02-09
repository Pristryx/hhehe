





using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ASAssignment212344H.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Gender { get; set; }

        public string NRIC { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }

        public string Resume { get; set; }

        public string WhoamI { get; set; }
    }
}
