﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model
{
    public class PaperVideo:baseModel
    {
        public virtual int PaperId { get; set; }
        public virtual string Path { get; set; }
    }
}
