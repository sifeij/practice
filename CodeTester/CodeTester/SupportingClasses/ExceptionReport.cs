﻿using System;
using System.Diagnostics;

namespace CodeTester
{
    public class ExceptionReport
    {
        public ExceptionReport()
        {
            Debug.WriteLine("*********** Creating ExceptionReport *********** ");

            throw new ApplicationException("Ex time: " + DateTime.Now.ToLongTimeString());
        }

        public void DoSomething()
        {
        }
    }
}