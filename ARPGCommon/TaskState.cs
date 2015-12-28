using System;
using System.Collections.Generic;
using System.Text;

namespace ARPGCommon
{
    public enum TaskState : byte
    {
        UnStarted,
        Accepted,
        Accomplished,
        Achieved
    }

    public enum TaskType : byte
    {
        Main,
        Reward,
        Daily
    }
}