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
}