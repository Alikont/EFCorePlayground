using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EFCoreOptimizationHints
{
    public static class QueryHints
    {
        public static string RecompileTag { get; } = "QueryHint:recompile";

        public static IQueryable<T> WithRecompile<T>(this IQueryable<T> query) => query.TagWith(RecompileTag);
    }
}
