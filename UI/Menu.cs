using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Services;
using GerenciadorDeTarefas.Utils;

namespace GerenciadorDeTarefas.UI;

public class Menu
{
    public static void ExibirMenu(ServicoDeTarefas servico)
    {
        var tarefas = servico.ObterTodas();
        int concluidas = tarefas.Count(t => t.Status == Tarefa.StatusTarefa.Concluida);
        int pendentes = tarefas.Count(t => t.Status == Tarefa.StatusTarefa.Pendente);
        
        Console.WriteLine("-----------------------------");
        Console.WriteLine("Gerenciador de tarefas");
        Console.WriteLine($"Pendentes: {pendentes} Concluídas: {concluidas}");
        Console.WriteLine("1 - Adicionar uma nova tarefa");
        Console.WriteLine("2 - Remover uma tarefa");
        Console.WriteLine("3 - Concluir uma tarefa");
        Console.WriteLine("4 - Reabrir tarefa");
        Console.WriteLine("5 - Editar tarefa");
        Console.WriteLine("6 - Listar todas as tarefas");
        Console.WriteLine("7 - Remover tarefas concluídas");
        Console.WriteLine("0 - Sair do programa");
        Console.WriteLine("------------------------------");
    }

    static bool TemTarefas(ServicoDeTarefas servico) => servico.ObterTodas().Count > 0;

    static bool TemTarefaConcluida(ServicoDeTarefas servico) =>
        servico.ObterTodas().Any(t => t.Status == Tarefa.StatusTarefa.Concluida);

    static void ListarTarefas(IReadOnlyList<Tarefa> tarefas)
    {
        for (int i = 0; i < tarefas.Count; i++)
        {
            var t = tarefas[i];
            var status = t.Status == Tarefa.StatusTarefa.Concluida ? "Concluída" : "Pendente";
            var inicio = t.DataInicio.ToString("dd/MM/yyyy HH:mm");
            var conclusao = t.DataConclusao?.ToString("dd/MM/yyyy HH:mm") ?? "-";
            
            Console.WriteLine($"{i + 1} - {t.Titulo} - {status} | Inicio: {inicio} | Conclusão {conclusao}");
        }
    }

    public static void Pausar()
    {
        Console.WriteLine("Pressione uma tecla para continuar.");
        Console.ReadKey();
    }

    public static void AdicionarTarefa(ServicoDeTarefas servico)
    {
        Console.WriteLine("Adicionar Tarefa");
        var adicionar = servico.Adicionar(Entrada.LerTexto("Digite o nome da tarefa: "));
        if (adicionar.IsSuccess)
        {
            Saida.Sucesso("Tarefa adicionada com sucesso");
        }
        else
        {
            Saida.Erro(adicionar.Error.Description);
        }
    }

    public static void RemoverTarefa(ServicoDeTarefas servico)
    {
        Console.WriteLine("Remover Tarefa");
        if (!TemTarefas(servico))
        {
            Saida.Aviso("Nenhuma tarefa cadastrada.");
            return;
        }
        ListarTarefas(servico.ObterTodas());
        Console.Write("Digite o número da tarefa que gostaria de remover: ");
        int.TryParse(Console.ReadLine(), out int numeroRemover);
        if (!Entrada.LerConfirmacao("Tem certeza que deseja remover? (S/N): "))
        {
            Console.WriteLine("Remoção cancelada.");
            return;
        }
        var remover = servico.Remover(numeroRemover);
        if (remover.IsSuccess)
        {
            Saida.Sucesso("Tarefa removida com sucesso");
        }
        else
        {
            Saida.Erro(remover.Error.Description);
        }
    }

    public static void ConcluirTarefa(ServicoDeTarefas servico)
    {
        Console.WriteLine("Concluir a tarefa");
        if (!TemTarefas(servico))
        {
            Saida.Aviso("Nenhuma tarefa cadastrada.");
            return;
        }
        ListarTarefas(servico.ObterTodas());
        Console.Write("Digite o número da tarefa que deseja marcar como concluída: ");
        int.TryParse(Console.ReadLine(), out int numeroConcluir);
        var concluir = servico.Concluir(numeroConcluir);
        if (concluir.IsSuccess)
        {
            Saida.Sucesso("Tarefa comcluida com sucesso.");
        }
        else
        {
            Saida.Erro(concluir.Error.Description);
        }
    }

    public static void ReabrirTarefa(ServicoDeTarefas servico)
    {
        Console.WriteLine("Desconcluir a tarefa");
        if (!TemTarefas(servico))
        {
            Saida.Aviso("Nenhuma tarefa cadastrada.");
            return;
        }

        if (!TemTarefaConcluida(servico))
        {
            Saida.Aviso("Nehnhuma tarefa concluida para reabrir");
            return;
        }
        ListarTarefas(servico.ObterTodas());
        Console.Write("Digite o número da tarefa que deseja reabrir: ");
        int.TryParse(Console.ReadLine(), out int numeroReabrir);
        var reabrir = servico.Reabrir(numeroReabrir);
        if (reabrir.IsSuccess)
        {
            Saida.Sucesso("Tarefa reaberta com sucesso.");
        }
        else
        {
            Saida.Erro(reabrir.Error.Description);
        }
    }

    public static void ListarTodasTarefas(ServicoDeTarefas servico)
    {
        Console.WriteLine("Lista de todas as tarefas");
        if (!TemTarefas(servico))
        {
            Saida.Aviso("Nenhuma tarefa cadastrada.");
            return;
        }
        ListarTarefas(servico.ObterTodas());
    }

    public static void EditarTarefa(ServicoDeTarefas servico)
    {
        Console.WriteLine("Editar Tarefa");
        if (!TemTarefas((servico)))
        {   
            Saida.Aviso("Nenhuma tarefa cadastrada.");
            return;
        }
        ListarTarefas(servico.ObterTodas());
        Console.Write("Digite o número da tarefa que deseja editar o titulo: ");
        int.TryParse(Console.ReadLine(), out int numeroEditar);
        var validacao = servico.ValidarNumero(numeroEditar);
        if (validacao.IsFailure)
        {
            Saida.Erro(validacao.Error.Description);
            return;
        }
        string novoTitulo = Entrada.LerTexto("Digite o novo titulo: ");
        var editar = servico.Editar(numeroEditar, novoTitulo);
        if (editar.IsSuccess)
        {
            Saida.Sucesso("Tarefa editada com sucesso.");
        }
        else
        {
            Saida.Erro(editar.Error.Description);
        }
    }

    public static void RemoverTodasConcluidas(ServicoDeTarefas servico)
    {
        Console.WriteLine("Remover tarefas concluídas");
        if (!TemTarefas(servico))
        {
            Saida.Aviso("Nenhuma tarefa cadastrada.");
            return;
        }
        if (!TemTarefaConcluida(servico))
        {
            Saida.Aviso("Nehnhuma tarefa concluida para remover");
            return;
        }
        if (!Entrada.LerConfirmacao("Tem certeza que deseja remover? (S/N): "))
        {
            Console.WriteLine("Remoção cancelada.");
            return;
        }
        var remover = servico.RemoverConcluidas();
        if (remover.IsSuccess)
        {
            Saida.Sucesso($"{remover.Value} Tarefa(s) concluída(s) removida(s) com sucesso.");
        }
        else
        {
            Saida.Erro(remover.Error.Description);
        }
    }
}