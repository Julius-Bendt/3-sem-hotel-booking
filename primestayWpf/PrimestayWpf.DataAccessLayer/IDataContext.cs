﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimestayWpf.DataAccessLayer
{
    public interface IDataContext
    {

    }

    public interface IDataContext<T> : IDataContext
    {
        T Open();
    }
}
