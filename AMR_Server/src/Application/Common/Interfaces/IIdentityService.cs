using AMR_Server.Application.Common.Models;
using AMR_Server.Domain.Dtos;
using AMR_Server.Domain.Entities;
using System.Threading.Tasks;

namespace AMR_Server.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
        Task<Result> DeleteUserAsync(string userId);
        Task<string> Login(string username, string password);
        Task<User> AuthenticateAD(string userName, string password);
        Task<User> Register(string username, string password);
    }
}
