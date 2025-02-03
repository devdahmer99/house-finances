using financesFlow.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace financesFlow.Infra.Migrations;
public static class DatabaseMigration
{
   public async static Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<financesFlowDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}
