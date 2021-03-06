﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Untity
{
    /// <summary>
    /// 工作流枚举
    /// </summary>
    public enum TWFTypeEnum
    {
        /// <summary>
        /// 长期飞行计划流程
        /// </summary>
        RepetitivePlan=1,
        /// <summary>
        /// 当日飞行计划流程
        /// </summary>
        CurrentPlan=3,
        /// <summary>
        /// 次日飞行计划流程
        /// </summary>
        FlightPlan = 2,
        /// <summary>
        /// 供求信息审批流程
        /// </summary>
        SupplyDemand = 4,
        /// <summary>
        /// 通航公司介绍流程
        /// </summary>
        CompanySummary = 5
    }
}
