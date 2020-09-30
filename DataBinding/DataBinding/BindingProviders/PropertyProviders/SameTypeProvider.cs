using System;
using System.Reflection;

namespace DataBinding.PropertyProviders
{
	public class SameTypeProvider<T> : PropertyBindingProvider<T, T>
	{
		public override void SyncTarget()
		{
			m_TargetSetter(m_SourceGetter());
		}

		public override void SyncSource()
		{
			if (m_BindType == BindType.TwoWay)
			{
				m_SourceSetter(m_TargetGetter());
			}
		}
	}
}
