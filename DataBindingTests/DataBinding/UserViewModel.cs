using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding.Tests
{
	public class UserViewModel
	{
		private string _name;
		private int _age;
		private float _height;

		public string name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		public int age
		{
			get
			{
				return _age;
			}
			set
			{
				_age = value;
			}
		}

		public float height
		{
			get
			{
				return _height;
			}
			set
			{
				_height = value;
			}
		}
	}
}
