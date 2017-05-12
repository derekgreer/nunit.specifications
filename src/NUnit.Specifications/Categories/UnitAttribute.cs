using System;
using NUnit.Framework;
using NUnit.Specifications.Annotations;

namespace NUnit.Specifications.Categories
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    public sealed class UnitAttribute : CategoryAttribute
    {
        public UnitAttribute()
            : base("Unit")
        {
        }
    }
}