namespace Api.ModelDto
{
    // DTO-модель регистрации юзера
    public class RegisterRequestDto : IRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}