using System;
using System.Collections.Generic;

namespace tp3.Models
{
    public partial class Aluno
    {
        public Aluno()
        {
            Curso = new HashSet<Curso>();
        }

        public int AlunoId { get; set; }
        public string Nome { get; set; } = null!;
        public DateTime? DataNascimento { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Curso> Curso { get; set; }
    }
}
