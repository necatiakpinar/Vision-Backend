using Microsoft.AspNetCore.Identity;
using Vision.Application.Repositories.Role;
using Vision.Domain.Identity;

namespace Vision.Infrastructure.Stores
{
    public class RoleStore<TRole> : IRoleStore<TRole> where TRole : AppRole
    {
        private readonly IRoleWriteRepository _roleWriteRepository;
        private readonly IRoleReadRepository _roleReadRepository;

        public RoleStore(IRoleWriteRepository roleWriteRepository, IRoleReadRepository roleReadRepository)
        {
            _roleWriteRepository = roleWriteRepository;
            _roleReadRepository = roleReadRepository;
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            var role = await _roleReadRepository.GetAsync(r => r.NormalizedName == roleName.ToUpperInvariant());
            return role != null;
        }

        public async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
        {
            await _roleWriteRepository.AddAsync(role, cancellationToken);
            await _roleWriteRepository.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
        {
            _roleWriteRepository.Update(role);
            await _roleWriteRepository.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
        {
            _roleWriteRepository.Remove(role);
            await _roleWriteRepository.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
        {
            return await Task.FromResult(role.Id);
        }

        public async Task<string?> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            return await Task.FromResult(role.Name);
        }

        public async Task SetRoleNameAsync(TRole role, string? roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            await Task.CompletedTask;
        }

        public async Task<string?> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            return await Task.FromResult(role.NormalizedName);
        }

        public async Task SetNormalizedRoleNameAsync(TRole role, string? normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            await Task.CompletedTask;
        }

        public async Task<TRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            var role = await _roleReadRepository.GetAsync(r => r.Id == roleId);
            if (role == null)
            {
                return null;
            }

            return (TRole)role;
        }

        public async Task<TRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var role = await _roleReadRepository.GetAsync(r => r.NormalizedName == normalizedRoleName);
            if (role == null)
            {
                return null;
            }

            return (TRole)role;
        }

        public void Dispose()
        {
            // Dispose işlemleri
        }
    }
}