using Com.MicroMarketConnect.API.Application.Read.Ports;
using Com.MicroMarketConnect.API.Application.Read.Queries.Identity.OrganizationMembers;
using Com.MicroMarketConnect.API.Core.Validation;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Com.MicroMarketConnect.API.Infrastructure.Identity.Organizations;

public class OrganizationMemberQueryRepository(
    IIdentityDbContext dbContext) : IOrganizationMemberQueryRepository
{
    public async Task<Result<IReadOnlyCollection<OrganizationMemberQueryModel>>> GetAll(string name)
    {
        try
        {
            return Result
                .Ok(await dbContext.OrganizationMembers
                    .Include(om => om.User)
                    .Where(om => om.Organization.Name.Equals(name))
                    .ToListAsync())
                .Map(ToModels());
        }
        catch (Exception ex)
        {
            return Result.Fail(Errors.ExceptionalError(ex));
        }
    }

    private static Func<List<OrganizationMemberEntity>, IReadOnlyCollection<OrganizationMemberQueryModel>> ToModels()
        => list => list.Select(ToModel()).ToList().AsReadOnly();
    private static Func<OrganizationMemberEntity, OrganizationMemberQueryModel> ToModel()
        => entity => new OrganizationMemberQueryModel()
        {
            Id = entity.User.Id,
            DisplayName = entity.User.DiplayName,
            Email = entity.User.Email,
            Role = entity.Role
        };
}
