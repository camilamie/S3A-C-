using S3A___GS.Exceptions;
using S3A___GS.Intrerfaces;
using S3A___GS.Models;
using S3A___GS.Services;
using S3A___GS.Utils;



try
{

    var db = new BancoDados();

    var droid = new MiniDroid("S3A-001");
    droid.AdicionarSensor(new SensorSismico("SIS-001"));
    droid.AdicionarSensor(new SensorOptico("OPT-001"));

    droid.Fincar(new CoordenadaPlanetaria(3.0, 154.0));




    var leituras = droid.ColetarDados();
    foreach (var l in leituras)
        Console.WriteLine(l.Resumo());
    foreach (var l in leituras)
    {
        Console.WriteLine(l.Resumo());
        db.SalvarLeitura(droid.Id, l.GetType().Name, l.Resumo(), l.Timestamp);
    }


    IAnalisador analisador = new AnalisadorGeofisico();
    var alertas = analisador.Analisar(droid.Id, leituras);
    foreach (var a in alertas)
        Console.WriteLine(a);
    foreach (var a in alertas)
    {
        Console.WriteLine(a);
        db.SalvarAlerta(a.DroidId, a.Nivel.ToString(), a.Descricao, a.Gerado);
    }

    
    droid.Transmitir("DSN-Terra");

    db.ExibirLeiturasSalvas();
}
catch (SensorInativoException ex)
{
    Console.WriteLine($"[ERRO SENSOR] {ex.Message}");
}
catch (DroidNaoOperacionalException ex)
{
    Console.WriteLine($"[ERRO DROID] {ex.Message}");
}
catch (S3AException ex)
{
    Console.WriteLine($"[ERRO S3A] {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"[ERRO CRÍTICO] {ex.Message}");
}



Console.ReadKey();