using SimuladorSO.Enums;

namespace SimuladorSO.Service
{
    public class Processo
    {
        private static int _contadorPid = 0;

        public int PID { get; }
        public string Nome { get; }
        public int TempoCpuRestante { get; set; }
        public int MemoriaOcupada { get; }
        public int Prioridade { get; }
        public EnumProcessState Estado { get; set; }

        public Processo(string nome, int tempoCpu, int memoria, int prioridade)
        {
            PID = ++_contadorPid;
            Nome = nome;
            TempoCpuRestante = tempoCpu;
            MemoriaOcupada = memoria;
            Prioridade = Math.Clamp(prioridade, 1, 5);
            Estado = EnumProcessState.Pronto;
        }

        public override string ToString()
        {
            return $"{PID,-3} | {Nome,-10} | {TempoCpuRestante,-3} | {MemoriaOcupada,-3} | {Prioridade,-4} | {Estado}";
        }
    }
}