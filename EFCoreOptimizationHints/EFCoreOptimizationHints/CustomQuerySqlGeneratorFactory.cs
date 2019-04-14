using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Sql.Internal;

namespace EFCoreOptimizationHints
{
    public class CustomQuerySqlGeneratorFactory : SqlServerQuerySqlGeneratorFactory
    {
        private readonly ISqlServerOptions _sqlServerOptions;

        public CustomQuerySqlGeneratorFactory(
            QuerySqlGeneratorDependencies dependencies,
            ISqlServerOptions sqlServerOptions)
            : base(dependencies, sqlServerOptions)
        {
            _sqlServerOptions = sqlServerOptions;
        }

        public override IQuerySqlGenerator CreateDefault(SelectExpression selectExpression) 
            => new CustomSqlBuilder(Dependencies, selectExpression, _sqlServerOptions.RowNumberPagingEnabled);
    }
}
