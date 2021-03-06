//------------------------------------------------------------------------------
// <copyright file="NumericUpDown.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Windows.Forms {
    using System;
  
    /// <devdoc>
    ///    Comprises the information specifying how acceleration should be performed
    ///     on a Windows up-down control when the up/down button is pressed for certain
    ///     amount of time.
    /// </devdoc>
    public class NumericUpDownAcceleration
    {
        private Int32 seconds;      // Ideally we would use UInt32 but it is not CLS-compliant.
        private Decimal increment;  // Ideally we would use UInt32 but NumericUpDown uses Decimal values.

        public NumericUpDownAcceleration(Int32 seconds, Decimal increment)
        {
            if (seconds < 0)
            {
                throw new ArgumentOutOfRangeException("seconds", seconds, SR.GetString(SR.NumericUpDownLessThanZeroError));
            }

            if (increment < Decimal.Zero)
            {
                throw new ArgumentOutOfRangeException("increment", increment, SR.GetString(SR.NumericUpDownLessThanZeroError));
            }

            this.seconds   = seconds;
            this.increment = increment;
        }

        /// <devdoc>
        ///     Determines the amount of time for the UpDown control to wait to set the increment
        ///     step when holding the up/down button.
        /// </devdoc>
        public Int32 Seconds
        {
            get
            {
                return this.seconds;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("seconds", value, SR.GetString(SR.NumericUpDownLessThanZeroError));
                }
                this.seconds = value;
            }
        }

        /// <devdoc>
        ///     Determines the amount to increment by.
        /// </devdoc>
        public Decimal Increment {

            get 
            {
                return this.increment;
            }

            set
            {
                if (value < Decimal.Zero)
                {
                    throw new ArgumentOutOfRangeException("increment", value, SR.GetString(SR.NumericUpDownLessThanZeroError));
                }
                this.increment = value;
            }
        }
    }

}
