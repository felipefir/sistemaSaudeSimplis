using System;
using System.Collections.Generic;
using System.Linq;

class Paciente
{
    public string Nome { get; set; } = "";
    public int Idade { get; set; }
    public int NivelDor { get; set; }
    public string Prioridade { get; set; } = "";
}

class Program
{
    static List<Paciente> pacientes = new List<Paciente>();

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
                case 1:
                    CadastrarPaciente();
                    break;
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
    // CADASTRO
    // ============================
    static void CadastrarPaciente()
    {
        string continuar;

        do
        {
            Console.Clear();
            Console.WriteLine("=== CADASTRO DE PACIENTE ===");

            Paciente p = new Paciente();

            Console.Write("Nome: ");
            p.Nome = Console.ReadLine() ?? "";

            Console.Write("Idade: ");
            int idade;
            while (!int.TryParse(Console.ReadLine(), out idade))
            {
                Console.Write("Digite uma idade válida: ");
            }
            p.Idade = idade;

            Console.Write("Nível de dor (0 a 10): ");
            int dor;
            while (!int.TryParse(Console.ReadLine(), out dor) || dor < 0 || dor > 10)
            {
                Console.Write("Digite um valor entre 0 e 10: ");
            }
            p.NivelDor = dor;

            // PRIORIDADE (TRIAGEM)
            if (p.NivelDor >= 9 || p.Idade >= 80)
                p.Prioridade = "PRIORIDADE ALTA"; // 🔴
            else if (p.NivelDor >= 5)
                p.Prioridade = "OBSERVAÇÃO TRIAGEM"; // 🟡
            else
                p.Prioridade = "AGUARDAR TRIAGEM"; // 🟢

            pacientes.Add(p);

            Console.WriteLine("\nPaciente cadastrado!");
            Console.WriteLine("Prioridade: " + p.Prioridade);

            Console.Write("\nCadastrar outro? (s/n): ");
            continuar = Console.ReadLine()?.ToLower() ?? "n";

        } while (continuar == "s");
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