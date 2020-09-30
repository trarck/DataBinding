using NUnit.Framework;
using DataBinding.Tests;

namespace DataBinding.PropertyProviders.Tests
{
	[TestFixture]
	public class PropertyBindingFloatStringTests
	{
		[Test]
		public void BindFloatStringTest()
		{
			UserViewModel uvm = new UserViewModel()
			{
				name = "aaa",
				age = 18,
				height = 1.82f
			};

			UserView uv = new UserView();

			FloatStringProvider bind = new FloatStringProvider();
			bind.Bind(uvm, "height", uv, "heightLabel", BindType.TwoWay);
			bind.SyncTarget();
			Assert.AreEqual(uv.heightLabel, "1.82");
			uv.heightLabel = "20";
			bind.SyncSource();
			Assert.AreEqual(uvm.height, 20);
		}
	}
}