﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ARPGCommon.Model
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
    }
}