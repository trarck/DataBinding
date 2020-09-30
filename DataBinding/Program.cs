using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using DataBinding.PropertyProviders;

namespace Test
{
	class MyObjA
	{
		private int _myi;
		private float _myf;
		private string _mys;

		public int Myi
		{
			get
			{
				return _myi;
			}
			set
			{
				_myi = value;
			}
		}
		public float Myf
		{
			get
			{
				return _myf;
			}
			set
			{
				_myf = value;
			}
		}
		public string Mys
		{
			get
			{
				return _mys;
			}
			set
			{
				_mys = value;
			}
		}
	}


	class Program
	{
		static int N = 1000000;
		static void Main(string[] args)
		{
			GenerateProviderRegisters();
			Console.Read();
		}

		static void Test1()
		{
			MyObjA myObjA = new MyObjA()
			{
				Myi = 1,
				Myf = 2.0f,
				Mys = "3"
			};
			Type t = myObjA.GetType();
			PropertyInfo propertyInfo = t.GetProperty("Myi");
			MethodInfo getMethodInfo = propertyInfo.GetGetMethod();
			Func<int> myiD = (Func<int>)Delegate.CreateDelegate(typeof(Func<int>), myObjA, getMethodInfo);

			Console.WriteLine(myiD());
		}

		static void Test2()
		{
			MyObjA myObjA = new MyObjA()
			{
				Myi = 1,
				Myf = 2.0f,
				Mys = "3"
			};

			Type t = myObjA.GetType();
			PropertyInfo propertyInfo = t.GetProperty("Myf");
			MethodInfo getMethodInfo = propertyInfo.GetGetMethod();

			Type funType = typeof(Func<>);
			Type[] funParamTypes = { typeof(float) };
			Type genType = funType.MakeGenericType(funParamTypes);

			Func<float> myiD = (Func<float>)Delegate.CreateDelegate(genType, myObjA, getMethodInfo);

			Console.WriteLine(myiD());
		}

		static void Test3()
		{
			MyObjA myObjA = new MyObjA()
			{
				Myi = 1,
				Myf = 2.0f,
				Mys = "3"
			};

			Type t = myObjA.GetType();
			PropertyInfo propertyInfo = t.GetProperty("Mys");
			MethodInfo getMethodInfo = propertyInfo.GetGetMethod();

			var genType = Type.GetType("System.Func`1[System.String]");

			Func<string> myiD = (Func<string>)Delegate.CreateDelegate(genType, myObjA, getMethodInfo);

			Console.WriteLine(myiD());
		}

		static void Bind<T>(string prop)
		{
			MyObjA myObjA = new MyObjA()
			{
				Myi = 1,
				Myf = 2.0f,
				Mys = "3"
			};
			Type t = myObjA.GetType();
			PropertyInfo propertyInfo = t.GetProperty(prop);
			MethodInfo getMethodInfo = propertyInfo.GetGetMethod();
			Func<T> myFunD = (Func<T>)Delegate.CreateDelegate(typeof(Func<T>), myObjA, getMethodInfo);

			Console.WriteLine(myFunD());
		}

		static void Test4()
		{
			Bind<int>("Myi");
			Bind<float>("Myf");
			Bind<string>("Mys");
		}

