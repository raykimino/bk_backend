using System.ComponentModel.DataAnnotations;

namespace bk_backend.Model;

/// <summary>
/// 个人简历类
/// </summary>
public class UserCv
{
    [Key]
    public required Guid UserCvId { get; set; }
    public Guid UserId { get; set; }
    [MaxLength(100)]
    public string? UserRealName { get; set; }
    [MaxLength(100)]
    public string? UserPhone { get; set; }
    [MaxLength(100)]
    public string? UserEmail { get; set; }
    [MaxLength(100)]
    public string? UserAddress { get; set; }
    [MaxLength(100)]
    public string? UserSchoolName { get; set; }
    [MaxLength(100)]
    public string? UserDegree { get; set; }
    [MaxLength(100)]
    public string? UserMajor { get; set; } 
    /// <summary>
    /// 技能 
    /// </summary>
    [MaxLength(500)]
    public string? UserAbility { get; set; }
    /// <summary>
    /// 用户项目经验
    /// </summary>
    [MaxLength(500)]
    public string? UserProjectExp { get; set; }
    /// <summary>
    /// 用户成就
    /// </summary>
    [MaxLength(500)]
    public string? UserAchievement { get; set; }
}