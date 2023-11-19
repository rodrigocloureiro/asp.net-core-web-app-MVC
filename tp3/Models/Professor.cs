using System;
using System.Collections.Generic;

namespace tp3.Models
{
    public partial class Professor
    {
        public Professor()
        {
            Disciplina = new HashSet<Disciplina>();
        }

        public int ProfessorId { get; set; }
        public string Nome { get; set; } = null!;
        public DateTime? DataContratacao { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Disciplina> Disciplina { get; set; }
    }
}
