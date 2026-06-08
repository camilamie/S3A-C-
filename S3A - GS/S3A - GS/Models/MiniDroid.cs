using S3A___GS.Intrerfaces;
using System;
using System.Collections.Generic;
using System.Text;
using S3A___GS.Exceptions;

namespace S3A___GS.Models
{
    
    public enum StatusDroid  // ← adicione isso
    {
     EmTransito,
     Disperso,
     Fincado,
     Operacional,
     Falha,
     SemEnergia
     }

        
    
    public struct CoordenadaPlanetaria
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public CoordenadaPlanetaria(double lat, double lon)
        {
            if (lat < -90 || lat > 90)
                throw new ArgumentOutOfRangeException(nameof(lat));
            Latitude = lat;
            Longitude = lon;
        }

        public override string ToString() => $"({Latitude:F4}°, {Longitude:F4}°)";
    }

    public class MiniDroid : ITransmissor
    {
        public string Id { get; private set; }
        public StatusDroid Status { get; private set; } 
        public double NivelEnergia { get; private set; }
        public CoordenadaPlanetaria? Posicao { get; private set; }
        public DateTime? MomentoFincagem { get; private set; }

        private readonly List<Sensor> _sensores = new();
        private readonly List<LeituraBase> _historico = new();
        public IReadOnlyList<LeituraBase> Historico => _historico.AsReadOnly();

        public MiniDroid(string id)
        {
            Id = id;
            Status = StatusDroid.EmTransito;
            NivelEnergia = 100.0;
        }

        public void AdicionarSensor(Sensor s) => _sensores.Add(s);

        public void Fincar(CoordenadaPlanetaria pos)
        {
            Posicao = pos;
            MomentoFincagem = DateTime.UtcNow; 
            Status = StatusDroid.Operacional;
            foreach (var s in _sensores) s.Ativar();
        }

        public List<LeituraBase> ColetarDados()
        {
            if (Status != StatusDroid.Operacional)
                throw new DroidNaoOperacionalException(Id, Status.ToString());

            var leituras = new List<LeituraBase>();
            foreach (var sensor in _sensores)
            {
                var leitura = sensor.Coletar();
                _historico.Add(leitura);
                leituras.Add(leitura);
                NivelEnergia -= 0.5;
            }
            return leituras;
        }

        public void Transmitir(string destino) 
        {
            Console.WriteLine($"[TX] Droid {Id} → {destino} | {_historico.Count} leitura(s)");
        }
    }
}
