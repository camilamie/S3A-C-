using System;
using System.Collections.Generic;
using System.Text;
using S3A___GS.Exceptions;


namespace S3A___GS.Models
{
        public class LeituraOptica : LeituraBase
        {
            public double IntensidadeReflexao { get; set; } 
            public double AnguloRefracao { get; set; }       
            public string AnaliseMineral { get; set; }
            public bool IndicioDegelo { get; set; }

            public LeituraOptica(string sensorId, double reflexao, double refracao, string mineral)
                : base(sensorId)
            {
                IntensidadeReflexao = reflexao;
                AnguloRefracao = refracao;
                AnaliseMineral = mineral;
                IndicioDegelo = reflexao > 70.0;
            }

            public override string Resumo() =>
                $"Reflexão: {IntensidadeReflexao:F1}% | Refração: {AnguloRefracao:F1}° | " +
                $"Mineral: {AnaliseMineral} | Gelo/Água: {(IndicioDegelo ? "INDÍCIO ⚠" : "Não detectado")}";
        }

        public class SensorOptico : Sensor
        {
            private readonly Random _rng = new Random();

            
            private static readonly string[] Minerais =
            {
            "Basalto", "Anortosita", "Piroxênio",
            "Olivina", "Perclorato", "Óxido de Ferro",
            "Gelo de CO₂", "Gelo de H₂O"
        };

            public SensorOptico(string id) : base(id, "Sensor Óptico") { }

            public override LeituraBase Coletar()
            {
                if (!Ativo)
                    throw new SensorInativoException(Id, Nome);

                double reflexao = _rng.NextDouble() * 100.0;
                double refracao = 10.0 + _rng.NextDouble() * 70.0;
                string mineral = Minerais[_rng.Next(Minerais.Length)];

                UltimaLeitura = DateTime.UtcNow;
                return new LeituraOptica(Id, reflexao, refracao, mineral);
            }
        }
    }

