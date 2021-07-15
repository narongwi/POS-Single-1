using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJCBCPOS
{
    public class CalculatorParseEventArgs : EventArgs
    {
        #region State

        private string m_Original = String.Empty;
        private string m_Parsed = String.Empty;

        #endregion //--State


        #region Construction

        /// <summary>
        /// Constructs a new CalculatorParseEventArgs with the given string.
        /// </summary>
        /// <param name="original"></param>
        public CalculatorParseEventArgs(string original)
        {
            m_Original = null == original ? String.Empty : original;
            m_Parsed = m_Original;
        }
        #endregion //--Construction


        #region Public Interface

        /// <summary>
        /// Gets the original string value.
        /// </summary>
        public string Original
        {
            get { return m_Original; }
        }

        /// <summary>
        /// Gets or sets the parsed string value.
        /// </summary>
        public string Parsed
        {
            get { return m_Parsed; }
            set { m_Parsed = value; }
        }

        /// <summary>
        /// Gets the double of the parsed string. Will return 0.0
        /// if the value cannot be parsed to a double.
        /// </summary>
        /// <returns></returns>
        public double GetResult()
        {
            double d = 0.0;
            Double.TryParse(m_Parsed, out d);

            return d;
        }
        #endregion //--Public Interface
    }
}
