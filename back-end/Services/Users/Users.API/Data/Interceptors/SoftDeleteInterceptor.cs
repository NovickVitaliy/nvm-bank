using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Users.API.Models.Domain;

namespace Users.API.Data.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is null) return result;
        
        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is { Entity: User user, State: EntityState.Deleted })
            {
                entry.State = EntityState.Modified;
                user.IsDeleted = true;
                
                foreach (var pn in user.PhoneNumbers)
                {
                    pn.IsDeleted = true;
                }
                
            }else if (entry is { Entity: PhoneNumber phoneNumber, State: EntityState.Deleted })
            {
                entry.State = EntityState.Modified;
                phoneNumber.IsDeleted = true;
            }
        }
        
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}