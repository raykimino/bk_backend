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
        public string? Salary { get; set; }
        public string? Degree { get; set; }
        public string? RecruitmentProfessional { get; set; }
        public string? WorkSpace { get; set; }
        public string? JobInfo { get; set; }
    }

    public class CompanyCvInputDto
    {
        public Guid UserId { get; set; }
        public Guid CvId { get; set; }
        public Guid EnterpriseId { get; set; }
        public int CvStatus { get; set; }
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

    [HttpGet("GetAllCompanyCv")]
    public async Task<ActionResult> GetAllCompanyCv()
    {
        List<CompanyCvOutDto> companyCvOutDtos = new List<CompanyCvOutDto>();
        List<CompanyCv> companyCvs = await _dbContext.CompanyCvs.ToListAsync();
        if(companyCvOutDtos.Count < 1)
        {
            return Ok("No Any CompanyCv Found!");
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
                    UserId = companyCv.UserId,
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
        return Ok(companyCvOutDtos);
    }

    [HttpDelete("DeleteCompanyCv")]
    public async Task<ActionResult> DeleteCompanyCv(Guid userId,Guid enterpriseId)
    {
        List<CompanyCv> companyCvs = await _dbContext.CompanyCvs.Where(cc => cc.UserId == userId).ToListAsync();
        if(companyCvs.Count < 1)
        {
            return BadRequest("error");
        }
        CompanyCv companyCv = companyCvs.Where(cc => cc.EnterpriseId == enterpriseId).FirstOrDefault();
        if(companyCv == null)
        {
            return BadRequest("error");
        }
        _dbContext.CompanyCvs.Remove(companyCv);
        await _dbContext.SaveChangesAsync();
        return Ok("success");

    }

    [HttpDelete("UpdateCompanyCvStatus")]
    public async Task<ActionResult> UpdateCompanyCvStatus(Guid userId, Guid enterpriseId, int cvStatus)
    {
        List<CompanyCv> companyCvs = await _dbContext.CompanyCvs.Where(cc => cc.UserId == userId).ToListAsync();
        if (companyCvs.Count < 1)
        {
            return BadRequest("error");
        }
        CompanyCv companyCv = companyCvs.Where(cc => cc.EnterpriseId == enterpriseId).FirstOrDefault();
        if (companyCv == null)
        {
            return BadRequest("error");
        }
        _dbContext.CompanyCvs.Where(cc => cc.Id == companyCv.Id).ExecuteUpdate(s => s.SetProperty(p => p.CvStatus, cvStatus));
        await _dbContext.SaveChangesAsync();
        return Ok("success");

    }

    [HttpPost("InsertCompanyCv")]
    public async Task<ActionResult> InsertCompanyCv(CompanyCvInputDto companyCvInputDto)
    {
        User user = await _dbContext.Users.Where(u => u.UserId == companyCvInputDto.UserId).FirstOrDefaultAsync();
        if(user == null)
        {
            return BadRequest("error");
        }
        UserCv userCv = await _dbContext.UserCvs.Where(uc => uc.UserCvId == companyCvInputDto.CvId).FirstOrDefaultAsync();
        if (userCv == null)
        {
            return Ok("你目前尚未生成简历");
        }
        Enterprise enterprise = await _dbContext.Enterprises.Where(e => e.EnterpriseInfoId == companyCvInputDto.EnterpriseId).FirstOrDefaultAsync();
        if (enterprise == null)
        {
            return BadRequest("error");
        }

        CompanyCv companyCv = new CompanyCv()
        {
            Id = Guid.NewGuid(),
            UserId = companyCvInputDto.UserId,
            CvId = companyCvInputDto.CvId,
            EnterpriseId = companyCvInputDto.EnterpriseId,
            CvStatus = 0
        };
        await _dbContext.CompanyCvs.AddAsync(companyCv);
        await _dbContext.SaveChangesAsync();
        return Ok("success");
    }
}