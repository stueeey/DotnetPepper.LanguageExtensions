using System;
using System.Runtime.ExceptionServices;
using System.Linq;

namespace System.Collections.Generic
{
    internal static class DotnetPepperEnumerableExtensions
    {
        internal class ForEachError<T>
        {
            internal ForEachError(T item, long itemIndex, Exception ex)
            {
                Item = item;
                ItemIndex = itemIndex;
                _exceptionHolder = ExceptionDispatchInfo.Capture(ex ?? throw new ArgumentNullException(nameof(ex)));
            }
            private System.Runtime.ExceptionServices.ExceptionDispatchInfo _exceptionHolder { get; }
            public T Item { get; }
            public long ItemIndex { get; }
            public Exception Exception => _exceptionHolder.SourceException;
            public void Throw() => _exceptionHolder.Throw();
        }

        internal static IEnumerable<ForEachError<T>> ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            long index = 0;
            foreach (var item in items)
            {
                Exception exception = null;
                try
                {
                    action(item);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                if (exception != null)
                    yield return new ForEachError<T>(item, index, exception);
                index++;
            }
        }

        internal static ICollection<ForEachError<T>> ForAll<T>(this IEnumerable<T> items, Action<T> action) => items.ForEach(action).ToArray();

        internal static void ThrowAsAggregateException<T>(this ICollection<ForEachError<T>> errors, string message = null)
        {
            if (!errors.Any())
                return;
            throw new AggregateException(message, errors.Select(e => e.Exception));
        }

        internal static void ThrowFirstException<T>(this ICollection<ForEachError<T>> errors)
        {
            var error = errors.FirstOrDefault();
            if (error != null)
                error.Throw();
        }

        internal static void ThrowIfErrors<T>(this ICollection<ForEachError<T>> errors)
        {
            switch (errors.Count)
            {
                case 0:
                    return;
                case 1:
                    errors.First().Throw();
                    break;
                default:
                    errors.ThrowAsAggregateException();
                    break;
            }
        }
    }
}
