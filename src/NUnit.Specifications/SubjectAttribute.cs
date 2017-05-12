using System;
using NUnit.Specifications.Annotations;

namespace NUnit.Specifications
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    public sealed class SubjectAttribute : Attribute
    {
        public SubjectAttribute(string subject)
        {
            Subject = subject;
        }

        public string Subject { get; set; }
    }
}