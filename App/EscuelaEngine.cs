using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;

namespace CoreEscuela.App
{
  public sealed class EscuelaEngine
  {
    public Escuela Escuela { get; set; }

    public EscuelaEngine()
    {

    }

    public void Inicializar()
    {
      Escuela = new Escuela("Platzi Academay", 2012, TiposEscuela.Primaria,
      ciudad: "Bogotá", pais: "Colombia"
      );

      CargarCursos();
      CargarAsignaturas();
      CargarEvaluaciones();

    }

    private void CargarEvaluaciones()
    {
      var rnd = new Random();
      foreach (var curso in Escuela.Cursos)
      {
        foreach (var asignatura in curso.Asignaturas)
        {
          foreach (var alumno in curso.Alumnos)
          {


            for (int i = 0; i < 5; i++)
            {
              var ev = new Evaluación
              {
                Asignatura = asignatura,
                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                // Nota = (float)Math.Round((5 * rnd.NextDouble()), 2),
                Nota = MathF.Round((float)(5 * rnd.NextDouble()), 2),
                Alumno = alumno
              };
              alumno.Evaluaciones.Add(ev);
            }
          }
        }
      }

    }

    public void ImprimirDiccionario(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> diccionario,
    bool imprEval = false)
    {
      foreach (var obj in diccionario)
      {
        Printer.WriteTitle(obj.Key.ToString());
        foreach (var val in obj.Value)
        {
          if (val is Evaluación && !imprEval)
          {
            continue;
          }
          Console.WriteLine(val);

        }
      }
    }

    public Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjeto()
    {
      var diccionario = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>>();
      diccionario.Add(LlaveDiccionario.Escuela, new[] { Escuela });
      diccionario.Add(LlaveDiccionario.Curso, Escuela.Cursos.Cast<ObjetoEscuelaBase>());
      var ListAsignatura = new List<Asignatura>();
      var ListAlumno = new List<Alumno>();
      var ListEvaluaciones = new List<Evaluación>();
      Escuela.Cursos.ForEach(curso =>
      {
        ListAsignatura.AddRange(curso.Asignaturas);
        ListAlumno.AddRange(curso.Alumnos);
        curso.Alumnos.ForEach(alumno => ListEvaluaciones.AddRange(alumno.Evaluaciones));
      });
      diccionario.Add(LlaveDiccionario.Asignatura, ListAsignatura);
      diccionario.Add(LlaveDiccionario.Alumno, ListAlumno);
      diccionario.Add(LlaveDiccionario.Evaluacion, ListEvaluaciones);

      return diccionario;
    }


    public IReadOnlyCollection<ObjetoEscuelaBase> GetObjetosEscuela(
        out int conteoEvaluaciones,
        out int conteoAlumnos,
        out int conteoAsignaturas,
        out int conteoCursos,
        bool traeEvaluaciones = true,
        bool traeAlumnos = true,
        bool traeAsignaturas = true,
        bool traeCursos = true

    )
    {
      conteoEvaluaciones = conteoAsignaturas = conteoAlumnos = 0;
      var listaObj = new List<ObjetoEscuelaBase>();
      listaObj.Add(Escuela);
      if (traeCursos)
        listaObj.AddRange(Escuela.Cursos);

      conteoCursos = Escuela.Cursos.Count;
      foreach (var curso in Escuela.Cursos)
      {
        conteoAsignaturas += curso.Asignaturas.Count;
        conteoAlumnos += curso.Alumnos.Count;
        if (traeAsignaturas)
          listaObj.AddRange(curso.Asignaturas);

        if (traeAlumnos)
          listaObj.AddRange(curso.Alumnos);


        if (traeEvaluaciones)
        {
          foreach (var alumno in curso.Alumnos)
          {
            listaObj.AddRange(alumno.Evaluaciones);
            conteoEvaluaciones += alumno.Evaluaciones.Count;
          }
        }
      }

      return listaObj.AsReadOnly();
    }

    private void CargarAsignaturas()
    {
      foreach (var curso in Escuela.Cursos)
      {
        var listaAsignaturas = new List<Asignatura>(){
              new Asignatura{Nombre="Matemáticas"} ,
              new Asignatura{Nombre="Educación Física"},
              new Asignatura{Nombre="Castellano"},
              new Asignatura{Nombre="Ciencias Naturales"}
        };
        curso.Asignaturas = listaAsignaturas;
      }
    }

    private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
    {
      string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
      string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
      string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

      var listaAlumnos = from n1 in nombre1
                         from n2 in nombre2
                         from a1 in apellido1
                         select new Alumno { Nombre = $"{n1} {n2} {a1}" };

      return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
    }

    private void CargarCursos()
    {
      Escuela.Cursos = new List<Curso>(){
            new Curso(){ Nombre = "101", Jornada = TiposJornada.Mañana },
            new Curso() {Nombre = "201", Jornada = TiposJornada.Mañana},
            new Curso{Nombre = "301", Jornada = TiposJornada.Mañana},
            new Curso(){ Nombre = "401", Jornada = TiposJornada.Tarde },
            new Curso() {Nombre = "501", Jornada = TiposJornada.Tarde},
      };

      Random rnd = new Random();
      foreach (var c in Escuela.Cursos)
      {
        int cantRandom = rnd.Next(5, 20);
        c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
      }
    }
  }
}