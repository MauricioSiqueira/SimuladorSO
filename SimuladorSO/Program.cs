using SimuladorSO.Service;

namespace SimuladorSO.Program;

public class Program
{
    static void Main()
    {
        var so = new AgentProcess();
        Console.WriteLine("=== Simulador de Sistema Operacional ===");
        Console.WriteLine("Comandos: create <nome>, list, run <fifo|sjf|rr|prio>, block <PID>, unblock <PID>, kill <PID>, exit");

        while (true)
        {
            Console.Write("\nSO> ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;

            var partes = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var comando = partes[0].ToLower();

            try
            {
                switch (comando)
                {
                    case "create":
                        if (partes.Length < 2)
                            Console.WriteLine("Uso: create <nome>");
                        else
                            so.CriarProcesso(partes[1]);
                        break;

                    case "list":
                        so.Listar();
                        break;

                    case "run":
                        if (partes.Length < 2)
                            Console.WriteLine("Uso: run <fifo|sjf|rr|prio>");
                        else
                            so.ExecutarEscalonador(partes[1]);
                        break;

                    case "block":
                        if (partes.Length < 2 || !int.TryParse(partes[1], out var pidB))
                            Console.WriteLine("Uso: block <PID>");
                        else
                            so.Bloquear(pidB);
                        break;

                    case "unblock":
                        if (partes.Length < 2 || !int.TryParse(partes[1], out var pidU))
                            Console.WriteLine("Uso: unblock <PID>");
                        else
                            so.Desbloquear(pidU);
                        break;

                    case "kill":
                        if (partes.Length < 2 || !int.TryParse(partes[1], out var pidK))
                            Console.WriteLine("Uso: kill <PID>");
                        else
                            so.Matar(pidK);
                        break;

                    case "exit":
                        Console.WriteLine("Encerrando sistema...");
                        return;

                    default:
                        Console.WriteLine("[!] Comando inválido.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Erro] {ex.Message}");
            }
        }
    }
}
