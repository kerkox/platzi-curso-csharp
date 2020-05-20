using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using platzi_curso_csharp.Entidades;
using static System.Console;

namespace CoreEscuela
{
  class Program
  {
    static void Main(string[] args)
    {
      var engine = new EscuelaEngine();
      engine.Inicializar();
      Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
      //Printer.Beep(10000, cantidad: 10);
      ImpimirCursosEscuela(engine.Escuela);
      var listaObjetos = engine.GetObjetosEscuela(traeEvaluaciones: false);


    }

    private static void ImpimirCursosEscuela(Escuela escuela)
    {

      Printer.WriteTitle("Cursos de la Escuela");


      if (escuela?.Cursos != null)
      {
        foreach (var curso in escuela.Cursos)
        {
          WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
        }
      }
    }
  }
}
