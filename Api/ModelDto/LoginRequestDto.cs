namespace Api.ModelDto
{
    // DTO-модель логина юзера (запрос от клиента)
    public class LoginRequestDto : IRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        // public string Email { get; set; }
    }
}