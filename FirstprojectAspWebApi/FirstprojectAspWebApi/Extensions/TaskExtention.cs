using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FirstprojectAspWebApi.Extensions
{
    public static class TaskExtention
    {
        public static ConfiguredTaskAwaitable AnyContext(this Task task)
        {
            return task.ConfigureAwait(false);
        }

        public static ConfiguredTaskAwaitable<T> AnyContext<T>(this Task<T> task)
        {
            return task.ConfigureAwait(false);
        }
    }
}
