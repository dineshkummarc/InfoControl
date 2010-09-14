using System;

namespace InfoControl.Data
{
	/// <summary>
	/// Exce��o criada para disparar por motivos de Tipos de dados inconsistentes
	/// </summary>
	public class DataTypeException: Exception
	{
		/// <summary>
		/// Exce��o criada para disparar por motivos de Tipos de dados inconsistentes
		/// </summary>
		public DataTypeException(string message):base(message)
		{}

		/// <summary>
		/// Exce��o criada para disparar por motivos de Tipos de dados inconsistentes
		/// </summary>
		public DataTypeException(string message, Exception inner):base(message, inner)
		{}
	}
}
