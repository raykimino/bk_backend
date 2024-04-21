using System.Text.Json.Nodes;
using bk_backend.EFCore;
using bk_backend.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bk_backend.Controller;

[Route("api/[controller]")]
[ApiController]
public class EnterpriseController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public EnterpriseController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public class EnterpriseDto
    {
        public required string EnterpriseName { get; set; }
        public required string EnterpriseAddress { get; set; }
        public required string EnterpriseIndustry { get; set; }
        public required string EnterpriseAreasInvolved { get; set; }
        public required string EnterpriseNature { get; set; }
        public required string EnterpriseArea { get; set; }
    }

    [HttpGet("GetAllEnterprise")]
    public async Task<ActionResult<List<Enterprise>>> GetAllEnterprise()
    {
        List<Enterprise> enterprises = await _appDbContext.Set<Enterprise>().ToListAsync();
        if (enterprises.Count < 1)
        {
            return Ok("false found enterprises");
        }
        return Ok(enterprises);
    }

    [HttpGet("GetEnterpriseById")]
    public async Task<ActionResult<Enterprise>> GetEnterpriseById(Guid id)
    {
        var enterprise = await _appDbContext.Set<Enterprise>().Where(e => e.EnterpriseInfoId == id).FirstOrDefaultAsync();
        if (enterprise == null)
        {
            return Ok("id is not found!");
        }
        return Ok(enterprise);
    }

    [HttpPost]
    public async Task<ActionResult> InsertEnterprise(EnterpriseDto enterpriseDto)
    {
        Enterprise enterprise = new Enterprise()
        {
            EnterpriseInfoId = new Guid(),
            EnterpriseNature = enterpriseDto.EnterpriseNature,
            EnterpriseName = enterpriseDto.EnterpriseName,
            EnterpriseAddress = enterpriseDto.EnterpriseAddress,
            EnterpriseArea = enterpriseDto.EnterpriseArea,
            EnterpriseAreasInvolved = enterpriseDto.EnterpriseAreasInvolved,
            EnterpriseIndustry = enterpriseDto.EnterpriseIndustry,

        };
        await _appDbContext.Set<Enterprise>().AddAsync(enterprise);
        await _appDbContext.SaveChangesAsync();
        return Ok("success insert");
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteEnterprise(Guid id)
    {
        var enterprise = await _appDbContext.Set<Enterprise>().Where(e => e.EnterpriseInfoId == id).FirstOrDefaultAsync();
        if (enterprise == null)
        {
            return Ok($"{id} is not found");
        }

        _appDbContext.Set<Enterprise>().Remove(enterprise);
        await _appDbContext.SaveChangesAsync();
        return Ok("success delete");
    }

    [HttpPut]
    public async Task<ActionResult> UpdateEnterprise(Enterprise enterprise)
    {
        var getEnterprise = await _appDbContext.Set<Enterprise>().Where(e => e.EnterpriseInfoId == enterprise.EnterpriseInfoId).FirstOrDefaultAsync();
        if (getEnterprise == null)
        {
            return Ok($"{enterprise.EnterpriseInfoId} is not found");
        }

        _appDbContext.Set<Enterprise>().Update(enterprise);
        await _appDbContext.SaveChangesAsync();
        return Ok("success update");
    }
}