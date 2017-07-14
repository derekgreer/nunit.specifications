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
        public class when_executing_a_specification_and_an_exception_is_thrown_in_the_establish
        {
            class SubjectUnderTest : ContextSpecification 
            {
                public Exception CaughtException => Exception.InnerException;
                Establish context = () => throw new Exception("context");
                Because of = () => throw new Exception("because");
            }

            [Test]
            public void It_should_bubble_the_exception_from_the_establish()
            {
                var subjectUnderTest = new SubjectUnderTest();
                subjectUnderTest.TestFixtureSetUp();
                Assert.AreEqual("context", subjectUnderTest.CaughtException.Message);
            }
        }

        [TestFixture]
        [Subcutaneous]
        [Subject("Context Specification")]
        public class when_executing_a_specification_and_an_exception_is_thrown_in_the_because
        {
            SubjectUnderTest subjectUnderTest;
            static bool establishHasRun;

            class SubjectUnderTest : ContextSpecification 
            {
                public Exception CaughtException => Exception.InnerException;
                Establish context = () => establishHasRun = true;
                Because of = () => throw new Exception("because");
            }

            [SetUp]
            public void SetUp()
            {
                subjectUnderTest = new SubjectUnderTest();
                subjectUnderTest.TestFixtureSetUp();
            }

            [Test]
            public void It_should_call_the_establish()
            {
                Assert.IsTrue(establishHasRun);
            }

            [Test]
            public void It_should_bubble_the_exception_from_the_because()
            {
                Assert.AreEqual("because", subjectUnderTest.CaughtException.Message);
            }
        }
    }
}