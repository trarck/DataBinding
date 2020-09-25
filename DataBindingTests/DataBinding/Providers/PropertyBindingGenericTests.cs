using NUnit.Framework;
using DataBinding.Providers;
using DataBinding.Tests;

namespace DataBinding.Providers.Tests
{
	[TestFixture()]
	public class PropertyBindingGenericTests
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

			PropertyBindingGeneric<int, string> bindIntStr = new PropertyBindingGeneric<int, string>();
			bindIntStr.Bind(uvm, "age", uv, "ageInput", PropertyBindType.TwoWay);
			bindIntStr.SyncTarget();
			Assert.AreEqual(uv.ageInput,uvm.age.ToString());
			uv.ageInput = "20";
			bindIntStr.SyncSource();
			Assert.AreEqual(uvm.age,20);
		}

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

			PropertyBindingGeneric<float, string> bindIntStr = new PropertyBindingGeneric<float, string>();
			bindIntStr.Bind(uvm, "height", uv, "heightLabel", PropertyBindType.OneWay);
			bindIntStr.SyncTarget();

			Assert.AreEqual(uv.heightLabel, uvm.height.ToString());
		}
	}
}