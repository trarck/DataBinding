using NUnit.Framework;
using DataBinding;
namespace DataBinding.Tests
{
	[TestFixture()]
	public class ProviderManagerTests
	{
		private class MyA
		{

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
			Assert.Fail();
		}

		[Test()]
		public void RegisterBindingProviderClassTest()
		{
			Assert.Fail();
		}

		[Test()]
		public void GetBindingProviderClassTest()
		{
			Assert.Fail();
		}

		[Test()]
		public void RemoveBindingProviderClassTest()
		{
			Assert.Fail();
		}

		[Test()]
		public void RegisterTypeConvertTest()
		{
			Assert.Fail();
		}

		[Test()]
		public void GetTypeConvertTest()
		{
			Assert.Fail();
		}

		[Test()]
		public void RemoveTypeConvertTest()
		{
			Assert.Fail();
		}

		[Test()]
		public void GenerateProvidersTest()
		{
			Assert.Fail();
		}

		[Test()]
		public void GenerateProviderRegistersTest()
		{
			Assert.Fail();
		}

		[Test()]
		public void CreateDefaultConvertsTest()
		{
			Assert.Fail();
		}
	}
}