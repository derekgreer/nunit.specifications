using System.IO;

namespace NUnit.Specifications.Specs.TestHelpers
{
	public class CreateFileAttribute : ContextAttribute
	{
		readonly string _fileName;

		public CreateFileAttribute(string fileName)
		{
			_fileName = fileName;
		}

		public override void Initialize(dynamic context)
		{
			using (var sw = new StreamWriter(_fileName, true))
			{
				//write to the file
			}
		}
	}
}