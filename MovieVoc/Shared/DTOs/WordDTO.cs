using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MovieVoc.Shared.DTOs
{
    public class WordDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bitte geben Sie ein Wort ein")]
        public string EnglischWord { get; set; }

        [Required]
        public string Translation { get; set; }

        [Required(ErrorMessage = "Bitte geben sie eine Übersetzun ein")]
        [Range(1, 3, ErrorMessage = "Bitte geben Sie eine Zahl zwischen 1 und 3 ein")]
        public int DifficultyLevel { get; set; }

    }
}
