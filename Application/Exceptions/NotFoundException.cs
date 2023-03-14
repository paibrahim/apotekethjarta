using System.Diagnostics.CodeAnalysis;

namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }

        public static void ThrowIfNull([NotNull] object? argument)
        {
            if (argument is not null)
            {
                return;
            }

            throw new NotFoundException();
        }
    }
}
