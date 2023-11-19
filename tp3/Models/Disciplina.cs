using System;
using System.Collections.Generic;

namespace tp3.Models
{
    public partial class Disciplina
    {
        public Disciplina()
        {
            Curso = new HashSet<Curso>();
        }

        public int DisciplinaId { get; set; }
        public string Nome { get; set; } = null!;
        public int? Horas { get; set; }
        public int? ProfessorId { get; set; }

        public virtual Professor? Professor { get; set; }
        public virtual ICollection<Curso> Curso { get; set; }
    }
}
