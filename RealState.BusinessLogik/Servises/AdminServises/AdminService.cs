using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealState.Dal.Contexs;
using RealState.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealState.BusinessLogik.Servises.AdminServises
{
    public class AdminService : IAdminService
    {
        private readonly RealStateDbContext _context;
        private readonly PasswordHasher<Admin> _passwordHasher;
        private readonly ILogger<AdminService> _logger;

        public AdminService(RealStateDbContext context, ILogger<AdminService> logger)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Admin>();
            _logger = logger;
        }

        public async Task<Admin> AddAdminAsync(string username, string password)
        {
            try
            {
                _logger.LogInformation("Attempting to add admin with username: {Username}", username);

                var existingUser = await _context.Admins.FirstOrDefaultAsync(u => u.AdminUserName == username);
                if (existingUser != null)
                {
                    _logger.LogWarning("Admin with username {Username} already exists.", username);
                    throw new Exception("این نام کاربری قبلاً ثبت شده است.");
                }

                var admin = new Admin
                {
                    AdminUserName = username,
                    AdminPassword = _passwordHasher.HashPassword(null, password)
                };

                _context.Admins.Add(admin);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Admin {Username} added successfully with Id={Id}", username, admin.Id);
                return admin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding admin with username {Username}", username);
                throw;
            }
        }

        public async Task<Admin> RemoveAdminAsync(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to remove admin with Id={Id}", id);
                var admin = new Admin { Id = id };
                _context.Admins.Remove(admin);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Admin with Id={Id} removed successfully", id);
                return admin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing admin with Id={Id}", id);
                throw;
            }
        }

        public async Task<Admin> EditAdminAsync(int id, string username, string password)
        {
            try
            {
                _logger.LogInformation("Attempting to edit admin with Id={Id}", id);
                var admin = await _context.Admins.FindAsync(id);
                if (admin == null)
                {
                    _logger.LogWarning("Admin with Id={Id} not found", id);
                    throw new Exception("ادمین یافت نشد.");
                }

                admin.AdminUserName = username;
                admin.AdminPassword = password.StartsWith("AQAAAA", StringComparison.Ordinal)
                    ? password
                    : _passwordHasher.HashPassword(admin, password);
                _context.Update(admin);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Admin with Id={Id} updated successfully", id);
                return admin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing admin with Id={Id}", id);
                throw;
            }
        }

        public List<Admin> GetAllAdmins()
        {
            try
            {
                _logger.LogInformation("Fetching all admins from database.");
                var admins = _context.Admins.ToList();
                _logger.LogInformation("Fetched {Count} admins.", admins.Count);
                return admins;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching admins from database.");
                throw;
            }
        }
    }
}
