using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAcme.Utilitarios
{
    public static class ArquivoUtilitario
    {
        private const string NOMETESTE = "ArquivoPermissaoEscrita";
        private const string EXTENSAOTESTE = "txt";



        /// <summary>
        /// Método Cria o Diretorio no caminho informado por parametro.
        /// </summary>
        /// <param name="aDiretorio"></param>
        public static void CriarDiretorio(string aDiretorio)
        {
            if (!VerificarSeDiretorioExiste(aDiretorio))
                Directory.CreateDirectory(aDiretorio);
        }

        /// <summary>
        /// Método remove todos os arquivos do diretorio.
        /// </summary>
        /// <param name="aDiretorio"></param>
        public static void LimparDiretorio(string aDiretorio)
        {
            if (VerificarSeDiretorioExiste(aDiretorio))
                //Deverá remover todos os arquivos comquaisquer extensões (*.*) contidos no diretório informando em aDiretorio. 
                ArquivoUtilitario.LimparDiretorio(aDiretorio, true, true);
        }

        /// <summary>
        /// Método verifica se a aplicação tem permissão de escrita no diretorio.
        /// </summary>
        /// <param name="aDiretorio"></param>
        /// <returns></returns>
        public static bool VerificarPermissaoEscritaDiretorio(string aDiretorio)
        {
            var arquivoCaminhoCompleto = Path.Combine(aDiretorio,
                                                string.Format("{0}_{1}.{2}",
                                                            NOMETESTE,
                                                            DateTime.Now.ToShortDateString().Replace("/", "-"),
                                                            EXTENSAOTESTE));

            //tenta criar arquivo, se conseguir, apaga o arquivo e retorna true, caso contrario retorna false
            try
            {
                if (!VerificarSeDiretorioExiste(aDiretorio))
                    if (VerificarPermissaoCriacaoDiretorio(aDiretorio))
                        CriarDiretorio(aDiretorio);
                    else
                        return false;

                if (File.Exists(arquivoCaminhoCompleto))
                    File.Delete(arquivoCaminhoCompleto);

                File.Create(arquivoCaminhoCompleto).Close();
                File.Delete(arquivoCaminhoCompleto);

                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }

        }

        /// <summary>
        /// Método verifica se o sistema tem permissão de criação de diretório.
        /// </summary>
        /// <param name="aDiretorio"></param>
        /// <returns></returns>
        public static bool VerificarPermissaoCriacaoDiretorio(string aDiretorio)
        {
            try
            {
                Directory.CreateDirectory(aDiretorio);
                Directory.Delete(aDiretorio);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica se o diretorio existe.
        /// </summary>
        /// <param name="aDiretorio"></param>
        /// <returns></returns>
        public static bool VerificarSeDiretorioExiste(string aDiretorio)
        {
            if (Directory.Exists(aDiretorio))
                return true;

            return false;
        }

        /// <summary>
        /// Método verifica o tamanho do arquivo.
        /// </summary>
        /// <param name="aNomeArquivo"></param>
        /// <returns></returns>
        public static long VerificarTamanhoArquivo(string aNomeArquivo)
        {
            //- Criar um objeto do tipo FileInfo.
            //- Associar a ele o arquivo informado via parâmetro.
            //- Obter o tamanho do arquivo com FileInfo.Lenght().
            //- Devolver o tamanho ao chamador.

            FileInfo fileInfo = new FileInfo(string.Format(@"{0}", aNomeArquivo));

            return fileInfo.Length;
        }

        /// <summary>
        /// Método verifica se arquivo existe no diretório.
        /// </summary>
        /// <param name="aExtensao"></param>
        /// <param name="aArquivo"></param>
        /// <param name="aDiretorio"></param>
        /// <returns></returns>
        public static bool VerificaSeArquivoExiste(string aExtensao, string aArquivo, string aDiretorio)
        {
            var arquivoCaminhoCompleto = Path.Combine(aDiretorio, Path.ChangeExtension(aArquivo.TrimEnd(), aExtensao.TrimEnd()));

            if (File.Exists(arquivoCaminhoCompleto))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Método verifica se arquivo existe no diretório.
        /// </summary>
        /// <param name="aNomeArquivoCompleto"></param>
        /// <returns></returns>
        public static bool VerificaSeArquivoExiste(string aNomeArquivoCompleto)
        {
            if (File.Exists(aNomeArquivoCompleto))
                return true;

            return false;
        }

        /// <summary>
        /// Método remove todos os arquivos do diretorio.
        /// </summary>
        /// <param name="aDiretorio"></param>
        /// <param name="aRecursivo"></param>
        /// <param name="aDiretorioPrincipal"></param>
        public static void LimparDiretorio(string aDiretorio, bool aRecursivo, bool aDiretorioPrincipal)
        {
            if (aRecursivo)
            {
                var subDiretorios = Directory.GetDirectories(aDiretorio);

                foreach (var diretorio in subDiretorios)
                {
                    LimparDiretorio(diretorio, aRecursivo, false);
                }
            }

            // Cria Lista de Arquivos com todos os arquivos de um diretorio
            var listaArquivos = Directory.GetFiles(aDiretorio);

            foreach (var arquivo in listaArquivos)
            {
                // Recupera atributos do arquivo
                var atributos = File.GetAttributes(arquivo);

                //testa se o arquivo é somente leitura e muda o atributo para poder apagar
                if ((atributos & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    File.SetAttributes(arquivo, atributos ^ FileAttributes.ReadOnly);

                // Apaga o arquivo
                File.Delete(arquivo);
            }

            //Apos apagar todos os arquivos e todas os subDiretorios verifica se é para apagar o diretorio principal também
            if (!aDiretorioPrincipal)
                Directory.Delete(aDiretorio);
        }

        /// <summary>
        /// Método apaga um arquivo caso ele exista no diretorio.
        /// </summary>
        /// <param name="arquivoCaminhoCompleto"></param>
        public static void ApagarArquivo(string arquivoCaminhoCompleto)
        {
            if (File.Exists(arquivoCaminhoCompleto))
                File.Delete(arquivoCaminhoCompleto);
        }

        /// <summary>
        /// Método Recria um arquivo
        /// </summary>
        /// <param name="arquivoCaminhoCompleto"></param>
        public static void RecriarArquivo(string arquivoCaminhoCompleto)
        {
            //cria/recria o arquivo no diretorio
            if (File.Exists(arquivoCaminhoCompleto))
                File.Delete(arquivoCaminhoCompleto);

            File.Create(arquivoCaminhoCompleto).Close();
        }

        /// <summary>
        /// Método move o arquivo.
        /// </summary>
        /// <param name="aOrigem"></param>
        /// <param name="aDestino"></param>
        /// <param name="aNomeArquivo"></param>
        /// <returns></returns>
        public static bool MoverArquivo(string aOrigem, string aDestino, string aNomeArquivo)
        {
            var nomeArquivo = Path.GetFileName(aNomeArquivo);

            if (!aOrigem.EndsWith("/"))
                aOrigem = string.Format("{0}{1}", aOrigem, "/");

            if (!aDestino.EndsWith("/"))
                aDestino = string.Format("{0}{1}", aDestino, "/");

            System.IO.File.Move(System.IO.Path.Combine(aOrigem, nomeArquivo), System.IO.Path.Combine(aDestino, nomeArquivo));

            return true;
        }       
    }
}