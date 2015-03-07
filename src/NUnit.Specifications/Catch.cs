using System;

namespace NUnit.Specifications
{
	public static class Catch
	{
		public static Exception Exception(Func<Exception> action)
		{
			Exception exception = null;

			try
			{
				action();
			}
			catch (Exception e)
			{
				exception = e;
			}

			return exception;
		}
	}
}