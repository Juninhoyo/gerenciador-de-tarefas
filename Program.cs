using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Utils;
using Newtonsoft.Json;


SalvarArquivoTarefa arquivoTarefa = new SalvarArquivoTarefa();
List<Tarefa> tarefas = arquivoTarefa.CarregarArquivo();

string opcao, titulo;
int numeroRemoverTarefa, numeroConcluirTarefa;

opcao = "";

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
                Console.WriteLine("Adicionar Tarefa");
                titulo = Entrada.LerTexto("Digite o nome da tarefa a ser adicionada: ");
                tarefas.Add(new Tarefa(titulo));
                arquivoTarefa.SalvarArquivo(tarefas);
                Console.WriteLine("Tarefa adicionada com sucesso.");
                Pausar();
                break;

            case "2":
                Console.Clear();
                Console.WriteLine("Remover Tarefa");
                ListarTarefas(tarefas);
                Console.WriteLine("Digite o número da tarefa que gostaria de remover: ");

                if (int.TryParse(Console.ReadLine(), out numeroRemoverTarefa) && numeroRemoverTarefa >= 1 &&
                    numeroRemoverTarefa <= tarefas.Count)
                {
                    tarefas.RemoveAt(numeroRemoverTarefa - 1);
                    arquivoTarefa.SalvarArquivo(tarefas);
                    Console.WriteLine("Tarefa removida com sucesso.");
                }
                else
                {
                    Console.WriteLine("Número da tarefa está inválido.");
                }
                Pausar();
                break;

            case "3":
                Console.Clear();
                Console.WriteLine("Concluir a tarefa");
                ListarTarefas(tarefas);
                Console.WriteLine("Digite o número da tarefa que deseja marcar como concluída");
                if (int.TryParse(Console.ReadLine(), out numeroConcluirTarefa) && numeroConcluirTarefa >= 1 &&
                    numeroConcluirTarefa <= tarefas.Count)
                {
                    tarefas[numeroConcluirTarefa - 1].Concluir();
                    arquivoTarefa.SalvarArquivo(tarefas);
                    Console.WriteLine("Tarefa concluída com sucesso.");
                }
                else
                {
                    Console.WriteLine("Número da tarefa está inválido.");
                }
                Pausar();
                break;

            case "4":
                Console.Clear();
                Console.WriteLine("Lista de tarefas");
                if (tarefas.Count == 0)
                {
                    Console.WriteLine("Nenhuma tarefa foi adicionada para ser listada");
                }
                else
                {
                    ListarTarefas(tarefas);
                }
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

static void ListarTarefas(List<Tarefa> tarefas)
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