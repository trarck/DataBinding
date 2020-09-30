using System;
using System.Reflection;

namespace DataBinding.PropertyProviders
{
	public class ShortStringProvider : PropertyBindingProvider<short,string>
	{
		public override void SyncTarget()
		{
			targetSetter(sourceGetter().ToString());
		}

		public override void SyncSource()
		{
			if (m_BindType == BindType.TwoWay)
			{
				short v = 0;
				if (short.TryParse(targetGetter(), out v))
				{
					sourceSetter(v);
				}
			}
		}
	}
}
