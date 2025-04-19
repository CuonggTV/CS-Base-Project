using System.ComponentModel.DataAnnotations;

namespace CS_Base_Project.DAL.Data.RequestDto.Auth;

public class LoginRequestDTO
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}