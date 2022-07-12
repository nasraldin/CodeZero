using System.Security.Claims;
using CodeZero.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApp.Domain.Entities;

namespace WebApp.Domain;

public partial class ApplicationDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    //private readonly IMediatorHandler _mediatorHandler;

    public virtual DbSet<Language> Languages { get; set; } = default!;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IHttpContextAccessor httpContextAccessor,
        IDomainEventDispatcher domainEventDispatcher)
    : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
        ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
        ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
        _httpContextAccessor = httpContextAccessor;
        _domainEventDispatcher = domainEventDispatcher;
        //_mediatorHandler = mediatorHandler;
    }

    public override async Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken =
        default)
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        // ignore events if no dispatcher provided
        if (_domainEventDispatcher == null) return result;

        HandleEntityTracker();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void HandleEntityTracker()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var currentUsername = !string.IsNullOrEmpty(userId)
            ? userId
            : "Anonymous";

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                // Handle entity state Added.
                case EntityState.Added:
                    entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                    entry.Property(x => x.CreatedBy).CurrentValue = currentUsername;
                    break;

                // Handle entity state Modified.
                case EntityState.Modified:
                    entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    entry.Property(x => x.UpdatedBy).CurrentValue = currentUsername;
                    break;

                // Handle entity state Deleted.
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Property(x => x.DeletionTime).CurrentValue = DateTime.UtcNow;
                    entry.Property(x => x.IsDeleted).CurrentValue = true;
                    entry.Property(x => x.DeletedBy).CurrentValue = currentUsername;
                    break;
            }
        }
    }

    //private void HandleAdded(string createdBy)
    //{
    //    var added = ChangeTracker.Entries<ICreation>().Where(e => e.State == EntityState.Added);
    //    foreach (var entry in added)
    //    {
    //        entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
    //        entry.Property(x => x.CreatedAt).IsModified = true;
    //        entry.Property(x => x.CreatedBy).CurrentValue = createdBy;
    //    }
    //}

    /// <summary>
    /// Handling Concurrency Conflicts.
    /// Timestamp/RowVersion
    /// </summary>
    //private void HandleVersioned()
    //{
    //    foreach (var versionedModel in ChangeTracker.Entries<IHasConcurrencyStamp>())
    //    {
    //        var versionProp = versionedModel.Property(o => o.ConcurrencyStamp);

    //        if (versionedModel.State == EntityState.Added)
    //        {
    //            versionProp.CurrentValue = DateTime.UtcNow.ToString();
    //        }
    //        else if (versionedModel.State == EntityState.Modified)
    //        {
    //            versionProp.CurrentValue = versionProp.OriginalValue + 1;
    //            versionProp.IsModified = true;
    //        }
    //    }
    //}

    //private void HandleValidatable()
    //{
    //    var validatableModels = ChangeTracker.Entries<IValidatableEntity>();

    //    foreach (var model in validatableModels)
    //        model.Entity.ValidationResult();
    //}

    //private async Task DispatchEvents()
    //{
    //    // dispatch events only if save was successful
    //    //var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
    //    //    .Select(e => e.Entity)
    //    //    .Where(e => e.DomainEvents.Any())
    //    //    .ToArray();

    //    //foreach (var entity in entitiesWithEvents)
    //    //{
    //    //    var events = entity.DomainEvents.ToArray();
    //    //    entity.ClearDomainEvents();
    //    //    foreach (var domainEvent in events)
    //    //    {
    //    //        await _domainEventDispatcher.Dispatch(domainEvent).ConfigureAwait(false);
    //    //    }
    //    //}

    //    while (true)
    //    {
    //        var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
    //            .SelectMany(entry => entry.Entity.DomainEvents)
    //            .FirstOrDefault(domainEvent => !domainEvent.IsPublished);

    //        if (domainEventEntity == null) break;

    //        domainEventEntity.IsPublished = true;
    //        await _domainEventDispatcher.Dispatch(domainEventEntity);
    //    }
    //}

    public override int SaveChanges() => throw new NotImplementedException("Use async version instead!");

    ////public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    ////{
    ////    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    ////    // ignore events if no dispatcher provided
    ////    //if (_dispatcher == null) return result;

    ////    // dispatch events only if save was successful
    ////    //var entitiesWithEvents = ChangeTracker.Entries<BaseEntity<int>>()
    ////    //    .Select(e => e.Entity)
    ////    //    .Where(e => e.Events.Any())
    ////    //    .ToArray();

    ////    //foreach (var entity in entitiesWithEvents)
    ////    //{
    ////    //    var events = entity.Events.ToArray();
    ////    //    entity.Events.Clear();
    ////    //    foreach (var domainEvent in events)
    ////    //    {
    ////    //        await _dispatcher.Dispatch(domainEvent).ConfigureAwait(false);
    ////    //    }
    ////    //}

    ////    return result;
    ////}

    ////public override int SaveChanges()
    ////{
    ////    return SaveChangesAsync().GetAwaiter().GetResult();
    ////}
}
