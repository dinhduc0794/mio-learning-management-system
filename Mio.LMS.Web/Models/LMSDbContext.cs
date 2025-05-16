﻿using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Mio.LMS.Web.Models;

public class LMSDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    // use to get thong tin user dang truy cap from httpcontext -> dung de lay userId create, update
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    // constructor nay duoc goi khi tao moi 1 doi tuong trong DbContext
    public LMSDbContext(DbContextOptions<LMSDbContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;     // moi noi trong class nay deu co the su dung _httpContextAccessor de lay userId
    }
    
    public override int SaveChanges()
    {
        ApplyAuditInfo();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInfo();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditInfo()
    {
        // HttpContext là nơi chứa thông tin của request hiện tại (ai đang gọi, trình duyệt gì, token gì…)
        // lay userId cua nguoi dung dang dang nhap tu httpcontext
        var userIdStr = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);  
        int? userId = int.TryParse(userIdStr, out var id) ? id : null;

        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseModel &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (BaseModel)entry.Entity;
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedByUserId = userId;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.Now;
                entity.CreatedByUserId = userId;
            }
        }
    }
}