using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace financesFlow.Dominio.Entidades
{
    public class Tag
    {
        public long Id { get; set; }
        public Enums.Tag Value { get; set; }

        public long DespesaId { get; set; }
        public Despesa Despesa { get; set; } = default!;
    }
}
