using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataBinding.PropertyProviders;

namespace DataBinding
{
	public class ProviderManager
	{
		#region property
		//TypeCode
		internal static readonly Type[] DefaultTypes= new Type[] { 
			null,//0 TypeCode.Empty
			typeof(object), typeof(DBNull),
			typeof(bool), typeof(char),
			typeof(sbyte), typeof(byte),
			typeof(short), typeof(ushort),
			typeof(int), typeof(uint),
			typeof(long), typeof(ulong),
			typeof(float), typeof(double),
			typeof(decimal), 
			typeof(DateTime),
			null,//17 is null
			typeof(string)
		};

		//绑定提供类。<sourceType,<targetType,ProviderClassType>>
		Dictionary<Type, Dictionary<Type, Type>> m_BindingProviderClasses = new Dictionary<Type, Dictionary<Type, Type>>();

		Dictionary<Type, Dictionary<Type, Stack<IBinding>>> m_BindingProviderPools = new Dictionary<Type, Dictionary<Type, Stack<IBinding>>>();

		//类型转换。<fromType,<toType,convertFunction>>
		Dictionary<Type, Dictionary<Type, Delegate>> m_TypeConverts = new Dictionary<Type, Dictionary<Type, Delegate>>();
		Type[] m_TypeArgs1 = new Type[1];
		Type[] m_TypeArgs2 = new Type[2];
		Delegate[] m_ConvertArgs2 = new Delegate[2];

		#endregion

		public void Init()
		{
			InitDefaultConverts();
			InitDefaultProviders();
		}

		#region Binding

		public IBinding GetPropertyBinding(Type sourceType, Type targetType)
		{
			IBinding binding = GetCustomPropertyBinding(sourceType, targetType);
			if (binding==null)
			{
				binding = CreateGenericProvider(sourceType, targetType);
			}

			return binding;
		}

		public IBinding CreateGenericProvider(Type sourceType, Type targetType)
		{
			IBinding binding = null;
			if (sourceType == targetType)
			{
				//get from same type 
				Type genericType = typeof(SameTypeProvider<>);
				m_TypeArgs1[0] = sourceType;
				Type genericClassType = genericType.MakeGenericType(m_TypeArgs1);
				binding = Activator.CreateInstance(genericClassType) as IBinding;
			}
			else
			{
				Delegate sourceToTargetConvert = GetTypeConvert(sourceType, targetType);
				Delegate targetToSourceConvert = GetTypeConvert(targetType, sourceType);

				if (sourceToTargetConvert == null && targetToSourceConvert == null)
				{
					Type genericType = typeof(SystemConvertProvider<,>);
					m_TypeArgs2[0] = sourceType;
					m_TypeArgs2[1] = targetType;
					Type genericClassType = genericType.MakeGenericType(m_TypeArgs2);

					binding = Activator.CreateInstance(genericClassType) as IBinding;
				}
				else
				{
					Type genericType = typeof(CustomConvertProvider<,>);
					m_TypeArgs2[0] = sourceType;
					m_TypeArgs2[1] = targetType;
					Type genericClassType = genericType.MakeGenericType(m_TypeArgs2);

					m_ConvertArgs2[0] = sourceToTargetConvert;
					m_ConvertArgs2[1] = targetToSourceConvert;

					binding = Activator.CreateInstance(genericClassType, m_ConvertArgs2) as IBinding;
				}
			}
			return binding;
		}

		public IBinding GetCustomPropertyBinding(Type sourceType, Type targetType)
		{
			IBinding binding = null;
			Type providerClass = GetBindingProviderClass(sourceType, targetType);
			if (providerClass != null)
			{
				binding = Activator.CreateInstance(providerClass) as IBinding;
			}
			return binding;
		}

		public void RegisterBindingProviderClass(Type sourceType, Type targetType, Type providerClass)
		{
			Dictionary<Type, Type> providerClasses = null;
			if (!m_BindingProviderClasses.TryGetValue(sourceType, out providerClasses))
			{
				providerClasses = new Dictionary<Type, Type>();
				m_BindingProviderClasses[sourceType] = providerClasses;
			}
			providerClasses[targetType] = providerClass;
		}

		public Type GetBindingProviderClass(Type sourceType, Type targetType)
		{
			Type providerClass = null;
			Dictionary<Type, Type> providerClasses = null;
			if (m_BindingProviderClasses.TryGetValue(sourceType, out providerClasses))
			{
				providerClasses.TryGetValue(targetType, out providerClass);
			}
			return providerClass;
		}

