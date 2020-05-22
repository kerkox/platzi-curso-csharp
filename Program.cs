using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
  class Program
  {
    static void Main(string[] args)
    {
      // AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
      var engine = new EscuelaEngine();
      engine.Inicializar();
      Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");

      var reporteador = new Reporteador(engine.GetDiccionarioObjeto());
      reporteador.GetListaEvaluaciones();
      var listaPromedioXAsig = reporteador.GetPromAlumnoPorAsignatura();
      // //Printer.Beep(10000, cantidad: 10);
      // ImpimirCursosEscuela(engine.Escuela);
      // //    int conteoEvaluaciones, conteoAlumnos, conteoAsignaturas, conteoCursos;
      // var dictmp = engine.GetDiccionarioObjeto();
      // engine.ImprimirDiccionario(dictmp, true);
    }

    private static void AccionDelEvento(object sender, EventArgs e)
    {
      Printer.WriteTitle("SALIENDO");
      Printer.Beep(500, 500, 3);
      Printer.WriteTitle("SALIO");
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
