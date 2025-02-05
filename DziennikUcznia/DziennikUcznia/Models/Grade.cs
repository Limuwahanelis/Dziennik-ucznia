﻿using DziennikUcznia.Models.View_Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DziennikUcznia.Models
{
    public class Grade
    {
        public enum GradeType
        {
            HOMEWORK,TEST,ACTIVITY,EGZAM
        }
        public Grade() { }
        public Grade (AddGradeModel model)
        {
            Value = model.Value;
            Type=model.Type;
        }
        public int Id { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public Student Student { get; set; } = null!;
        [Range(1,6)]
        public int Value { get; set; }
        [Column("Grade_type")]
        public GradeType Type { get; set;}
        public Subject Subject { get; set; } = null!;
    }
}
