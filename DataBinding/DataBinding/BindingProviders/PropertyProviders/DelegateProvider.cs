using System;
using System.Reflection;

namespace DataBinding.PropertyProviders
{
	public class DelegateProvider<TSourceProperty, TTargetProperty> : PropertyBindingProvider<TSourceProperty,TTargetProperty>
	{
		protected Action<DelegateProvider<TSourceProperty, TTargetProperty>> m_SyncTargetHandle;
		protected Action<DelegateProvider<TSourceProperty, TTargetProperty>> m_SyncSourceHandle;

		protected Type m_SourcePropertyType;
		protected Type m_TargetPropertyType;

		public DelegateProvider(Action<DelegateProvider<TSourceProperty, TTargetProperty>> syncTargetHandle, 
			Action<DelegateProvider<TSourceProperty, TTargetProperty>> syncSourceHandle)
		{
			m_SyncTargetHandle = syncTargetHandle;
			m_SyncSourceHandle = syncSourceHandle;
		}

		public DelegateProvider()
		{
			m_SyncTargetHandle = DefaultSyncTargetHandle;

			m_SyncSourceHandle = DefaultSyncSourceHandle;
		}

		protected override void SetupGetterSetterExt(Type sourceType, PropertyInfo sourcePropertyInfo, Type targetType, PropertyInfo targetPropertyInfo)
		{
			m_SourcePropertyType = sourcePropertyInfo.PropertyType;
			m_TargetPropertyType = targetPropertyInfo.PropertyType;
		}

		public override void SyncTarget()
		{
			if (m_SyncTargetHandle != null)
			{
				m_SyncTargetHandle(this);
			}
		}

		public override void SyncSource()
		{
			if (m_BindType == BindType.TwoWay)
			{
				if (m_SyncSourceHandle != null)
				{
					m_SyncSourceHandle(this);
				}
			}
		}

		private void DefaultSyncTargetHandle(DelegateProvider<TSourceProperty, TTargetProperty> self)
		{
			TSourceProperty v = self.sourceGetter();
			self.targetSetter((TTargetProperty)Convert.ChangeType(v, m_TargetPropertyType));
		}

		private void DefaultSyncSourceHandle(DelegateProvider<TSourceProperty, TTargetProperty> self)
		{
			TTargetProperty v = self.targetGetter();
			self.sourceSetter((TSourceProperty)Convert.ChangeType(v, m_SourcePropertyType));
		}

		public Action<DelegateProvider<TSourceProperty, TTargetProperty>> syncTargetHandle
		{
			get
			{
				return m_SyncTargetHandle;
			}
			set
			{
				m_SyncTargetHandle = value;
			}
		}

		public Action<DelegateProvider<TSourceProperty, TTargetProperty>> syncSourceHandle
		{
			get
			{
				return m_SyncSourceHandle;
			}
			set
			{
				m_SyncSourceHandle = value;
			}
		}
	}
}
