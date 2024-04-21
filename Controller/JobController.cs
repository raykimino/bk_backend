using bk_backend.EFCore;
using bk_backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bk_backend.Controller;

[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public JobController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public class NewJobDto
    {
        public Guid EnterpriseId { get; set; }
        public string? RecruitName { get; set; }
        public int Salary { get; set; }
        public string? Degree { get; set; }
        public string? RecruitmentProfessional { get; set; }
        public string? WorkSpace { get; set; }
        public string? JobInfo { get; set; }
    }
    
    public class ChangeJobDto
    {
        public Guid JobId { get; set; }
        public Guid EnterpriseId { get; set; }
        public string? RecruitName { get; set; }
        public int Salary { get; set; }
        public string? Degree { get; set; }
        public string? RecruitmentProfessional { get; set; }
        public string? WorkSpace { get; set; }
        public string? JobInfo { get; set; }
    }


    [HttpGet("findJobById")]
    public async Task<ActionResult> FindJobById(Guid jobId)
    {
        return Ok(await _dbContext.Jobs.FirstOrDefaultAsync(x => x.JobId == jobId));
    }
    
    [HttpGet("jobList")]
    public async Task<ActionResult> GetJobList()
    {
        return Ok(await _dbContext.Jobs.AsNoTracking().ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult> CreateNewJob(NewJobDto newJobDto)
    {
        var job = new Job()
        {
            JobId = new Guid(),
            EnterpriseId = newJobDto.EnterpriseId,
            RecruitName = newJobDto.RecruitName,
            Salary = newJobDto.Salary,
            Degree = newJobDto.Degree,
            RecruitmentProfessional = newJobDto.RecruitmentProfessional,
            WorkSpace = newJobDto.WorkSpace,
            JobInfo = newJobDto.JobInfo
        };

        await _dbContext.Jobs.AddAsync(job);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> ChangeJob(ChangeJobDto changeJobDto)
    {
        var jb = await _dbContext.Jobs.FirstOrDefaultAsync(x => x.JobId == changeJobDto.JobId);
        if (jb == null)
        {
            return Ok("job item not exists");
        }

        jb.RecruitName = changeJobDto.RecruitName;
        jb.Salary = changeJobDto.Salary;
        jb.Degree = changeJobDto.Degree;
        jb.RecruitmentProfessional = changeJobDto.RecruitmentProfessional;
        jb.WorkSpace = changeJobDto.WorkSpace;
        jb.JobInfo = changeJobDto.JobInfo;

        _dbContext.Update(jb);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteJob(Guid jobId)
    {
        var jb = await _dbContext.Jobs.FirstOrDefaultAsync(x => x.JobId == jobId);
        if (jb == null)
        {
            return Ok("job item not exists");
        }

        _dbContext.Remove(jb);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

}