﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStay.DataAccessLayer
{
    public interface IDataContext
    {

    }

    public interface IDataContext<T> : IDataContext
    {
        T Open();
    }
}
