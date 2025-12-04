namespace Buscador.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PasswordValid { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }
}
