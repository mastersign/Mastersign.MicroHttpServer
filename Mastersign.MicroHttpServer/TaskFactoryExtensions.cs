using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    public static class TaskFactoryExtensions
    {
        private static readonly Task CompletedTask = Task.FromResult<object>(null);

        public static Task GetCompleted(this TaskFactory _) => CompletedTask;
    }
}