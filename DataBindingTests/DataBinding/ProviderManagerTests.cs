using NUnit.Framework;
using DataBinding;
using DataBinding.PropertyProviders;
using System;

namespace DataBinding.Tests
{
	[TestFixture()]
	public class ProviderManagerTests
	{
		private class MyA
		{
			public int intValue;
			public float floatValue;
			public string strValue;
		}

		private class TestView
		{
			public string userInput { get; set; }
		}

		private class TestViewModel
		{
			public int intField { get; set; }
			public float floatField
			{
				get; set;
			}
			public MyA myA
			{
				get; set;
			}
		}

		private class IntMyAProvider : PropertyBindingProvider<int, MyA>
		{
			public override void SyncTarget()
			{
				MyA myA = targetGetter();
				myA.intValue = sourceGetter();
			}

			public override void SyncSource()
			{
				MyA myA = targetGetter();
				sourceSetter(myA.intValue);
			}
		}

		private class SingleMyAProvider : PropertyBindingProvider<float, MyA>
		{
			public override void SyncTarget()
			{
				MyA myA = targetGetter();
				myA.floatValue = sourceGetter();
			}

			public override void SyncSource()
			{
				MyA myA = targetGetter();
				sourceSetter(myA.floatValue);
			}
		}

		private class StringMyAProvider : PropertyBindingProvider<string, MyA>
		{
			public override void SyncTarget()
			{
				MyA myA = targetGetter();
				myA.strValue = sourceGetter();
			}

			public override void SyncSource()
			{
				MyA myA = targetGetter();
				sourceSetter(myA.strValue);
			}
		}

		private class MyAStringProvider : PropertyBindingProvider<MyA,string>
		{
			public override void SyncTarget()
			{
				MyA myA = sourceGetter();
				targetSetter(myA.strValue);
			}

			public override void SyncSource()
			{
				MyA myA = sourceGetter();
				myA.strValue = targetGetter();
			}
		}

		private ProviderManager m_ProviderManager;

		[SetUp]
		public void SetupManager()
		{
			m_ProviderManager = new ProviderManager();
			m_ProviderManager.Init();
		}

		[Test()]
		public void GetPropertyBindingTest()
		{
			IBinding binding = m_ProviderManager.GetPropertyBinding(typeof(int), typeof(string));
			Assert.NotNull(binding);
		}

		[Test()]
		public void CreateGenericProviderTest()
		{
			IBinding binding = m_ProviderManager.CreateGenericProvider(typeof(int), typeof(string));
			Assert.NotNull(binding);

			binding = m_ProviderManager.CreateGenericProvider(typeof(int), typeof(int));
			Assert.NotNull(binding);

			binding = m_ProviderManager.CreateGenericProvider(typeof(int), typeof(MyA));
			Assert.NotNull(binding);
		}

		[Test()]
		public void GetCustomPropertyBindingTest()
		{
			IBinding binding = m_ProviderManager.GetCustomPropertyBinding(typeof(int), typeof(string));
			Assert.NotNull(binding);

			binding = m_ProviderManager.GetCustomPropertyBinding(typeof(int), typeof(MyA));
			Assert.Null(binding);
		}

		[Test()]
		public void CustomPropertyBindingTest()
		{
			TestView testView = new TestView();
			TestViewModel testViewModel = new TestViewModel()
			{
				intField = 10,
				floatField = 20.2f,
				myA = new MyA()
				{
					intValue = 1,
					floatValue = 2.2f,
					strValue = "abc"
				}
			};

			m_ProviderManager.RegisterBindingProviderClass( typeof(MyA), typeof(string), typeof(MyAStringProvider));

			IBinding binding = m_ProviderManager.GetCustomPropertyBinding(typeof(MyA), typeof(string));

			Assert.NotNull(binding);

			binding.Bind(testViewModel, "myA", testView, "userInput",BindType.OneWay);

			binding.SyncTarget();

			Assert.AreEqual(testViewModel.myA.strValue, testView.userInput);
		}

		[Test()]
		public void CustomPropertyBindingTest2()
		{
			TestView testView = new TestView();
			TestViewModel testViewModel = new TestViewModel()
			{
				intField = 10,
				floatField = 20.2f,
				myA = new MyA()
				{
					intValue = 1,
					floatValue = 2.2f,
					strValue = "abc"
				}
			};

			m_ProviderManager.RegisterBindingProviderClass(typeof(MyA), typeof(string), typeof(MyAStringProvider));

			IBinding binding = m_ProviderManager.GetCustomPropertyBinding(typeof(MyA),typeof(string));

			Assert.NotNull(binding);

			binding.Bind(testViewModel, "myA", testView, "userInput", BindType.TwoWay);

			binding.SyncTarget();						
			Assert.AreEqual(testView.userInput,"abc");

			testView.userInput = "cde";
			binding.SyncSource();
			Assert.AreEqual(testViewModel.myA.strValue, "cde");
		}

		[Test()]
		public void CustomPropertyBindingTest3()
		{
			TestViewModel testViewModel = new TestViewModel()
			{
				intField = 10,
				floatField = 20.2f,
				myA = new MyA()
				{
					intValue = 1,
					floatValue = 2.2f,
					strValue = "abc"
				}
			};

			m_ProviderManager.RegisterBindingProviderClass(typeof(int), typeof(MyA), typeof(IntMyAProvider));

			IBinding binding = m_ProviderManager.GetCustomPropertyBinding(typeof(int), typeof(MyA));

			Assert.NotNull(binding);

			binding.Bind( testViewModel, "intField", testViewModel, "myA", BindType.TwoWay);

			binding.SyncTarget();
			Assert.AreEqual(testViewModel.myA.intValue, 10);

			testViewModel.myA.intValue = 1;
			binding.SyncSource();
			Assert.AreEqual(testViewModel.intField, 1);
		}

		[Test()]
		public void CustomPropertyBindingTest4()
		{
			TestViewModel testViewModel = new TestViewModel()
			{
				intField = 10,
				floatField = 20.2f,
				myA = new MyA()
				{
					intValue = 1,
					floatValue = 2.2f,
					strValue = "abc"
				}
			};

			m_ProviderManager.RegisterBindingProviderClass(typeof(float), typeof(MyA), typeof(SingleMyAProvider));

			IBinding binding = m_ProviderManager.GetCustomPropertyBinding(typeof(float), typeof(MyA));

			Assert.NotNull(binding);

			binding.Bind(testViewModel, "floatField", testViewModel, "myA", BindType.TwoWay);

			binding.SyncTarget();
			Assert.AreEqual(testViewModel.myA.floatValue, 20.2f);

			testViewModel.myA.floatValue = 1.1f;
			binding.SyncSource();
			Assert.AreEqual(testViewModel.floatField, 1.1f);
		}
	}
}