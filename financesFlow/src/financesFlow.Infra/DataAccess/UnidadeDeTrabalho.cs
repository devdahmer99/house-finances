using financesFlow.Dominio.Repositories;

namespace financesFlow.Infra.DataAccess
{
    internal class UnidadeDeTrabalho : IUnidadeDeTrabalho
    {
        private readonly financesFlowDbContext _db;
        public UnidadeDeTrabalho(financesFlowDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task Commit()
        {
           await _db.SaveChangesAsync();
        }
    }
}
