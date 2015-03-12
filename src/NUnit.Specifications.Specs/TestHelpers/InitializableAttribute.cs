namespace NUnit.Specifications.Specs.TestHelpers
{
	public class InitializableAttribute : ContextAttribute
	{
		static int _count;

		public override void Initialize(dynamic context)
		{
			context.IsInitialized = true;
			_count++;
			context.InitializeCount = _count;
		}
	}
}