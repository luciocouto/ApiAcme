using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAcme.UtilitarioEnum;

namespace ApiAcme.Enumerador
{
    public enum SortOrderPostEnum
    {
        [ValorEnumAtributo("1"), DescricaoEnumAtributo("Titulo Asc")]
        Title_Asc = 1,
        [ValorEnumAtributo("2"), DescricaoEnumAtributo("Data Postagem Asc")]
        PostDate_Asc = 2,
        [ValorEnumAtributo("3"), DescricaoEnumAtributo("Titulo Desc")]
        Title_Desc = 3,
        [ValorEnumAtributo("4"), DescricaoEnumAtributo("Data Postagem Desc")]
        PostDate_Desc = 4

    }
}

