using System;
using System.Xml.Serialization;

namespace ApiAcme.UtilitarioEnum
{
    public class ValorEnumAtributo : XmlEnumAttribute
    {
        public ValorEnumAtributo(string aValor) : base(aValor) { }
    }
}
