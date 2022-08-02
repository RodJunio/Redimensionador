using System;
using System.Threading;

namespace redimensionador
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("iniciando nosso redimensionador");

            Thread thread = new Thread(Redimencionar);
            thread.Start();

            
            Console.Read();
        }

        static void Redimencionar()
        {
            #region "Diretorios"
            string diretorio_entrada = "Arquivos_Entrada";
            string diretorio_finalizado = "Arquivos_Finalizados";
            string diretorio_redimensionado = "Arquivos.Redimensionados";

            if (!Directory.Exists(diretorio_entrada))
            {
               Directory.CreateDirectory(diretorio_entrada);
            }

            if(!Directory.Exists(diretorio_redimensionado))
            {
                Directory.CreateDirectory(diretorio_redimensionado);
            }
            if(!Directory.Exists(diretorio_finalizado))
            {
                Directory.CreateDirectory(diretorio_finalizado);
            }
            #endregion
            
            while(true)
            {
                //meu programa vai olhar para a pasta de entrada
                //Se tiver arquivo ele vai redimensionar
                var arquivosEntrada = Directory.EnumerateFiles(diretorio_entrada);

                //Ler o tamanho que irá redmensionar

                //Redimensiona

                //copia os arquivos redimensionaod para a pasta redimensionados

                //move o arquivo de entrada para a pasta de finalizados
                Thread.Sleep(new TimeSpan(0, 0, 3));
            }
        }
    }
}
