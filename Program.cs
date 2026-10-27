using GerenciadorDeTarefas.Exceptions;
using GerenciadorDeTarefas.Services;
using GerenciadorDeTarefas.Utils;
using GerenciadorDeTarefas.UI;

ServicoDeTarefas servico;

try
{
    servico = new ServicoDeTarefas(new SalvarArquivoTarefa());
}
catch (ArquivoCorrompidoException ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
    Console.WriteLine("O programa sera encerrado.");
    Menu.Pausar();
    return;
}

string opcao = "";

do
{
    try
    {
        Console.Clear();
        Menu.ExibirMenu(servico);
        opcao = Entrada.LerTexto("Digite a opção que gostaria de continuar: ");

        switch (opcao)
        {
            case "1":
                Console.Clear();
                Menu.AdicionarTarefa(servico);
                Menu.Pausar();
                break;

            case "2":
                Console.Clear();
                Menu.RemoverTarefa(servico);
                Menu.Pausar();
                break;

            case "3":
                Console.Clear();
                Menu.ConcluirTarefa(servico);
                Menu.Pausar();
                break;
            
            case "4":
                Console.Clear();
                Menu.ReabrirTarefa(servico);
                Menu.Pausar();
                break;
            
            case "5":
                Console.Clear();
                Menu.EditarTarefa(servico);
                Menu.Pausar();
                break;

            case "6":
                Console.Clear();
                Menu.ListarTodasTarefas(servico);
                Menu.Pausar();
                break;
            
            case "7":
                Console.Clear();
                Menu.RemoverTodasConcluidas(servico);
                Menu.Pausar();
                break;
            
            case "8":
                Console.Clear();
                Menu.OrdenarTarefas(servico);
                Menu.Pausar();
                    break;

            case "0":
                Console.WriteLine("Saindo do programa.");
                break;

            default:
                Console.WriteLine("Opção inválida.");
                Menu.Pausar();
                break;
        }
    }
    catch (ArquivoInacessivelException ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
        Menu.Pausar();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro inesperado {ex.Message}");
        Menu.Pausar();
    }
} while (opcao != "0");
    
