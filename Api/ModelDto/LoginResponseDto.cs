namespace Api.ModelDto
{
    // DTO-модель ответа сервера при успешном логине юзера
    public class LoginResponseDto : IRequestDto
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        // public string Email { get; set; }
    }
}