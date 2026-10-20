namespace GerenciadorDeTarefas.Exceptions;

public class ArquivoCorrompidoException : Exception
{
    public ArquivoCorrompidoException(string mensagem) : base(mensagem)
    {
        
    }

    public ArquivoCorrompidoException(string mensagem, Exception inner) : base(mensagem, inner)
    {
        
    }
}