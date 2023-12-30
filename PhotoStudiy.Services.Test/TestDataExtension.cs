using PhotoStudiy.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Repositories.Test
{
    internal static class TestDataExtension
    {
        public static void BaseAuditSetParamtrs<TEntity>(this TEntity entity) where TEntity : BaseAuditEntity
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.CreatedBy = $"CreatedBy{Guid.NewGuid():N}";
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}";
        }
    }
}
