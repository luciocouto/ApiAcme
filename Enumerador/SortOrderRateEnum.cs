using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAcme.UtilitarioEnum;

namespace ApiAcme.Enumerador
{
    public enum SortOrderRateEnum
    {
        [ValorEnumAtributo("1"), DescricaoEnumAtributo("Primeiro Nome Autor Asc")]
        FirstNameAuthor_Asc = 1,
        [ValorEnumAtributo("2"), DescricaoEnumAtributo("Data Avaliação Asc")]
        Ratedate_Asc = 2,
        [ValorEnumAtributo("3"), DescricaoEnumAtributo("Primeiro Nome Desc")]
        FirstNameAuthor_Desc = 3,
        [ValorEnumAtributo("4"), DescricaoEnumAtributo("Data Avaliação Desc")]
        Ratedate_Desc = 4
    }
}
