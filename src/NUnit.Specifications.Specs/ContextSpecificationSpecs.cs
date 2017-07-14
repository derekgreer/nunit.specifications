using System;
using System.Collections;
using NUnit.Framework;
using NUnit.Specifications.Categories;

namespace NUnit.Specifications.Specs
{
    public class ContextSpecificationSpecs
    {
        [Subcutaneous]
        [Subject("Context Specification")]
        public class when_executing_a_specification : ContextSpecification
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

        [TestFixture]
        [Subcutaneous]
        [Subject("Context Specification")]
        public class when_executing_a_specification_and_an_exception_is_thrown_in_the_establish : ContextSpecification
        {
            static SubjectUnderTest _subjectUnderTest;
            static Exception _exception;

            Establish context = () => _subjectUnderTest = new SubjectUnderTest();

            Because of = () => _exception = Catch.Exception(() => _subjectUnderTest.TestFixtureSetUp());

            It should_bubble_the_exception_from_the_establish = () => Assert.AreEqual("context", _subjectUnderTest.CaughtException.Message);

            class SubjectUnderTest : ContextSpecification 
            {
                public Exception CaughtException => Exception.InnerException;
                Establish context = () => throw new Exception("context");
                Because of = () => throw new Exception("because");
            }
        }

        [TestFixture]
        [Subcutaneous]
        [Subject("Context Specification")]
        public class when_executing_a_specification_and_an_exception_is_thrown_in_the_because: ContextSpecification
        {
            static SubjectUnderTest _subjectUnderTest;
            static Exception _exception;
            static bool _establishHasRun;

            Establish context = () => _subjectUnderTest = new SubjectUnderTest();

            Because of = () => _exception = Catch.Exception(() => _subjectUnderTest.TestFixtureSetUp());

            It should_call_the_establish = () => Assert.IsTrue(_establishHasRun);

            It should_bubble_the_exception_from_the_because = () => Assert.AreEqual("because", _subjectUnderTest.CaughtException.Message);

            class SubjectUnderTest : ContextSpecification 
            {
                public Exception CaughtException => Exception.InnerException;
                Establish context = () => _establishHasRun = true;
                Because of = () => throw new Exception("because");
            }
        }
    }
}