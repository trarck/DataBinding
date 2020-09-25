using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindig
{
	using DataBinding.Providers;
	class Program
	{
		static void Main(string[] args)
		{
			PropertyBindingDelegate<int, string> pdb = new PropertyBindingDelegate<int, string>((self) =>
			{
				int v = 0;
				if (int.TryParse(self.targetGetter(), out v))
				{
					self.sourceSetter(v);
				}
			},
			(self) =>
			{
				self.targetSetter(self.sourceGetter().ToString());
			});
		}
	}
}
