using NUnit.Framework;
using System.Collections.Generic;
using DataBinding.Tests;
using System;

namespace DataBinding.PropertyProviders.Tests
{
	[TestFixture]
	public class PropertyBindingCustomConvertTests
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

			CustomConvertProvider<int, string> bind = new CustomConvertProvider<int, string>(
			(v) =>
			{
				return v.ToString();
			},
			(s) =>
			{
				return int.Parse(s);
			});

			bind.Bind(uvm, "age", uv, "ageInput", BindType.TwoWay);
			uvm.age = 10;
			bind.SyncTarget();
			Assert.AreEqual(uv.ageInput, "10");
			uv.ageInput = "20";
			bind.SyncSource();
			Assert.AreEqual(uvm.age, 20);
		}

		[Test]
		public void BindIntStringUseDelegateTest()
		{
			UserViewModel uvm = new UserViewModel()
			{
				name = "aaa",
				age = 18,
				height = 1.82f
			};

			UserView uv = new UserView();

			Dictionary<long, Delegate> mapSetter = new Dictionary<long, Delegate>();
			Dictionary<long, Delegate> mapGetter = new Dictionary<long, Delegate>();

			long hash = typeof(int).GetHashCode() << 32 | typeof(string).GetHashCode();

			mapSetter[hash] = (Func<int,string>)IntToString;
			mapGetter[hash] = (Func<string, int>)StringToInt;

			CustomConvertProvider<int, string> bind = new CustomConvertProvider<int, string>(mapSetter[hash], mapGetter[hash]);

			bind.Bind(uvm, "age", uv, "ageInput", BindType.TwoWay);
			uvm.age = 10;
			bind.SyncTarget();
			Assert.AreEqual(uv.ageInput, "10");
			uv.ageInput = "20";
			bind.SyncSource();
			Assert.AreEqual(uvm.age, 20);
		}

		[Test]
		public void DynamicCreateProviderTest()
		{
			UserViewModel uvm = new UserViewModel()
			{
				name = "aaa",
				age = 18,
				height = 1.82f
			};

			UserView uv = new UserView();

			Dictionary<long, Delegate> mapSetter = new Dictionary<long, Delegate>();
			Dictionary<long, Delegate> mapGetter = new Dictionary<long, Delegate>();

			long hash = typeof(int).GetHashCode() << 32 | typeof(string).GetHashCode();

			mapSetter[hash] = (Func<int, string>)IntToString;
			mapGetter[hash] = (Func<string, int>)StringToInt;

			Type genericType = typeof(CustomConvertProvider<,>);
			Type[] typeArgs = { typeof(int), typeof(string) };
			Type genericClassType = genericType.MakeGenericType(typeArgs);
			IBinding bind = Activator.CreateInstance(genericClassType, new object[] { mapSetter[hash], mapGetter[hash] }) as IBinding;

			bind.Bind(uvm, "age", uv, "ageInput", BindType.TwoWay);
			uvm.age = 10;
			bind.SyncTarget();
			Assert.AreEqual(uv.ageInput, "10");
			uv.ageInput = "20";
			bind.SyncSource();
			Assert.AreEqual(uvm.age, 20);
		}

		private string IntToString(int i)
		{
			return i.ToString();			
		}

		private int StringToInt(string s)
		{
			return int.Parse(s);
		}
	}
}