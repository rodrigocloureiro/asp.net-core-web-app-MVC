using System;
using System.Collections.Generic;

namespace tp3.Models
{
    public partial class Curso
    {
        public int CursoId { get; set; }
        public string Nome { get; set; } = null!;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int? AlunoId { get; set; }
        public int? DisciplinaId { get; set; }

        public virtual Aluno? Aluno { get; set; }
        public virtual Disciplina? Disciplina { get; set; }
    }
}
