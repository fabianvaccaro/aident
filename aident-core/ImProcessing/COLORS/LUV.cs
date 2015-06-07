using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImProcessing
{
    /// <summary>
    /// Structure to define CIE LUV
    /// </summary>
    public struct LUV
    {
        /// <summary>
        /// Gets an empty CIELUV structure.
        /// </summary>
        public static readonly LUV Empty = new LUV();

        #region Fields
        private double l;
        private double u;
        private double v;

        #endregion

        #region Operators
        public static bool operator ==(LUV item1, LUV item2)
        {
            return (
                item1.L == item2.L
                && item1.U == item2.U
                && item1.V == item2.V
                );
        }

        public static bool operator !=(LUV item1, LUV item2)
        {
            return (
                item1.L != item2.L
                || item1.U != item2.U
                || item1.U != item2.U
                );
        }

        #endregion

        #region Accessors
        /// <summary>
        /// Gets or sets L component.
        /// </summary>
        public double L
        {
            get
            {
                return this.l;
            }
            set
            {
                this.l = value;
            }
        }

        /// <summary>
        /// Gets or sets a component.
        /// </summary>
        public double U
        {
            get
            {
                return this.u;
            }
            set
            {
                this.u = value;
            }
        }

        /// <summary>
        /// Gets or sets a component.
        /// </summary>
        public double V
        {
            get
            {
                return this.v;
            }
            set
            {
                this.v = value;
            }
        }

        #endregion

        public LUV(double l, double u, double v)
        {
            this.l = l;
            this.u = u;
            this.v = v;
        }

        #region Methods
        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            return (this == (LUV)obj);
        }

        public override int GetHashCode()
        {
            return L.GetHashCode() ^ u.GetHashCode() ^ v.GetHashCode();
        }

        #endregion
    }
}
