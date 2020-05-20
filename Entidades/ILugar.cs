namespace platzi_curso_csharp.Entidades
{
  public interface ILugar
  {
    string Direccion { get; set; }

    void LimpiarLugar();
  }
}