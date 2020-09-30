using NUnit.Framework;
using DataBinding.Tests;

namespace DataBinding.PropertyProviders.Tests
{
	[TestFixture]
	public class PropertyBindingIntStringTests
	{
		[Test]
		public void BindIntStringTest()
		{
			UserViewModel uvm = new UserViewModel()
			{
				name = "aaa",
				age = 18,
				height = 1.82f
			};

			UserView uv = new UserView();

			IntStringProvider bind = new IntStringProvider();
			bind.Bind(uvm, "age", uv, "ageInput", BindType.TwoWay);
			bind.SyncTarget();
			Assert.AreEqual(uv.ageInput, uvm.age.ToString());
			uv.ageInput = "20";
			bind.SyncSource();
			Assert.AreEqual(uvm.age, 20);
		}
	}
}