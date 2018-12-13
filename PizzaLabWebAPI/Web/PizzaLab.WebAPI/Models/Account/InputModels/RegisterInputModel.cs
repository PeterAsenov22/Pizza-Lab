namespace PizzaLab.WebAPI.Models.Account.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterInputModel
    {
        private const int PasswordMinLength = 8;
        private const int UsernameMinLength = 3;
        private const int NameMinLength = 2;

        [Required]
        [EmailAddress(ErrorMessage = "Please enter valid e-mail address.")]
        public string Email { get; set; }

        [Required]
        [MinLength(UsernameMinLength, ErrorMessage = "Username should be at least 3 symbols long.")]
        public string Username { get; set; }

        [Required]
        [MinLength(NameMinLength, ErrorMessage = "First name should be at least 2 symbols long.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(NameMinLength, ErrorMessage = "Last name should be at least 2 symbols long.")]
        public string LastName { get; set; }

        [Required]
        [MinLength(PasswordMinLength, ErrorMessage = "Password should be at least 8 symbols long.")]
        public string Password { get; set; }
    }
}
