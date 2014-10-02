using System;

namespace rt.srz.ui.pvp.Controls.CustomPager
{
    /// <summary>
    /// Аргументы для событий пэйджера
    /// </summary>
    public class CustomPageChangeArgs : EventArgs
    {
        public int CurrentPageIndex
        {
            get;
            set;
        }

        public int TotalPages
        {
            get;
            set;
        }

        public int CurrentPageSize
        {
            get;
            set;
        }
    }
    
    
    /// <summary>
    /// Делегат для событий пэйджера
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void PageChangedEventHandler(object sender, CustomPageChangeArgs e);
}