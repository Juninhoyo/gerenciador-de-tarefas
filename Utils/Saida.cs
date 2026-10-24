namespace GerenciadorDeTarefas.Utils;

public class Saida
{
    public static void DestacarMensagem(string mensagem, ConsoleColor fg)
    {
        Console.ForegroundColor = fg;
        try
        {
            Console.WriteLine(mensagem);
        }
        finally
        {
            Console.ResetColor();    
        }
    }
    
    public static void Sucesso(string mensagem) => DestacarMensagem(mensagem, ConsoleColor.Green);
    
    public static void Erro(string mensagem) => DestacarMensagem(mensagem, ConsoleColor.Red);
    
    public static void Aviso(string mensagem) => DestacarMensagem(mensagem, ConsoleColor.Yellow);
}