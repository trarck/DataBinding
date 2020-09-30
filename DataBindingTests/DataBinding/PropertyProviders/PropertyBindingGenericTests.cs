using NUnit.Framework;
using DataBinding.PropertyProviders;
using DataBinding.Tests;

namespace DataBinding.PropertyProviders.Tests
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

			SystemConvertProvider<int, string> bind = new SystemConvertProvider<int, string>();
			bind.Bind(uvm, "age", uv, "ageInput", BindType.TwoWay);
			bind.SyncTarget();
			Assert.AreEqual(uv.ageInput,uvm.age.ToString());
			uv.ageInput = "20";
			bind.SyncSource();
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

			SystemConvertProvider<float, string> bind = new SystemConvertProvider<float, string>();
			bind.Bind(uvm, "height", uv, "heightLabel", BindType.OneWay);
			bind.SyncTarget();

			Assert.AreEqual(uv.heightLabel, uvm.height.ToString());
		}

		[Test]
		public void BindIntFloatTest()
		{
			UserViewModel uvm = new UserViewModel()
			{
				name = "aaa",
				age = 18,
				height = 1.82f
			};

			UserView uv = new UserView();

			SystemConvertProvider<int, float> bind = new SystemConvertProvider<int, float>();
			bind.Bind(uvm, "age", uv, "percent", BindType.TwoWay);
			uvm.age = 10;
			bind.SyncTarget();
			Assert.AreEqual(uv.percent, 10);
			uv.percent = 20;
			bind.SyncSource();
			Assert.AreEqual(uvm.age, 20);
		}

		[Test]
		public void BindSameTypeStringTest()
		{
			UserViewModel uvm = new UserViewModel()
			{
				name = "aaa",
				age = 18,
				height = 1.82f
			};

			UserView uv = new UserView();

			SameTypeProvider<string> bind = new SameTypeProvider<string>();
			bind.Bind(uvm, "name", uv, "nameLabel", BindType.TwoWay);
			bind.SyncTarget();
			Assert.AreEqual(uv.nameLabel, uvm.name);
			uv.nameLabel = "bbb";
			bind.SyncSource();
			Assert.AreEqual(uvm.name, "bbb");
		}

		[Test]
		public void BindSameTypeFloatTest()
		{
			UserViewModel uvm = new UserViewModel()
			{
				name = "aaa",
				age = 18,
				height = 1.82f
			};

			UserView uv = new UserView();

			SameTypeProvider<float> bind = new SameTypeProvider<float>();
			bind.Bind(uvm, "height", uv, "percent", BindType.TwoWay);
			bind.SyncTarget();
			Assert.AreEqual(uv.percent,uvm.height);
			uv.percent = 20.2f;
			bind.SyncSource();
			Assert.AreEqual(uvm.height, 20.2f);
		}
	}
}