using System;
using System.Reflection;

namespace DataBinding.PropertyProviders
{
	public class CustomConvertProvider<TSourceProperty, TTargetProperty> : PropertyBindingProvider<TSourceProperty, TTargetProperty>
	{
		protected Func<TSourceProperty, TTargetProperty> m_SourceToTargetConvert;
		protected Func<TTargetProperty, TSourceProperty> m_TargetToSourceConvert;

		protected Type m_SourcePropertyType;
		protected Type m_TargetPropertyType;

		public CustomConvertProvider(Func<TSourceProperty, TTargetProperty> sourceToTargetConvert,
			Func<TTargetProperty, TSourceProperty> targetToSourceConvert)
		{
			if (sourceToTargetConvert != null)
			{
				m_SourceToTargetConvert = sourceToTargetConvert;
			}
			else
			{
				m_SourceToTargetConvert = DefaultSourceToTarget;
			}

			if (targetToSourceConvert != null)
			{
				m_TargetToSourceConvert = targetToSourceConvert;
			}
			else
			{
				m_TargetToSourceConvert = DefaultTargetToSource;
			}
		}

		public CustomConvertProvider(Delegate sourceToTargetConvert, Delegate targetToSourceConvert)
		{

			if (sourceToTargetConvert != null)
			{
				m_SourceToTargetConvert = (Func<TSourceProperty, TTargetProperty>)sourceToTargetConvert;
			}
			else
			{
				m_SourceToTargetConvert = DefaultSourceToTarget;
			}

			if (targetToSourceConvert != null)
			{
				m_TargetToSourceConvert = (Func<TTargetProperty, TSourceProperty>)targetToSourceConvert;
			}
			else
			{
				m_TargetToSourceConvert = DefaultTargetToSource;
			}
		}

		public CustomConvertProvider()
		{
			m_SourceToTargetConvert = DefaultSourceToTarget;
			m_TargetToSourceConvert = DefaultTargetToSource;
		}

		protected override void SetupGetterSetterExt(Type sourceType, PropertyInfo sourcePropertyInfo, Type targetType, PropertyInfo targetPropertyInfo)
		{
			m_SourcePropertyType = sourcePropertyInfo.PropertyType;
			m_TargetPropertyType = targetPropertyInfo.PropertyType;
		}

		public override void SyncTarget()
		{
			if (m_SourceToTargetConvert != null)
			{
				TSourceProperty v = m_SourceGetter();
				m_TargetSetter(m_SourceToTargetConvert(v));
			}
		}

		public override void SyncSource()
		{
			if (m_BindType == BindType.TwoWay)
			{
				if (m_TargetToSourceConvert != null)
				{
					TTargetProperty v = m_TargetGetter();
					m_SourceSetter(m_TargetToSourceConvert(v));
				}
			}
		}

		private TTargetProperty DefaultSourceToTarget(TSourceProperty v)
		{
			return (TTargetProperty)Convert.ChangeType(v, m_TargetPropertyType);
		}

		private TSourceProperty DefaultTargetToSource(TTargetProperty v)
		{
			return (TSourceProperty)Convert.ChangeType(v, m_SourcePropertyType);
		}

		public Func<TSourceProperty, TTargetProperty> sourceToTargetConvert
		{
			get
			{
				return m_SourceToTargetConvert;
			}
			set
			{
				m_SourceToTargetConvert = value;
			}
		}

		public Func<TTargetProperty, TSourceProperty> targetToSourceConvert
		{
			get
			{
				return m_TargetToSourceConvert;
			}
			set
			{
				m_TargetToSourceConvert = value;
			}
		}
	}
}
