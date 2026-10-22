namespace GerenciadorDeTarefas.Utils;

public class Entrada
{
    public static string LerTexto(string mensagem)
    {
        while (true)
        {
            Console.WriteLine(mensagem);
            string? entrada = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(entrada))
            {
                return entrada.Trim();
            }
            Console.WriteLine("Entrada inválida");
        }
    }

    public static bool LerConfirmacao(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            ConsoleKeyInfo tecla = Console.ReadKey();
            Console.WriteLine("\n");
            if (tecla.Key == ConsoleKey.S)
            {
                return true;
            }
            if (tecla.Key == ConsoleKey.N)
            {
                return false;
            }
            Console.WriteLine("Resposta inválida. Digite S ou N.");
        }
    }
}