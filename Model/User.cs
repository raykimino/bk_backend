using System.ComponentModel.DataAnnotations;

namespace bk_backend.Model;

/// <summary>
/// 用户信息类
/// </summary>
public class User
{
    [Key]
    public required Guid UserId { get; set; }
    [MaxLength(100)]
    public string? UserName { get; set; }
    [MaxLength(512)]
    public string? Password { get; set; }
    public int UserType { get; set; } = 1;
}