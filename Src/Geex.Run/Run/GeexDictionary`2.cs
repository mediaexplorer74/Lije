
// Type: Geex.Run.GeexDictionary`2
// Assembly: Geex.Run, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7538DACB-8222-45C8-AAEF-59106350173C
// Assembly location: C:\Users\Admin\Desktop\RE\Lije\Geex.Run.dll

using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace Geex.Run
{
  public class GeexDictionary<TKey, TValue> : IXmlSerializable
  {
    private Dictionary<TKey, TValue> localDictionary = new Dictionary<TKey, TValue>();

    public TValue this[TKey key]
    {
      get => this.localDictionary[key];
      set => this.localDictionary[key] = value;
    }

    public Dictionary<TKey, TValue>.KeyCollection Keys => this.localDictionary.Keys;

    public Dictionary<TKey, TValue>.ValueCollection Values => this.localDictionary.Values;

    public int Count => this.localDictionary.Count;

    public XmlSchema GetSchema() => (XmlSchema) null;

    public bool ContainsKey(TKey key) => this.localDictionary.ContainsKey(key);

    public int CountKeysWithSameValue(TValue val)
    {
      int num = 0;
      foreach (TKey key in this.localDictionary.Keys)
      {
        if (this.localDictionary[key].Equals((object) val))
          ++num;
      }
      return num;
    }

    public void Add(TKey key, TValue value) => this.localDictionary.Add(key, value);

    public void Clear() => this.localDictionary.Clear();

    public bool Remove(TKey key) => this.localDictionary.Remove(key);

    public void ReadXml(XmlReader reader)
    {
      XmlSerializer xmlSerializer1 = new XmlSerializer(typeof (TKey));
      XmlSerializer xmlSerializer2 = new XmlSerializer(typeof (TValue));
      this.Clear();
      int num = reader.IsEmptyElement ? 1 : 0;
      reader.Read();
      if (num != 0)
        return;
      while (reader.NodeType != XmlNodeType.EndElement)
      {
        reader.ReadStartElement("i");
        reader.ReadStartElement("k");
        TKey key = (TKey) xmlSerializer1.Deserialize(reader);
        reader.ReadEndElement();
        reader.ReadStartElement("v");
        TValue obj = (TValue) xmlSerializer2.Deserialize(reader);
        reader.ReadEndElement();
        this.localDictionary.Add(key, obj);
        reader.ReadEndElement();
        int content = (int) reader.MoveToContent();
      }
      reader.ReadEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
      XmlSerializer xmlSerializer1 = new XmlSerializer(typeof (TKey));
      XmlSerializer xmlSerializer2 = new XmlSerializer(typeof (TValue));
      foreach (TKey key in this.localDictionary.Keys)
      {
        writer.WriteStartElement("i");
        writer.WriteStartElement("k");
        xmlSerializer1.Serialize(writer, (object) key);
        writer.WriteEndElement();
        writer.WriteStartElement("v");
        TValue local = this.localDictionary[key];
        xmlSerializer2.Serialize(writer, (object) local);
        writer.WriteEndElement();
        writer.WriteEndElement();
      }
    }
  }
}
