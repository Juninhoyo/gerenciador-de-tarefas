namespace GerenciadorDeTarefas.Utils;

public class Entrada
{
    public static string LerTexto(string menssagem)
    {
        while (true)
        {
            Console.WriteLine(menssagem);
            string? entrada = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(entrada))
            {
                return entrada.Trim();
            }
            Console.WriteLine("Entrada inválida");
        }
    }
}