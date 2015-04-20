using NUnit.Framework;

namespace NUnit.Specifications.Specs
{
  public class TDDNetSpecs
  {
    // TestDriven.Net seems to require a using for NUnit.Framework.
    [Category("TestDriven.Net")]
    public class when_running_with_tddnet : ContextSpecification
    {
      It should_run_successfully = () => Assert.IsTrue(true);
    }
  }
}