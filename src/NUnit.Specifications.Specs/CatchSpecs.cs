using System;
using Shouldly;

namespace NUnit.Specifications.Specs
{
	public class CatchSpecs
	{
		[Component]
		public class when_catching_an_exception : ContextSpecification
		{
			static Exception _exception;

			Because of = () => _exception = Catch.Exception(() => { throw new Exception("oh nos!"); });

			It should_catch_the_exception = () => _exception.ShouldNotBe(null);
		}
	}
}