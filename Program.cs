using System;

namespace APPcadastro
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por usar os nossos serviços.");
            Console.ReadLine();
        }
        
        private static void ExcluirSerie()
        {
            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Exclui(indiceSerie);
            Console.WriteLine("*** Série excluida com Sucesso ***");
        }

        private static void VisualizarSerie()
        {
            Console.Write("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);
        }

        private static void AtualizarSerie()
        {
            Console.Write("Digite o id da série: ");
            string indiceSerie = Console.ReadLine();

            InserirAtualizar(indiceSerie);
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluido*" : ""));
            }
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            string opcao = "inserir";

            InserirAtualizar(opcao);
        }       

        private static string ObterOpcaoUsuario()
        {       
            Console.WriteLine();
            Console.WriteLine("*** Moonvies Séries ***");
            Console.WriteLine("Informe a opção desejada: ");

            Console.WriteLine("1 - Listar Séries");
            Console.WriteLine("2 - Inserir Séries");
            Console.WriteLine("3 - Atualizar Séries");
            Console.WriteLine("4 - Excluir Séries");
            Console.WriteLine("5 - Visualizar Série");
            Console.WriteLine("C - Limpar Série");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static void InserirAtualizar(string opcao) {
            foreach (int index in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("|{0} - {1}", index, Enum.GetName(typeof(Genero), index));
            }
            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            foreach (int index in Enum.GetValues(typeof(Genero)))
            {
                if (entradaGenero == index)
                {
                    Console.WriteLine("Você escolheu: {0} ", Enum.GetName(typeof(Genero), index));
                }
            }

            Console.Write("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano de Início da Série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            int currentId = 0;
            if (opcao != "inserir") 
            {
                currentId = int.Parse(opcao);
            }
            else
            {
                currentId = repositorio.ProximoId();
            }

            Serie novaSerie = new Serie(id: currentId, 
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo, ano: entradaAno,
                                        descricao: entradaDescricao);
            
            if (opcao == "inserir") {
                Console.WriteLine();
                Console.WriteLine("*** Serie Cadastrada com Sucesso ***");
                repositorio.Insere(novaSerie);
            }
            else 
            {
                Console.WriteLine();
                Console.WriteLine("*** Série atualizada com Sucesso ***");
                repositorio.Atualiza(currentId, novaSerie);                
            }
        }
    }
}
