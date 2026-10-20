using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Services;
using GerenciadorDeTarefas.Utils;

var servico = new ServicoDeTarefas(new SalvarArquivoTarefa());

string opcao = "";

do
{
    try
    {
        Console.Clear();
        ExibirMenu();
        opcao = Entrada.LerTexto("Digite a opção que gostaria de continuar: ");

        switch (opcao)
        {
            case "1":
                Console.Clear();
                AdicionarTarefa(servico);
                Pausar();
                break;

            case "2":
                Console.Clear();
                RemoverTarefa(servico);
                Pausar();
                break;

            case "3":
                Console.Clear();
                ConcluirTarefa(servico);
                Pausar();
                break;

            case "4":
                Console.Clear();
                ListarTodasTarefas(servico);
                Pausar();
                break;

            case "0":
                Console.WriteLine("Saindo do programa");
                break;

            default:
                Console.WriteLine("Opção inválida");
                Pausar();
                break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
        Console.ReadKey();
    }
} while (opcao != "0");
    
static void ExibirMenu()
{
    Console.WriteLine("-----------------------------");
    Console.WriteLine("1 - Adicionar uma nova tarefa");
    Console.WriteLine("2 - Remover uma tarefa");
    Console.WriteLine("3 - Concluir uma tarefa");
    Console.WriteLine("4 - Listar todas as tarefas");
    Console.WriteLine("0 - Sair do programa");
    Console.WriteLine("------------------------------");
}

static void ListarTarefas(IReadOnlyList<Tarefa> tarefas)
{
    for (int i = 0; i < tarefas.Count; i++)
    {
        Console.WriteLine($"{i + 1} - {tarefas[i].Titulo} - {(tarefas[i].Concluida ? "Concluída" : "Pendente")}");
    }
}

static void Pausar()
{
    Console.WriteLine("Pressione uma tecla para continuar");
    Console.ReadKey();
}

static void AdicionarTarefa(ServicoDeTarefas servico)
{
    Console.WriteLine("Adicionar Tarefa");
    var adicionar = servico.Adicionar(Entrada.LerTexto("Digite o nome da tarefa: "));
    Console.WriteLine(adicionar.IsSuccess ? "Tarefa adicionada com sucesso." : adicionar.Error.Description);
}

static void RemoverTarefa(ServicoDeTarefas servico)
{
    Console.WriteLine("Remover Tarefa");
    if (!TemTarefas(servico))
    {
        Console.WriteLine("Nenhuma tarefa cadastrada.");
        return;
    }
    ListarTarefas(servico.ObterTodas());
    Console.WriteLine("Digite o número da tarefa que gostaria de remover: ");
    int.TryParse(Console.ReadLine(), out int numeroRemover);
    var remover = servico.Remover(numeroRemover);
    Console.WriteLine(remover.IsSuccess ? "Tarefa removida com sucesso." : remover.Error.Description);
}

static void ConcluirTarefa(ServicoDeTarefas servico)
{
    Console.WriteLine("Concluir a tarefa");
    if (!TemTarefas(servico))
    {
        Console.WriteLine("Nenhuma tarefa cadastrada.");
        return;
    }
    ListarTarefas(servico.ObterTodas());
    Console.Write("Digite o número da tarefa que deseja marcar como concluída");
    int.TryParse(Console.ReadLine(), out int numeroConcluir);
    var concluir = servico.Concluir(numeroConcluir);
    Console.WriteLine((concluir.IsSuccess ? "Tarefa comcluida com sucesso." : concluir.Error.Description));
}

static void ListarTodasTarefas(ServicoDeTarefas servico)
{
    Console.WriteLine("Lista de todas as tarefas");
    if (!TemTarefas(servico))
    {
        Console.WriteLine("Nenhuma tarefa cadastrada.");
        return;
    }
    ListarTarefas(servico.ObterTodas());
}

static bool TemTarefas(ServicoDeTarefas servico) => servico.ObterTodas().Count > 0;