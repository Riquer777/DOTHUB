//DOTHUB
//
//Copyright (C) 2026 Riquer777
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License version 3.

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Transactions;

class Program
{
    static int Menu_t_up = 3;
    static int Menu_s_poisi = 0;
    static bool Enter_Clik = false;
    static int Menu_secao = 0;
    static string Erro_console = "";
    static bool Linux = !RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    
    static int Ler_input()
    {
        int resultado = 0;
        while(resultado == 0)
        {
            var input_lido = Console.ReadKey(true);
        switch (input_lido.Key)
        {
            case ConsoleKey.DownArrow or ConsoleKey.S:
                resultado = 2;
                break;
            case ConsoleKey.UpArrow or ConsoleKey.W:
                resultado = 1;
                break;
            case ConsoleKey.Enter:
                resultado = 3;
                break;
        }
        }
        return(resultado);
    }
    static bool ExecutarComando(string comando, bool Linux, out string erro)
{
    string erroLocal = "";

    try
    {
        Process processo = new Process();

        if (Linux)
        {
            processo.StartInfo.FileName = "/bin/bash";
            processo.StartInfo.Arguments = "-c \"" + comando + "\"";
        }
        else
        {
            processo.StartInfo.FileName = "cmd.exe";
            processo.StartInfo.Arguments = "/c " + comando;
        }

        processo.StartInfo.RedirectStandardError = true;
        processo.StartInfo.RedirectStandardOutput = true;
        processo.StartInfo.UseShellExecute = false;
        processo.StartInfo.CreateNoWindow = true;

        processo.OutputDataReceived += (s, e) =>
        {
            if (e.Data != null)
                Console.WriteLine(e.Data);
        };

        processo.ErrorDataReceived += (s, e) =>
        {
            if (e.Data != null)
            {
                erroLocal += e.Data + "\n";
                Console.WriteLine(e.Data);
            }
        };

        processo.Start();

        processo.BeginOutputReadLine();
        processo.BeginErrorReadLine();

        processo.WaitForExit();
        processo.WaitForExit(); // 👈 ESSENCIAL

        erro = erroLocal;
        return processo.ExitCode == 0;
    }
    catch (Exception ex)
    {
        erro = ex.Message;
        return false;
    }
}
    static void Menu_navagaco(int inputlido)
    {
        if  ((inputlido == 1) || (inputlido == 2) || (inputlido == 3))
        {
            switch (inputlido)
            {
                case 1:
                    Menu_s_poisi -= 1;
                    break;
                case  2:
                    Menu_s_poisi += 1;
                    break;
            }
            if (inputlido == 3)
            {
                Enter_Clik = true;
            }
            else
            {
                Enter_Clik = false;
            }
            if (Menu_s_poisi < 0)
            {
                Menu_s_poisi = 0;
            }
            if (Menu_s_poisi > Menu_t_up)
            {
                Menu_s_poisi = Menu_t_up;
            }
        }
    }
    static void Menu_userr()
    {
    switch (Menu_secao)
        {
            case 0:
            Console.SetCursorPosition(0,0);
            Console.WriteLine("DOTHUB\n|Main:\n|[" + (Menu_s_poisi == 0 ? $"{Cor.verde}*{Cor.reset}" : $"{Cor.amarelo}#{Cor.reset}") + "]"+"Run");
            Console.WriteLine("|[" + (Menu_s_poisi == 1 ? $"{Cor.verde}*{Cor.reset}" : $"{Cor.amarelo}#{Cor.reset}") + "]"+"Compilation options");
            Console.WriteLine("|[" + (Menu_s_poisi == 2 ? $"{Cor.verde}*{Cor.reset}" : $"{Cor.amarelo}#{Cor.reset}") + "]"+"Options for the dotnet console");
            Console.WriteLine("|[" + (Menu_s_poisi == 3 ? $"{Cor.verde}*{Cor.reset}" : $"{Cor.amarelo}#{Cor.reset}") + "]"+"Exit");
            break;
            case 1:
            Console.SetCursorPosition(0,0);
            Console.WriteLine("DOTHUB\n|Compilation options:\n|[" + (Menu_s_poisi == 0 ? $"{Cor.verde}*{Cor.reset}" : $"{Cor.amarelo}#{Cor.reset}") + "]"+"compile Linux");
            Console.WriteLine("|[" + (Menu_s_poisi == 1 ? $"{Cor.verde}*{Cor.reset}" : $"{Cor.amarelo}#{Cor.reset}") + "]"+"compile Linux(self-contained)");
            Console.WriteLine("|[" + (Menu_s_poisi == 2 ? $"{Cor.verde}*{Cor.reset}" : $"{Cor.amarelo}#{Cor.reset}") + "]"+"compile Windows");
            Console.WriteLine("|[" + (Menu_s_poisi == 3 ? $"{Cor.verde}*{Cor.reset}" : $"{Cor.amarelo}#{Cor.reset}") + "]"+"compile Windows(self-contained)");
            Console.WriteLine("|[" + (Menu_s_poisi == 4 ? $"{Cor.verde}*{Cor.reset}" : $"{Cor.amarelo}#{Cor.reset}") + "]"+"Back to menu");
            break;
            case 2:
            Console.SetCursorPosition(0,0);
            Console.WriteLine("DOTHUB\n|Options for the dotnet console:\n|[" + (Menu_s_poisi == 0 ? $"{Cor.verde}*{Cor.reset}" : $"{Cor.amarelo}#{Cor.reset}") + "]"+"dotnet new console");
            Console.WriteLine("|[" + (Menu_s_poisi == 1 ? $"{Cor.verde}*{Cor.reset}" : $"{Cor.amarelo}#{Cor.reset}") + "]"+"Back");
            break;
        }
    }
    static void Menu_action()
    {
    if (Enter_Clik)
    {
        Enter_Clik = false;

        if (Menu_secao == 0)
        {
            if (Menu_s_poisi == 0)
            {
                ExecutarComando("dotnet run", Linux, out Erro_console);
                Console.WriteLine("enter for continue");
                Console.ReadLine();
                Console.Clear();
            }

            if(Menu_s_poisi == 1)
                {
                    Menu_t_up = 4;
                    Menu_s_poisi = 0;
                    Menu_secao = 1;
                    Console.Clear();
                    Thread.Sleep(250);
                    return;
                }
            if(Menu_s_poisi == 2)
                {
                    Menu_t_up = 1;
                    Menu_s_poisi = 0;
                    Menu_secao = 2;
                    Console.Clear();
                    Thread.Sleep(250);
                    return;
                }

            if (Menu_s_poisi == 3)
            {
                Environment.Exit(0);
            }
        }
        if (Menu_secao == 1)
            {
                if (Menu_s_poisi == 4)
                {
                    Menu_t_up = 3;
                    Menu_s_poisi = 0;
                    Menu_secao = 0;
                    Console.Clear();
                    Thread.Sleep(250);
                    return;
                }
                if (Menu_s_poisi == 0)
                {
                ExecutarComando("dotnet publish -c Release -r linux-x64 --self-contained false", Linux, out Erro_console);
                Console.WriteLine("enter for continue");
                Console.ReadLine();
                Console.Clear();
                }
                if (Menu_s_poisi == 1)
                {
                ExecutarComando("dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true", Linux, out Erro_console);
                Console.WriteLine("enter for continue");
                Console.ReadLine();
                Console.Clear();
                }
                if (Menu_s_poisi == 2)
                {
                ExecutarComando("dotnet publish -c Release -r win-x64 --self-contained false", Linux, out Erro_console);
                Console.WriteLine("enter for continue");
                Console.ReadLine();
                Console.Clear();
                }
                if (Menu_s_poisi == 3)
                {
                ExecutarComando("dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true", Linux, out Erro_console);
                Console.WriteLine("enter for continue");
                Console.ReadLine();
                Console.Clear();
                }
            }
            if (Menu_secao == 2)
            {
                if (Menu_s_poisi == 0)
                {
                ExecutarComando("dotnet new console", Linux, out Erro_console);
                Console.WriteLine("enter for continue");
                Console.ReadLine();
                Console.Clear();
                }
                if (Menu_s_poisi == 1)
                {
                    Menu_t_up = 3;
                    Menu_s_poisi = 0;
                    Menu_secao = 0;
                    Console.Clear();
                    Thread.Sleep(250);
                    return;
                }
            }
    }
    }
    static void Main()
    {
        Console.CursorVisible = false;
        Console.WriteLine("Linux user:" + Linux);
        Console.WriteLine("You need to have .NET installed in the PATH for Dothub to work correctly.(enter to continue)");
        Console.ReadLine();
        Console.Clear();
        while (true)
        {
            Menu_userr();
            Enter_Clik = false;
            Menu_navagaco(Ler_input()); 
            Menu_action(); 
        }
        //Console.CursorVisible = true;
        }
}
public static class Cor
{
    public const string preto = "\u001b[30m";
    public const string vermelho = "\u001b[31m";
    public const string verde = "\u001b[32m";
    public const string amarelo = "\u001b[33m";
    public const string azul = "\u001b[34m";
    public const string magenta = "\u001b[35m";
    public const string ciano = "\u001b[36m";
    public const string branco = "\u001b[37m";

    public const string pretoBright = "\u001b[90m";
    public const string vermelhoBright = "\u001b[91m";
    public const string verdeBright = "\u001b[92m";
    public const string amareloBright = "\u001b[93m";
    public const string azulBright = "\u001b[94m";
    public const string magentaBright = "\u001b[95m";
    public const string cianoBright = "\u001b[96m";
    public const string brancoBright = "\u001b[97m";

    public const string reset = "\u001b[0m";
}
