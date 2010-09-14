using System;
using System.Collections.Generic;
using System.Text;

namespace InfoControl
{
    public class Check
    {
#if !CompactFramework
        /// <summary>
        /// Fun��o que verifica se um objeto independente do formato pode ser convertido para num�rico
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns>Se a convers�o for possivel ent�o retorna True. Caso contr�rio retorna False.</returns>
        public static bool IsNumeric(object Expression)
        {
            // Define uma vari�vel que coletara o resultado da fun��o TryParse.
            // Se a convers�o falhar essa vari�vel ter� zero.
            double retNum;

            // O TryParse n�o gera exce��o quando a convers�o falha.
            return Double.TryParse(
                Convert.ToString(Expression),
                System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out retNum);
        }
#endif

        /// <summary>
        /// Verifica se um objeto n�o � nulo
        /// </summary>
        /// <param name="valueToCheck">Valor a ser verificado se � nulo</param>
        /// <returns>Se o objeto n�o for nulo ent�o retorna True, caso contr�rio retorna False</returns>
        public static bool IsNotNull(object valueToCheck)
        {
            return (valueToCheck != null);
        }
    }
}
