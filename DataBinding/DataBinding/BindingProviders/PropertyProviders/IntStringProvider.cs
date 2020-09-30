namespace DataBinding.PropertyProviders
{
	public class IntStringProvider : PropertyBindingProvider<int, string>
	{
		public override void SyncTarget()
		{
			targetSetter(sourceGetter().ToString());
		}

		public override void SyncSource()
		{
			if (m_BindType == BindType.TwoWay)
			{
				int v = 0;
				if (int.TryParse(targetGetter(), out v))
				{
					sourceSetter(v);
				}
			}
		}

	}
}
