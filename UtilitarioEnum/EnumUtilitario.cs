using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace ApiAcme.UtilitarioEnum
{
    /// <summary>
    /// Contém métodos utilitários para trabalhar com Enums
    /// </summary>
    public static class EnumUtilitario
    {
        /// <summary>
        /// Retorna uam lista com todos os items declarados em um Enum
        /// </summary>
        /// <typeparam name="T">Tipo do enum relativo ao qual os itens deve ser retornados</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ListarValoresEnum<T>() where T : struct
        {
            return EnumUtilitario<T>.ListarValoresEnum();
        }

        /// <summary>
        /// Retorna a descrição para um determinado Enum, declarados no atributo Descricao ou Description
        /// </summary>
        /// <typeparam name="T">Tipo do enum relativo ao qual a descrição deve ser retornada</typeparam>
        /// <param name="aItem">Item relativo ao qual deve ser retornada a Descrição</param>
        /// <returns></returns>
        public static string Descricao<T>(this T aItem) where T : struct
        {
            return EnumUtilitario<T>.DescricaoEnum(aItem);
        }

        /// <summary>
        /// Retorna a descrição para um determinado Enum, declarados no atributo Descricao ou Description
        /// </summary>
        /// <typeparam name="T">Tipo do enum relativo ao qual a descrição deve ser retornada</typeparam>
        /// <param name="aItem">Item relativo ao qual deve ser retornada a Descrição</param>
        /// <returns></returns>
        public static string Descricao<T>(this T? aItem) where T : struct
        {
            return EnumUtilitario<T>.DescricaoEnum(aItem);
        }

        /// <summary>
        /// Retorna o valor string para um determinado Enum, declarado no atributo Valor ou XmlElement
        /// </summary>
        /// <typeparam name="T">Tipo do enum relativo ao qual o valor deve ser retornado</typeparam>
        /// <param name="aItem">Item relativo ao qual deve ser retornado o valor</param>
        /// <returns></returns>
        public static string ComoString<T>(this T aItem) where T : struct
        {
            return EnumUtilitario<T>.EnumComoString(aItem);
        }

        /// <summary>
        /// Retorna o valor string para um determinado Enum, declarado no atributo Valor ou XmlElement
        /// </summary>
        /// <typeparam name="T">Tipo do enum relativo ao qual o valor deve ser retornado</typeparam>
        /// <param name="aItem">Item relativo ao qual deve ser retornado o valor</param>
        /// <returns></returns>
        public static string ComoString<T>(this T? aItem) where T : struct
        {
            return EnumUtilitario<T>.EnumComoString(aItem);
        }

        /// <summary>
        /// Retorna o item do enum para um determinado Enum relativo a um valor string, declarado no atributo Valor ou XmlElement
        /// </summary>
        /// <typeparam name="T">Tipo do enum relativo ao qual o item deve ser retornado</typeparam>
        /// <param name="aValor">Valor relativo ao qual deve ser retornado o item</param>
        /// <returns></returns>
        public static T? ComoEnum<T>(this string aValor) where T : struct
        {
            return EnumUtilitario<T>.StringComoEnum(aValor);
        }

        ///// <summary>
        ///// Popular um controle que implemente IControleDataBind a partir de um enum
        ///// </summary>
        ///// <typeparam name="T">Tipo do enum onde serão extraídos os valores e descrições</typeparam>
        ///// <param name="aControle">Controle que será populado</param>
        ///// <param name="aIncluirItemVazio">Indica ser deve ser incluído um item vazio no início da lista</param>
        ///// <param name="aFiltro">Lambda expression que permite filtrar os valores a serem montados</param>
        ///// <param name="aOrdenacao">Lambda expression que permite order os valores a serem montados</param>
        ///// <param name="aOrdenacaoAscendente">Se true, indica que a ordenação deve ser ascendente; caso contrário, a ordenação será descendente</param>
        //public static void PopularItens<T>(
        //    this IControleDataBind aControle, bool aIncluirItemVazio,
        //    Func<T, bool> aFiltro = null,
        //    Func<T, IComparable> aOrdenacao = null,
        //    bool aOrdenacaoAscendente = true)
        //    where T : struct
        //{

        //    var lista = ListarValoresEnum<T>();

        //    if (aFiltro != null)
        //        lista = lista.Where(aFiltro);

        //    if (aOrdenacao == null)
        //        if (aOrdenacaoAscendente)
        //            lista = lista.OrderBy(x => x);
        //        else
        //            lista = lista.OrderByDescending(x => x);
        //    else if (aOrdenacaoAscendente)
        //        lista = lista.OrderBy(aOrdenacao);
        //    else
        //        lista = lista.OrderByDescending(aOrdenacao);

        //    var itens = from item in lista
        //                select new KeyValuePair<string, string>(item.ComoString(), item.Descricao());

        //    if (aIncluirItemVazio)
        //    {
        //        itens = (from item in new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(string.Empty, string.Empty) }
        //                 select item)
        //            .Union(itens);
        //    }

        //    aControle.DataSource = itens;
        //    aControle.DataValueField = "Key";
        //    aControle.DataTextField = "Value";

        //    aControle.DataBind();
        //    //aControle.Items.Insert(0, "");
        //}

        /// <summary>
        /// Popular um ListControl (DropDownList, RadioButtonList, CheckBoxList, etc) a partir de um enum
        /// </summary>
        /// <typeparam name="T">Tipo do enum onde serão extraídos os valores e descrições</typeparam>
        /// <param name="aControle">Controle que será populado</param>
        /// <param name="aIncluirItemVazio">Indica ser deve ser incluído um item vazio no início da lista</param>
        /// <param name="aFiltro">Lambda expression que permite filtrar os valores a serem montados</param>
        /// <param name="aOrdenacao">Lambda expression que permite order os valores a serem montados</param>
        /// <param name="aOrdenacaoAscendente">Se true, indica que a ordenação deve ser ascendente; caso contrário, a ordenação será descendente</param>
        //public static void PopularItens<T>(
        //    this ListControl aControle, bool aIncluirItemVazio,
        //    Func<T, bool> aFiltro = null,
        //    Func<T, IComparable> aOrdenacao = null,
        //    bool aOrdenacaoAscendente = true)
        //    where T : struct
        //{

        //    var lista = ListarValoresEnum<T>();

        //    if (aFiltro != null)
        //        lista = lista.Where(aFiltro);

        //    if (aOrdenacao == null)
        //        if (aOrdenacaoAscendente)
        //            lista = lista.OrderBy(x => x);
        //        else
        //            lista = lista.OrderByDescending(x => x);
        //    else if (aOrdenacaoAscendente)
        //        lista = lista.OrderBy(aOrdenacao);
        //    else
        //        lista = lista.OrderByDescending(aOrdenacao);

        //    var itens = from item in lista
        //                select new KeyValuePair<string, string>(item.ComoString(), item.Descricao());

        //    if (aIncluirItemVazio)
        //    {
        //        itens = (from item in new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(string.Empty, string.Empty) }
        //                 select item)
        //            .Union(itens);
        //    }

        //    aControle.DataSource = itens;
        //    aControle.DataValueField = "Key";
        //    aControle.DataTextField = "Value";

        //    aControle.DataBind();
        //    //aControle.Items.Insert(0, "");
        //}


        private static IDictionary<Type, IEnumConverter> _conversores = new Dictionary<Type, IEnumConverter>();
        private static void GarantirConvesor(Type aTipoEnum)
        {
            lock (_conversores)
            {
                if (_conversores.ContainsKey(aTipoEnum)) return;

                var ctor = typeof(EnumUtilitario<>).MakeGenericType(aTipoEnum).GetConstructor(new Type[] { });

                _conversores.Add(aTipoEnum, (IEnumConverter)ctor.Invoke(new object[] { }));
            }
        }

        ///// <summary>
        ///// Obtem o valor de enum referente a uma representação em string, uitilizando o tipo informado como parâmetro
        ///// </summary>
        ///// <param name="aTipoEnum">Tipo do enum a ser utilizado</param>
        ///// <param name="aValor">Representação como string</param>
        ///// <returns></returns>
        //[Obsolete("Este método é destinado para uso interno da DrNet. Aplicações construídas com a DrNet deve, utilizar o método de extesão 'ComoEnum'.")]
        //public static object ConvertStringParaEnum(Type aTipoEnum, string aValor)
        //{
        //    GarantirConvesor(aTipoEnum);

        //    return _conversores[aTipoEnum].StringToEnum(aValor);
        //}

        ///// <summary>
        ///// Obtem o valor a representação em string referente a um enum, uitilizando o tipo informado como parâmetro
        ///// </summary>
        ///// <param name="aTipoEnum">Tipo do enum a ser utilizado</param>
        ///// <param name="aValor">Valor como enum</param>
        ///// <returns></returns>
        //[Obsolete("Este método é destinado para uso interno da DrNet. Aplicações construídas com a DrNet deve, utilizar o método de extesão 'ComoString'.")]
        //public static string ConvertEnumParaString(Type aTipoEnum, object aValor)
        //{
        //    GarantirConvesor(aTipoEnum);

        //    return _conversores[aTipoEnum].EnumToString(aValor);
        //}

    }

    internal interface IEnumConverter
    {
        object StringToEnum(string aValor);
        string EnumToString(object aValor);
    }

    internal class EnumUtilitario<T> : IEnumConverter where T : struct

    {

        private static Dictionary<T, string> _mapaDeEnum;
        private static Dictionary<string, T> _mapaParaEnum;
        private static Exception _erroInicializacaoTraducao;
        private static T[] _listaValores;

        private static Dictionary<T, Tuple<string, bool>> _mapaDescricoes;

        //Construtor static: Inicializa mapas de tipos somente uma vez por especialização
        static EnumUtilitario()
        {
            object[] attributes;
            string name;
            T myEnum;

            try
            {
                if (typeof(T).BaseType != typeof(System.Enum))
                    throw new ArgumentException("T must be of type System.Enum");

                _mapaDeEnum = new Dictionary<T, string>();
                _mapaParaEnum = new Dictionary<string, T>();
                _mapaDescricoes = new Dictionary<T, Tuple<string, bool>>();
                var listaValores = new List<T>();

                foreach (FieldInfo field in typeof(T).GetFields())
                {
                    if (field.IsStatic)
                    {
                        myEnum = (T)field.GetValue(null);
                        listaValores.Add(myEnum);

                        attributes = field.GetCustomAttributes(typeof(XmlEnumAttribute), false);

                        if (attributes == null || attributes.Length == 0)
                            name = myEnum.ToString();
                        else
                            name = (attributes[0] as XmlEnumAttribute).Name;

                        _mapaDeEnum.Add(myEnum, name);
                        if (!_mapaParaEnum.ContainsKey(name))
                            _mapaParaEnum.Add(name, myEnum);

                        attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                        //if (attributes != null && attributes.Length > 0)
                        //{
                        //    _mapaDescricoes.Add(myEnum, Tuple.Create((attributes[0] as DescriptionAttribute).Description, attributes[0] is DescricaoResourceEnumAtributo));
                        //}
                        //else
                            _mapaDescricoes.Add(myEnum, Tuple.Create(myEnum.ToString(), false));

                    }
                }

                _listaValores = listaValores.ToArray();
            }
            catch (Exception ex)
            {
                //Se ocorrer erro no construtor, guardar para que os
                //métodos possam testar se ocorreu erro na inicialização
                _erroInicializacaoTraducao = ex;
            }

        }

        public static IEnumerable<T> ListarValoresEnum()
        {
            //Testar se ocorreu erro no construtor static
            if (_erroInicializacaoTraducao != null) throw _erroInicializacaoTraducao;

            foreach (T valor in _listaValores)
                yield return valor;
        }

        private static string _prefixoContexto = typeof(T).FullName + ":";

        public static string DescricaoEnum(T? aItem)
        {
            //Testar se ocorreu erro no construtor static
            if (_erroInicializacaoTraducao != null) throw _erroInicializacaoTraducao;

            if (aItem.HasValue)
            {
                string descricaoContexto = null;
                //Obtem o gestor de contexto e globalização em uso
                //var gestorContexto = ContainerDependencias.ParGlobal.GestorContexto;

                ////Se não houver gestor, não será feita tradução de termos
                //if (gestorContexto != null)
                //    descricaoContexto = gestorContexto.RetornarValor(_prefixoContexto + _mapaDeEnum[aItem.Value]);

                //Se o termo foi alterado na globalização/sensibilidade a contexto, retornar o valor alterado
                if (descricaoContexto != null)
                    return descricaoContexto;

                try
                {
                    var descricao = _mapaDescricoes[aItem.Value];

                    //if (descricao.Item2)
                    //    return ContainerDependencias.ParGlobal.GestorContexto.RetornarValor(descricao.Item1);
                    //else
                        return descricao.Item1;

                }
                catch (Exception ex)
                {
                    throw new ValorNaoEncontradoExcecao<T>(aItem.Value, ex);
                }
            }
            else
                return string.Empty;
        }

        public static string EnumComoString(T? aItem)
        {
            //Testar se ocorreu erro no construtor static
            if (_erroInicializacaoTraducao != null) throw _erroInicializacaoTraducao;

            if (aItem.HasValue)
            {
                try
                {
                    return _mapaDeEnum[aItem.Value];
                }
                catch (Exception ex)
                {
                    throw new ValorNaoEncontradoExcecao<T>(aItem.Value, ex);
                }
            }
            else
                return null;
        }

        public static T? StringComoEnum(string aValor)
        {
            //Testar se ocorreu erro no construtor static
            if (_erroInicializacaoTraducao != null) throw _erroInicializacaoTraducao;

            if (string.IsNullOrWhiteSpace(aValor)) return default(T?);

            try
            {
                return _mapaParaEnum[aValor.Trim()];
            }
            catch (Exception ex)
            {
                throw new ValorNaoEncontradoExcecao<T>(aValor, ex);
            }

        }


        object IEnumConverter.StringToEnum(string aValor)
        {
            return StringComoEnum(aValor);
        }

        string IEnumConverter.EnumToString(object aValor)
        {
            return EnumComoString((T)aValor);
        }
    }

    /// <summary>
    /// Exceção de quando não são encontrados valores ao acessar mapeamentos para Enums
    /// </summary>
    public abstract class ValorNaoEncontradoExcecao : Exception
    {

        /// <summary>
        /// Construtor da Exceção
        /// </summary>
        /// <param name="aMensagem">Mensagem associada à exceção</param>
        protected ValorNaoEncontradoExcecao(string aMensagem)
            : base(aMensagem)
        {
        }

        /// <summary>
        /// Construtor da Exceção
        /// </summary>
        /// <param name="aMensagem">Mensagem associada à exceção</param>
        /// <param name="aInnerException">Exceção para atribuir como 'Inner Exception' a nova exceção</param>
        protected ValorNaoEncontradoExcecao(string aMensagem, Exception aInnerException)
            : base(aMensagem, aInnerException)
        {
        }

    }

    /// <summary>
    /// Exceção de quando não são encontrados valores ao acessar mapeamentos para Enums, relativo a um enum específico
    /// <typeparam name="T">Tipo do enum ao qual a exceção se refere</typeparam>
    /// </summary>
    public class ValorNaoEncontradoExcecao<T> : ValorNaoEncontradoExcecao
    {

        /// <summary>
        /// Construtor da Exceção
        /// </summary>
        /// <param name="aValor">Valor não encontrado no mapeamento</param>
        public ValorNaoEncontradoExcecao(string aValor) :
            base(string.Format("Valor '{0}' não encontrado para estrutura {1}", aValor, typeof(T).Name))
        {

        }

        /// <summary>
        /// Construtor da Exceção
        /// </summary>
        /// <param name="aValor">Valor não encontrado no mapeamento</param>
        /// <param name="aExcecao">Excecao original gerada</param>
        public ValorNaoEncontradoExcecao(string aValor, Exception aExcecao) :
            base(string.Format("Valor '{0}' não encontrado para estrutura {1}", aValor, typeof(T).Name), aExcecao)
        {

        }

        /// <summary>
        /// Construtor da Exceção
        /// </summary>
        /// <param name="aValor">Valor não encontrado no mapeamento</param>
        public ValorNaoEncontradoExcecao(T aValor) :
            base(string.Format("Valor '{0}' não encontrado para estrutura {1}", aValor.ToString(), typeof(T).Name))
        {

        }

        /// <summary>
        /// Construtor da Exceção
        /// </summary>
        /// <param name="aValor">Valor não encontrado no mapeamento</param>
        /// <param name="aExcecao">Excecao original gerada</param>
        public ValorNaoEncontradoExcecao(T aValor, Exception aExcecao) :
            base(string.Format("Valor '{0}' não encontrado para estrutura {1}", aValor.ToString(), typeof(T).Name), aExcecao)
        {

        }
    }
}
