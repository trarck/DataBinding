using System;
using System.Reflection;

namespace DataBinding.Providers
{
	public class PropertyBindingGeneric<TSourceProperty, TTargetProperty> : PropertyBindingBase<TSourceProperty,TTargetProperty>
	{
		protected Type m_SourcePropertyType;
		protected Type m_TargetPropertyType;

		protected override void SetupGetterSetterExt(Type sourceType, PropertyInfo sourcePropertyInfo, Type targetType, PropertyInfo targetPropertyInfo)
		{
			m_SourcePropertyType = sourcePropertyInfo.PropertyType;
			m_TargetPropertyType = targetPropertyInfo.PropertyType;
		}

		public override void SyncSource()
		{
			if (m_BindType == PropertyBindType.TwoWay)
			{
				TTargetProperty v = m_TargetGetter();
				m_SourceSetter((TSourceProperty)Convert.ChangeType(v, m_SourcePropertyType));
			}
		}

		public override void SyncTarget()
		{
			TSourceProperty v = m_SourceGetter();
			m_TargetSetter((TTargetProperty)Convert.ChangeType(v, m_TargetPropertyType));
		}
	}
}
