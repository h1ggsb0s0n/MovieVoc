using MovieVoc.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MovieVoc.Shared.DTOs
{
    public class MovieDTO
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Bitte geben Sie einen Titel ein")]
        [MinLength(2, ErrorMessage = "Ein Titel muss mindestens 2 Buchstaben haben")]
        [MaxLength(50, ErrorMessage = "Ein Titel kann maximum 50 Buchstaben haben")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Bitte geben Sie eine Zusammenfassung Titel ein")]
        [MinLength(2, ErrorMessage = "Eine Zusammenfassung muss mindestens 50 Buchstaben haben")]
        [MaxLength(250, ErrorMessage = "Eine Zusammenfassung kann maximum 250 Buchstaben haben")]
        public string Summary { get; set; }

        [Required (ErrorMessage = "Bitte geben Sie ein Release Date ein")]
        public DateTime? ReleaseDate { get; set; }

        public string Poster { get; set; }

        public string TitleBrief
        {
            get
            {
                if (string.IsNullOrEmpty(Title))
                {
                    return null;
                }

                if (Title.Length > 60)
                {
                    return Title.Substring(0, 60) + "...";
                }
                else
                {
                    return Title;
                }
            }
        }
    }
}
