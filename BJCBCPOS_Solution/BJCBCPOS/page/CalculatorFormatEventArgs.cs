using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJCBCPOS
{
    public class CalculatorFormatEventArgs : EventArgs
    {
        #region State

        private double m_Result = 0.0;
        private string m_FormattedResult = String.Empty;

        #endregion //--State


        #region Construction

        /// <summary>
        /// Constructs a new CalculatorFormatEventArgs with the result of
        /// the calculation.
        /// </summary>
        /// <param name="result"></param>
        public CalculatorFormatEventArgs(double result)
        {
            m_Result = result;
            m_FormattedResult = m_Result.ToString();
        }
        #endregion //--Construction


        #region Public Interface

        /// <summary>
        /// Gets the result of the calculation.
        /// </summary>
        public double Result
        {
            get { return m_Result; }
        }

        /// <summary>
        /// Gets or sets the formatted result.
        /// </summary>
        public string FormattedResult
        {
            get { return m_FormattedResult; }
            set { m_FormattedResult = value; }
        }
        #endregion //--Public Interface
    }
}
