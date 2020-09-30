using NUnit.Framework;
using DataBinding.PropertyProviders;
using DataBinding.Tests;

namespace DataBinding.PropertyProviders.Tests
{
	[TestFixture]
	public class PropertyBindingDelegateTests
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

			DelegateProvider<int,string> bind = new DelegateProvider<int, string>(
			(binding)=>
			{
				binding.targetSetter(binding.sourceGetter().ToString());
			},
			(binding)=>
			{
				int v = 0;
				if (int.TryParse(binding.targetGetter(), out v))
				{
					binding.sourceSetter(v);
				}
			});

			bind.Bind(uvm, "age", uv, "ageInput", BindType.TwoWay);
			uvm.age = 10;
			bind.SyncTarget();
			Assert.AreEqual(uv.ageInput,"10");
			uv.ageInput = "20";
			bind.SyncSource();
			Assert.AreEqual(uvm.age, 20);
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

			DelegateProvider<float, string> bind = new DelegateProvider<float, string>(
			(binding) =>
			{
				binding.targetSetter(binding.sourceGetter().ToString());
			},
			(binding) =>
			{
				float v = 0;
				if (float.TryParse(binding.targetGetter(), out v))
				{
					binding.sourceSetter(v);
				}
			});

			bind.Bind(uvm, "height", uv, "heightLabel", BindType.OneWay);
			uvm.height = 2.5f;
			bind.SyncTarget();
			Assert.AreEqual(uv.heightLabel, "2.5");
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

			DelegateProvider<int, float> bind = new DelegateProvider<int, float>(
			(binding) =>
			{
				binding.targetSetter(binding.sourceGetter());
			},
			(binding) =>
			{
				binding.sourceSetter((int)binding.targetGetter());
			});

			bind.Bind(uvm, "age", uv, "percent", BindType.TwoWay);
			uvm.age = 10;
			bind.SyncTarget();
			Assert.AreEqual(uv.percent, 10);
			uv.percent = 20.2f;
			bind.SyncSource();
			Assert.AreEqual(uvm.age, 20);
		}

		[Test]
		public void DefaultHandleTest()
		{
			UserViewModel uvm = new UserViewModel()
			{
				name = "aaa",
				age = 18,
				height = 1.82f
			};

			UserView uv = new UserView();

			DelegateProvider<float, string> bind = new DelegateProvider<float, string>();

			bind.Bind(uvm, "height", uv, "heightLabel", BindType.OneWay);
			uvm.height = 2.5f;
			bind.SyncTarget();
			Assert.AreEqual(uv.heightLabel, "2.5");
		}
	}
}