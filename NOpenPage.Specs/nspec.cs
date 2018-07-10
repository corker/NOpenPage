using System.Linq;
using FluentAssertions;
using NSpec;
using NSpec.Domain;
using NSpec.Domain.Formatters;
using NUnit.Framework;

namespace NOpenPage.Specs
{
    [TestFixture]
    public abstract class nspec : NSpec.nspec
    {
        private static readonly bool IsDebug;

        static nspec()
        {
#if DEBUG
            IsDebug = true;
#endif
        }

        [Test]
        public void debug()
        {
            var spec = GetType();
            var finder = new SpecFinder(new[] {spec});
            var tags = new Tags().Parse(spec.Name);
            var builder = new ContextBuilder(finder, tags, new DefaultConventions());
            var formatter = IsDebug ? (IFormatter) new ConsoleFormatter() : new XUnitFormatter();
            var runner = new ContextRunner(tags, formatter, false);
            var contexts = builder.Contexts().Build();
            var results = runner.Run(contexts);

            //assert that there aren't any failures
            results.Failures().Count().Should().Be(0);
        }
    }
}