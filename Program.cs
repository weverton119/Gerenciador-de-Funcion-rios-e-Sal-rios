using System;
using System.Collections.Generic;
using System.Globalization;

namespace Portfolio_ClassesAbstratas
{
    // Classe abstrata base
    abstract class Funcionario
    {
        // Inicializo Nome para evitar aviso CS8618 quando nullable está ativo
        public string Nome { get; set; } = string.Empty;
        public double SalarioBase { get; set; }

        public abstract double CalcularSalario();

        public virtual void ExibirDados()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Cargo: {this.GetType().Name}");
            Console.WriteLine($"Nome: {Nome}");
            Console.WriteLine($"Salário Base: {SalarioBase.ToString("C", CultureInfo.CurrentCulture)}");
            Console.WriteLine($"Salário Final: {CalcularSalario().ToString("C", CultureInfo.CurrentCulture)}");
            Console.WriteLine("-----------------------------");
        }
    }

    class Administrativo : Funcionario
    {
        public override double CalcularSalario() => SalarioBase * 1.10; // +10%
    }

    class Tecnico : Funcionario
    {
        public override double CalcularSalario() => SalarioBase * 1.20; // +20%
    }

    class Estagiario : Funcionario
    {
        public override double CalcularSalario() => SalarioBase / 2.0; // metade
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();

            Console.WriteLine("=== Sistema de Cadastro de Funcionários ===\n");

            while (true)
            {
                Console.WriteLine("Escolha o tipo de funcionário:");
                Console.WriteLine("1 - Administrativo");
                Console.WriteLine("2 - Técnico");
                Console.WriteLine("3 - Estagiário");
                Console.Write("Opção: ");
                string tipo = Console.ReadLine()?.Trim() ?? "";

                if (tipo != "1" && tipo != "2" && tipo != "3")
                {
                    Console.WriteLine("Tipo inválido! Tente novamente.\n");
                    continue;
                }

                Console.Write("Digite o nome do funcionário: ");
                string nome = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(nome))
                {
                    Console.WriteLine("Nome inválido! Tente novamente.\n");
                    continue;
                }

                double salario;
                while (true)
                {
                    Console.Write("Digite o salário base (use vírgula ou ponto conforme seu sistema): ");
                    string salarioInput = Console.ReadLine() ?? "";
                    // usa a cultura atual para aceitar vírgula em PT-BR
                    if (double.TryParse(salarioInput, NumberStyles.Number, CultureInfo.CurrentCulture, out salario))
                    {
                        if (salario >= 0) break;
                        Console.WriteLine("Salário não pode ser negativo. Tente novamente.\n");
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido! Digite um número válido para o salário.\n");
                    }
                }

                Funcionario func = tipo switch
                {
                    "1" => new Administrativo { Nome = nome, SalarioBase = salario },
                    "2" => new Tecnico { Nome = nome, SalarioBase = salario },
                    "3" => new Estagiario { Nome = nome, SalarioBase = salario },
                    _ => null
                };

                if (func != null)
                    funcionarios.Add(func);

                Console.Write("\nDeseja cadastrar outro funcionário? (S/N): ");
                string opcaoResposta = Console.ReadLine()?.Trim().ToUpper() ?? "N";
                if (opcaoResposta == "N" || opcaoResposta == "NAO")
                    break;

                Console.WriteLine();
            }

            Console.WriteLine("\n=== RELATÓRIO DE FUNCIONÁRIOS ===");
            foreach (var f in funcionarios)
            {
                f.ExibirDados();
            }

            Console.WriteLine("Fim do programa. Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
