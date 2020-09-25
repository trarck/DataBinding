using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding.Tests
{
	public class UserView
	{
		private string _nameLabel;
		private string _ageInput;
		private string _heightLabel;

		private float _percent;

		public string nameLabel
		{
			get
			{
				return _nameLabel;
			}
			set
			{
				_nameLabel = value;
			}
		}

		public string ageInput
		{
			get
			{
				return _ageInput;
			}
			set
			{
				_ageInput = value;
			}
		}

		public string heightLabel
		{
			get
			{
				return _heightLabel;
			}
			set
			{
				_heightLabel = value;
			}
		}

		public float percent
		{
			get
			{
				return _percent;
			}
			set
			{
				_percent = value;
			}
		}
	}
}
