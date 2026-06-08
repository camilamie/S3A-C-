using System;
using System.Collections.Generic;
using System.Text;

namespace S3A___GS.Exceptions
{
   
    public class S3AException : Exception
    {
        public S3AException(string message) : base(message) { }
    }

    
    public class SensorInativoException : S3AException
    {
        public SensorInativoException(string id, string nome)
            : base($"Sensor '{nome}' (ID: {id}) está inativo. Ative-o antes de coletar.") { }
    }

   
    public class DroidNaoOperacionalException : S3AException
    {
        public DroidNaoOperacionalException(string id, string status)
            : base($"Droid {id} não está operacional. Status: {status}.") { }
    }

   
    public class EnergiaInsuficienteException : S3AException
    {
        public EnergiaInsuficienteException(string id)
            : base($"Droid {id} sem energia suficiente.") { }
    }
}
