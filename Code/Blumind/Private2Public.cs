using Blumind.ChartPageView;
using Blumind.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blumind
{
    public class Private2Public
    {
        private MultiChartsView mcv;
        private MyTabControl mtc;
        public Private2Public()
        {
            this.mcv = new MultiChartsView();
            this.mtc = new MyTabControl();
        }
        public MultiChartsView GetMCV()
        {
            return this.mcv;
        }

    }
}
