using GerenciadorDeTarefas.Exceptions;
using GerenciadorDeTarefas.Models;
using Newtonsoft.Json;

namespace GerenciadorDeTarefas.Utils;

public class SalvarArquivoTarefa
{
    private readonly string _arquivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GerenciadorDeTarefas", "tarefasSalvas.json");

    public void SalvarArquivo(IEnumerable<Tarefa> tarefas)
    {
        try
        {
            string? pasta = Path.GetDirectoryName(_arquivo);
            if (!string.IsNullOrEmpty(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            string json = JsonConvert.SerializeObject(tarefas, Formatting.Indented);
            File.WriteAllText(_arquivo, json);
        }
        catch (Exception ex) when(ex is IOException || ex is UnauthorizedAccessException)
        {
            throw new ArquivoInacessivelException("Não foi possivel salvar as tarefas no arquivo.", ex);
        }
    }

    public List<Tarefa> CarregarArquivo()
    {
        try
        {
            if (!File.Exists(_arquivo))
            {
                return new List<Tarefa>();
            }

            string json = File.ReadAllText(_arquivo);
            return JsonConvert.DeserializeObject<List<Tarefa>>(json) ?? new List<Tarefa>();
        }
        catch (JsonException ex)
        {
            throw new ArquivoCorrompidoException("O arquivo de tarefas está corrompido.", ex);
        }
        catch (Exception ex) when(ex is IOException || ex is UnauthorizedAccessException)
        {
            throw new ArquivoInacessivelException("Não foi possivel salvar as tarefas no arquivo.", ex);
        }
    }
}