using System;
using System.Reflection;

namespace DataBinding.PropertyProviders
{
	public abstract class PropertyBindingProvider<TSourceProperty, TTargetProperty> : PropertyBinding
	{
		//target.property =	source.property
		protected Func<TSourceProperty> m_SourceGetter;
		protected Action<TTargetProperty> m_TargetSetter;

		//source.property =	target.property
		protected Action<TSourceProperty> m_SourceSetter;
		protected Func<TTargetProperty> m_TargetGetter;

		protected override void SetupGetterSetter()
		{
			Type sourceType = m_Source.GetType();
			Type targetType = m_Target.GetType();

			PropertyInfo sourcePropertyInfo = sourceType.GetProperty(m_SourcePropertyName);
			PropertyInfo targetPropertyInfo = targetType.GetProperty(m_TargetPropertyName);

			MethodInfo getMethodInfo = sourcePropertyInfo.GetGetMethod();
			MethodInfo setMethodInfo = targetPropertyInfo.GetSetMethod();

			m_SourceGetter = BindingHelper.CreateGetter<TSourceProperty>(m_Source, getMethodInfo);
			m_TargetSetter = BindingHelper.CreateSetter<TTargetProperty>(m_Target, setMethodInfo);

			if (m_BindType == BindType.TwoWay)
			{
				setMethodInfo = sourcePropertyInfo.GetSetMethod();
				getMethodInfo = targetPropertyInfo.GetGetMethod();

				m_SourceSetter = BindingHelper.CreateSetter<TSourceProperty>(m_Source, setMethodInfo);
				m_TargetGetter = BindingHelper.CreateGetter<TTargetProperty>(m_Target, getMethodInfo);
			}

			SetupGetterSetterExt(sourceType, sourcePropertyInfo, targetType, targetPropertyInfo);
		}

		protected virtual void SetupGetterSetterExt(Type sourceType,PropertyInfo sourcePropertyInfo, Type targetType,PropertyInfo targetPropertyInfo)
		{
			
		}

		public Func<TSourceProperty> sourceGetter
		{
			get
			{
				return m_SourceGetter;
			}
		}

		public Action<TSourceProperty> sourceSetter
		{
			get
			{
				return m_SourceSetter;
			}
		}

		public Func<TTargetProperty> targetGetter
		{
			get
			{
				return m_TargetGetter;
			}
		}

		public Action<TTargetProperty> targetSetter
		{
			get
			{
				return m_TargetSetter;
			}
		}
	}
}
