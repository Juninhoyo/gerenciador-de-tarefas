namespace GerenciadorDeTarefas.Models;

public class Tarefa
{
    public string Titulo { get; private set; }
    public bool Concluida { get; private set; }


    public Tarefa(string titulo, bool concluida = false)
    {
        ValidarTitulo(titulo);

        Titulo = titulo.Trim();
        Concluida = concluida;
    }

    public void Concluir()
    {
        Concluida = true;
    }

    private static void ValidarTitulo(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
        {
            throw new ArgumentException("O título não pode estar vazio");
        }
    }
}