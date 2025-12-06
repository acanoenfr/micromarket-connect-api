using Com.MicroMarketConnect.API.Application.Read.Ports;
using Com.MicroMarketConnect.API.Application.Read.Queries.Identity.Organizations;
using Com.MicroMarketConnect.API.Core.Validation;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Com.MicroMarketConnect.API.Infrastructure.Identity.Organizations;

public record OrganizationQueryRepository(
    IIdentityDbContext dbContext) : IOrganizationQueryRepository
{
    public async Task<Result<OrganizationQueryModel>> Get(string name)
    {
        try
        {
            var organization = await dbContext.Organizations
                .Where(o => o.Name.Equals(name))
                .FirstOrDefaultAsync();

            if (organization is null)
                return Result.Fail(Errors.NotFound);

            return Result
                .Ok(organization)
                .Map(ToModel());
        }
        catch (Exception ex)
        {
            return Result.Fail(Errors.ExceptionalError(ex));
        }
    }

    private static Func<OrganizationEntity, OrganizationQueryModel> ToModel()
        => entity => new OrganizationQueryModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            DisplayName = entity.DisplayName,
            Description = entity.Description
        };
}
