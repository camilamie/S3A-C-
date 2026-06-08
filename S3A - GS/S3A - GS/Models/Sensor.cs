using S3A___GS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace S3A___GS.Models { 

public abstract class Sensor
{
    public string Id { get; protected set; }
    public string Nome { get; protected set; }
    public bool Ativo { get; protected set; }
    public DateTime UltimaLeitura { get; protected set; }

    protected Sensor(string id, string nome)
    {
        Id = id;
        Nome = nome;
        Ativo = false;
    }

    public abstract LeituraBase Coletar();

    public virtual void Ativar()
    {
        Ativo = true;
        Console.WriteLine($"Sensor {Nome} ativado.");
    }
}}