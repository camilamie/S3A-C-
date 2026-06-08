using System;
using System.Collections.Generic;
using System.Text;
using S3A___GS.Exceptions;

namespace S3A___GS.Models
{
    public class LeituraSismica : LeituraBase
    {
        public double Amplitude { get; set; }    
        public double Frequencia { get; set; }   
        public double Profundidade { get; set; } 
        public bool DetectouCavidade { get; set; }

        public LeituraSismica(string sensorId, double amp, double freq, double prof)
            : base(sensorId)
        {
            Amplitude = amp;
            Frequencia = freq;
            Profundidade = prof;
            DetectouCavidade = amp > 10.0;
        }

        public override string Resumo() =>
            $"Amp: {Amplitude:F2} nm/s | Freq: {Frequencia:F3} Hz | " +
            $"Cavidade: {(DetectouCavidade ? "PROVÁVEL" : "Não")}";
    }

    public class SensorSismico : Sensor
    {
        private readonly Random _rng = new Random();

        public SensorSismico(string id) : base(id, "Sismômetro") { }

        public override LeituraBase Coletar()
        {
            if (!Ativo)
                throw new SensorInativoException(Id, Nome); 

            double amp = _rng.NextDouble() * 120.0;
            double freq = 0.5 + _rng.NextDouble() * 4.5;
            double prof = 0.1 + _rng.NextDouble() * 2.4;

            UltimaLeitura = DateTime.UtcNow;
            return new LeituraSismica(Id, amp, freq, prof);
        }
    }
}
