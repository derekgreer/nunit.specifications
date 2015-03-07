using NUnit.Framework;
using Shouldly;

namespace NUnit.Specifications.Specs
{
	public class TDDNetSpecs
	{
		// TestDriven.Net seems to require a using for NUnit.Framework.
		[Category("component")]
		public class when_running_with_tddnet : ContextSpecification
		{
			It should_run_successfully = () => true.ShouldBe(true);
		} 
	}
}