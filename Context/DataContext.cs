using Microsoft.EntityFrameworkCore;

namespace ConsultaAPICodeFirst.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

    }
}
