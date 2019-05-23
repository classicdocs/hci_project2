﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Models
{
    public class ResourcePoint
    {
        public Resource resource;
        public Point point;

        public ResourcePoint() { }
        public ResourcePoint(Resource res, Point p)
        {
            resource = res;
            point = p;
        }
    }
}