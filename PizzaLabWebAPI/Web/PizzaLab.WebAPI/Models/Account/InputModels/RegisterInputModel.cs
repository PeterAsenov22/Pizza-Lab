namespace PizzaLab.WebAPI.Models.Account.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterInputModel
    {
        private const int PasswordMinLength = 8;
        private const int UsernameMinLength = 4;

        [Required]
        [EmailAddress(ErrorMessage = "Please enter valid e-mail address.")]
        public string Email { get; set; }

        [Required]
        [MinLength(UsernameMinLength, ErrorMessage = "Username should be at least 4 symbols long.")]
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [MinLength(PasswordMinLength, ErrorMessage = "Password should be at least 8 symbols long.")]
        public string Password { get; set; }
    }
}