		public void RemoveBindingProviderClass(Type sourceType, Type targetType)
		{
			Dictionary<Type, Type> providerClasses = null;
			if (m_BindingProviderClasses.TryGetValue(sourceType, out providerClasses))
			{
				providerClasses.Remove(targetType);
			}
		}

		protected void InitDefaultProviders()
		{
			//bool
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Boolean], typeof(BooleanBooleanProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Boolean], typeof(CharBooleanProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Boolean], typeof(SByteBooleanProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Boolean], typeof(ByteBooleanProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Boolean], typeof(Int16BooleanProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Boolean], typeof(UInt16BooleanProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Boolean], typeof(Int32BooleanProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Boolean], typeof(UInt32BooleanProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Boolean], typeof(Int64BooleanProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Boolean], typeof(UInt64BooleanProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Boolean], typeof(SingleBooleanProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Boolean], typeof(DoubleBooleanProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Boolean], typeof(DecimalBooleanProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Boolean], typeof(DateTimeBooleanProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Boolean], typeof(StringBooleanProvider));
			//char
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Char], typeof(BooleanCharProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Char], typeof(CharCharProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Char], typeof(SByteCharProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Char], typeof(ByteCharProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Char], typeof(Int16CharProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Char], typeof(UInt16CharProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Char], typeof(Int32CharProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Char], typeof(UInt32CharProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Char], typeof(Int64CharProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Char], typeof(UInt64CharProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Char], typeof(SingleCharProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Char], typeof(DoubleCharProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Char], typeof(DecimalCharProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Char], typeof(DateTimeCharProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Char], typeof(StringCharProvider));
			//sbyte
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.SByte], typeof(BooleanSByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.SByte], typeof(CharSByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.SByte], typeof(SByteSByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.SByte], typeof(ByteSByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.SByte], typeof(Int16SByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.SByte], typeof(UInt16SByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.SByte], typeof(Int32SByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.SByte], typeof(UInt32SByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.SByte], typeof(Int64SByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.SByte], typeof(UInt64SByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.SByte], typeof(SingleSByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.SByte], typeof(DoubleSByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.SByte], typeof(DecimalSByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.SByte], typeof(DateTimeSByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.SByte], typeof(StringSByteProvider));
			//byte
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Byte], typeof(BooleanByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Byte], typeof(CharByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Byte], typeof(SByteByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Byte], typeof(ByteByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Byte], typeof(Int16ByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Byte], typeof(UInt16ByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Byte], typeof(Int32ByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Byte], typeof(UInt32ByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Byte], typeof(Int64ByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Byte], typeof(UInt64ByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Byte], typeof(SingleByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Byte], typeof(DoubleByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Byte], typeof(DecimalByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Byte], typeof(DateTimeByteProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Byte], typeof(StringByteProvider));
			//short
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Int16], typeof(BooleanInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Int16], typeof(CharInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Int16], typeof(SByteInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Int16], typeof(ByteInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Int16], typeof(Int16Int16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Int16], typeof(UInt16Int16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Int16], typeof(Int32Int16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Int16], typeof(UInt32Int16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Int16], typeof(Int64Int16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Int16], typeof(UInt64Int16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Int16], typeof(SingleInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Int16], typeof(DoubleInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Int16], typeof(DecimalInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Int16], typeof(DateTimeInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Int16], typeof(StringInt16Provider));
			//ushort
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.UInt16], typeof(BooleanUInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.UInt16], typeof(CharUInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.UInt16], typeof(SByteUInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.UInt16], typeof(ByteUInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.UInt16], typeof(Int16UInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.UInt16], typeof(UInt16UInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.UInt16], typeof(Int32UInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.UInt16], typeof(UInt32UInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.UInt16], typeof(Int64UInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.UInt16], typeof(UInt64UInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.UInt16], typeof(SingleUInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.UInt16], typeof(DoubleUInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.UInt16], typeof(DecimalUInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.UInt16], typeof(DateTimeUInt16Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.UInt16], typeof(StringUInt16Provider));
			//int
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Int32], typeof(BooleanInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Int32], typeof(CharInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Int32], typeof(SByteInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Int32], typeof(ByteInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Int32], typeof(Int16Int32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Int32], typeof(UInt16Int32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Int32], typeof(Int32Int32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Int32], typeof(UInt32Int32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Int32], typeof(Int64Int32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Int32], typeof(UInt64Int32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Int32], typeof(SingleInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Int32], typeof(DoubleInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Int32], typeof(DecimalInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Int32], typeof(DateTimeInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Int32], typeof(StringInt32Provider));
			//uint
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.UInt32], typeof(BooleanUInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.UInt32], typeof(CharUInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.UInt32], typeof(SByteUInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.UInt32], typeof(ByteUInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.UInt32], typeof(Int16UInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.UInt32], typeof(UInt16UInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.UInt32], typeof(Int32UInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.UInt32], typeof(UInt32UInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.UInt32], typeof(Int64UInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.UInt32], typeof(UInt64UInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.UInt32], typeof(SingleUInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.UInt32], typeof(DoubleUInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.UInt32], typeof(DecimalUInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.UInt32], typeof(DateTimeUInt32Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.UInt32], typeof(StringUInt32Provider));
			//long
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Int64], typeof(BooleanInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Int64], typeof(CharInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Int64], typeof(SByteInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Int64], typeof(ByteInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Int64], typeof(Int16Int64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Int64], typeof(UInt16Int64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Int64], typeof(Int32Int64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Int64], typeof(UInt32Int64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Int64], typeof(Int64Int64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Int64], typeof(UInt64Int64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Int64], typeof(SingleInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Int64], typeof(DoubleInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Int64], typeof(DecimalInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Int64], typeof(DateTimeInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Int64], typeof(StringInt64Provider));
			//ulong
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.UInt64], typeof(BooleanUInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.UInt64], typeof(CharUInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.UInt64], typeof(SByteUInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.UInt64], typeof(ByteUInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.UInt64], typeof(Int16UInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.UInt64], typeof(UInt16UInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.UInt64], typeof(Int32UInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.UInt64], typeof(UInt32UInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.UInt64], typeof(Int64UInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.UInt64], typeof(UInt64UInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.UInt64], typeof(SingleUInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.UInt64], typeof(DoubleUInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.UInt64], typeof(DecimalUInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.UInt64], typeof(DateTimeUInt64Provider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.UInt64], typeof(StringUInt64Provider));
			//float
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Single], typeof(BooleanSingleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Single], typeof(CharSingleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Single], typeof(SByteSingleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Single], typeof(ByteSingleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Single], typeof(Int16SingleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Single], typeof(UInt16SingleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Single], typeof(Int32SingleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Single], typeof(UInt32SingleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Single], typeof(Int64SingleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Single], typeof(UInt64SingleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Single], typeof(SingleSingleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Single], typeof(DoubleSingleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Single], typeof(DecimalSingleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Single], typeof(DateTimeSingleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Single], typeof(StringSingleProvider));
			//double
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Double], typeof(BooleanDoubleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Double], typeof(CharDoubleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Double], typeof(SByteDoubleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Double], typeof(ByteDoubleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Double], typeof(Int16DoubleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Double], typeof(UInt16DoubleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Double], typeof(Int32DoubleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Double], typeof(UInt32DoubleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Double], typeof(Int64DoubleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Double], typeof(UInt64DoubleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Double], typeof(SingleDoubleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Double], typeof(DoubleDoubleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Double], typeof(DecimalDoubleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Double], typeof(DateTimeDoubleProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Double], typeof(StringDoubleProvider));
			//Decimal
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Decimal], typeof(BooleanDecimalProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Decimal], typeof(CharDecimalProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Decimal], typeof(SByteDecimalProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Decimal], typeof(ByteDecimalProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Decimal], typeof(Int16DecimalProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Decimal], typeof(UInt16DecimalProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Decimal], typeof(Int32DecimalProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Decimal], typeof(UInt32DecimalProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Decimal], typeof(Int64DecimalProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Decimal], typeof(UInt64DecimalProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Decimal], typeof(SingleDecimalProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Decimal], typeof(DoubleDecimalProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Decimal], typeof(DecimalDecimalProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Decimal], typeof(DateTimeDecimalProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Decimal], typeof(StringDecimalProvider));
			//DateTime
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.DateTime], typeof(BooleanDateTimeProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.DateTime], typeof(CharDateTimeProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.DateTime], typeof(SByteDateTimeProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.DateTime], typeof(ByteDateTimeProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.DateTime], typeof(Int16DateTimeProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.DateTime], typeof(UInt16DateTimeProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.DateTime], typeof(Int32DateTimeProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.DateTime], typeof(UInt32DateTimeProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.DateTime], typeof(Int64DateTimeProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.DateTime], typeof(UInt64DateTimeProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.DateTime], typeof(SingleDateTimeProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.DateTime], typeof(DoubleDateTimeProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.DateTime], typeof(DecimalDateTimeProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.DateTime], typeof(DateTimeDateTimeProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.DateTime], typeof(StringDateTimeProvider));
			//string
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.String], typeof(BooleanStringProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.String], typeof(CharStringProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.String], typeof(SByteStringProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.String], typeof(ByteStringProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.String], typeof(Int16StringProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.String], typeof(UInt16StringProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.String], typeof(Int32StringProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.String], typeof(UInt32StringProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.String], typeof(Int64StringProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.String], typeof(UInt64StringProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.String], typeof(SingleStringProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.String], typeof(DoubleStringProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.String], typeof(DecimalStringProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.String], typeof(DateTimeStringProvider));
			RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.String], typeof(StringStringProvider));
		}

		#endregion

		#region Binding Pool

		protected IBinding GetBindingProvider(Type sourceType,Type targetType)
		{
			IBinding provider = null;
			Dictionary<Type, Stack<IBinding>> targetMap = null;
			if (m_BindingProviderPools.TryGetValue(sourceType, out targetMap))
			{
				Stack<IBinding> providerPool = null;
				if (targetMap.TryGetValue(targetType, out providerPool))
				{
					if (providerPool.Count > 0)
					{
						provider = providerPool.Pop();
					}
				}
				else
				{
					providerPool = new Stack<IBinding>();
					targetMap[targetType] = providerPool;
				}
			}
			return provider;
		}

		protected void ReleaseBindingProvider()
		{

		}

		#endregion

		#region Convert
		public void RegisterTypeConvert(Type from, Type to, Delegate convert)
		{
			Dictionary<Type, Delegate> converts = null;
			if (!m_TypeConverts.TryGetValue(from, out converts))
			{
				converts = new Dictionary<Type, Delegate>();
				m_TypeConverts[from] = converts;
			}
			converts[to] = convert;
		}

		public Delegate GetTypeConvert(Type from, Type to)
		{
			Delegate convert = null;
			Dictionary<Type, Delegate> converts = null;
			if (m_TypeConverts.TryGetValue(from, out converts))
			{
				converts.TryGetValue(to, out convert);
			}
			return convert;
		}

		public void RemoveTypeConvert(Type from, Type to)
		{
			Dictionary<Type, Delegate> converts = null;
			if (m_TypeConverts.TryGetValue(from, out converts))
			{
				converts.Remove(to);
			}
		}

		protected void InitDefaultConverts()
		{
			//bool
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Boolean], (Func<bool, bool>)Convert.ToBoolean);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Boolean], (Func<char, bool>)Convert.ToBoolean);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Boolean], (Func<sbyte, bool>)Convert.ToBoolean);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Boolean], (Func<byte, bool>)Convert.ToBoolean);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Boolean], (Func<short, bool>)Convert.ToBoolean);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Boolean], (Func<ushort, bool>)Convert.ToBoolean);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Boolean], (Func<int, bool>)Convert.ToBoolean);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Boolean], (Func<uint, bool>)Convert.ToBoolean);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Boolean], (Func<long, bool>)Convert.ToBoolean);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Boolean], (Func<ulong, bool>)Convert.ToBoolean);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Boolean], (Func<float, bool>)Convert.ToBoolean);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Boolean], (Func<double, bool>)Convert.ToBoolean);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Boolean], (Func<Decimal, bool>)Convert.ToBoolean);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Boolean], (Func<DateTime, bool>)Convert.ToBoolean);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Boolean], (Func<string, bool>)Convert.ToBoolean);
			//char
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Char], (Func<bool, char>)Convert.ToChar);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Char], (Func<char, char>)Convert.ToChar);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Char], (Func<sbyte, char>)Convert.ToChar);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Char], (Func<byte, char>)Convert.ToChar);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Char], (Func<short, char>)Convert.ToChar);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Char], (Func<ushort, char>)Convert.ToChar);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Char], (Func<int, char>)Convert.ToChar);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Char], (Func<uint, char>)Convert.ToChar);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Char], (Func<long, char>)Convert.ToChar);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Char], (Func<ulong, char>)Convert.ToChar);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Char], (Func<float, char>)Convert.ToChar);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Char], (Func<double, char>)Convert.ToChar);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Char], (Func<Decimal, char>)Convert.ToChar);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Char], (Func<DateTime, char>)Convert.ToChar);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Char], (Func<string, char>)Convert.ToChar);
			//sbyte
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.SByte], (Func<bool, sbyte>)Convert.ToSByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.SByte], (Func<char, sbyte>)Convert.ToSByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.SByte], (Func<sbyte, sbyte>)Convert.ToSByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.SByte], (Func<byte, sbyte>)Convert.ToSByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.SByte], (Func<short, sbyte>)Convert.ToSByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.SByte], (Func<ushort, sbyte>)Convert.ToSByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.SByte], (Func<int, sbyte>)Convert.ToSByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.SByte], (Func<uint, sbyte>)Convert.ToSByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.SByte], (Func<long, sbyte>)Convert.ToSByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.SByte], (Func<ulong, sbyte>)Convert.ToSByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.SByte], (Func<float, sbyte>)Convert.ToSByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.SByte], (Func<double, sbyte>)Convert.ToSByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.SByte], (Func<Decimal, sbyte>)Convert.ToSByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.SByte], (Func<DateTime, sbyte>)Convert.ToSByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.SByte], (Func<string, sbyte>)Convert.ToSByte);
			//byte
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Byte], (Func<bool, byte>)Convert.ToByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Byte], (Func<char, byte>)Convert.ToByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Byte], (Func<sbyte, byte>)Convert.ToByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Byte], (Func<byte, byte>)Convert.ToByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Byte], (Func<short, byte>)Convert.ToByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Byte], (Func<ushort, byte>)Convert.ToByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Byte], (Func<int, byte>)Convert.ToByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Byte], (Func<uint, byte>)Convert.ToByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Byte], (Func<long, byte>)Convert.ToByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Byte], (Func<ulong, byte>)Convert.ToByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Byte], (Func<float, byte>)Convert.ToByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Byte], (Func<double, byte>)Convert.ToByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Byte], (Func<Decimal, byte>)Convert.ToByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Byte], (Func<DateTime, byte>)Convert.ToByte);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Byte], (Func<string, byte>)Convert.ToByte);
			//short
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Int16], (Func<bool, short>)Convert.ToInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Int16], (Func<char, short>)Convert.ToInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Int16], (Func<sbyte, short>)Convert.ToInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Int16], (Func<byte, short>)Convert.ToInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Int16], (Func<short, short>)Convert.ToInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Int16], (Func<ushort, short>)Convert.ToInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Int16], (Func<int, short>)Convert.ToInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Int16], (Func<uint, short>)Convert.ToInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Int16], (Func<long, short>)Convert.ToInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Int16], (Func<ulong, short>)Convert.ToInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Int16], (Func<float, short>)Convert.ToInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Int16], (Func<double, short>)Convert.ToInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Int16], (Func<Decimal, short>)Convert.ToInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Int16], (Func<DateTime, short>)Convert.ToInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Int16], (Func<string, short>)Convert.ToInt16);
			//ushort
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.UInt16], (Func<bool, ushort>)Convert.ToUInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.UInt16], (Func<char, ushort>)Convert.ToUInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.UInt16], (Func<sbyte, ushort>)Convert.ToUInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.UInt16], (Func<byte, ushort>)Convert.ToUInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.UInt16], (Func<short, ushort>)Convert.ToUInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.UInt16], (Func<ushort, ushort>)Convert.ToUInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.UInt16], (Func<int, ushort>)Convert.ToUInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.UInt16], (Func<uint, ushort>)Convert.ToUInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.UInt16], (Func<long, ushort>)Convert.ToUInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.UInt16], (Func<ulong, ushort>)Convert.ToUInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.UInt16], (Func<float, ushort>)Convert.ToUInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.UInt16], (Func<double, ushort>)Convert.ToUInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.UInt16], (Func<Decimal, ushort>)Convert.ToUInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.UInt16], (Func<DateTime, ushort>)Convert.ToUInt16);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.UInt16], (Func<string, ushort>)Convert.ToUInt16);
			//int
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Int32], (Func<bool, int>)Convert.ToInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Int32], (Func<char, int>)Convert.ToInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Int32], (Func<sbyte, int>)Convert.ToInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Int32], (Func<byte, int>)Convert.ToInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Int32], (Func<short, int>)Convert.ToInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Int32], (Func<ushort, int>)Convert.ToInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Int32], (Func<int, int>)Convert.ToInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Int32], (Func<uint, int>)Convert.ToInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Int32], (Func<long, int>)Convert.ToInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Int32], (Func<ulong, int>)Convert.ToInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Int32], (Func<float, int>)Convert.ToInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Int32], (Func<double, int>)Convert.ToInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Int32], (Func<Decimal, int>)Convert.ToInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Int32], (Func<DateTime, int>)Convert.ToInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Int32], (Func<string, int>)Convert.ToInt32);
			//uint
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.UInt32], (Func<bool, uint>)Convert.ToUInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.UInt32], (Func<char, uint>)Convert.ToUInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.UInt32], (Func<sbyte, uint>)Convert.ToUInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.UInt32], (Func<byte, uint>)Convert.ToUInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.UInt32], (Func<short, uint>)Convert.ToUInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.UInt32], (Func<ushort, uint>)Convert.ToUInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.UInt32], (Func<int, uint>)Convert.ToUInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.UInt32], (Func<uint, uint>)Convert.ToUInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.UInt32], (Func<long, uint>)Convert.ToUInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.UInt32], (Func<ulong, uint>)Convert.ToUInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.UInt32], (Func<float, uint>)Convert.ToUInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.UInt32], (Func<double, uint>)Convert.ToUInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.UInt32], (Func<Decimal, uint>)Convert.ToUInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.UInt32], (Func<DateTime, uint>)Convert.ToUInt32);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.UInt32], (Func<string, uint>)Convert.ToUInt32);
			//long
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Int64], (Func<bool, long>)Convert.ToInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Int64], (Func<char, long>)Convert.ToInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Int64], (Func<sbyte, long>)Convert.ToInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Int64], (Func<byte, long>)Convert.ToInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Int64], (Func<short, long>)Convert.ToInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Int64], (Func<ushort, long>)Convert.ToInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Int64], (Func<int, long>)Convert.ToInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Int64], (Func<uint, long>)Convert.ToInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Int64], (Func<long, long>)Convert.ToInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Int64], (Func<ulong, long>)Convert.ToInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Int64], (Func<float, long>)Convert.ToInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Int64], (Func<double, long>)Convert.ToInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Int64], (Func<Decimal, long>)Convert.ToInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Int64], (Func<DateTime, long>)Convert.ToInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Int64], (Func<string, long>)Convert.ToInt64);
			//ulong
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.UInt64], (Func<bool, ulong>)Convert.ToUInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.UInt64], (Func<char, ulong>)Convert.ToUInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.UInt64], (Func<sbyte, ulong>)Convert.ToUInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.UInt64], (Func<byte, ulong>)Convert.ToUInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.UInt64], (Func<short, ulong>)Convert.ToUInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.UInt64], (Func<ushort, ulong>)Convert.ToUInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.UInt64], (Func<int, ulong>)Convert.ToUInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.UInt64], (Func<uint, ulong>)Convert.ToUInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.UInt64], (Func<long, ulong>)Convert.ToUInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.UInt64], (Func<ulong, ulong>)Convert.ToUInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.UInt64], (Func<float, ulong>)Convert.ToUInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.UInt64], (Func<double, ulong>)Convert.ToUInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.UInt64], (Func<Decimal, ulong>)Convert.ToUInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.UInt64], (Func<DateTime, ulong>)Convert.ToUInt64);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.UInt64], (Func<string, ulong>)Convert.ToUInt64);
			//float
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Single], (Func<bool, float>)Convert.ToSingle);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Single], (Func<char, float>)Convert.ToSingle);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Single], (Func<sbyte, float>)Convert.ToSingle);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Single], (Func<byte, float>)Convert.ToSingle);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Single], (Func<short, float>)Convert.ToSingle);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Single], (Func<ushort, float>)Convert.ToSingle);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Single], (Func<int, float>)Convert.ToSingle);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Single], (Func<uint, float>)Convert.ToSingle);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Single], (Func<long, float>)Convert.ToSingle);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Single], (Func<ulong, float>)Convert.ToSingle);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Single], (Func<float, float>)Convert.ToSingle);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Single], (Func<double, float>)Convert.ToSingle);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Single], (Func<Decimal, float>)Convert.ToSingle);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Single], (Func<DateTime, float>)Convert.ToSingle);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Single], (Func<string, float>)Convert.ToSingle);
			//double
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Double], (Func<bool, double>)Convert.ToDouble);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Double], (Func<char, double>)Convert.ToDouble);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Double], (Func<sbyte, double>)Convert.ToDouble);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Double], (Func<byte, double>)Convert.ToDouble);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Double], (Func<short, double>)Convert.ToDouble);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Double], (Func<ushort, double>)Convert.ToDouble);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Double], (Func<int, double>)Convert.ToDouble);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Double], (Func<uint, double>)Convert.ToDouble);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Double], (Func<long, double>)Convert.ToDouble);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Double], (Func<ulong, double>)Convert.ToDouble);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Double], (Func<float, double>)Convert.ToDouble);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Double], (Func<double, double>)Convert.ToDouble);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Double], (Func<Decimal, double>)Convert.ToDouble);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Double], (Func<DateTime, double>)Convert.ToDouble);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Double], (Func<string, double>)Convert.ToDouble);
			//Decimal
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.Decimal], (Func<bool, Decimal>)Convert.ToDecimal);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.Decimal], (Func<char, Decimal>)Convert.ToDecimal);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.Decimal], (Func<sbyte, Decimal>)Convert.ToDecimal);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.Decimal], (Func<byte, Decimal>)Convert.ToDecimal);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.Decimal], (Func<short, Decimal>)Convert.ToDecimal);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.Decimal], (Func<ushort, Decimal>)Convert.ToDecimal);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.Decimal], (Func<int, Decimal>)Convert.ToDecimal);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.Decimal], (Func<uint, Decimal>)Convert.ToDecimal);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.Decimal], (Func<long, Decimal>)Convert.ToDecimal);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.Decimal], (Func<ulong, Decimal>)Convert.ToDecimal);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.Decimal], (Func<float, Decimal>)Convert.ToDecimal);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.Decimal], (Func<double, Decimal>)Convert.ToDecimal);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.Decimal], (Func<Decimal, Decimal>)Convert.ToDecimal);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.Decimal], (Func<DateTime, Decimal>)Convert.ToDecimal);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.Decimal], (Func<string, Decimal>)Convert.ToDecimal);
			//DateTime
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.DateTime], (Func<bool, DateTime>)Convert.ToDateTime);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.DateTime], (Func<char, DateTime>)Convert.ToDateTime);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.DateTime], (Func<sbyte, DateTime>)Convert.ToDateTime);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.DateTime], (Func<byte, DateTime>)Convert.ToDateTime);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.DateTime], (Func<short, DateTime>)Convert.ToDateTime);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.DateTime], (Func<ushort, DateTime>)Convert.ToDateTime);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.DateTime], (Func<int, DateTime>)Convert.ToDateTime);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.DateTime], (Func<uint, DateTime>)Convert.ToDateTime);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.DateTime], (Func<long, DateTime>)Convert.ToDateTime);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.DateTime], (Func<ulong, DateTime>)Convert.ToDateTime);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.DateTime], (Func<float, DateTime>)Convert.ToDateTime);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.DateTime], (Func<double, DateTime>)Convert.ToDateTime);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.DateTime], (Func<Decimal, DateTime>)Convert.ToDateTime);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.DateTime], (Func<DateTime, DateTime>)Convert.ToDateTime);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.DateTime], (Func<string, DateTime>)Convert.ToDateTime);
			//string
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Boolean], DefaultTypes[(int)TypeCode.String], (Func<bool, string>)Convert.ToString);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Char], DefaultTypes[(int)TypeCode.String], (Func<char, string>)Convert.ToString);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.SByte], DefaultTypes[(int)TypeCode.String], (Func<sbyte, string>)Convert.ToString);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Byte], DefaultTypes[(int)TypeCode.String], (Func<byte, string>)Convert.ToString);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int16], DefaultTypes[(int)TypeCode.String], (Func<short, string>)Convert.ToString);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt16], DefaultTypes[(int)TypeCode.String], (Func<ushort, string>)Convert.ToString);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int32], DefaultTypes[(int)TypeCode.String], (Func<int, string>)Convert.ToString);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt32], DefaultTypes[(int)TypeCode.String], (Func<uint, string>)Convert.ToString);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Int64], DefaultTypes[(int)TypeCode.String], (Func<long, string>)Convert.ToString);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.UInt64], DefaultTypes[(int)TypeCode.String], (Func<ulong, string>)Convert.ToString);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Single], DefaultTypes[(int)TypeCode.String], (Func<float, string>)Convert.ToString);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Double], DefaultTypes[(int)TypeCode.String], (Func<double, string>)Convert.ToString);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.Decimal], DefaultTypes[(int)TypeCode.String], (Func<Decimal, string>)Convert.ToString);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.DateTime], DefaultTypes[(int)TypeCode.String], (Func<DateTime, string>)Convert.ToString);
			RegisterTypeConvert(DefaultTypes[(int)TypeCode.String], DefaultTypes[(int)TypeCode.String], (Func<string, string>)Convert.ToString);
		}

		#endregion

		#region Tools

		static string[] ShortNames = new string[] { "bool", "char", "sbyte", "byte", "short", "ushort", "int", "uint", "long", "ulong", "float", "double", "Decimal", "DateTime", "string" };
		static string[] TypeCodes = new string[] { "Boolean", "Char", "SByte", "Byte", "Int16", "UInt16", "Int32", "UInt32", "Int64", "UInt64", "Single", "Double", "Decimal", "DateTime", "String" };


		public static void GenerateProviders(string projectDir)
		{
			string codeTemplate = @"
    public class {0}{1}Provider : PropertyBindingProvider<{2}, {3}>
    {{
        public override void SyncTarget()
        {{
            targetSetter(Convert.To{1}(sourceGetter()));
        }}

        public override void SyncSource()
        {{
            sourceSetter(Convert.To{0}(targetGetter()));
        }}
    }}";

			StringBuilder sb = new StringBuilder();
			sb.Append("using System;\n");
			sb.Append("namespace DataBinding.PropertyProviders\n{\n");

			for (int i = 0; i < TypeCodes.Length; ++i)
			{
				sb.Append("\n    //========================================").Append(ShortNames[i]).Append("========================================\n");
				for (int j = 0; j < TypeCodes.Length; ++j)
				{
					sb.Append(string.Format(codeTemplate,
						TypeCodes[j], TypeCodes[i], ShortNames[j], ShortNames[i]))
						.Append("\n");
				}
			}
			sb.Append("}");

			string saveFileName = "AutoGeneratedProviders.cs";
			string saveFilePath = Path.Combine(projectDir, "DataBinding/BindingProviders/PropertyProviders");
			saveFilePath = Path.Combine(saveFilePath, saveFileName);
			File.WriteAllText(saveFilePath, sb.ToString());
		}

		public static string GenerateProviderRegisters()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < TypeCodes.Length; ++i)
			{
				sb.Append("//" + ShortNames[i]).Append("\n");
				for (int j = 0; j < TypeCodes.Length; ++j)
				{
					sb.Append(string.Format(
						"RegisterBindingProviderClass(DefaultTypes[(int)TypeCode.{0}], DefaultTypes[(int)TypeCode.{1}], typeof({0}{1}Provider));",
						TypeCodes[j], TypeCodes[i]))
						.Append("\n");
				}
			}
			return sb.ToString();
		}

		public static string CreateDefaultConverts()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < TypeCodes.Length; ++i)
			{
				sb.Append("//" + ShortNames[i]).Append("\n");
				for (int j = 0; j < TypeCodes.Length; ++j)
				{
					sb.Append(string.Format(
						"RegisterTypeConvert(DefaultTypes[(int)TypeCode.{0}], DefaultTypes[(int)TypeCode.{1}], (Func<{2},{3}>)Convert.To{4});",
						TypeCodes[j], TypeCodes[i], ShortNames[j], ShortNames[i], TypeCodes[i]))
						.Append("\n");
				}
			}
			return sb.ToString();
		}


		#endregion


	}
}
