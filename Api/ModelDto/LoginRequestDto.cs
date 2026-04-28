namespace Api.ModelDto
{
    // модель входа в систему (запрос от клиента)
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}