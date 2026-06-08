using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace S3A___GS.Utils
{
    public class BancoDados
    {
        private readonly string _connectionString;

        public BancoDados()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;" +
                                "Initial Catalog=S3A_Missao;" +
                                "Integrated Security=True;" +
                                "Connect Timeout=30;" +
                                "Encrypt=True;" +
                                "Trust Server Certificate=False;" +
                                "Application Intent=ReadWrite;" +
                                "Multi Subnet Failover=False;" +
                                "Command Timeout=30";
            InicializarTabelas();
        }

        private void InicializarTabelas()
        {
            using var conexao = new SqlConnection(_connectionString);
            conexao.Open();

            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Leituras')
                CREATE TABLE Leituras (
                    Id          INT IDENTITY(1,1) PRIMARY KEY,
                    DroidId     NVARCHAR(50)  NOT NULL,
                    Tipo        NVARCHAR(50)  NOT NULL,
                    Resumo      NVARCHAR(500) NOT NULL,
                    Timestamp   DATETIME      NOT NULL
                );

                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Alertas')
                CREATE TABLE Alertas (
                    Id          INT IDENTITY(1,1) PRIMARY KEY,
                    DroidId     NVARCHAR(50)  NOT NULL,
                    Nivel       NVARCHAR(20)  NOT NULL,
                    Descricao   NVARCHAR(500) NOT NULL,
                    Gerado      DATETIME      NOT NULL
                );
            ";
            comando.ExecuteNonQuery();

            Console.WriteLine("[DB] Banco de dados inicializado.");
        }
     

    public void SalvarLeitura(string droidId, string tipo, string resumo, DateTime timestamp)
        {
            using var conexao = new SqlConnection(_connectionString);
            conexao.Open();

            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                INSERT INTO Leituras (DroidId, Tipo, Resumo, Timestamp)
                VALUES (@droidId, @tipo, @resumo, @timestamp)
            ";
            comando.Parameters.AddWithValue("@droidId", droidId);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@resumo", resumo);
            comando.Parameters.AddWithValue("@timestamp", timestamp);

            comando.ExecuteNonQuery();
        }

        public void SalvarAlerta(string droidId, string nivel, string descricao, DateTime gerado)
        {
            using var conexao = new SqlConnection(_connectionString);
            conexao.Open();

            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                INSERT INTO Alertas (DroidId, Nivel, Descricao, Gerado)
                VALUES (@droidId, @nivel, @descricao, @gerado)
            ";
            comando.Parameters.AddWithValue("@droidId", droidId);
            comando.Parameters.AddWithValue("@nivel", nivel);
            comando.Parameters.AddWithValue("@descricao", descricao);
            comando.Parameters.AddWithValue("@gerado", gerado);

            comando.ExecuteNonQuery();
        }

        public void ExibirLeiturasSalvas()
        {
            using var conexao = new SqlConnection(_connectionString);
            conexao.Open();

            var comando = conexao.CreateCommand();
            comando.CommandText = "SELECT DroidId, Tipo, Resumo, Timestamp FROM Leituras";

            using var leitor = comando.ExecuteReader();
            Console.WriteLine("\n[DB] Leituras salvas no banco:");
            while (leitor.Read())
            {
                Console.WriteLine($"  {leitor["Timestamp"]} | {leitor["DroidId"]} | {leitor["Resumo"]}");
            }
        }
    } 
}

