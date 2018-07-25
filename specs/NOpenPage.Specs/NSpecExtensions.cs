using System.Linq;
using FluentAssertions;
using NSpec;
using NSpec.Domain;
using NSpec.Domain.Formatters;
using Xunit.Abstractions;

namespace NOpenPage.Specs
{
    public static class NSpecExtensions
    {
        public static void Run<T>(this ITestOutputHelper helper) where T : NSpec.nspec
        {
            var spec = typeof(T);
            var finder = new SpecFinder(new[] {spec});
            var tags = new Tags().Parse(spec.Name);
            var builder = new ContextBuilder(finder, tags, new DefaultConventions());
            var formatter = new MyFormatter(helper);
            var runner = new ContextRunner(tags, formatter, false);
            var contexts = builder.Contexts().Build();
            var results = runner.Run(contexts);

            //assert that there aren't any failures
            results.Failures().Count().Should().Be(0);
        }

        private class MyFormatter : ConsoleFormatter
        {
            public MyFormatter(ITestOutputHelper helper)
            {
                WriteLineDelegate = helper.WriteLine;
            }
        }
    }
}