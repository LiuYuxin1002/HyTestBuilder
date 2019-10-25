/*---------------------------------------------------------------------------
 * File: LBMeterThreshold.cs
 * Utente: lucabonotto
 * Date: 05/04/2009
 *-------------------------------------------------------------------------*/

using System;
using System.Collections;
using System.Drawing;

namespace HyTestRTDataService.Controls.Meters
{
    /// <summary>
    /// Class for the meter threshold
    /// </summary>
	public class HTMeterThreshold
	{
		#region (* Properties variables *)
		private	Color	color = Color.Empty;
		private double	startValue = 0.0;
		private double	endValue = 1.0;
		#endregion
		
		#region (* Constructor *)
		public HTMeterThreshold()
		{			
		}
		#endregion
		
		#region (* Properties *)
		public Color Color
		{
			set { this.color = value; }
			get { return this.color; }
		}
		
		public double StartValue
		{
			set { this.startValue = value; }
			get { return this.startValue; }
		}
		
		public double EndValue
		{
			set { this.endValue = value; }
			get { return this.endValue; }
		}
		#endregion
		
		#region (* Public methods *)
		public bool IsInRange ( double val )
		{
			if ( val > this.EndValue )
				return false;
			
			if ( val < this.StartValue )
				return false;
			
			return true;
		}
		#endregion
	}
		
	/// <summary>
	/// Collection of the meter thresolds
	/// </summary>
	public class HTMeterThresholdCollection : CollectionBase
    {
        #region (* Properties variables *)
		private bool _IsReadOnly = false;
		#endregion
		
		#region (* Constructor *)
        public HTMeterThresholdCollection()
        {
        }
		#endregion
		
        #region (* Properties *)
        public virtual HTMeterThreshold this[int index]
        {
            get { return (HTMeterThreshold)InnerList[index]; }
            set { InnerList[index] = value; }
        }

        public virtual bool IsReadOnly
        {
            get { return _IsReadOnly; }
        }
		#endregion
		
		#region (* Public methods *)
		/// <summary>
		/// Add an object to the collection
		/// </summary>
		/// <param name="sector"></param>
		public virtual void Add(HTMeterThreshold sector)
        {
            InnerList.Add(sector);
        }

		/// <summary>
		/// Remove an object from the collection
		/// </summary>
		/// <param name="sector"></param>
		/// <returns></returns>
        public virtual bool Remove(HTMeterThreshold sector) 
        {
            bool result = false;

            //loop through the inner array's indices
            for (int i = 0; i < InnerList.Count; i++)
            {
                //store current index being checked
                HTMeterThreshold obj = (HTMeterThreshold)InnerList[i];

                //compare the values of the objects
                if ( ( obj.StartValue == sector.StartValue ) && 
                    ( obj.EndValue == sector.EndValue ) )
                {
                    //remove item from inner ArrayList at index i
                    InnerList.RemoveAt(i);
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Check if the object is containing in the collection
        /// </summary>
        /// <param name="sector"></param>
        /// <returns></returns>
        public bool Contains(HTMeterThreshold sector)
        {
            //loop through the inner ArrayList
            foreach (HTMeterThreshold obj in InnerList)
            {
               //compare the values of the objects
                if ( ( obj.StartValue == sector.StartValue ) && 
                    ( obj.EndValue == sector.EndValue ) )
                {
                    //if it matches return true
                    return true;
                }
            }
            
            //no match
            return false;
        }
 
        /// <summary>
        /// Copy the collection
        /// </summary>
        /// <param name="LBAnalogMeterSectorArray"></param>
        /// <param name="index"></param>
        public virtual void CopyTo(HTMeterThreshold[] MeterThresholdArray, int index)
        {
            throw new Exception("This Method is not valid for this implementation.");
        }
		#endregion
	}
}
