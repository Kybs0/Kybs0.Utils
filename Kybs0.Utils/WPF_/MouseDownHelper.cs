﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kybs0.Net.Utils
{
    public class MouseDownHelper
    {

        private static TimeSpan _timeSpan = new TimeSpan();
        private static DateTime _dateTime = new DateTime();
        private static Point _prePoint = new Point();

        /// <summary>
        /// 判断是否双击
        /// </summary>
        /// <param name="point"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static bool IsDoubleClick(Point point, DateTime dataTime)
        {
            var duration = dataTime.Subtract(_dateTime).Duration();

            if (duration.TotalMilliseconds < 500 &&
                Math.Abs(point.X - _prePoint.X) < 2 && Math.Abs(point.Y - _prePoint.Y) < 2)
                return true;

            _dateTime = dataTime;
            _prePoint = point;
            return false;
        }
    }
}
