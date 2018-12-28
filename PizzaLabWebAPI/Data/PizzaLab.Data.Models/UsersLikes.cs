namespace PizzaLab.Data.Models
{
    public class UsersLikes
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }
    }
}
