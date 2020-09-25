using System;
using System.Reflection;

namespace DataBinding.Providers
{
	public class PropertyBindingFloatString : PropertyBinding
	{
		Func<float> m_SourceGetter;
		Action<string> m_TargetSetter;

		Action<float> m_SourceSetter;
		Func<string> m_TargetGetter;

		protected override void SetupGetterSetter()
		{
			Type st = m_Source.GetType();
			Type tt = m_Target.GetType();

			PropertyInfo sPropertyInfo = st.GetProperty(m_SourcePropertyName);
			PropertyInfo tPropertyInfo = tt.GetProperty(m_TargetPropertyName);

			MethodInfo getMethodInfo = sPropertyInfo.GetGetMethod();
			MethodInfo setMethodInfo = tPropertyInfo.GetSetMethod();

			m_SourceGetter = BindingHelper.CreateGetter<float>(m_Source, getMethodInfo);
			m_TargetSetter = BindingHelper.CreateSetter<string>(m_Target, setMethodInfo);

			if (m_BindType == PropertyBindType.TwoWay)
			{
				setMethodInfo = sPropertyInfo.GetSetMethod();
				getMethodInfo = tPropertyInfo.GetGetMethod();

				m_SourceSetter = BindingHelper.CreateSetter<float>(m_Target, setMethodInfo);
				m_TargetGetter = BindingHelper.CreateGetter<string>(m_Source, getMethodInfo);
			}

		}

		public override void SyncSource()
		{
			if (m_BindType == PropertyBindType.TwoWay)
			{
				float v = 0;
				
				if (float.TryParse(m_TargetGetter(), out v))
				{
					m_SourceSetter(v);
				}
			}
		}

		public override void SyncTarget()
		{
			m_TargetSetter(m_SourceGetter().ToString());
		}
	}
}
