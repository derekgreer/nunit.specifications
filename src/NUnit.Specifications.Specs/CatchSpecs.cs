using System;
using System.Text;
using Should;

namespace NUnit.Specifications.Specs
{
	public class CatchSpecs
	{
		[Component]
		public class when_catching_an_exception : ContextSpecification
		{
			static Exception _exception;

			Because of = () => _exception = Catch.Exception(() => { throw new Exception("oh nos!"); });

			It should_catch_the_exception = () => _exception.ShouldNotBeNull();
		}

		[Component]
		public class when_an_exception_is_not_thrown : ContextSpecification
		{
			static Exception _exception;

			Because of = () => _exception = Catch.Exception(() => new StringBuilder("nothing wrong here"));

			It should_catch_the_exception = () => _exception.ShouldBeNull();
		}
	}
}