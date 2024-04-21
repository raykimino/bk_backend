namespace bk_backend.Model;

/// <summary>
/// 个人简历类
/// </summary>
public class UserCv
{
    public required Guid UserCvId { get; set; }
    public Guid UserId { get; set; }
    public string? UserRealName { get; set; }
    public string? UserPhone { get; set; }
    public string? UserEmail { get; set; }
    public string? UserAddress { get; set; }
    public string? UserSchoolName { get; set; }
    public string? UserDegree { get; set; }
    public string? UserMajor { get; set; } 
    /// <summary>
    /// 技能 
    /// </summary>
    public string? UserAbility { get; set; }
    /// <summary>
    /// 用户项目经验
    /// </summary>
    public string? UserProjectExp { get; set; }
    /// <summary>
    /// 用户成就
    /// </summary>
    public string? UserAchievement { get; set; }
}