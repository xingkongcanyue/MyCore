using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core.DataManager
{
    public enum ShowType
    {
        TextBox=1,

        TimeBox=2,
        DateBox=4,
        DateTimeBox=6,
        NumberBox=10,

        CheckBox=20,
        
        Select=30,
        /// <summary>
        /// 多行文本框
        /// </summary>
        TextArea,

        /// <summary>
        /// 富文本
        /// </summary>
        RichBox=100



    }
}
