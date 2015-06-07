using System;
using System.ComponentModel;

namespace ImProcessing
{
	/// <summary>
	/// Structure to define RGB.
	/// </summary>
	public struct nRGB
	{
		/// <summary>
		/// Gets an empty RGB structure;
		/// </summary>
		public static readonly nRGB Empty = new nRGB();

		#region Fields
		private Double red;
        private Double green;
        private Double blue;

		#endregion

		#region Operators
		public static bool operator ==(nRGB item1, nRGB item2)
		{
			return (
				item1.Red == item2.Red 
				&& item1.Green == item2.Green 
				&& item1.Blue == item2.Blue
				);
		}

		public static bool operator !=(nRGB item1, nRGB item2)
		{
			return (
				item1.Red != item2.Red 
				|| item1.Green != item2.Green 
				|| item1.Blue != item2.Blue
				);
		}

		#endregion

		#region Accessors
		[Description("Red component."),]
        public Double Red
		{
			get
			{
				return red;
			}
			set
			{
				red = (value>1)? 1 : ((value<0)?0 : value);
			}
		}

		[Description("Green component."),]
        public Double Green
		{
			get
			{
				return green;
			}
			set
			{
				green = (value>1)? 1 : ((value<0)?0 : value);
			}
		}

		[Description("Blue component."),]
        public Double Blue
		{
			get
			{
				return blue;
			}
			set
			{
				blue = (value>1)? 1 : ((value<0)?0 : value);
			}
		}
		#endregion

        public nRGB(Double nR, Double nG, Double nB) 
		{
			red = (nR>1)? 1 : ((nR<0)?0 : nR);
			green = (nG>1)? 1 : ((nG<0)?0 : nG);
			blue = (nB>1)? 1 : ((nB<0)?0 : nB);
		}

		#region Methods
		public override bool Equals(Object obj) 
		{
			if(obj==null || GetType()!=obj.GetType()) return false;

			return (this == (nRGB)obj);
		}

		public override int GetHashCode() 
		{
			return Red.GetHashCode() ^ Green.GetHashCode() ^ Blue.GetHashCode();
		}

		#endregion
	} 
}
