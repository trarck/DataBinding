namespace DataBinding
{
	public enum PropertyBindType
	{
		OneWay,
		TwoWay,
		Event
	}

	public interface IBinding
	{
		void Bind(object source, string sourcePropertyName, object target, string targetPropertyName, PropertyBindType bindType);

		void SyncSource();
		void SyncTarget();
	}
}
