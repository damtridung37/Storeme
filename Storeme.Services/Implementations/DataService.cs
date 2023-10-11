using Storeme.Data;

namespace Storeme.Services.Implementations
{
    public abstract class DataService
    {
        protected readonly StoremeDbContext context;

        public DataService(StoremeDbContext context)
        {
            this.context = context;
        }
    }
}
