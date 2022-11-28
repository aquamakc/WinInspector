﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ConInspector.DB.DbModels
{
    public class Frequency
    {
        [Key]
        public int Id { get; set; }

        public double Value { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
