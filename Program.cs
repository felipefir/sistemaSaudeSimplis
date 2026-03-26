using System;
using System.Collections.Generic;
using System.Linq;


namespace desafio1;

public class Program
{    
    static void Main(string[] args)
    {
        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("==== SISTEMA HEALTHCONNECT ====");
            Console.WriteLine("1 - Cadastrar paciente");
            Console.WriteLine("2 - Listar pacientes");
            Console.WriteLine("3 - Triagem e atendimento");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            int.TryParse(Console.ReadLine(), out opcao);

            switch (opcao)
            {
               
                case 2:
                    ListarPacientes();
                    break;
                case 3:
                    AtenderTriagem();
                    break;
                case 0:
                    Console.WriteLine("Encerrando...");
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }

    
    
        
       
        

    // ============================
    // LISTAGEM
    // ============================
    static void ListarPacientes()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE PACIENTES ===\n");

        if (pacientes.Count == 0)
        {
            Console.WriteLine("Nenhum paciente cadastrado.");
        }
        else
        {
            var lista = pacientes
                .OrderByDescending(p => p.Prioridade == "PRIORIDADE ALTA")
                .ThenByDescending(p => p.NivelDor)
                .ThenByDescending(p => p.Idade);

            foreach (var p in lista)
            {
                // CORES
                if (p.Prioridade == "PRIORIDADE ALTA")
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (p.Prioridade == "OBSERVAÇÃO TRIAGEM")
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"Nome: {p.Nome}");
                Console.WriteLine($"Idade: {p.Idade}");
                Console.WriteLine($"Dor: {p.NivelDor}");
                Console.WriteLine($"Prioridade: {p.Prioridade}");
                Console.WriteLine("----------------------");

                Console.ResetColor();
            }
        }

        Console.WriteLine("\nPressione qualquer tecla...");
        Console.ReadKey();
    }

    // ============================
    // TRIAGEM + ATENDIMENTO
    // ============================
    static void AtenderTriagem()
    {
        Console.Clear();
        Console.WriteLine("=== TRIAGEM E ATENDIMENTO ===\n");

        if (pacientes.Count == 0)
        {
            Console.WriteLine("Nenhum paciente na fila.");
            Console.ReadKey();
            return;
        }

        // 🔴 1 VERMELHO
        var vermelhos = pacientes
            .Where(p => p.Prioridade == "PRIORIDADE ALTA")
            .OrderByDescending(p => p.NivelDor)
            .Take(1)
            .ToList();

        // 🟡 2 AMARELOS
        var amarelos = pacientes
            .Where(p => p.Prioridade == "OBSERVAÇÃO TRIAGEM")
            .OrderByDescending(p => p.NivelDor)
            .Take(2)
            .ToList();

        // 🟢 1 VERDE
        var verdes = pacientes
            .Where(p => p.Prioridade == "AGUARDAR TRIAGEM")
            .OrderByDescending(p => p.NivelDor)
            .Take(1)
            .ToList();

        var atendidos = vermelhos.Concat(amarelos).Concat(verdes).ToList();

        if (atendidos.Count == 0)
        {
            Console.WriteLine("Nenhum paciente disponível.");
        }
        else
        {
            Console.WriteLine("=== EM ATENDIMENTO ===\n");

            foreach (var p in atendidos)
            {
                if (p.Prioridade == "PRIORIDADE ALTA")
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (p.Prioridade == "OBSERVAÇÃO TRIAGEM")
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"Nome: {p.Nome}");
                Console.WriteLine($"Idade: {p.Idade}");
                Console.WriteLine($"Dor: {p.NivelDor}");
                Console.WriteLine($"Prioridade: {p.Prioridade}");
                Console.WriteLine("----------------------");

                Console.ResetColor();
            }

            // REMOVE DA FILA
            foreach (var p in atendidos)
            {
                pacientes.Remove(p);
            }

            Console.WriteLine("\nPacientes atendidos e removidos!");
        }

        Console.WriteLine("\nPressione qualquer tecla...");
        Console.ReadKey();
    }
}