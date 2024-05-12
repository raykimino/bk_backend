using System.ComponentModel.DataAnnotations;

namespace bk_backend.Model;

/// <summary>
/// 个人简历类
/// </summary>
public class UserCv
{
    [Key]
    public required Guid UserCvId { get; set; }//
    public Guid UserId { get; set; }//
    [MaxLength(100)]
    public string? UserRealName { get; set; }//
    [MaxLength(100)]
    public string? UserPhone { get; set; }//
    [MaxLength(100)]
    public string? UserEmail { get; set; }//
    [MaxLength(100)]
    public string? UserBirthDay { get; set; }//
    [MaxLength(100)]
    public string? UserSchoolName { get; set; }//
    [MaxLength(100)]
    public string? UserDegree { get; set; }//
    [MaxLength(100)]
    public string? UserMajor { get; set; } //
    
    [MaxLength(500)]
    public string? UserProjectExp { get; set; }//

    [MaxLength(500)]
    public string? UserAchievement { get; set; }//
    
    [MaxLength(10)]
    public string? UserGender { get; set; }//
   
    [MaxLength(50)]
    public string? UserPolicyFace { get; set; }//
    
    [MaxLength(500)]
    public string? UserBirthPlace { get; set; }//
    
    [MaxLength(100)]
    public string? AimWorkPlace { get; set; }//

    [MaxLength(50)]
    public string? AimJob { get; set; }//
   
    [MaxLength(50)]
    public string? AimJobPosition { get; set; }//
    
    [MaxLength(50)]
    public string? JobSeekingStatus { get; set; }//
    
    [MaxLength(500)]
    public string? UserSkill { get; set; }//
    
    [MaxLength(100)]
    public string? SalaryStart { get; set; }//
    
    [MaxLength(100)]
    public string? SalaryEnd { get; set; }//

    [MaxLength(100)]
    public string? StartWorkTime { get; set; }//

    [MaxLength(100)]
    public string? WorkType { get; set; }//

    public UserCv()
    {
    }
}