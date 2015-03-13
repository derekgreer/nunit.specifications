using System;
using NUnit.Framework;

namespace NUnit.Specifications
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true,
		Inherited = true)]
	public sealed class SubcutaneousAttribute : CategoryAttribute
	{
		public SubcutaneousAttribute() : base("Subcutaneous")
		{
		}
	}
}