﻿using Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class ChapterModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string videoUrl { get; set; }
        public Boolean isPulished { get; set; }
        public int CourseId { get; set; }
    }
}