using System.Threading;
using System.Threading.Tasks;

namespace Afonsoft.Extensions
{
    public static class TaskExtensions
    {
        //https://stackoverflow.com/questions/10134310/how-to-cancel-a-task-in-await

#if NET40
        public static Task<T> WaitOrCancel<T>(this Task<T> task, CancellationToken token)

#else
        public async static Task<T> WaitOrCancel<T>(this Task<T> task, CancellationToken token)
#endif
        {
            token.ThrowIfCancellationRequested();
#if NET40
            Task.WaitAny(task, token.WhenCanceled());
#else
            await Task.WhenAny(task, token.WhenCanceled());
#endif
            token.ThrowIfCancellationRequested();
#if NET40
            return task;
#else
            return await task;
#endif
        }

        public static Task WhenCanceled(this CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
            return tcs.Task;
        }
    }
}