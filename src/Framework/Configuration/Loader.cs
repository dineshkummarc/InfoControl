using System;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;


using InfoControl.Runtime;


namespace InfoControl.Configuration
{
    /// <summary>
    /// Classe abstrata que encapsula todas as opera��es necess�rias
    /// para criar um objeto de configura��o e XML
    /// </summary>
    public class ConfigLoader<T>
    {
        /// <summary>
        /// Monta as configura��es de acordo com um arquivo XML
        /// </summary>
        /// <param name="filename"></param>        
        /// <returns></returns>
        public T Load(string filename)
        {
            // Carrega o arquivo de configura��o
            System.Xml.XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            return doc.OuterXml.DeserializeFromXml<T>();
        }
    }
}
