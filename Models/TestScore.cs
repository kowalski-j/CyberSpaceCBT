using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberSpaceCBT.Models
{
    public class TestScore
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Score { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        //public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}