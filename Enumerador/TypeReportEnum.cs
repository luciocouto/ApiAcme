using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAcme.UtilitarioEnum;

namespace ApiAcme.Enumerador
{

    public enum TypeReportEnum
    {
        [ValorEnumAtributo("1"), DescricaoEnumAtributo("Xlsx")]
        Xlsx = 1,
        [ValorEnumAtributo("2"), DescricaoEnumAtributo("Pdf")]
        Pdf = 2,
        [ValorEnumAtributo("3"), DescricaoEnumAtributo("Html")]
        Html = 3

    }
}
