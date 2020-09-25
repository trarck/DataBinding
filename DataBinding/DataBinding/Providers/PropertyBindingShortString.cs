using System;
using System.Reflection;

namespace DataBinding.Providers
{
	public class PropertyBindingSortString : PropertyBindingBase<short,string>
	{
		public override void SyncSource()
		{
			if (m_BindType == PropertyBindType.TwoWay)
			{
				short v = 0;
				if (short.TryParse(targetGetter(), out v))
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
