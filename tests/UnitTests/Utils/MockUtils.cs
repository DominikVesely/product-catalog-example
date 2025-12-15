using Data;
using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using NSubstitute;
using System.Linq.Expressions;

namespace UnitTests.Utils;

public static class MockUtils
{
    public static AppDbContext MockDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().Options;
        var context = Substitute.For<AppDbContext>(options);

        return context;
    }

    public static AppDbContext MockDbSet<T>(this AppDbContext contextSub, Expression<Func<AppDbContext, DbSet<T>>> dbsetExpr, List<T> list)
        where T : class
    {
        var dbSetSub = list.BuildMockDbSet();

        dbsetExpr.Compile().Invoke(contextSub).Returns(dbSetSub);

        return contextSub;
    }
}