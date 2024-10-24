﻿using SQLite;
using System;

namespace WordMate.Core.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int WordsCount { get; set; }
    }
}
