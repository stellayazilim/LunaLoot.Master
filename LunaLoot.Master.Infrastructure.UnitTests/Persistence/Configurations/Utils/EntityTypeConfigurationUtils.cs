using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LunaLoot.Master.Infrastructure.UnitTests.Persistence.Configurations.Utils;


public static partial class Utils
{
   
    public static EntityTypeBuilder<TEntity> CreateEntityTypeBuilder<TEntity>() where TEntity : class
    {
        var entityType = new EntityType(
            typeof(TEntity).Name, 
            typeof(TEntity), 
            new Model(),
            false, 
            ConfigurationSource.Convention);

        #pragma warning disable EF1001
        return new EntityTypeBuilder<TEntity>(entityType);
        #pragma warning restore EF1001
    }
  
}
