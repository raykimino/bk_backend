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

    public class UpdateCvStuta
    {
        public Guid id { get; set; }
        public int cvStatus { get; set; }
    }
    public class CompanyCvOutDto
    {
        public Guid Id { get; set; }
        public Guid userId { get; set; }
        public Guid cvId { get; set; }
        public Guid enterpriseId { get; set; }
        public string? enterpriseName { get; set; }
        public int cvStatus { get; set; }
        public string? salary { get; set; }
        public string? degree { get; set; }
        public string? recruitmentProfessional { get; set; }
        public string? workSpace { get; set; }
        public string? jobInfo { get; set; }
        public string? recruitName { get; set; }
    }
    public class GetUserBaseInfoAndCv
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public string? schoolName { get; set; }
        public string? majorName { get; set; }
        public string? aimPosition { get; set; }
        public Guid? jobId { get; set; }
        public int cvStatus { get; set; }
    }
    public class CompanyCvInputDto
    {
        public Guid UserId { get; set; }
        public Guid CvId { get; set; }
        public Guid JobId { get; set; }
        public Guid EnterpriseId { get; set; }
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
        foreach (CompanyCv companyCv in companyCvs)//每个简历,组织数据
        {
            Job job = await _dbContext.Jobs.Where(j => j.JobId == companyCv.JobId).FirstOrDefaultAsync();
            
            Enterprise? enterprise = await _dbContext.Enterprises.Where(e => e.EnterpriseInfoId == companyCv.EnterpriseId).FirstOrDefaultAsync();
            companyCvOutDtos.Add(new CompanyCvOutDto()
            {
                Id = companyCv.Id,
                userId = userId,
                cvId = companyCv.CvId,
                enterpriseId = companyCv.EnterpriseId,
                enterpriseName = enterprise.EnterpriseName == null ? "" : enterprise.EnterpriseName,
                cvStatus = companyCv.CvStatus,
                salary = job.Salary.ToString(),
                degree = job.Degree == null ? "" : job.Degree,
                recruitmentProfessional = job.RecruitmentProfessional == null ? "" : job.RecruitmentProfessional,
                workSpace = job.WorkSpace == null ? "" : job.WorkSpace,
                jobInfo = job.JobInfo == null ? "" : job.JobInfo,
                recruitName = job.RecruitName
            });
        }

        return Ok(companyCvOutDtos);
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
                    userId = companyCv.UserId,
                    cvId = companyCv.CvId,
                    enterpriseId = companyCv.EnterpriseId,
                    enterpriseName = enterprise.EnterpriseName == null ? "" : enterprise.EnterpriseName,
                    cvStatus = companyCv.CvStatus,
                    salary = item.Salary.ToString(),
                    degree = item.Degree == null ? "" : item.Degree,
                    recruitmentProfessional = item.RecruitmentProfessional == null ? "" : item.RecruitmentProfessional,
                    workSpace = item.WorkSpace == null ? "" : item.WorkSpace,
                    jobInfo = item.JobInfo == null ? "" : item.JobInfo
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

    [HttpPut("UpdateCompanyCvStatus")]
    public async Task<ActionResult> UpdateCompanyCvStatus(UpdateCvStuta updateCvStuta)
    {
        CompanyCv companyCv = await _dbContext.CompanyCvs.Where(cc => cc.Id == updateCvStuta.id).FirstOrDefaultAsync();
        if (companyCv == null)
        {
            return BadRequest("error");
        }
        _dbContext.CompanyCvs.Where(cc => cc.Id == updateCvStuta.id).ExecuteUpdate(s => s.SetProperty(p => p.CvStatus, updateCvStuta.cvStatus));
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
        UserCv userCv = await _dbContext.UserCvs.Where(uc => uc.UserId == companyCvInputDto.UserId).FirstOrDefaultAsync();
        if (userCv == null)
        {
            return Ok("你目前尚未生成简历");
        }
        Enterprise enterprise = await _dbContext.Enterprises.Where(e => e.EnterpriseInfoId == companyCvInputDto.EnterpriseId).FirstOrDefaultAsync();
        if (enterprise == null)
        {
            return BadRequest("error");
        }

        List<CompanyCv> companyCvs = await _dbContext.CompanyCvs.Where(cc => cc.UserId == companyCvInputDto.UserId).ToListAsync();
        if(companyCvs.Count > 0)
        {
            var companyCv1 = companyCvs.Where(cc => cc.JobId == companyCvInputDto.JobId).FirstOrDefault();
            if (companyCv1 != null)
            {
                return Ok("你已经投递过改简历");
            }
        }

        CompanyCv companyCv = new CompanyCv()
        {
            Id = Guid.NewGuid(),
            UserId = companyCvInputDto.UserId,
            CvId = companyCvInputDto.CvId,
            EnterpriseId = companyCvInputDto.EnterpriseId,
            JobId = companyCvInputDto.JobId,
            CvStatus = 0
        };
        await _dbContext.CompanyCvs.AddAsync(companyCv);
        await _dbContext.SaveChangesAsync();
        return Ok("success");
    }

    [HttpGet("GetPostedCv")]
    public async Task<ActionResult> GetPostedCv()
    {
        List<CompanyCv> companyCvs = await _dbContext.CompanyCvs.ToListAsync();
        if (companyCvs.Count < 1)
        {
            return Ok();
        }
        List<GetUserBaseInfoAndCv> getUserBaseInfoAndCvs = new List<GetUserBaseInfoAndCv>();
        foreach(var item in companyCvs)
        {
            //组织姓名
            User user = await _dbContext.Users.Where(u => u.UserId == item.UserId).FirstOrDefaultAsync();
            //组织学校和专业
            UserCv userCv = await _dbContext.UserCvs.Where(uc => uc.UserCvId == item.CvId).FirstOrDefaultAsync();
            //组织招聘名称\岗位id
            Job job = await _dbContext.Jobs.Where(j => j.JobId == item.JobId).FirstOrDefaultAsync();
            //组织投递状态
            getUserBaseInfoAndCvs.Add(new GetUserBaseInfoAndCv()
            {
                id = item.Id,
                name = user.UserName,
                schoolName = userCv.UserSchoolName,
                majorName = userCv.UserMajor,
                aimPosition = job.RecruitName,
                jobId = job.JobId,
                cvStatus = item.CvStatus
            });
        }
        return Ok(getUserBaseInfoAndCvs);

    }
}