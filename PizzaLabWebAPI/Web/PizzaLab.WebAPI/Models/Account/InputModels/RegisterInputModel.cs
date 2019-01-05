namespace PizzaLab.WebAPI.Models.Account.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterInputModel
    {
        private const int UsernameMinimumLength = 4;
        private const int PasswordMinimumLength = 8;
        
        private const string EmailErrorMessage = "Please enter valid e-mail address.";
        private const string UsernameRegex = "^[^@]*$";
        private const string UsernameRegexErrorMessage = "Username should not contain @ symbol.";
        private const string UsernameErrorMessage = "Username should be at least 4 symbols long.";
        private const string PasswordErrorMessage = "Password should be at least 8 symbols long.";

        [Required]
        [EmailAddress(ErrorMessage = EmailErrorMessage)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(UsernameRegex, ErrorMessage = UsernameRegexErrorMessage)]
        [MinLength(UsernameMinimumLength, ErrorMessage = UsernameErrorMessage)]
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [MinLength(PasswordMinimumLength, ErrorMessage = PasswordErrorMessage)]
        public string Password { get; set; }
    }
}
