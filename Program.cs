using System;
using System.Drawing;
using System.Threading;

namespace redimensionador
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("iniciando redimensionador");

            Thread thread = new Thread(Redimencionar);
            thread.Start();


            Console.Read();
        }

        static void Redimencionar()
        {
            #region "Diretorios"
            string diretorio_entrada = "Arquivos_Entrada";
            string diretorio_finalizado200 = "Arquivos_Finalizado200";
            string diretorio_finalizado400 = "Arquivos_Finalizado400";
            string diretorio_redimensionado = "Arquivos.Redimensionados";
            string diretorio_redimensionado400 = "Arquivos.Redimensionados400";

            if (!Directory.Exists(diretorio_entrada))
            {
                Directory.CreateDirectory(diretorio_entrada);
            }

            if (!Directory.Exists(diretorio_redimensionado))
            {
                Directory.CreateDirectory(diretorio_redimensionado);
            }
            if (!Directory.Exists(diretorio_finalizado200))
            {
                Directory.CreateDirectory(diretorio_finalizado200);
            }
            if (!Directory.Exists(diretorio_finalizado400))
            {
                Directory.CreateDirectory(diretorio_finalizado400);
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
                int altura200 = 200;
                int altura400 = 400;

                foreach (var arquivo in arquivosEntrada)
                {
                    FileStream = new FileStream(arquivo, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    FileInfo = new FileInfo(arquivo);

                    string caminho = Environment.CurrentDirectory + @"\" + diretorio_redimensionado + @"\" + FileInfo.Name + DateTime.Now.Millisecond.ToString() + "_" + FileInfo.Name;

                    //Redimensiona + copia os arquivos redimensionado para a pasta redimensionados
                    Redimensionador(Image.FromStream(FileStream), altura200, caminho);

                    //Fecha o arquivo
                    FileStream.Close();

                    //move o arquivo de entrada para a pasta de finalizados
                    string caminhoFinalizado = Environment.CurrentDirectory + @"\" + diretorio_finalizado200 + @"\" + FileInfo.Name;                   
                    FileInfo.MoveTo(caminhoFinalizado);
                    
                }

                foreach (var arquivo2 in arquivosEntrada)
                {
                    FileStream = new FileStream(arquivo2, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    FileInfo = new FileInfo(arquivo2);

                    string caminho = Environment.CurrentDirectory + @"\" + diretorio_redimensionado400 + @"\" + FileInfo.Name + DateTime.Now.Millisecond.ToString() + "_" + FileInfo.Name;

                    //Redimensiona + copia os arquivos redimensionado para a pasta redimensionados
                    Redimensionador(Image.FromStream(FileStream), altura400, caminho);

                    //Fecha o arquivo
                    FileStream.Close();

                    //move o arquivo de entrada para a pasta de finalizados
                    string caminhoFinalizado = Environment.CurrentDirectory + @"\" + diretorio_finalizado400 + @"\" + FileInfo.Name;
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
