using S3A___GS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace S3A___GS.Intrerfaces
{
    public interface ITransmissor
    {
        void Transmitir(string destino);
    }

    public interface IAnalisador
    {
        List<Alerta> Analisar(string droidId, IReadOnlyList<LeituraBase> leituras);
        double EstimarProbabilidadeAquifero(IReadOnlyList<LeituraBase> leituras);
    }
}
