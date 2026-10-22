using GerenciadorDeTarefas.Common;
using Newtonsoft.Json;

namespace GerenciadorDeTarefas.Models;

public class Tarefa
{
    public string Titulo { get; private set; }
    public bool Concluida { get; private set; }

    [JsonConstructor]
    public Tarefa(string titulo, bool concluida = false)
    {
        Titulo = titulo;
        Concluida = concluida;
    }
    
    public void Concluir()
    {
        Concluida = true;
    }

    public void Reabrir()
    {
        Concluida = false;
    }

    public static Result<Tarefa> Criar(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
        {
            return TarefaErrors.TituloVazio;
        }

        return new Tarefa(titulo.Trim());
    }

    public Result Editar(string novoTitulo)
    {
        if (string.IsNullOrWhiteSpace(novoTitulo))
        {
            return TarefaErrors.TituloVazio;
        }

        Titulo = novoTitulo.Trim();
        return Result.Success();
    }
    
}