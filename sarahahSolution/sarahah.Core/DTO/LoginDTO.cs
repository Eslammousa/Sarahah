using System.ComponentModel.DataAnnotations;

namespace sarahah.Core.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Please write your user name")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Please write your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
