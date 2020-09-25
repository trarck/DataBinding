namespace DataBinding
{
	public abstract class PropertyBinding:IBinding
	{
		protected PropertyBindType m_BindType;

		protected object m_Source;
		protected object m_Target;
		protected string m_SourcePropertyName;
		protected string m_TargetPropertyName;

		//protected Type m_SourcePropertyType;
		//protected Type m_TargetPropertyType;

		public virtual void Bind(object source, string sourcePropertyName, object target, string targetPropertyName, PropertyBindType bindType)
		{
			m_Source = source;
			m_SourcePropertyName = sourcePropertyName;
			m_Target = target;
			m_TargetPropertyName = targetPropertyName;

			m_BindType = bindType;

			SetupGetterSetter();
		}

		protected virtual void SetupGetterSetter()
		{
			
		}

		public virtual void SyncSource()
		{

		}

		public virtual void SyncTarget()
		{

		}
	}
}
