using GerenciadorDeTarefas.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GerenciadorDeTarefas.Models;

public class Tarefa
{
    public string Titulo { get; private set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public StatusTarefa Status { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime? DataConclusao { get; private set; }

    
    public enum StatusTarefa
    {
        Pendente,
        Concluida
    }

    [JsonConstructor]
    public Tarefa(string titulo, StatusTarefa status, DateTime dataInicio, DateTime? dataConclusao)
    {
        Titulo = titulo;
        Status = status;
        DataInicio = dataInicio;
        DataConclusao = dataConclusao;
    }

    public void Concluir()
    {
        Status = StatusTarefa.Concluida;
        DataConclusao = DateTime.Now;
    }

    public void Reabrir()
    {
        Status = StatusTarefa.Pendente;
        DataConclusao = null;
    }

    public static Result<Tarefa> Criar(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
        {
            return TarefaErrors.TituloVazio;
        }

        return new Tarefa(titulo.Trim(), StatusTarefa.Pendente, DateTime.Now, null);
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