using financesFlow.Dominio.Repositories;
using Microsoft.Extensions.Logging;

namespace financesFlow.Infra.DataAccess
{
    internal class UnidadeDeTrabalho : IUnidadeDeTrabalho
    {
        private readonly financesFlowDbContext _db;
        private readonly ILogger<UnidadeDeTrabalho> _logger;
        public UnidadeDeTrabalho(financesFlowDbContext dbContext, ILogger<UnidadeDeTrabalho> logger)
        {
            _db = dbContext;
            _logger = logger;
        }
        public async Task Commit()
        {
            try
            {
                await _db.SaveChangesAsync();
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }      
        }
    }
}
