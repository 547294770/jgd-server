﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SK.BL;
using SK.Common;
using SK.Common.Caches;
using SK.Entities;

namespace SK.Handler
{
    public class BaseHttpModule : IHttpModule
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
            context.EndRequest += context_EndRequest;
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication context = (HttpApplication)sender;
        }

        void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication context = (HttpApplication)sender;
            //context.Context.Response.Write("context_EndRequest....");
            //context.Context.Response.End();
        }
    }
}
