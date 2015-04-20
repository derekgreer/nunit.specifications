using NUnit.Framework;

namespace NUnit.Specifications.Specs
{
  public class UncategorizedSpecs
  {
    public class when_a_specification_is_uncategorized : ContextSpecification
    {
      It should_be_run_successfully = () => Assert.IsTrue(true);
    }
  }
}