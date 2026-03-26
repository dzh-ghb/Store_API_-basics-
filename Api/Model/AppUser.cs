using Microsoft.AspNetCore.Identity;

namespace Api.Model
{
    // демонстрация процесса добавления полей в таблицу БД
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
    }
}