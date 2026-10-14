using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Utils;

List<Tarefa> tarefas = new List<Tarefa>();

string opcao, titulo;
int proximoId, numeroRemoverTarefa, numeroConcluirTarefa;


do
{
    Console.Clear();
    ExibirMenu();
    opcao = Entrada.LerTexto("Digite a opção que gostaria de continuar");

    switch (opcao)
    {
        case "1":
            titulo = Entrada.LerTexto("Digite o nome da tarefa a ser adicionada: ");
            proximoId = tarefas.Any() ? tarefas.Max(t => t.Id) + 1 : 1;
            tarefas.Add(new Tarefa(titulo, proximoId));
            Console.WriteLine("Tarefa adicionada com sucesso. Pressione uma tecla para continuar");
            Console.ReadKey();
            break;

        case "2":
            ListarTarefas(tarefas);
            Console.WriteLine("Digite o número da tarefa que gostaria de remover: ");

            if (int.TryParse(Console.ReadLine(), out numeroRemoverTarefa) && numeroRemoverTarefa >= 1 &&
                numeroRemoverTarefa <= tarefas.Count)
            {
                tarefas.RemoveAt(numeroRemoverTarefa - 1);
                Console.WriteLine("Tarefa removida com sucesso. Pressione uma tecla para continuar");
            }
            else
            {
                Console.WriteLine("Número da tarefa está inválido. Pressione uma tecla para continuar");
            }

            Console.ReadKey();
            break;

        case "3":
            ListarTarefas(tarefas);
            Console.WriteLine("Digite o número da tarefa que deseja marcar como concluída");
            if (int.TryParse(Console.ReadLine(), out numeroConcluirTarefa) && numeroConcluirTarefa >= 1 &&
                numeroConcluirTarefa <= tarefas.Count)
            {
                tarefas[numeroConcluirTarefa - 1].Concluir();
                Console.WriteLine("Tarefa concluída com sucesso. Pressione uma tecla para continuar");
            }
            else
            {
                Console.WriteLine("Número da tarefa está inválido. Pressione uma tecla para continuar");
            }
            Console.ReadKey();
            break;

        case "4":
            Console.WriteLine("Lista de tarefas");
            if (tarefas.Count == 0)
            {
                Console.WriteLine("Nenhuma tarefa foi adicionada para ser listada");
            }
            else
            {
                ListarTarefas(tarefas);
            }
            Console.WriteLine("Pressione uma tecla para continuar");
            Console.ReadKey();
            break;

        case "0":
            Console.WriteLine("Saindo do programa");
            break;

        default:
            Console.WriteLine("Opção inválida");
            break;
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