		static void Test5()
		{
			MethodInfo m = typeof(Program).GetMethod("Bind", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
			MethodInfo bindInt = m.MakeGenericMethod(typeof(int));
			bindInt.Invoke(null, new object[] { "Myi" });

			MethodInfo bindFloat = m.MakeGenericMethod(typeof(float));
			bindFloat.Invoke(null, new object[] { "Myf" });

			MethodInfo bindStr = m.MakeGenericMethod(typeof(string));
			bindStr.Invoke(null, new object[] { "Mys" });
		}

		static void Bind<T, K>(object source, string sprop, object target, string tprop)
		{
			Type st = source.GetType();
			Type tt = target.GetType();
			PropertyInfo propertyInfo = st.GetProperty(sprop);
			MethodInfo getMethodInfo = propertyInfo.GetGetMethod();
			Func<T> getM = (Func<T>)Delegate.CreateDelegate(typeof(Func<T>), source, getMethodInfo);

			propertyInfo = tt.GetProperty(tprop);
			MethodInfo setMethodInfo = propertyInfo.GetSetMethod();
			Action<K> setM = (Action<K>)Delegate.CreateDelegate(typeof(Action<K>), target, setMethodInfo);

			DateTime start = DateTime.Now;
			for (int i = 0; i < N; ++i)
			{
				setM((K)Convert.ChangeType(getM(), typeof(K)));
			}
			TimeSpan timeSpan = DateTime.Now - start;
			Console.WriteLine("Bind<T,K> use :" + timeSpan.TotalMilliseconds);
		}

		static void BindInt<T>(object source, string sprop, object target, string tprop) where T : IConvertible
		{
			Type st = source.GetType();
			Type tt = target.GetType();
			PropertyInfo propertyInfo = st.GetProperty(sprop);
			MethodInfo getMethodInfo = propertyInfo.GetGetMethod();
			Func<T> getM = (Func<T>)Delegate.CreateDelegate(typeof(Func<T>), source, getMethodInfo);

			propertyInfo = tt.GetProperty(tprop);
			MethodInfo setMethodInfo = propertyInfo.GetSetMethod();
			Action<int> setM = (Action<int>)Delegate.CreateDelegate(typeof(Action<int>), target, setMethodInfo);

			DateTime start = DateTime.Now;
			for (int i = 0; i < N; ++i)
				setM(getM().ToInt32(null));

			TimeSpan timeSpan = DateTime.Now - start;
			Console.WriteLine("BindInt<T> use :" + timeSpan.TotalMilliseconds);
		}

		static void Bind(object source, string sprop, object target, string tprop)
		{
			Type st = source.GetType();
			Type tt = target.GetType();
			PropertyInfo sPropertyInfo = st.GetProperty(sprop);
			PropertyInfo tPropertyInfo = tt.GetProperty(tprop);
			MethodInfo getMethodInfo = sPropertyInfo.GetGetMethod();
			MethodInfo setMethodInfo = tPropertyInfo.GetSetMethod();

			Type spt = sPropertyInfo.PropertyType;
			Type tpt = tPropertyInfo.PropertyType;

			Func<float> getM = CreateGetter<float>(source, getMethodInfo);
			Action<int> setM = CreateSetter<int>(target, setMethodInfo);

			DateTime start = DateTime.Now;
			for (int i = 0; i < N; ++i)
				setM((int)getM());

			TimeSpan timeSpan = DateTime.Now - start;
			Console.WriteLine("Bind use :" + timeSpan.TotalMilliseconds);

			//if (spt == typeof(int))
			//{
			//	Func<int> getM = CreateGetter<int>(source, getMethodInfo);
			//	if (tpt == typeof(int))
			//	{
			//		Action<int> setM = CreateSetter<int>(target, setMethodInfo);
			//		setM(getM());
			//	}
			//	else if (tpt == typeof(float))
			//	{
			//		Action<float> setM = CreateSetter<float>(target, setMethodInfo);
			//		setM(getM());
			//	}
			//	else if (tpt == typeof(string))
			//	{
			//		Action<string> setM = CreateSetter<string>(target, setMethodInfo);
			//		setM(getM().ToString());
			//	}
			//}
		}

		static void BindByReflect(object source, string sprop, object target, string tprop)
		{
			Type st = source.GetType();
			Type tt = target.GetType();
			PropertyInfo sPropertyInfo = st.GetProperty(sprop);
			PropertyInfo tPropertyInfo = tt.GetProperty(tprop);

			Type spt = sPropertyInfo.PropertyType;
			Type tpt = tPropertyInfo.PropertyType;

			DateTime start = DateTime.Now;
			for (int i = 0; i < N; ++i)
			{
				tPropertyInfo.SetValue(target, Convert.ChangeType(sPropertyInfo.GetValue(source), tpt));
			}


			TimeSpan timeSpan = DateTime.Now - start;
			Console.WriteLine("BindByReflect use :" + timeSpan.TotalMilliseconds);
		}

		static void BindByReflectMethod(object source, string sprop, object target, string tprop)
		{
			Type st = source.GetType();
			Type tt = target.GetType();
			PropertyInfo sPropertyInfo = st.GetProperty(sprop);
			PropertyInfo tPropertyInfo = tt.GetProperty(tprop);
			MethodInfo getMethodInfo = sPropertyInfo.GetGetMethod();
			MethodInfo setMethodInfo = tPropertyInfo.GetSetMethod();

			Type tpt = tPropertyInfo.PropertyType;

			object[] pars = new object[1];

			DateTime start = DateTime.Now;
			for (int i = 0; i < N; ++i)
			{
				pars[0] = Convert.ChangeType(getMethodInfo.Invoke(source, null), tpt);
				setMethodInfo.Invoke(target, pars);
			}

			TimeSpan timeSpan = DateTime.Now - start;
			Console.WriteLine("BindByReflectMethod use :" + timeSpan.TotalMilliseconds);
		}

		static void BindVar(object source, string sprop, object target, string tprop)
		{
			Type st = source.GetType();
			Type tt = target.GetType();
			PropertyInfo sPropertyInfo = st.GetProperty(sprop);
			PropertyInfo tPropertyInfo = tt.GetProperty(tprop);
			MethodInfo getMethodInfo = sPropertyInfo.GetGetMethod();
			MethodInfo setMethodInfo = tPropertyInfo.GetSetMethod();

			Type spt = sPropertyInfo.PropertyType;
			Type tpt = tPropertyInfo.PropertyType;

			var getM = CreateGetter<float>(source, getMethodInfo);
			var setM = CreateSetter<int>(target, setMethodInfo);

			DateTime start = DateTime.Now;
			for (int i = 0; i < N; ++i)
				setM((int)getM());

			TimeSpan timeSpan = DateTime.Now - start;
			Console.WriteLine("BindVar use :" + timeSpan.TotalMilliseconds);
		}

		static void BindProp(object source, string sprop, object target, string tprop)
		{
			Type st = source.GetType();
			Type tt = target.GetType();
			PropertyInfo sPropertyInfo = st.GetProperty(sprop);
			PropertyInfo tPropertyInfo = tt.GetProperty(tprop);
			MethodInfo getMethodInfo = sPropertyInfo.GetGetMethod();
			MethodInfo setMethodInfo = tPropertyInfo.GetSetMethod();

			Type spt = sPropertyInfo.PropertyType;
			Type tpt = tPropertyInfo.PropertyType;

			var getM = CreateGetter<float>(source, getMethodInfo);
			var setM = CreateSetter<int>(target, setMethodInfo);

			MyObjA so = source as MyObjA;
			MyObjA to = target as MyObjA;
			DateTime start = DateTime.Now;
			for (int i = 0; i < N; ++i)
			{
				to.Myi = (int)so.Myf;
			}

			TimeSpan timeSpan = DateTime.Now - start;
			Console.WriteLine("BindProp use :" + timeSpan.TotalMilliseconds);
		}


		static Func<T> CreateGetter<T>(object obj, MethodInfo methodInfo)
		{
			return (Func<T>)Delegate.CreateDelegate(typeof(Func<T>), obj, methodInfo);
		}
		static Action<T> CreateSetter<T>(object obj, MethodInfo methodInfo)
		{
			return (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), obj, methodInfo);
		}

