using System;
using NUnit.Framework;

namespace NUnit.Specifications
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true,
		Inherited = true)]
	public sealed class IntegrationAttribute : CategoryAttribute
	{
		public IntegrationAttribute() : base("integration")
		{
		}
	}
}