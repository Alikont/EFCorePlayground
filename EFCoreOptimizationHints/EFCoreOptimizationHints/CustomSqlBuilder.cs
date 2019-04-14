using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Sql.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EFCoreOptimizationHints
{
    public class CustomSqlBuilder : SqlServerQuerySqlGenerator
    {
        public CustomSqlBuilder(
            QuerySqlGeneratorDependencies dependencies,
            SelectExpression selectExpression,
            bool rowNumberPagingEnabled)
            : base(dependencies, selectExpression, rowNumberPagingEnabled)
        {
        }

        public override Expression VisitSelect(SelectExpression selectExpression)
        {
            base.VisitSelect(selectExpression);

            //And here we check if query requires recompilation
            if (selectExpression.Tags.Contains(QueryHints.RecompileTag))
            {
                Sql.AppendLine().AppendLine("option(recompile)");
            }

            return selectExpression;
        }
    }
}
