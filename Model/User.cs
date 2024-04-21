namespace bk_backend.Model;

/// <summary>
/// 用户信息类
/// </summary>
public class User
{
    public required Guid UserId { get; set; } = new Guid();
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public int UserType { get; set; } = 1;
}