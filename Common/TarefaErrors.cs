namespace GerenciadorDeTarefas.Common;

public static class TarefaErrors
{
    public static readonly Error TituloVazio =
        new("Tarefa.TituloVazio", "O título da tarefa não pode estar vazio.");

    public static readonly Error NumeroInvalido =
        new("Tarefa.NumeroInvalido", "Número da tarefa inválido.");

    public static readonly Error ListaVazia =
        new("Tarefa.ListaVazia", "Nenhuma tarefa cadastrada.");

    public static readonly Error NenhumaConcluida =
        new("Tarefa.NenhumaConcluida", "Nenhuma tarefa concluída para remover");
}