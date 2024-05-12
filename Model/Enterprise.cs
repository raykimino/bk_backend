using System.ComponentModel.DataAnnotations;

namespace bk_backend.Model;

/// <summary>
/// 企业信息类
/// </summary>
public class Enterprise
{
    [Key]
    public required Guid EnterpriseInfoId { get; set; }
    [MaxLength(100)]
    public string? EnterpriseName { get; set; }
    [MaxLength(100)]
    public string? EnterpriseAddress { get; set; }
    [MaxLength(100)]
    public string? EnterpriseIndustry { get; set; }
    [MaxLength(100)]
    public string? EnterpriseAreasInvolved { get; set; }
    [MaxLength(100)]
    public string? EnterpriseNature { get; set; }
    [MaxLength(100)]
    public string? EnterpriseArea { get; set; }
    public Enterprise()
    {

    }
}