using System.Collections;
using System.Linq;
using Should;

namespace NUnit.Specifications.Specs
{
	public class ContextSpecificationSpecs
	{
		[Subcutaneous]
		public class when_executing_a_specification : ContextSpecification
		{
			static readonly Stack _executionStack = new Stack();

			Establish context = () => _executionStack.Push("context");

			Because of = () => _executionStack.Push("because");

			It should_execute_the_establish_delegate_successfully = () => _executionStack.ToArray().Last().ShouldEqual("context");

			It should_execute_the_because_delegate_successfully = () => _executionStack.ToArray().First().ShouldEqual("because");

			It should_execute_the_it_delegate_successfully = () => true.ShouldBeTrue();
		}
	}
}