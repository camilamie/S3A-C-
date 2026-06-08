using System;
using System.Collections.Generic;
using System.Text;

namespace S3A___GS.Models
{
    public abstract class LeituraBase
    {
        public string SensorId { get; set; }
        public DateTime Timestamp { get; set; }

        protected LeituraBase(string sensorId)
        {
            SensorId = sensorId;
            Timestamp = DateTime.UtcNow; 
        }

        public abstract string Resumo();
    }
}
