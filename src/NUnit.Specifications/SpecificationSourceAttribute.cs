using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace NUnit.Specifications
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class SpecificationSourceAttribute : NUnitAttribute, ITestBuilder, IImplyFixture
    {
        readonly NUnitTestCaseBuilder _builder = new NUnitTestCaseBuilder();

        public SpecificationSourceAttribute(string sourceName)
        {
            SourceName = sourceName;
        }

        public string SourceName { get; }

        public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
        {
            var classNameTokens = suite.FullName.Split('.', '+');
            suite.FullName = classNameTokens[classNameTokens.Length - 1].Replace("_", " ");
            suite.Name = suite.FullName;

            foreach (var testCaseParameters in GetTestCasesFor(method))
                yield return _builder.BuildTestMethod(method, suite, testCaseParameters);
        }

        IEnumerable<TestCaseParameters> GetTestCasesFor(IMethodInfo method)
        {
            var list = new List<TestCaseParameters>();
            try
            {
                var testCaseSource = GetTestCaseSource(method);
                if (testCaseSource != null)
                    foreach (var obj in testCaseSource)
                    {
                        var testCaseData = obj as TestCaseParameters;
                        list.Add(testCaseData);
                    }
            }
            catch (Exception ex)
            {
                list.Clear();
                list.Add(new TestCaseParameters(ex));
            }
            return list;
        }

        IEnumerable GetTestCaseSource(IMethodInfo method)
        {
            var type = method.TypeInfo.Type;
            var specification = Reflect.Construct(type);

            var member = type.GetMember(SourceName,
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (member.Length == 1)
            {
                var memberInfo = member[0];
                var methodInfo = memberInfo as MethodInfo;
                if (methodInfo != null)
                    return (IEnumerable) methodInfo.Invoke(specification, null);
            }
            return null;
        }
    }
}