namespace GerenciadorDeTarefas.Exceptions;

public class ArquivoInacessivelException : Exception
{
    public ArquivoInacessivelException(string mensagem) : base(mensagem)
    {
        
    }

    public ArquivoInacessivelException(string mensagem, Exception inner) : base(mensagem, inner)
    {
        
    }
}