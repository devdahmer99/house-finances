using financesFlow.Dominio.Entidades;
using PdfSharp.Drawing;

namespace WebApi.Test.Resources
{
    public class ExpenseIdentityManager
    {
        private readonly Despesa _expense;

        public ExpenseIdentityManager(Despesa expense)
        {
            _expense = expense;
        }

        public long GetId() => _expense.Id;
        public DateTime GetDate() => _expense.DataDespesa;
    }
}
