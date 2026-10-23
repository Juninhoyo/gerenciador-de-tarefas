using GerenciadorDeTarefas.Common;
using Newtonsoft.Json;

namespace GerenciadorDeTarefas.Models;

public class Tarefa
{
    public string Titulo { get; private set; }
    public bool Concluida { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime? DataConclusao { get; private set; }

    [JsonConstructor]
    public Tarefa(string titulo, bool concluida, DateTime dataInicio, DateTime? dataConclusao)
    {
        Titulo = titulo;
        Concluida = concluida;
        DataInicio = dataInicio;
        DataConclusao = dataConclusao;
    }
    
    public void Concluir()
    {
        Concluida = true;
        DataConclusao = DateTime.Now;
    }

    public void Reabrir()
    {
        Concluida = false;
        DataConclusao = null;
    }

    public static Result<Tarefa> Criar(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
        {
            return TarefaErrors.TituloVazio;
        }

        return new Tarefa(titulo.Trim(),false, DateTime.Now, null);
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