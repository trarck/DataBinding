namespace DataBinding
{
	public enum BindType
	{
		OneWay,
		TwoWay,
		Event
	}

	public interface IBinding
	{
		void Bind(object source, string sourceMemberName, object target, string targetMemberName, BindType bindType);
		void SyncTarget();
		void SyncSource();
	}
}
