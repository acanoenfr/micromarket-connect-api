using Com.MicroMarketConnect.API.Application.Write.Ports;
using Com.MicroMarketConnect.API.Core.Validation;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Com.MicroMarketConnect.API.Infrastructure.Identity.Users;

public class UserCommandRepository(
    IIdentityDbContext dbContext,
    IHasherProvider passwordHasher,
    ITokenProvider tokenProvider) : IUserCommandRepository
{
    public async Task<Result<bool>> Exists(string email)
    {
        try
        {
            return Result.Ok(await dbContext.Users
                .Where(u => u.Email.Equals(email))
                .AnyAsync());
        }
        catch (Exception ex)
        {
            return Result.Fail(Errors.ExceptionalError(ex));
        }
    }

    public async Task<Result<string>> Login(string username, string password)
    {
        try
        {
            var user = await dbContext.Users
                .Where(u => u.Email.Equals(username))
                .FirstOrDefaultAsync();

            if (user is null)
                return Result.Fail(Errors.NotFound);

            var passwordHash = new PasswordHash(user.PasswordHash, user.PasswordSalt);
            if (!passwordHasher.Verify(password, passwordHash))
                return Result.Fail(Errors.NotFound);

            var token = tokenProvider.GenerateJwtToken(user.Id, user.Email, [.. user.UserRoles.Select(ur => ur.RoleName)]);

            return Result.Ok(token);
        }
        catch (Exception ex)
        {
            return Result.Fail(Errors.ExceptionalError(ex));
        }
    }
}
