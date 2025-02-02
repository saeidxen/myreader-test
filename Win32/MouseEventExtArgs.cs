﻿using System.Windows.Forms;

namespace SimpleReaderDemo.Win32
{
    /// <summary>
    /// Mouse hook extension event
    /// </summary>
    public class MouseEventExtArgs : MouseEventArgs
    {
        #region variable
        /// <summary>
        /// If true, prevent further processing of other application events.
        /// </summary>
        private bool _handled;
        #endregion

        #region constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="clicks"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="delta"></param>
        public MouseEventExtArgs(MouseButtons buttons, int clicks, int x, int y, int delta)
            : base(buttons, clicks, x, y, delta)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        internal MouseEventExtArgs(MouseEventArgs e)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
        }

        #endregion

        #region Attrbutes
        /// <summary>
        /// If true, prevent further processing of other application events.
        /// </summary>
        public bool Handled
        {
            get { return this._handled; }
            set { this._handled = value; }
        }
        #endregion
    }
}
