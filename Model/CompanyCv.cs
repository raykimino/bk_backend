using System.ComponentModel.DataAnnotations;

namespace bk_backend.Model;

public class CompanyCv
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CvId { get; set; }
    public Guid EnterpriseId { get; set; }
    public int CvStatus { get; set; }//0-待查看,1-拒绝,2-同意
}