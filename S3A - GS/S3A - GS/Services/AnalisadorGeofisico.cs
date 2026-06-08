using S3A___GS.Intrerfaces;
using S3A___GS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace S3A___GS.Services
{
    public class AnalisadorGeofisico : IAnalisador
    {
        public List<Alerta> Analisar(string droidId, IReadOnlyList<LeituraBase> leituras)
        {
            var alertas = new List<Alerta>();

            foreach (var leitura in leituras)
            {
                if (leitura is LeituraSismica sis && sis.DetectouCavidade)
                    alertas.Add(new Alerta(droidId, NivelAlerta.Critico,
                        $"Cavidade provável a {sis.Profundidade:F1} km"));
            }

            return alertas;
        }

        public double EstimarProbabilidadeAquifero(IReadOnlyList<LeituraBase> leituras)
        {
            
            int positivos = 0;
            foreach (var l in leituras)
                if (l is LeituraSismica s && s.DetectouCavidade) positivos++;

            return leituras.Count > 0 ? Math.Round((double)positivos / leituras.Count, 2) : 0;
        }
    }
}
