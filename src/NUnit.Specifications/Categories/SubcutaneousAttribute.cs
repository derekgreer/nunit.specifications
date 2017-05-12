using System;
using NUnit.Framework;
using NUnit.Specifications.Annotations;

namespace NUnit.Specifications.Categories
{
    [AttributeUsage(AttributeTargets.Class)]
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    public sealed class SubcutaneousAttribute : CategoryAttribute
    {
        public SubcutaneousAttribute() : base("Subcutaneous")
        {
        }
    }
}