using System;
using System.Collections.Generic;
using System.Text;

namespace ARPGCommon.Model
{
    public class TaskDb
    {
        public virtual int Id { get; set; }
        public virtual Role Role { get; set; }
        public virtual int TaskId { get; set; }
        public virtual byte TaskType { get; set; }
        public virtual byte TaskState { get; set; }
        public virtual DateTime LastUpdateTime { get; set; }
    }
}