		static void Test6()
		{
			MyObjA myObjA = new MyObjA()
			{
				Myi = 1,
				Myf = 2.0f,
				Mys = "3"
			};

			MyObjA myObjB = new MyObjA()
			{
				Myi = 1,
				Myf = 2.0f,
				Mys = "3"
			};

			Bind<float, int>(myObjA, "Myf", myObjB, "Myi");
			BindInt<float>(myObjA, "Myf", myObjB, "Myi");
			Bind(myObjA, "Myf", myObjB, "Myi");
			BindByReflect(myObjA, "Myf", myObjB, "Myi");
			BindByReflectMethod(myObjA, "Myf", myObjB, "Myi");
			BindVar(myObjA, "Myf", myObjB, "Myi");
			BindProp(myObjA, "Myf", myObjB, "Myi");
		}

		static void Test()
		{
			String methodName = "IndexOf";
			Type[] argType = new Type[] { typeof(char) };
			String testWord = "TheQuickBrownFoxJumpedOverTheLazyDog";

			MethodInfo method = typeof(string).GetMethod(methodName, argType);

			Func<char, int> converted = (Func<char, int>)Delegate.CreateDelegate
				(typeof(Func<char, int>), testWord, method);

			int count = GC.CollectionCount(0);
			Console.WriteLine(count);
			for (int i = 0; i < 10000000; i++)
			{
				int l = converted('l');

				if (GC.CollectionCount(0) != count)
					Console.WriteLine("Collect");
			}

		}

		static int ParseInt32(NameValueCollection col, string key)
		{
			return Parse(col, key, int.Parse);
		}
		static double ParseDouble(NameValueCollection col, string key)
		{
			return Parse(col, key, double.Parse);
		}
		static float ParseFloat(NameValueCollection col, string key)
		{
			return Parse(col, key, float.Parse);
		}
		static T Parse<T>(NameValueCollection col, string key, Func<string, T> parse)
		{
			string value = col[key];

			if (string.IsNullOrEmpty(value))
				return default(T);

			return parse(value);
		}


		static void TestConvert()
		{
			NameValueCollection col = new NameValueCollection();
			col["age"] = "18";
			col["f"] = "1.8";

			int age = ParseInt32(col, "age");
			Console.WriteLine("age:" + age);
			float f = ParseFloat(col, "f");
			Console.WriteLine("f:" + f);
		}

		static float IntToFloat(int i)
		{
			return i;
		}

		static int FloatToInt(float f)
		{
			return (int)f;
		}

