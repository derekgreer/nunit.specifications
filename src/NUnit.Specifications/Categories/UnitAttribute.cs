using System;
using NUnit.Framework;

namespace NUnit.Specifications
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true,
		Inherited = true)]
	public sealed class UnitAttribute : CategoryAttribute
	{
		public UnitAttribute()
			: base("Unit")
		{
		}
	}
}