﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda
{
    interface IClient
    {
        void onNumReceived(int num);
    }
}