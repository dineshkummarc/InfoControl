using System;
using System.Data;
using System.Collections;

namespace InfoControl.Data
{
	/// <summary>
	/// Classe que vincula um Enumerador a um DataReder assim de acordo com os 
	/// eventos do Enumerator pode-se  mudar o estado do DataReader.
	/// </summary>
	public class DataReaderEnumerator: IEnumerator
	{
		#region Variables
		private DataReader _dataReader;
		private IEnumerator _enu;
        
		#endregion
		
		#region Properties

		#endregion

		#region Constructor
		/// <summary>
		/// Classe que vincula um Enumerador a um DataReder assim de acordo com os 
		/// eventos do Enumerator pode-se  mudar o estado do DataReader.
		/// </summary>
		/// <param name="dr">DataReader que ser� manipulado</param>		
        public DataReaderEnumerator(DataReader dr)
		{
			_dataReader = dr;
			_enu=(dr._dataReader as IEnumerable).GetEnumerator() ;
		}
		#endregion

		#region IEnumerator Members
		/// <summary>
		/// Seta o enumerator para a posi��o inicial, antes do primeiro elemento da cole��o
		/// </summary>
		public void Reset()
		{
			_enu.Reset();
		}

		/// <summary>
		/// Retorna o elemento atual na cole��o
		/// </summary>
		public object Current
		{
			get
			{
				return(_enu.Current);
			}
		}

		/// <summary>
		/// Avan�a o enumerator para o pr�ximo elemento na cole��o
		/// </summary>
		/// <returns></returns>
		public bool MoveNext()
		{
			return(_dataReader.CloseIfEnded(_enu.MoveNext()));
		}

        #endregion

        
	}
}
