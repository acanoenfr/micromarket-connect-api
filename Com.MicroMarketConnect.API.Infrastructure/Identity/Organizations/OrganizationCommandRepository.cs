using Com.MicroMarketConnect.API.Application.Write.Ports;
using Com.MicroMarketConnect.API.Core.Validation;
using Com.MicroMarketConnect.API.Domain.IdentityModule.Aggregates.Enums;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Com.MicroMarketConnect.API.Infrastructure.Identity.Organizations;

public class OrganizationCommandRepository(
    IIdentityDbContext dbContext) : IOrganizationCommandRepository
{
    public async Task<Result<bool>> Exists(string name)
    {
        try
        {
            return Result.Ok(await dbContext.Organizations
                .Where(o => o.Name.Equals(name))
                .AnyAsync());
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
