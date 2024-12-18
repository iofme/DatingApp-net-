using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Upadate(AppUser user);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser?> GetUserByIdAsync(int id);
        Task<AppUser?> GetUserByUsernameAsync(string username);
        Task<PagedList<MemberDto>> GetMemberAsync(UserParams userParams);
        Task<AppUser?> GetUserByPhotoId(int photoId); 
        Task<MemberDto> GetMemberAsync(string username, bool isCurrentUser);
        Task<MemberDto?> GetMemberAsync(string username);
    }
}