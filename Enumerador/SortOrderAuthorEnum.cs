using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAcme.UtilitarioEnum;

namespace ApiAcme.Enumerador
{
    public enum SortOrderAuthorEnum
    {
        [ValorEnumAtributo("1"), DescricaoEnumAtributo("Primeiro Nome Asc")]
        FirstName_Asc = 1,
        [ValorEnumAtributo("2"), DescricaoEnumAtributo("Data Aniversario Asc")]
        Birthdate_Asc = 2,
       [ValorEnumAtributo("3"), DescricaoEnumAtributo("Primeiro Nome Desc")]
        FirstName_Desc = 3,
        [ValorEnumAtributo("4"), DescricaoEnumAtributo("Data Aniversario Desc")]
        Birthdate_Desc = 4

    }
}
