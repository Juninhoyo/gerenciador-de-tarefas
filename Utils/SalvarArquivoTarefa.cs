using GerenciadorDeTarefas.Models;
using Newtonsoft.Json;

namespace GerenciadorDeTarefas.Utils;

public class SalvarArquivoTarefa
{
    private readonly string _arquivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GerenciadorDeTarefas", "tarefasSalvas.json");

    public void SalvarArquivo(IEnumerable<Tarefa> tarefas)
    {
        string? pasta = Path.GetDirectoryName(_arquivo);
        if (!string.IsNullOrEmpty(pasta))
        {
            Directory.CreateDirectory(pasta);
        }
        string json = JsonConvert.SerializeObject(tarefas, Formatting.Indented);
        File.WriteAllText(_arquivo, json);
    }

    public List<Tarefa> CarregarArquivo()
    {
        if (!File.Exists(_arquivo))
        {
            return new List<Tarefa>();
        }

        string json = File.ReadAllText(_arquivo);
        return JsonConvert.DeserializeObject<List<Tarefa>>(json) ?? new List<Tarefa>();
    }
}