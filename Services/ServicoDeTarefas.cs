using GerenciadorDeTarefas.Common;
using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Utils;

namespace GerenciadorDeTarefas.Services;

public sealed class ServicoDeTarefas
{
    private readonly SalvarArquivoTarefa _repositorio;
    private readonly List<Tarefa> _tarefas;

    public ServicoDeTarefas(SalvarArquivoTarefa repositorio)
    {
        _repositorio = repositorio;
        _tarefas = repositorio.CarregarArquivo();
    }

    public IReadOnlyList<Tarefa> ObterTodas() => _tarefas;

    private bool NumeroValido(int numero) => numero >= 1 && numero <= _tarefas.Count;
    
    public Result<Tarefa> Adicionar(string titulo)
    {
        var resultado = Tarefa.Criar(titulo);
        if (resultado.IsFailure)
        {
            return resultado.Error;
        }
        _tarefas.Add(resultado.Value);
        _repositorio.SalvarArquivo(_tarefas);
        return resultado;
    }

    public Result ValidarNumero(int numero)
    {
        if (_tarefas.Count == 0)
        {
            return TarefaErrors.ListaVazia;
        }
        if (!NumeroValido(numero))
        {
            return TarefaErrors.NumeroInvalido;
        }

        return Result.Success();
    }

    public Result Remover(int numero)
    {
        ValidarNumero(numero);
        _tarefas.RemoveAt(numero - 1);
        _repositorio.SalvarArquivo(_tarefas);
        return Result.Success();
    }

    public Result Concluir(int numero)
    {
        ValidarNumero(numero);
        _tarefas[numero - 1].Concluir();
        _repositorio.SalvarArquivo(_tarefas);
        return Result.Success();
    }

    public Result Reabrir(int numero)
    {
        ValidarNumero(numero);
        _tarefas[numero - 1].Reabrir();
        _repositorio.SalvarArquivo(_tarefas);
        return Result.Success();
    } 
    
    public Result Editar(int numero, string novoTitulo)
    {
        ValidarNumero(numero);
        var resultado = _tarefas[numero - 1].Editar(novoTitulo);
        if (resultado.IsFailure)
        {
            return resultado;
        }
        _repositorio.SalvarArquivo(_tarefas);
        return Result.Success();
    }
}