using System;
using System.Drawing;
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

            if (!Directory.Exists(diretorio_redimensionado))
            {
                Directory.CreateDirectory(diretorio_redimensionado);
            }
            if (!Directory.Exists(diretorio_finalizado))
            {
                Directory.CreateDirectory(diretorio_finalizado);
            }
            #endregion

            FileStream FileStream;
            FileInfo FileInfo;

            while (true)
            {
                //meu programa vai olhar para a pasta de entrada
                //Se tiver arquivo ele vai redimensionar
                var arquivosEntrada = Directory.EnumerateFiles(diretorio_entrada);

                //Ler o tamanho que irá redmensionar
                int novaAltura = 200;
                
                foreach (var arquivo in arquivosEntrada)
                {
                    FileStream = new FileStream(arquivo, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    FileInfo = new FileInfo(arquivo);

                    string caminho = Environment.CurrentDirectory + @"\" + diretorio_redimensionado + @"\" + FileInfo.Name + DateTime.Now.Millisecond.ToString() + "_" + FileInfo.Name;

                    //Redimensiona + copia os arquivos redimensionado para a pasta redimensionados
                    Redimensionador(Image.FromStream(FileStream), novaAltura, caminho);

                    //Fecha o arquivo
                    FileStream.Close();

                    //move o arquivo de entrada para a pasta de finalizados
                    string caminhoFinalizado = Environment.CurrentDirectory + @"\" + diretorio_finalizado + @"\" + FileInfo.Name;                   
                    FileInfo.MoveTo(caminhoFinalizado);

                    
                }


                Thread.Sleep(new TimeSpan(0, 0, 3));
            }
        }

        static void Redimensionador(Image imagem, int altura, string caminho)
        {
            double ratio = (double)altura / imagem.Height;
            int novaLargura = (int)(imagem.Width * ratio);
            int novaAltura = (int)(imagem.Height * ratio);

            Bitmap novaImagem = new Bitmap(novaLargura, novaAltura);

            using (Graphics g = Graphics.FromImage(novaImagem))
            {
                g.DrawImage(imagem, 0, 0, novaLargura, novaAltura);
            }

            novaImagem.Save(caminho);
            imagem.Dispose();
        }
    }
}