		static void Bind2<T, K>(object source, string sprop, object target, string tprop, Func<T, K> convert)
		{
			Type st = source.GetType();
			Type tt = target.GetType();
			PropertyInfo propertyInfo = st.GetProperty(sprop);
			MethodInfo getMethodInfo = propertyInfo.GetGetMethod();
			Func<T> getM = (Func<T>)Delegate.CreateDelegate(typeof(Func<T>), source, getMethodInfo);

			propertyInfo = tt.GetProperty(tprop);
			MethodInfo setMethodInfo = propertyInfo.GetSetMethod();
			Action<K> setM = (Action<K>)Delegate.CreateDelegate(typeof(Action<K>), target, setMethodInfo);

			DateTime start = DateTime.Now;
			for (int i = 0; i < N; ++i)
			{
				setM(convert(getM()));
			}
			TimeSpan timeSpan = DateTime.Now - start;
			Console.WriteLine("Bind<T,K> 2 use :" + timeSpan.TotalMilliseconds);
		}

		static Delegate MyConvert;
		static void Bind3<T, K>(object source, string sprop, object target, string tprop)
		{
			Type st = source.GetType();
			Type tt = target.GetType();
			PropertyInfo propertyInfo = st.GetProperty(sprop);
			MethodInfo getMethodInfo = propertyInfo.GetGetMethod();
			Func<T> getM = (Func<T>)Delegate.CreateDelegate(typeof(Func<T>), source, getMethodInfo);

			propertyInfo = tt.GetProperty(tprop);
			MethodInfo setMethodInfo = propertyInfo.GetSetMethod();
			Action<K> setM = (Action<K>)Delegate.CreateDelegate(typeof(Action<K>), target, setMethodInfo);

			DateTime start = DateTime.Now;
			for (int i = 0; i < N; ++i)
			{
				Func<T, K> func = (Func<T, K>)MyConvert;
				setM(func(getM()));
			}
			TimeSpan timeSpan = DateTime.Now - start;
			Console.WriteLine("Bind<T,K> 3 use :" + timeSpan.TotalMilliseconds);
		}

		static void Bind4<T, K>(object source, string sprop, object target, string tprop)
		{
			Type st = source.GetType();
			Type tt = target.GetType();
			PropertyInfo propertyInfo = st.GetProperty(sprop);
			MethodInfo getMethodInfo = propertyInfo.GetGetMethod();
			Func<T> getM = (Func<T>)Delegate.CreateDelegate(typeof(Func<T>), source, getMethodInfo);

			propertyInfo = tt.GetProperty(tprop);
			MethodInfo setMethodInfo = propertyInfo.GetSetMethod();
			Action<K> setM = (Action<K>)Delegate.CreateDelegate(typeof(Action<K>), target, setMethodInfo);


			Func<T, K> func = (Func<T, K>)MyConvert;
			DateTime start = DateTime.Now;
			for (int i = 0; i < N; ++i)
			{
				setM(func(getM()));
			}
			TimeSpan timeSpan = DateTime.Now - start;
			Console.WriteLine("Bind<T,K> 3 use :" + timeSpan.TotalMilliseconds);
		}

		static void Test7()
		{
			MyObjA myObjA = new MyObjA()
			{
				Myi = 1,
				Myf = 2.0f,
				Mys = "3"
			};

			MyObjA myObjB = new MyObjA()
			{
				Myi = 1,
				Myf = 2.0f,
				Mys = "3"
			};

			MyConvert = (Func<float, int>)FloatToInt;

			Bind2<float, int>(myObjA, "Myf", myObjB, "Myi", FloatToInt);
			Bind3<float, int>(myObjA, "Myf", myObjB, "Myi");
		}

		static void Test8()
		{
			Delegate d=(Func<int,bool>)Convert.ToBoolean;

			Func<int, bool> f = (Func<int, bool>)d;

			bool b = f(1);
			Console.WriteLine(b);

			d = (Func<byte, bool>)Convert.ToBoolean;
			Func<byte, bool> f1 = (Func<byte, bool>)d;

			bool b1 = f1(0);
			Console.WriteLine(b1);
		}

		static void CreateGenericObject()
		{
			var d1 = typeof(SystemConvertProvider<,>);
			Type[] typeArgs = { typeof(float), typeof(int) };
			var makeme = d1.MakeGenericType(typeArgs);
			object o = Activator.CreateInstance(makeme);
		}

		static void CreateDefaultConverts()
		{
			Console.WriteLine(DataBinding.ProviderManager.CreateDefaultConverts());
		}

		static void GenerateProviderRegisters()
		{
			Console.WriteLine(DataBinding.ProviderManager.GenerateProviderRegisters());
		}

		static void GenerateDefaultProviders()
		{
			var saveDir = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "../../");
			DataBinding.ProviderManager.GenerateProviders(saveDir);
		}
	}

}
