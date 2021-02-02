using System.Threading.Tasks;

namespace CodeZero.ExceptionHandling
{
    public abstract class ExceptionSubscriber : IExceptionSubscriber
    {
        public abstract Task HandleAsync(ExceptionNotificationContext context);
    }
}