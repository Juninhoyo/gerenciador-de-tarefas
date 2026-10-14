namespace GerenciadorDeTarefas.Models;

public class Tarefa
{
    public int Id { get; private set; }
    public string Titulo { get; private set; }
    public bool Concluida { get; private set; }


    public Tarefa(string titulo, int id, bool concluida = false)
    {
        ValidarTitulo(titulo);

        Titulo = titulo.Trim();
        Id = id;
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