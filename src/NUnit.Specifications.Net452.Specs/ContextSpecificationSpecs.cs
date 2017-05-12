using System.Collections;
using NUnit.Framework;
using NUnit.Specifications.Categories;

namespace NUnit.Specifications.Net452.Specs
{
    public class ContextSpecificationSpecs
    {
        [Subcutaneous]
        [Subject("Context Specification")]
        public class when_executing_a_specification_under_net452 : ContextSpecification
        {
            static readonly Stack _executionStack = new Stack();

            Establish context = () => _executionStack.Push("context");

            Because of = () => _executionStack.Push("because");

            It should_execute_the_because_delegate_successfully =
                () => Assert.AreEqual(_executionStack.ToArray()[0], "because");

            It should_execute_the_establish_delegate_successfully =
                () => Assert.AreEqual(_executionStack.ToArray()[_executionStack.Count - 1], "context");

            It should_execute_the_it_delegate_successfully = () => Assert.IsTrue(true);
        }
    }
}