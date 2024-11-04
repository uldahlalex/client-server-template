using System.ComponentModel.DataAnnotations;

namespace Service;

public class AppOptions
{
    [Required] [MinLength(1)] public string Database { get; set; } = null!;
    [Required] public bool RunInTestContainer { get; set; } = false;
    [Required] public string JwtKey { get; set; } = null!;
}