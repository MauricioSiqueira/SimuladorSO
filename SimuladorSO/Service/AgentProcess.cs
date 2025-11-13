using SimuladorSO.Enums;

namespace SimuladorSO.Service
{
    public class AgentProcess
    {
        private readonly List<Processo> _processos = new();
        private readonly Random _rand = new();
        private const int Quantum = 2;

        public void CriarProcesso(string nome)
        {
            int tempoCpu = _rand.Next(2, 8);
            int memoria = _rand.Next(50, 150);
            int prioridade = _rand.Next(1, 6);

            var p = new Processo(nome, tempoCpu, memoria, prioridade);
            _processos.Add(p);

            Console.WriteLine($"[+] Processo criado: {p.Nome} (PID {p.PID})");
        }

        public void Listar()
        {
            Console.WriteLine("\nPID | Nome       | CPU | MEM | PRIO | Estado");
            foreach (var p in _processos)
                Console.WriteLine(p);
        }

        public void Bloquear(int pid) => AlterarEstado(pid, EnumProcessState.Bloqueado, "[⛔] Bloqueado");
        public void Desbloquear(int pid) => AlterarEstado(pid, EnumProcessState.Pronto, "[↩] Desbloqueado");
        public void Matar(int pid) => AlterarEstado(pid, EnumProcessState.Finalizado, "[✖] Finalizado");

        private void AlterarEstado(int pid, EnumProcessState novoEstado, string msg)
        {
            var p = _processos.FirstOrDefault(x => x.PID == pid);
            if (p == null)
            {
                Console.WriteLine($"[!] PID {pid} não encontrado.");
                return;
            }

            p.Estado = novoEstado;
            Console.WriteLine($"{msg}: {p.Nome} (PID {p.PID})");
        }

        public void ExecutarEscalonador(string algoritmo)
        {
            var prontos = _processos.Where(p => p.Estado == EnumProcessState.Pronto).ToList();

            if (!prontos.Any())
            {
                Console.WriteLine("[!] Nenhum processo pronto para execução.");
                return;
            }

            switch (algoritmo.ToLower())
            {
                case "fifo":
                    ExecutarFIFO(prontos);
                    break;
                case "sjf":
                    ExecutarSJF(prontos);
                    break;
                case "rr":
                    ExecutarRoundRobin(prontos);
                    break;
                case "prio":
                    ExecutarPrioridade(prontos);
                    break;
                default:
                    Console.WriteLine("[!] Algoritmo inválido. Use: fifo | sjf | rr | prio");
                    break;
            }
        }

        private void ExecutarFIFO(List<Processo> fila)
        {
            Console.WriteLine("\nExecutando FIFO...");
            foreach (var p in fila)
                ExecutarProcesso(p);
        }

        private void ExecutarSJF(List<Processo> fila)
        {
            Console.WriteLine("\nExecutando SJF...");
            foreach (var p in fila.OrderBy(p => p.TempoCpuRestante))
                ExecutarProcesso(p);
        }

        private void ExecutarPrioridade(List<Processo> fila)
        {
            Console.WriteLine("\nExecutando por Prioridade...");
            foreach (var p in fila.OrderBy(p => p.Prioridade))
                ExecutarProcesso(p);
        }

        private void ExecutarRoundRobin(List<Processo> fila)
        {
            Console.WriteLine($"\nExecutando Round Robin (quantum = {Quantum})...");
            var queue = new Queue<Processo>(fila);

            while (queue.Count > 0)
            {
                var p = queue.Dequeue();
                if (p.Estado == EnumProcessState.Finalizado)
                    continue;

                p.Estado = EnumProcessState.Executando;
                Console.WriteLine($"\n-> Executando {p.Nome} (PID {p.PID}) - CPU restante: {p.TempoCpuRestante}");

                int ciclos = Math.Min(Quantum, p.TempoCpuRestante);
                for (int i = 0; i < ciclos; i++)
                {
                    ExecutarCiclo(p);
                    MostrarEstados();
                }

                if (p.TempoCpuRestante > 0)
                {
                    p.Estado = EnumProcessState.Pronto;
                    queue.Enqueue(p); // volta ao final da fila
                    Console.WriteLine($"[⏸] Processo {p.Nome} pausado com {p.TempoCpuRestante} restantes.");
                }
                else
                {
                    p.Estado = EnumProcessState.Finalizado;
                    Console.WriteLine($"✓ Processo {p.PID} finalizado!");
                }
            }
        }

        private void ExecutarProcesso(Processo p)
        {
            p.Estado = EnumProcessState.Executando;
            Console.WriteLine($"\n-> Executando {p.Nome} (PID {p.PID}) - CPU restante: {p.TempoCpuRestante}");

            while (p.TempoCpuRestante > 0)
            {
                ExecutarCiclo(p);
                MostrarEstados();
            }

            p.Estado = EnumProcessState.Finalizado;
            Console.WriteLine($"✓ Processo {p.PID} finalizado!");
        }

        private void ExecutarCiclo(Processo p)
        {
            p.TempoCpuRestante--;
            Thread.Sleep(250); // simula tempo de CPU
        }

        private void MostrarEstados()
        {
            Console.WriteLine("\nPID | Nome       | CPU | MEM | PRIO | Estado");
            foreach (var p in _processos)
                Console.WriteLine(p);
        }
    }
}