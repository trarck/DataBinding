using System;
using System.Reflection;

namespace DataBinding.PropertyProviders
{
	public class FloatStringProvider : PropertyBindingProvider<float, string>
	{
		public override void SyncTarget()
		{
			m_TargetSetter(m_SourceGetter().ToString());
		}

		public override void SyncSource()
		{
			if (m_BindType == BindType.TwoWay)
			{
				float v = 0;
				
				if (float.TryParse(m_TargetGetter(), out v))
				{
					m_SourceSetter(v);
				}
			}
		}


	}
}
