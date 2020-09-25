using System;
using System.Reflection;

namespace DataBinding.Providers
{
	public class PropertyBindingIntString : PropertyBindingBase<int, string>
	{
		public override void SyncSource()
		{
			if (m_BindType == PropertyBindType.TwoWay)
			{
				int v = 0;
				if (int.TryParse(targetGetter(), out v))
				{
					sourceSetter(v);
				}
			}
		}

		public override void SyncTarget()
		{
			targetSetter(sourceGetter().ToString());
		}
	}
}
