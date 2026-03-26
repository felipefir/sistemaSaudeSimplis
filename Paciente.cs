using System;
using System.ComponentModel;

namespace desafio1;


public class Paciente()
{
    public string? nome;
    public string? idadePacienteString;
    public int idade;
    public int nivelDor;
    List<string>listaVermelha = new List<string>();
    List<string>listaAmarela = new List<string>();
     List<string>listaVerde = new List<string>();
     List<string>listaPacientes = new List<string>();
    

public void adicionarPaciente()
    {
        Console.WriteLine("digite o Nome do paciente: ");
        nome = Console.ReadLine() ?? "";
        Console.WriteLine(" digite a Idade paciente: ");
        idadePacienteString = Console.ReadLine();
        if(int.TryParse(Console.ReadLine(), out idade))
        {   
        Console.WriteLine("digite o nivel da dor do paciente:");   
        if(int.TryParse(Console.ReadLine(), out nivelDor))
        {
            ClassificarProridade(nome, idade, nivelDor);
        }
        else
        {
            Console.WriteLine("dor nao indentifidada.Digite um numero.");
        }
    }
     else
     {
        Console.WriteLine("a idade nao e numero:");
     }
    }
    public void ClassificarProridade(String nome,int idade, int nivelDor)
    {
        if(nivelDor>=9 || idade >=80)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("prioridade alta - pulseira vermelha");
            listaVermelha.Add(nome);
            Console.ResetColor();
        }
        else if (nivelDor >= 5)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("prioridade media - pulseira amarela");
            listaAmarela.Add(nome);
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("prioridade baixa - pulseira verde");
            listaVerde.Add(nome);
            Console.ResetColor();
        }
    }

    public void ListarPaciente()
    {
        int contadoramrelo = 0;
        int contadorverde = 0; 
        listaPacientes =new List<string>();
        foreach(var nome in listaVermelha)
        {
            listaPacientes.Add(nome);
        }
        while(contadoramrelo<listaAmarela.Count() || contadorverde<listaVerde.Count())
        {
            int i=0;
            while(contadoramrelo<listaAmarela.Count() && i < 2)
            {
                listaPacientes.Add(listaAmarela[contadorverde]);
                contadoramrelo++;
                i++;
            }
            listaPacientes.Add(listaVerde[contadorverde]);
            contadorverde++;
        }

    }

    public void ChamarProximo()
    {
        ListarPaciente();

        Console.WriteLine($"sr.(a){listaPacientes[0]}dirigir-se ao consutorio");

        ExcluirPaciente();

    }
    public void ExcluirPaciente()
    {
        if (listaVermelha.Contains(listaPacientes[0]))
        {
             listaVermelha.RemoveAt(0);
        }
        else if (listaAmarela.Contains(listaPacientes[0]))
        {
            listaAmarela.RemoveAt(0);
        }
        else
        {
            listaVerde.RemoveAt(0);
        }

         listaPacientes.RemoveAt(0);
    }
    public void VerificarListaAtendimento()
    {
        int i=1;
        ListarPaciente();
        foreach ( var nome in listaPacientes)
        {
            Console.WriteLine($"{i} - {nome}");
             i++;
        }
    }
}
        
         
        

       
        

        
       
        
           
           
        
    

    
