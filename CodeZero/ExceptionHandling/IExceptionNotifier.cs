using System.Threading.Tasks;
using JetBrains.Annotations;

namespace CodeZero.ExceptionHandling
{
    public interface IExceptionNotifier
    {
        Task NotifyAsync([NotNull] ExceptionNotificationContext context);
    }
}