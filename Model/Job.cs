using System.ComponentModel.DataAnnotations;

namespace bk_backend.Model;

/// <summary>
/// 职位类
/// </summary>
public class Job
{
    [Key]
    public required Guid JobId { get; set; }
    public Guid EnterpriseId { get; set; }
    /// <summary>
    /// 招聘名称
    /// </summary>
    [MaxLength(100)]
    public string? RecruitName { get; set; }

    public int Salary { get; set; } = 0;
    /// <summary>
    /// 学历
    /// </summary>
    [MaxLength(100)]
    public string? Degree { get; set; }
    /// <summary>
    /// 招聘专业
    /// </summary>
    [MaxLength(100)]
    public string? RecruitmentProfessional { get; set; }
    [MaxLength(100)]
    public string? WorkSpace { get; set; }
    [MaxLength(100)]
    public string? JobInfo { get; set; }
}