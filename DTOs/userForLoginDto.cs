using System.ComponentModel.DataAnnotations;

namespace Datingapp.API.DTOs
{
    public class userForLoginDto
    {

        [Required]
        public string username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 8 character")]
        public string password { get; set; }
    }
}