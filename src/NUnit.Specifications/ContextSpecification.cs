using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace NUnit.Specifications
{
	public abstract class ContextAttribute : Attribute
	{
		public abstract void Initialize(dynamic context);
	}

	[DebuggerStepThrough]
	[TestFixture]
	public abstract class ContextSpecification
	{
		public static dynamic Context { get; protected set; }

		public delegate void Because();

		public delegate void Cleanup();

		public delegate void Establish();

		public delegate void It();

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			InitializeContext();
			InvokeEstablish();
			InvokeBecause();
		}

		void InitializeContext()
		{
			Context = new ExpandoObject();
			Type t = GetType();

			IEnumerable<ContextAttribute> contexts =
				t.GetCustomAttributes(typeof (ContextAttribute), true).Cast<ContextAttribute>();

			foreach (ContextAttribute context in contexts)
			{
				context.Initialize(Context);
			}
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			InvokeCleanup();
		}

		void InvokeEstablish()
		{
			var types = new Stack<Type>();
			Type type = GetType();

			do
			{
				types.Push(type);
				type = type.BaseType;
			} while (type != typeof (ContextSpecification));

			foreach (Type t in types)
			{
				FieldInfo establishFieldInfo =
					t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
						.SingleOrDefault(x => x.FieldType.Name.Equals("Establish"));

				Delegate establish = null;

				if (establishFieldInfo != null) establish = establishFieldInfo.GetValue(this) as Delegate;
				if (establish != null) establish.DynamicInvoke(null);
			}
		}

		void InvokeBecause()
		{
			Type t = GetType();

			FieldInfo becauseFieldInfo =
				t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
					.SingleOrDefault(x => x.FieldType.Name.Equals("Because"));

			Delegate because = null;

			if (becauseFieldInfo != null) because = becauseFieldInfo.GetValue(this) as Delegate;
			if (because != null) because.DynamicInvoke(null);
		}

		void InvokeCleanup()
		{
			try
			{
				Type t = GetType();

				FieldInfo cleanupFieldInfo =
					t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
						.SingleOrDefault(x => x.FieldType.Name.Equals("Cleanup"));

				Delegate cleanup = null;

				if (cleanupFieldInfo != null) cleanup = cleanupFieldInfo.GetValue(this) as Delegate;
				if (cleanup != null) cleanup.DynamicInvoke(null);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public IEnumerable GetObservations()
		{
			Type t = GetType();

			var category = (CategoryAttribute) t.GetCustomAttributes(typeof (CategoryAttribute), true).FirstOrDefault();
			string categoryName = null;
			if (category != null)
				categoryName = category.Name;

			IEnumerable<FieldInfo> itFieldInfos =
				t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
					.Where(x => x.FieldType.Name.Equals("It"));

			return
				itFieldInfos.Select(
					fieldInfo => new TestCaseData(fieldInfo.GetValue(this)).SetName(fieldInfo.Name).SetCategory(categoryName));
		}

		[Test, TestCaseSource("GetObservations")]
		public void Observation(It observation)
		{
			observation();
		}
	}
}