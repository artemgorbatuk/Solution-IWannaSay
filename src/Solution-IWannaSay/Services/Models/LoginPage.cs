using System.ComponentModel.DataAnnotations;

namespace Services.Models;

public class LoginPage {
    [Required(ErrorMessage = "Не указан логин")]
    public string Login { get; set; } = default!;

    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;
}