using bk_backend.EFCore;
using bk_backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bk_backend.Controller;

[ApiController]
[Route("api/[Controller]")]
public class CompanyCvController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public CompanyCvController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public class CompanyCvOutDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CvId { get; set; }
        public Guid EnterpriseId { get; set; }
        public string? EnterpriseName { get; set; }
        public int CvStatus { get; set; }
        public string Salary { get; set; };
        public required string Degree { get; set; }
        public required string RecruitmentProfessional { get; set; }
        public required string WorkSpace { get; set; }
        public required string JobInfo { get; set; }
    }

    [HttpGet("GetListCvByUserId")]
    public async Task<ActionResult> GetListCvByUserId(Guid userId)
    {
        List<CompanyCvOutDto> companyCvOutDtos = new List<CompanyCvOutDto>();
        List<CompanyCv> companyCvs = await _dbContext.CompanyCvs.Where(cc => cc.UserId == userId).ToListAsync();
        if (companyCvs.Count < 1)
        {
            return Ok("你未投递过简历");
        }
        foreach (CompanyCv companyCv in companyCvs)
        {
            List<Job> jobs = await _dbContext.Jobs.Where(j => j.EnterpriseId == companyCv.EnterpriseId).ToListAsync();
            Enterprise? enterprise = await _dbContext.Enterprises.Where(e => e.EnterpriseInfoId == companyCv.EnterpriseId).FirstOrDefaultAsync();
            foreach (var item in jobs)
            {
                companyCvOutDtos.Add(new CompanyCvOutDto()
                {
                    Id = companyCv.Id,
                    UserId = userId,
                    CvId = companyCv.CvId,
                    EnterpriseId = companyCv.EnterpriseId,
                    EnterpriseName = enterprise.EnterpriseName == null ? "" : enterprise.EnterpriseName,
                    CvStatus = companyCv.CvStatus,
                    Salary = item.Salary.ToString(),
                    Degree = item.Degree == null ? "" : item.Degree,
                    RecruitmentProfessional = item.RecruitmentProfessional == null ? "" : item.RecruitmentProfessional,
                    WorkSpace = item.WorkSpace == null ? "" : item.WorkSpace,
                    JobInfo = item.JobInfo == null ? "" : item.JobInfo
                });
            }
        }

        return Ok(companyCvs);
    }
    
    [HttpGet("GetListCvByCompanyId")]
    public async Task<ActionResult> GetListCvByCompanyId(Guid companyId)
    {
        List<CompanyCv> companyCvs = await _dbContext.CompanyCvs.Where(cc => cc.EnterpriseId == companyId).ToListAsync();
        if (companyCvs.Count < 1)
        {
            return Ok("该企业未有学生投递简历");
        }

        return Ok(companyCvs);
    }
    
    
}