using System.IO;
using NUnit.Framework;
using NUnit.Specifications.Specs.TestHelpers;
using Shouldly;

namespace NUnit.Specifications.Specs
{
	public class ContextAttributeSpecs
	{
		[Initializable]
		public class when_a_specification_is_decorated_with_a_context_attribute : ContextSpecification
		{
			It should_initialize_the_attribute = () => ((bool) Context.IsInitialized).ShouldBe(true);

			It should_initialize_the_attribute_only_once = () => ((int) Context.InitializeCount).ShouldBe(1);
		}
	}

	// Used to manually validate that excluded categories don't get executed
	[CreateFile("touch.txt"), Category("exclude")]
	public class when_an_specification_is_decorated_with_a_context_attribute_that_creates_a_file : ContextSpecification
	{
		Cleanup after = () => File.Delete("touch.txt");

		It should_not_execute = () => File.Exists("touch.txt").ShouldBe(true);
	}
}