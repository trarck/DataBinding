using System;
using System.Reflection;

namespace DataBinding
{
	public class BindingHelper
	{
		public static Func<T> CreateGetter<T>(object obj, MethodInfo methodInfo)
		{
			return (Func<T>)Delegate.CreateDelegate(typeof(Func<T>), obj, methodInfo);
		}
		public static Action<T> CreateSetter<T>(object obj, MethodInfo methodInfo)
		{
			return (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), obj, methodInfo);
		}

	}
}
