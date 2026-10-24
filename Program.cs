using GerenciadorDeTarefas.Exceptions;
using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Services;
using GerenciadorDeTarefas.Utils;

ServicoDeTarefas servico;

try
{
    servico = new ServicoDeTarefas(new SalvarArquivoTarefa());
}
catch (ArquivoCorrompidoException ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
    Console.WriteLine("O programa sera encerrado.");
    Pausar();
    return;
}

string opcao = "";

do
{
    try
    {
        Console.Clear();
        ExibirMenu(servico);
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
                ReabrirTarefa(servico);
                Pausar();
                break;
            
            case "5":
                Console.Clear();
                EditarTarefa(servico);
                Pausar();
                break;

            case "6":
                Console.Clear();
                ListarTodasTarefas(servico);
                Pausar();
                break;
            
            case "7":
                Console.Clear();
                RemoverTodasConcluidas(servico);
                Pausar();
                break;

            case "0":
                Console.WriteLine("Saindo do programa.");
                break;

            default:
                Console.WriteLine("Opção inválida.");
                Pausar();
                break;
        }
    }
    catch (ArquivoInacessivelException ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
        Pausar();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro inesperado {ex.Message}");
        Pausar();
    }
} while (opcao != "0");
    
static void ExibirMenu(ServicoDeTarefas servico)
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

static void Pausar()
{
    Console.WriteLine("Pressione uma tecla para continuar.");
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
    Console.Write("Digite o número da tarefa que gostaria de remover: ");
    int.TryParse(Console.ReadLine(), out int numeroRemover);
    if (!Entrada.LerConfirmacao("Tem certeza que deseja remover? (S/N): "))
    {
        Console.WriteLine("Remoção cancelada.");
        return;
    }
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
    Console.Write("Digite o número da tarefa que deseja marcar como concluída: ");
    int.TryParse(Console.ReadLine(), out int numeroConcluir);
    var concluir = servico.Concluir(numeroConcluir);
    Console.WriteLine((concluir.IsSuccess ? "Tarefa comcluida com sucesso." : concluir.Error.Description));
}

static void ReabrirTarefa(ServicoDeTarefas servico)
{
    Console.WriteLine("Desconcluir a tarefa");
    if (!TemTarefas(servico))
    {
        Console.WriteLine("Nenhuma tarefa cadastrada.");
        return;
    }
    ListarTarefas(servico.ObterTodas());
    Console.Write("Digite o número da tarefa que deseja reabrir: ");
    int.TryParse(Console.ReadLine(), out int numeroReabrir);
    var reabrir = servico.Reabrir(numeroReabrir);
    Console.WriteLine((reabrir.IsSuccess ? "Tarefa reaberta com sucesso." : reabrir.Error.Description));
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

static void EditarTarefa(ServicoDeTarefas servico)
{
    Console.WriteLine("Editar Tarefa");
    if (!TemTarefas((servico)))
    {   
        Console.WriteLine("Nenhuma tarefa cadastrada.");
        return;
    }
    ListarTarefas(servico.ObterTodas());
    Console.Write("Digite o número da tarefa que deseja editar o titulo: ");
    int.TryParse(Console.ReadLine(), out int numeroEditar);
    var validacao = servico.ValidarNumero(numeroEditar);
    if (validacao.IsFailure)
    {
        Console.WriteLine(validacao.Error.Description);
        return;
    }
    string novoTitulo = Entrada.LerTexto("Digite o novo titulo: ");
    var editar = servico.Editar(numeroEditar, novoTitulo);
    Console.WriteLine((editar.IsSuccess ? "Tarefa editada com sucesso. " : editar.Error.Description));
}

static void RemoverTodasConcluidas(ServicoDeTarefas servico)
{
    Console.WriteLine("Remover tarefas concluídas");
    if (!TemTarefas(servico))
    {
        Console.WriteLine("Nenhuma tarefa cadastrada.");
        return;
    }

    if (!Entrada.LerConfirmacao("Tem certeza que deseja remover? (S/N): "))
    {
        Console.WriteLine("Remoção cancelada.");
        return;
    }

    var remover = servico.RemoverConcluidas();
    Console.WriteLine(remover.IsSuccess ? $"{remover.Value} Tarefa(s) concluída(s) removida(s) com sucesso. " : remover.Error.Description);
}