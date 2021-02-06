using System;
using GoogleHashCode.Base;

namespace GoogleHashCode.Model
{
	public record Output : IOutput
	{
		public string[] GetOutputFormat()
		{
			throw new NotImplementedException();
		}

		public int GetScore()
		{
			throw new NotImplementedException();
		}
	}
}