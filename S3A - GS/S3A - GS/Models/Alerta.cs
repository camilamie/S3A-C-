using System;
using System.Collections.Generic;
using System.Text;

namespace S3A___GS.Models
{
    public enum NivelAlerta { Informativo, Atencao, Critico }

    public struct Alerta
    {
        public string DroidId { get; }
        public NivelAlerta Nivel { get; }
        public string Descricao { get; }
        public DateTime Gerado { get; } 

        public Alerta(string droidId, NivelAlerta nivel, string descricao)
        {
            DroidId = droidId;
            Nivel = nivel;
            Descricao = descricao;
            Gerado = DateTime.UtcNow;
        }

        public override string ToString() =>
            $"[{Nivel}] Droid {DroidId} @ {Gerado:HH:mm:ss} — {Descricao}";
    }
}
