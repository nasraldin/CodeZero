using System.Threading.Tasks;
using JetBrains.Annotations;

namespace CodeZero.ExceptionHandling
{
    public interface IExceptionSubscriber
    {
        Task HandleAsync([NotNull] ExceptionNotificationContext context);
    }
}
