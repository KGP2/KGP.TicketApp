﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketAPP.Utils.Validation
{
    public interface IValidator<T> 
    {
        public bool Validate(T obj, out string error);
    }
}
