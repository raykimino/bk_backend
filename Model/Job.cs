namespace bk_backend.Model;

/// <summary>
/// 职位类
/// </summary>
public class Job
{
    public required Guid JobId { get; set; } = new Guid();
    public Guid EnterpriseId { get; set; }
    /// <summary>
    /// 招聘名称
    /// </summary>
    public string? RecruitName { get; set; }

    public int Salary { get; set; } = 0;
    public string? Degree { get; set; }
    /// <summary>
    /// 招聘专业
    /// </summary>
    public List<string>? RecruitmentProfessional { get; set; }
    public string? WorkSpace { get; set; }
    public string? JobInfo { get; set; }
}