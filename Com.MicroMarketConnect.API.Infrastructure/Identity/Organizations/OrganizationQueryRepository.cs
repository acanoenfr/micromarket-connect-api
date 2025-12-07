using Com.MicroMarketConnect.API.Application.Read.Ports;
using Com.MicroMarketConnect.API.Application.Read.Queries.Identity.Organizations;
using Com.MicroMarketConnect.API.Core.Validation;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;
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
                .Include(o => o.Members)
                .ThenInclude(om => om.User)
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

    public async Task<Result<bool>> IsGrantAccess(string name, string role, Guid userId)
    {
        try
        {
            var organization = await dbContext.Organizations
                .Include(o => o.Members)
                .Where(o => o.Name.Equals(name))
                .FirstOrDefaultAsync();

            if (organization is null)
                return Result.Fail(Errors.NotFound);

            return Result.Ok(organization.Members
                .Where(m => m.UserId.Equals(userId) && GetInheritedRoles(role).Contains(m.Role))
                .Any());
        }
        catch (Exception ex)
        {
            return Result.Fail(Errors.ExceptionalError(ex));
        }
    }

    #region Private methods

    private static Func<OrganizationEntity, OrganizationQueryModel> ToModel()
        => entity => new OrganizationQueryModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            DisplayName = entity.DisplayName,
            Description = entity.Description,
            Members = ToMembersModel(entity.Members.ToList().AsReadOnly())
        };
    private static IReadOnlyCollection<OrganizationMember> ToMembersModel(IReadOnlyCollection<OrganizationMemberEntity> list)
        => list.Select(ToMemberModel()).ToList().AsReadOnly();
    private static Func<OrganizationMemberEntity, OrganizationMember> ToMemberModel()
        => entity => new OrganizationMember()
        {
            Id = entity.UserId,
            DisplayName = entity.User.DiplayName,
            Email = entity.User.Email,
            Role = entity.Role
        };

    private static string[] GetInheritedRoles(string role)
    {
        var inheritedRoles = new List<string>();

        switch (role)
        {
            case OrganizationRoleClaims.Owner:
                inheritedRoles.AddRange([OrganizationRoleClaims.Owner, OrganizationRoleClaims.Manager, OrganizationRoleClaims.Member]);
                break;
            case OrganizationRoleClaims.Manager:
                inheritedRoles.AddRange([OrganizationRoleClaims.Manager, OrganizationRoleClaims.Member]);
                break;
            default:
            case OrganizationRoleClaims.Member:
                inheritedRoles.AddRange([OrganizationRoleClaims.Member]);
                break;
        }

        return [.. inheritedRoles];
    }

    #endregion
}
