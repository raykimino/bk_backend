namespace bk_backend.Model;

/// <summary>
/// 企业信息类
/// </summary>
public class Enterprise
{
    public required Guid EnterpriseInfoId { get; set; } = new Guid();
    public string? EnterpriseName { get; set; }
    public string? EnterpriseAddress { get; set; }
    public string? EnterpriseIndustry { get; set; }
    public string? EnterpriseAreasInvolved { get; set; }
    public string? EnterpriseNature { get; set; }
    public string? EnterpriseArea { get; set; }
}