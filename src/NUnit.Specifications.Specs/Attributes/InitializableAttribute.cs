namespace NUnit.Specifications.Specs.Attributes
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