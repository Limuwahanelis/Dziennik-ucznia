﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DziennikUcznia.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Class Class { get; set; } = null!;
        public ICollection<Grade> Grades { get; } = new List<Grade>();

    }
}
