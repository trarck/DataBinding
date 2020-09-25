using System;
using System.Reflection;

namespace DataBinding.Providers
{
	public class PropertyBindingDelegate<TSourceProperty, TTargetProperty> : PropertyBindingBase<TSourceProperty,TTargetProperty>
	{
		protected Action<PropertyBindingDelegate<TSourceProperty, TTargetProperty>> m_SyncTargetHandle;
		protected Action<PropertyBindingDelegate<TSourceProperty, TTargetProperty>> m_SyncSourceHandle;

		protected Type m_SourcePropertyType;
		protected Type m_TargetPropertyType;

		public PropertyBindingDelegate(Action<PropertyBindingDelegate<TSourceProperty, TTargetProperty>> syncTargetHandle, 
			Action<PropertyBindingDelegate<TSourceProperty, TTargetProperty>> syncSourceHandle)
		{
			m_SyncTargetHandle = syncTargetHandle;
			m_SyncTargetHandle = syncSourceHandle;
		}

		public PropertyBindingDelegate()
		{
			m_SyncTargetHandle = (self) =>
			{
				TSourceProperty v = self.sourceGetter();
				self.targetSetter((TTargetProperty)Convert.ChangeType(v, m_TargetPropertyType));
			};

			m_SyncSourceHandle = (self) =>
			{
				TTargetProperty v = self.targetGetter();
				self.sourceSetter((TSourceProperty)Convert.ChangeType(v, m_SourcePropertyType));
			};
		}

		protected override void SetupGetterSetterExt(Type sourceType, PropertyInfo sourcePropertyInfo, Type targetType, PropertyInfo targetPropertyInfo)
		{
			m_SourcePropertyType = sourcePropertyInfo.PropertyType;
			m_TargetPropertyType = targetPropertyInfo.PropertyType;
		}

		public override void SyncSource()
		{
			if (m_BindType == PropertyBindType.TwoWay)
			{
				if (m_SyncSourceHandle != null)
				{
					m_SyncSourceHandle(this);
				}
			}
		}

		public override void SyncTarget()
		{
			if (m_SyncTargetHandle != null)
			{
				m_SyncTargetHandle(this);
			}
		}

		public Action<PropertyBindingDelegate<TSourceProperty, TTargetProperty>> syncTargetHandle
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

		public Action<PropertyBindingDelegate<TSourceProperty, TTargetProperty>> syncSourceHandle
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

	//	PropertyBindingDelegate<int, string> pdb = new PropertyBindingDelegate<int, string>((self) =>
	//	{
	//		int v = 0;
	//		if (int.TryParse(self.targetGetter(), out v))
	//		{
	//			self.sourceSetter(v);
	//		}
	//	},
	//(self) =>
	//{
	//	self.targetSetter(self.sourceGetter().ToString());
	//});
}
