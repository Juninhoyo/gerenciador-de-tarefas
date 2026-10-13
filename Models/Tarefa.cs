using System.Text.Json.Serialization;

namespace GerenciadorDeTarefas.Models;

public class Tarefa
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public bool Concluido { get; set; }
}