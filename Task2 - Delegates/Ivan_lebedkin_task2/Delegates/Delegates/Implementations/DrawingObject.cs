﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delegates.Core.Abstractions;

namespace Delegates.Implementations
{
    class DrawingObject : IDrawingObject
    {
        public string Name { get; set; }
        public override bool Equals(object obj)//
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }           
            
            return this.GetType() == obj.GetType();
        }
    }
}
