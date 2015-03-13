using System;

namespace NUnit.Specifications
{
    public abstract class ContextAttribute : Attribute
    {
        public abstract void Initialize(dynamic context);
    }
}